﻿using FluentValidation;
using PlexRipper.Application;
using PlexRipper.Data.Common;

namespace PlexRipper.Data.PlexServers
{
    public class CreatePlexServerStatusCommandValidator : AbstractValidator<CreatePlexServerStatusCommand>
    {
        public CreatePlexServerStatusCommandValidator()
        {
            RuleFor(x => x.PlexServerStatus).NotNull();
            RuleFor(x => x.PlexServerStatus.Id).Equal(0);
            RuleFor(x => x.PlexServerStatus.PlexServerId).GreaterThan(0);
            RuleFor(x => x.PlexServerStatus.LastChecked).NotNull();
            RuleFor(x => x.PlexServerStatus.StatusMessage).NotEmpty();
            RuleFor(x => x.PlexServerStatus.PlexServer).NotNull();
            RuleFor(x => x.PlexServerStatus.PlexServer.Id).GreaterThan(0);
        }
    }

    public class CreatePlexServerStatusCommandHandler : BaseHandler, IRequestHandler<CreatePlexServerStatusCommand, Result<int>>
    {
        public CreatePlexServerStatusCommandHandler(PlexRipperDbContext dbContext) : base(dbContext) { }

        public async Task<Result<int>> Handle(CreatePlexServerStatusCommand command, CancellationToken cancellationToken)
        {
            Log.Debug("Creating a new PlexServerStatus in the DB");

            command.PlexServerStatus.PlexServer = null;
            await _dbContext.PlexServerStatuses.AddAsync(command.PlexServerStatus, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
            await _dbContext.Entry(command.PlexServerStatus).GetDatabaseValuesAsync(cancellationToken);

            return Result.Ok(command.PlexServerStatus.Id);
        }
    }
}