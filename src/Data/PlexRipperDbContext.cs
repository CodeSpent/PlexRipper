using System.Globalization;
using System.Reflection;
using AppAny.Quartz.EntityFrameworkCore.Migrations;
using AppAny.Quartz.EntityFrameworkCore.Migrations.SQLite;
using Environment;
using Logging.Interface;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NaturalSort.Extension;
using PlexRipper.Data.Common;

namespace PlexRipper.Data;

public sealed class PlexRipperDbContext : DbContext, ISetup
{
    private readonly IPathProvider _pathProvider;

    private readonly ILog<PlexRipperDbContext> _log = LogManager.CreateLogInstance<PlexRipperDbContext>();

    #region Properties

    #region Tables

    public DbSet<PlexAccount> PlexAccounts { get; set; }

    public DbSet<DownloadTask> DownloadTasks { get; set; }

    public DbSet<DownloadWorkerTask> DownloadWorkerTasks { get; set; }

    public DbSet<DownloadWorkerLog> DownloadWorkerTasksLogs { get; set; }

    public DbSet<FolderPath> FolderPaths { get; set; }

    public DbSet<DownloadFileTask> FileTasks { get; set; }

    public DbSet<Notification> Notifications { get; set; }

    public DbSet<PlexGenre> PlexGenres { get; set; }

    public DbSet<PlexLibrary> PlexLibraries { get; set; }

    #region PlexMovie

    public DbSet<PlexMovie> PlexMovies { get; set; }

    #endregion

    #region PlexTvShow

    public DbSet<PlexTvShow> PlexTvShows { get; set; }

    public DbSet<PlexTvShowSeason> PlexTvShowSeason { get; set; }

    public DbSet<PlexTvShowEpisode> PlexTvShowEpisodes { get; set; }

    #endregion

    public DbSet<PlexRole> PlexRoles { get; set; }

    #region PlexServers

    public DbSet<PlexServer> PlexServers { get; set; }

    public DbSet<PlexServerConnection> PlexServerConnections { get; set; }

    public DbSet<PlexServerStatus> PlexServerStatuses { get; set; }

    #endregion

    #endregion

    #region JoinTables

    public DbSet<PlexAccountServer> PlexAccountServers { get; set; }

    public DbSet<PlexAccountLibrary> PlexAccountLibraries { get; set; }

    public DbSet<PlexMovieGenre> PlexMovieGenres { get; set; }

    public DbSet<PlexMovieRole> PlexMovieRoles { get; set; }

    #endregion

    public string DatabaseName { get; set; }

    public string DatabasePath { get; set; }

    public string ConfigDirectory { get; set; }

    /// <summary>
    /// Determines if this <see cref="PlexRipperDbContext"/> has been setup already during integration or unit testing.
    /// </summary>
    public bool HasBeenSetup { get; set; }

    private static readonly NaturalSortComparer NaturalComparer = new(StringComparison.InvariantCultureIgnoreCase);


    #endregion Properties

    #region Constructors

    public PlexRipperDbContext() { }

    public PlexRipperDbContext(IPathProvider pathProvider)
    {
        _pathProvider = pathProvider;
        DatabaseName = _pathProvider.DatabaseName;
        DatabasePath = _pathProvider.DatabasePath;
        ConfigDirectory = _pathProvider.ConfigDirectory;
    }

    public PlexRipperDbContext(DbContextOptions<PlexRipperDbContext> options, string databaseName = "") : base(options)
    {
        DatabaseName = databaseName;
        Database.OpenConnection();
        Database.EnsureCreated();
    }

    #endregion Constructors

    #region Methods

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Source: https://github.com/tompazourek/NaturalSort.Extension
            SqliteConnection databaseConnection = new(PathProvider.DatabaseConnectionString);
            databaseConnection.CreateCollation(OrderByNaturalExtensions.CollationName, (x, y) => NaturalComparer.Compare(x, y));

            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            optionsBuilder.LogTo(text => LogManager.DbContextLogger(text), LogLevel.Error);
            optionsBuilder.EnableDetailedErrors();
            optionsBuilder.UseSqlite(databaseConnection, b => b.MigrationsAssembly(typeof(PlexRipperDbContext).Assembly.FullName));
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.UseCollation(OrderByNaturalExtensions.CollationName);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.AddQuartz(x => x.UseSqlite());

        // NOTE: This has been added to PlexRipperDbContext.OnModelCreating
        // Based on: https://stackoverflow.com/a/63992731/8205497
        builder.Entity<PlexMovie>()
            .Property(x => x.MediaData)
            .HasJsonValueConversion();

        builder.Entity<PlexTvShow>()
            .Property(x => x.MediaData)
            .HasJsonValueConversion();

        builder.Entity<PlexTvShowSeason>()
            .Property(x => x.MediaData)
            .HasJsonValueConversion();

        builder.Entity<PlexTvShowEpisode>()
            .Property(x => x.MediaData)
            .HasJsonValueConversion();

        builder = PlexRipperDBContextSeed.SeedDatabase(builder);

        base.OnModelCreating(builder);
    }

    public Result Setup()
    {
        try
        {
            _log.InformationLine("Setting up the PlexRipper database");

            // Don't migrate when running in memory, this causes error:
            // "Relational-specific methods can only be used when the context is using a relational database provider."
            if (!Database.IsInMemory() && !EnvironmentExtensions.IsIntegrationTestMode())
            {
                _log.InformationLine("Attempting to migrate database");
                Database.Migrate();
            }
        }
        catch (SqliteException e)
        {
            _log.ErrorLine("Failed to migrate the database or the database is corrupted");
            _log.Error(e);
            ResetDatabase();
        }

        // Check if database exists and can be connected to.
        var exist = Database.CanConnect();
        if (exist)
        {
            if (!EnvironmentExtensions.IsIntegrationTestMode())
            {
                _log.InformationLine("Database was successfully connected!");
                _log.Information("Database connected at: {DatabasePath}", DatabasePath);
            }

            return Result.Ok();
        }

        _log.ErrorLine("Database could not be created and or migrated");
        return Result.Fail($"Could not create database {DatabaseName} in {ConfigDirectory}").LogError();
    }

    public Result ResetDatabase()
    {
        try
        {
            _log.InformationLine("Resetting PlexRipper database now");
            Database.CloseConnection();
            BackUpDatabase();
            Database.EnsureDeleted();
            Database.Migrate();
            return Result.Ok();
        }
        catch (Exception e)
        {
            _log.FatalLine("Failed to reset database!");
            _log.FatalLine("TO FIX THIS: DELETE DATABASE MANUALLY FROM THE CONFIG DIRECTORY");
            Result.Fail(new ExceptionalError(e)).LogFatal();
            throw;
        }
    }

    private Result BackUpDatabase()
    {
        _log.InformationLine("Attempting to back-up the PlexRipper database");
        if (!File.Exists(_pathProvider.DatabasePath))
            return Result.Fail($"Could not find Database at path: {_pathProvider.DatabasePath}").LogError();

        var dateString = DateTime.UtcNow.ToString("yy-MM-dd_hh-mm", CultureInfo.InvariantCulture);
        var dbBackUpPath = Path.Combine(_pathProvider.DatabaseBackupDirectory, dateString);

        try
        {
            Directory.CreateDirectory(dbBackUpPath);

            // Wait until the database is available.
            StreamExtensions.WaitForFile(_pathProvider.DatabasePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None)?.Dispose();

            foreach (var databaseFilePath in _pathProvider.DatabaseFiles)
            {
                if (File.Exists(databaseFilePath))
                {
                    var destinationPath = Path.Combine(dbBackUpPath, Path.GetFileName(databaseFilePath));
                    try
                    {
                        File.Copy(databaseFilePath, destinationPath);
                        _log.Here()
                            .Information("Successfully copied \"{DatabaseFilePath}\" to back-up location\"{DestinationPath}\"", databaseFilePath,
                                destinationPath);
                    }
                    catch (Exception e)
                    {
                        _log.Here().Error("Failed to copy {DatabaseFilePath} to back-up location {DestinationPath}", databaseFilePath, destinationPath);
                        _log.Error(e);
                    }

                    continue;
                }

                _log.Warning("Could not find: {DatabaseFilePath} to backup", databaseFilePath);
            }

            return Result.Ok();
        }
        catch (Exception e)
        {
            return Result.Fail(new ExceptionalError(e)).LogError();
        }
    }

    #endregion Methods
}