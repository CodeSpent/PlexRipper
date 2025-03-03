using Application.Contracts;
using BackgroundServices.Contracts;
using Data.Contracts;
using Logging.Interface;
using PlexApi.Contracts;

namespace PlexRipper.Application.PlexAccounts;

public class PlexAccountService : IPlexAccountService
{
    #region Fields

    private readonly IInspectServerScheduler _inspectServerScheduler;
    private readonly ILog _log;
    private readonly IMediator _mediator;

    private readonly IPlexApiService _plexApiService;

    #endregion

    #region Constructors

    public PlexAccountService(
        ILog log,
        IMediator mediator,
        IPlexApiService plexApiService,
        IInspectServerScheduler inspectServerScheduler)
    {
        _log = log;
        _mediator = mediator;
        _plexApiService = plexApiService;
        _inspectServerScheduler = inspectServerScheduler;
    }

    #endregion

    #region Methods

    #region Public

    public virtual async Task<Result<PlexAccount>> ValidatePlexAccountAsync(PlexAccount plexAccount)
    {
        if (plexAccount is null)
            return ResultExtensions.IsNull(nameof(plexAccount));

        if (plexAccount.Username == string.Empty || plexAccount.Password == string.Empty)
            return Result.Fail("Either the username or password were empty").LogWarning();

        var plexSignInResult = await _plexApiService.PlexSignInAsync(plexAccount);
        if (plexSignInResult.IsFailed)
        {
            // Check if 2FA might be enabled
            if (plexSignInResult.HasPlexErrorEnterVerificationCode())
            {
                plexAccount.Is2Fa = true;
                return Result.Ok(plexAccount);
            }

            return plexSignInResult;
        }

        _log.Debug("The PlexAccount with displayName {PlexAccountDisplayName} has been validated", plexAccount.DisplayName);
        return plexSignInResult;
    }

    /// <inheritdoc/>
    public async Task<Result> RefreshPlexAccount(int plexAccountId = 0)
    {
        var plexAccountIds = new List<int>();

        if (plexAccountId == 0)
        {
            var enabledAccounts = await _mediator.Send(new GetAllPlexAccountsQuery(onlyEnabled: true));
            if (enabledAccounts.IsFailed)
                return enabledAccounts.ToResult();

            plexAccountIds.AddRange(enabledAccounts.Value.Select(x => x.Id));
        }
        else
            plexAccountIds.Add(plexAccountId);

        foreach (var id in plexAccountIds)
            await _inspectServerScheduler.QueueRefreshAccessiblePlexServersJob(id);

        return Result.Ok();
    }

    /// <summary>
    /// Checks if an <see cref="PlexAccount"/> with the same username already exists.
    /// </summary>
    /// <param name="username">The username to check for.</param>
    /// <returns>true if username is available.</returns>
    public virtual async Task<Result<bool>> CheckIfUsernameIsAvailableAsync(string username)
    {
        var result = await _mediator.Send(new GetPlexAccountByUsernameQuery(username));

        if (result.Has404NotFoundError())
            return Result.Ok(true);

        if (result.IsFailed)
            return result.ToResult();

        if (result.Value != null)
        {
            _log.Warning("An Account with the username: {UserName} already exists", username);
            return Result.Ok(false);
        }

        _log.Debug("The username: {UserName} is available", username);
        return Result.Ok(true);
    }

    #endregion

    #endregion

    #region Authentication

    private string GeneratePlexAccountClientId()
    {
        return StringExtensions.RandomString(24, true, true);
    }

    /// <summary>
    /// If 2FA is enabled for a PlexAccount, then a Pin needs to be requested and checked every second for having be resolved.
    /// </summary>
    /// <param name="clientId">The account clientId.</param>
    /// <returns></returns>
    public Task<Result<AuthPin>> Get2FAPin(string clientId)
    {
        return _plexApiService.Get2FAPin(clientId);
    }

    public Task<Result<AuthPin>> Check2FAPin(int pinId, string clientId)
    {
        return _plexApiService.Check2FAPin(pinId, clientId);
    }

    #endregion

    #region CRUD

    /// <summary>
    /// Returns the <see cref="PlexAccount"/> with the accessible <see cref="PlexServer"/>s and all <see cref="PlexLibrary"/>.
    /// </summary>
    /// <param name="accountId">The Id to retrieve the <see cref="PlexAccount"/> by.</param>
    /// <returns>The account found.</returns>
    public async Task<Result<PlexAccount>> GetPlexAccountAsync(int accountId)
    {
        var result = await _mediator.Send(new GetPlexAccountByIdQuery(accountId, true, true));

        if (result.IsFailed)
            return result;

        if (result.Value != null)
        {
            _log.Debug("Found an Account with the id: {AccountId}", accountId);
            return result;
        }

        _log.Warning("Could not find an Account with id: {AccountId}", accountId);
        return result;
    }

    /// <summary>
    /// Retrieves all <see cref="PlexAccount"/>s with the included <see cref="PlexServer"/>s and <see cref="PlexLibrary"/>s.
    /// </summary>
    /// <param name="onlyEnabled">Should only enabled <see cref="PlexAccount"/>s be retrieved.</param>
    /// <returns>A list of all <see cref="PlexAccount"/>s.</returns>
    public Task<Result<List<PlexAccount>>> GetAllPlexAccountsAsync(bool onlyEnabled = false)
    {
        _log.Debug("Returning {Category} PlexAccounts", onlyEnabled ? "only enabled" : "all");
        return _mediator.Send(new GetAllPlexAccountsQuery(true, true, onlyEnabled));
    }

    /// <summary>
    /// Creates an <see cref="PlexAccount"/> in the Database and performs an SetupAccountAsync().
    /// </summary>
    /// <param name="plexAccount">The unique account.</param>
    /// <returns>Returns the added account after setup.</returns>
    public async Task<Result<PlexAccount>> CreatePlexAccountAsync(PlexAccount plexAccount)
    {
        _log.Debug("Creating account with username {UserName}", plexAccount.Username);
        var result = await CheckIfUsernameIsAvailableAsync(plexAccount.Username);

        // Fail on validation errors
        if (result.IsFailed)
            return result.ToResult();

        if (!result.Value)
        {
            var msg =
                $"Account with username {plexAccount.Username} cannot be created due to an account with the same username already existing";
            return result.ToResult().WithError(msg).LogWarning();
        }

        // Create PlexAccount
        plexAccount.ClientId = GeneratePlexAccountClientId();
        var createResult = await _mediator.Send(new CreatePlexAccountCommand(plexAccount));
        if (createResult.IsFailed)
            return createResult.ToResult();

        await _inspectServerScheduler.QueueInspectPlexServerByPlexAccountIdJob(createResult.Value);

        return await _mediator.Send(new GetPlexAccountByIdQuery(createResult.Value, true, true));
    }

    public async Task<Result<PlexAccount>> UpdatePlexAccountAsync(PlexAccount plexAccount, bool inspectServers = false)
    {
        var result = await _mediator.Send(new UpdatePlexAccountCommand(plexAccount));
        if (result.IsFailed)
        {
            var msg = "Failed to validate the PlexAccount that will be updated";
            result.Errors.Add(new Error(msg));
            return result.LogError();
        }

        var plexAccountDb = await _mediator.Send(new GetPlexAccountByIdQuery(plexAccount.Id));
        if (plexAccountDb.IsFailed)
            return plexAccountDb;

        // Re-validate if the credentials changed
        if (inspectServers || plexAccountDb.Value.Username != plexAccount.Username || plexAccountDb.Value.Password != plexAccount.Password)
            return await GetPlexAccountAsync(plexAccountDb.Value.Id);

        return plexAccountDb;
    }

    /// <summary>
    /// Hard deletes the PlexAccount from the Database.
    /// </summary>
    /// <param name="plexAccountId"></param>
    /// <returns></returns>
    public async Task<Result> DeletePlexAccountAsync(int plexAccountId)
    {
        var deleteAccountResult = await _mediator.Send(new DeletePlexAccountCommand(plexAccountId));
        if (deleteAccountResult.IsFailed)
            return deleteAccountResult;

        // TODO Decide what to do with PlexServers that cannot be accessed anymore
        return await _mediator.Send(new RemoveInaccessibleServersCommand());
    }

    #endregion
}