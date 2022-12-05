﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlexRipper.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FolderPaths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    FolderType = table.Column<string>(type: "TEXT", unicode: false, maxLength: 50, nullable: false),
                    MediaType = table.Column<string>(type: "TEXT", unicode: false, maxLength: 50, nullable: false),
                    DirectoryPath = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FolderPaths", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Level = table.Column<string>(type: "TEXT", unicode: false, maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Message = table.Column<string>(type: "TEXT", nullable: true),
                    Hidden = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlexAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    Username = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: true),
                    IsEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsValidated = table.Column<bool>(type: "INTEGER", nullable: false),
                    ValidatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PlexId = table.Column<long>(type: "INTEGER", nullable: false),
                    Uuid = table.Column<string>(type: "TEXT", nullable: true),
                    ClientId = table.Column<string>(type: "TEXT", nullable: true),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    HasPassword = table.Column<bool>(type: "INTEGER", nullable: false),
                    AuthenticationToken = table.Column<string>(type: "TEXT", nullable: true),
                    IsMain = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlexAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlexGenres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Tag = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlexGenres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlexRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Tag = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlexRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlexServers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    OwnerId = table.Column<long>(type: "INTEGER", nullable: false),
                    PlexServerOwnerUsername = table.Column<string>(type: "TEXT", nullable: true),
                    Device = table.Column<string>(type: "TEXT", nullable: true),
                    Platform = table.Column<string>(type: "TEXT", nullable: true),
                    PlatformVersion = table.Column<string>(type: "TEXT", nullable: true),
                    Product = table.Column<string>(type: "TEXT", nullable: true),
                    ProductVersion = table.Column<string>(type: "TEXT", nullable: true),
                    Provides = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastSeenAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MachineIdentifier = table.Column<string>(type: "TEXT", nullable: true),
                    PublicAddress = table.Column<string>(type: "TEXT", nullable: true),
                    PreferredConnectionId = table.Column<int>(type: "INTEGER", nullable: false),
                    Owned = table.Column<bool>(type: "INTEGER", nullable: false),
                    Home = table.Column<bool>(type: "INTEGER", nullable: false),
                    Synced = table.Column<bool>(type: "INTEGER", nullable: false),
                    Relay = table.Column<bool>(type: "INTEGER", nullable: false),
                    Presence = table.Column<bool>(type: "INTEGER", nullable: false),
                    HttpsRequired = table.Column<bool>(type: "INTEGER", nullable: false),
                    PublicAddressMatches = table.Column<bool>(type: "INTEGER", nullable: false),
                    DnsRebindingProtection = table.Column<bool>(type: "INTEGER", nullable: false),
                    NatLoopbackSupported = table.Column<bool>(type: "INTEGER", nullable: false),
                    ServerFixApplyDNSFix = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlexServers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlexAccountServers",
                columns: table => new
                {
                    PlexAccountId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlexServerId = table.Column<int>(type: "INTEGER", nullable: false),
                    AuthToken = table.Column<string>(type: "TEXT", nullable: true),
                    AuthTokenCreationDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlexAccountServers", x => new { x.PlexAccountId, x.PlexServerId });
                    table.ForeignKey(
                        name: "FK_PlexAccountServers_PlexAccounts_PlexAccountId",
                        column: x => x.PlexAccountId,
                        principalTable: "PlexAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlexAccountServers_PlexServers_PlexServerId",
                        column: x => x.PlexServerId,
                        principalTable: "PlexServers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlexLibraries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(type: "TEXT", unicode: false, maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Key = table.Column<string>(type: "TEXT", nullable: true),
                    LibraryLocationPath = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ScannedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SyncedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Uuid = table.Column<Guid>(type: "TEXT", nullable: false),
                    LibraryLocationId = table.Column<int>(type: "INTEGER", nullable: false),
                    MetaData = table.Column<string>(type: "TEXT", nullable: true),
                    PlexServerId = table.Column<int>(type: "INTEGER", nullable: false),
                    DefaultDestinationId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlexLibraries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlexLibraries_FolderPaths_DefaultDestinationId",
                        column: x => x.DefaultDestinationId,
                        principalTable: "FolderPaths",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_PlexLibraries_PlexServers_PlexServerId",
                        column: x => x.PlexServerId,
                        principalTable: "PlexServers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlexServerConnections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Protocol = table.Column<string>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    Port = table.Column<int>(type: "INTEGER", nullable: false),
                    Local = table.Column<bool>(type: "INTEGER", nullable: false),
                    Relay = table.Column<bool>(type: "INTEGER", nullable: false),
                    IPv6 = table.Column<bool>(type: "INTEGER", nullable: false),
                    PlexServerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlexServerConnections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlexServerConnections_PlexServers_PlexServerId",
                        column: x => x.PlexServerId,
                        principalTable: "PlexServers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DownloadTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Key = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    Percentage = table.Column<decimal>(type: "TEXT", nullable: false),
                    DataReceived = table.Column<long>(type: "INTEGER", nullable: false),
                    DataTotal = table.Column<long>(type: "INTEGER", nullable: false),
                    MediaType = table.Column<string>(type: "TEXT", unicode: false, maxLength: 20, nullable: false),
                    DownloadStatus = table.Column<string>(type: "TEXT", unicode: false, maxLength: 20, nullable: false),
                    DownloadTaskType = table.Column<string>(type: "TEXT", unicode: false, maxLength: 50, nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", nullable: true),
                    FileLocationUrl = table.Column<string>(type: "TEXT", nullable: true),
                    DownloadUrl = table.Column<string>(type: "TEXT", nullable: true),
                    FullTitle = table.Column<string>(type: "TEXT", nullable: true),
                    Quality = table.Column<string>(type: "TEXT", nullable: true),
                    DownloadDirectory = table.Column<string>(type: "TEXT", nullable: true),
                    DestinationDirectory = table.Column<string>(type: "TEXT", nullable: true),
                    DownloadSpeed = table.Column<int>(type: "INTEGER", nullable: false),
                    ServerMachineIdentifier = table.Column<string>(type: "TEXT", nullable: true),
                    Priority = table.Column<long>(type: "INTEGER", nullable: false),
                    PlexServerId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlexLibraryId = table.Column<int>(type: "INTEGER", nullable: false),
                    DestinationFolderId = table.Column<int>(type: "INTEGER", nullable: false),
                    DownloadFolderId = table.Column<int>(type: "INTEGER", nullable: false),
                    ParentId = table.Column<int>(type: "INTEGER", nullable: true),
                    RootDownloadTaskId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DownloadTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DownloadTasks_DownloadTasks_ParentId",
                        column: x => x.ParentId,
                        principalTable: "DownloadTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DownloadTasks_DownloadTasks_RootDownloadTaskId",
                        column: x => x.RootDownloadTaskId,
                        principalTable: "DownloadTasks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DownloadTasks_FolderPaths_DestinationFolderId",
                        column: x => x.DestinationFolderId,
                        principalTable: "FolderPaths",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DownloadTasks_FolderPaths_DownloadFolderId",
                        column: x => x.DownloadFolderId,
                        principalTable: "FolderPaths",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DownloadTasks_PlexLibraries_PlexLibraryId",
                        column: x => x.PlexLibraryId,
                        principalTable: "PlexLibraries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DownloadTasks_PlexServers_PlexServerId",
                        column: x => x.PlexServerId,
                        principalTable: "PlexServers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlexAccountLibraries",
                columns: table => new
                {
                    PlexAccountId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlexLibraryId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlexServerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlexAccountLibraries", x => new { x.PlexAccountId, x.PlexLibraryId, x.PlexServerId });
                    table.ForeignKey(
                        name: "FK_PlexAccountLibraries_PlexAccounts_PlexAccountId",
                        column: x => x.PlexAccountId,
                        principalTable: "PlexAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlexAccountLibraries_PlexLibraries_PlexLibraryId",
                        column: x => x.PlexLibraryId,
                        principalTable: "PlexLibraries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlexAccountLibraries_PlexServers_PlexServerId",
                        column: x => x.PlexServerId,
                        principalTable: "PlexServers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlexMovie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Key = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    SortTitle = table.Column<string>(type: "TEXT", nullable: true),
                    Duration = table.Column<int>(type: "INTEGER", nullable: false),
                    MediaSize = table.Column<long>(type: "INTEGER", nullable: false),
                    MetaDataKey = table.Column<int>(type: "INTEGER", nullable: false),
                    Studio = table.Column<string>(type: "TEXT", nullable: true),
                    Summary = table.Column<string>(type: "TEXT", nullable: true),
                    ContentRating = table.Column<string>(type: "TEXT", nullable: true),
                    Rating = table.Column<double>(type: "REAL", nullable: false),
                    ChildCount = table.Column<int>(type: "INTEGER", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    OriginallyAvailableAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Index = table.Column<int>(type: "INTEGER", nullable: false),
                    HasThumb = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasArt = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasBanner = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasTheme = table.Column<bool>(type: "INTEGER", nullable: false),
                    FullTitle = table.Column<string>(type: "TEXT", nullable: true),
                    MediaData = table.Column<string>(type: "TEXT", nullable: true),
                    PlexLibraryId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlexServerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlexMovie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlexMovie_PlexLibraries_PlexLibraryId",
                        column: x => x.PlexLibraryId,
                        principalTable: "PlexLibraries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlexMovie_PlexServers_PlexServerId",
                        column: x => x.PlexServerId,
                        principalTable: "PlexServers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlexTvShows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Key = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    SortTitle = table.Column<string>(type: "TEXT", nullable: true),
                    Duration = table.Column<int>(type: "INTEGER", nullable: false),
                    MediaSize = table.Column<long>(type: "INTEGER", nullable: false),
                    MetaDataKey = table.Column<int>(type: "INTEGER", nullable: false),
                    Studio = table.Column<string>(type: "TEXT", nullable: true),
                    Summary = table.Column<string>(type: "TEXT", nullable: true),
                    ContentRating = table.Column<string>(type: "TEXT", nullable: true),
                    Rating = table.Column<double>(type: "REAL", nullable: false),
                    ChildCount = table.Column<int>(type: "INTEGER", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    OriginallyAvailableAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Index = table.Column<int>(type: "INTEGER", nullable: false),
                    HasThumb = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasArt = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasBanner = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasTheme = table.Column<bool>(type: "INTEGER", nullable: false),
                    FullTitle = table.Column<string>(type: "TEXT", nullable: true),
                    MediaData = table.Column<string>(type: "TEXT", nullable: true),
                    PlexLibraryId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlexServerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlexTvShows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlexTvShows_PlexLibraries_PlexLibraryId",
                        column: x => x.PlexLibraryId,
                        principalTable: "PlexLibraries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlexTvShows_PlexServers_PlexServerId",
                        column: x => x.PlexServerId,
                        principalTable: "PlexServers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlexServerStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StatusCode = table.Column<int>(type: "INTEGER", nullable: false),
                    IsSuccessful = table.Column<bool>(type: "INTEGER", nullable: false),
                    StatusMessage = table.Column<string>(type: "TEXT", nullable: true),
                    LastChecked = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PlexServerId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlexServerConnectionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlexServerStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlexServerStatuses_PlexServerConnections_PlexServerConnectionId",
                        column: x => x.PlexServerConnectionId,
                        principalTable: "PlexServerConnections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlexServerStatuses_PlexServers_PlexServerId",
                        column: x => x.PlexServerId,
                        principalTable: "PlexServers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DownloadWorkerTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FileName = table.Column<string>(type: "TEXT", nullable: true),
                    PartIndex = table.Column<int>(type: "INTEGER", nullable: false),
                    StartByte = table.Column<long>(type: "INTEGER", nullable: false),
                    EndByte = table.Column<long>(type: "INTEGER", nullable: false),
                    DownloadStatus = table.Column<string>(type: "TEXT", unicode: false, maxLength: 20, nullable: false),
                    BytesReceived = table.Column<long>(type: "INTEGER", nullable: false),
                    TempDirectory = table.Column<string>(type: "TEXT", nullable: true),
                    ElapsedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    DownloadUrl = table.Column<string>(type: "TEXT", nullable: true),
                    DownloadTaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlexServerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DownloadWorkerTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DownloadWorkerTasks_DownloadTasks_DownloadTaskId",
                        column: x => x.DownloadTaskId,
                        principalTable: "DownloadTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DownloadWorkerTasks_PlexServers_PlexServerId",
                        column: x => x.PlexServerId,
                        principalTable: "PlexServers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FilePathsCompressed = table.Column<string>(type: "TEXT", nullable: true),
                    DownloadTaskId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileTasks_DownloadTasks_DownloadTaskId",
                        column: x => x.DownloadTaskId,
                        principalTable: "DownloadTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlexMovieGenres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    PlexGenreId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlexMoviesId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlexMovieId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlexMovieGenres", x => new { x.PlexMoviesId, x.PlexGenreId });
                    table.ForeignKey(
                        name: "FK_PlexMovieGenres_PlexGenres_PlexGenreId",
                        column: x => x.PlexGenreId,
                        principalTable: "PlexGenres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlexMovieGenres_PlexMovie_PlexMovieId",
                        column: x => x.PlexMovieId,
                        principalTable: "PlexMovie",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlexMovieRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    PlexGenreId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlexMoviesId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlexMovieId = table.Column<int>(type: "INTEGER", nullable: true),
                    PlexRoleId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlexMovieRoles", x => new { x.PlexMoviesId, x.PlexGenreId });
                    table.ForeignKey(
                        name: "FK_PlexMovieRoles_PlexGenres_PlexGenreId",
                        column: x => x.PlexGenreId,
                        principalTable: "PlexGenres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlexMovieRoles_PlexMovie_PlexMovieId",
                        column: x => x.PlexMovieId,
                        principalTable: "PlexMovie",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlexMovieRoles_PlexRoles_PlexRoleId",
                        column: x => x.PlexRoleId,
                        principalTable: "PlexRoles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlexTvShowGenre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    PlexGenreId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlexTvShowId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlexTvShowGenre", x => new { x.PlexTvShowId, x.PlexGenreId });
                    table.ForeignKey(
                        name: "FK_PlexTvShowGenre_PlexGenres_PlexGenreId",
                        column: x => x.PlexGenreId,
                        principalTable: "PlexGenres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlexTvShowGenre_PlexTvShows_PlexTvShowId",
                        column: x => x.PlexTvShowId,
                        principalTable: "PlexTvShows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlexTvShowRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    PlexGenreId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlexTvShowId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlexTvShowRole", x => new { x.PlexTvShowId, x.PlexGenreId });
                    table.ForeignKey(
                        name: "FK_PlexTvShowRole_PlexGenres_PlexGenreId",
                        column: x => x.PlexGenreId,
                        principalTable: "PlexGenres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlexTvShowRole_PlexTvShows_PlexTvShowId",
                        column: x => x.PlexTvShowId,
                        principalTable: "PlexTvShows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlexTvShowSeason",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Key = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    SortTitle = table.Column<string>(type: "TEXT", nullable: true),
                    Duration = table.Column<int>(type: "INTEGER", nullable: false),
                    MediaSize = table.Column<long>(type: "INTEGER", nullable: false),
                    MetaDataKey = table.Column<int>(type: "INTEGER", nullable: false),
                    Studio = table.Column<string>(type: "TEXT", nullable: true),
                    Summary = table.Column<string>(type: "TEXT", nullable: true),
                    ContentRating = table.Column<string>(type: "TEXT", nullable: true),
                    Rating = table.Column<double>(type: "REAL", nullable: false),
                    ChildCount = table.Column<int>(type: "INTEGER", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    OriginallyAvailableAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Index = table.Column<int>(type: "INTEGER", nullable: false),
                    HasThumb = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasArt = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasBanner = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasTheme = table.Column<bool>(type: "INTEGER", nullable: false),
                    FullTitle = table.Column<string>(type: "TEXT", nullable: true),
                    MediaData = table.Column<string>(type: "TEXT", nullable: true),
                    ParentKey = table.Column<int>(type: "INTEGER", nullable: false),
                    TvShowId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlexLibraryId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlexServerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlexTvShowSeason", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlexTvShowSeason_PlexLibraries_PlexLibraryId",
                        column: x => x.PlexLibraryId,
                        principalTable: "PlexLibraries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlexTvShowSeason_PlexServers_PlexServerId",
                        column: x => x.PlexServerId,
                        principalTable: "PlexServers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlexTvShowSeason_PlexTvShows_TvShowId",
                        column: x => x.TvShowId,
                        principalTable: "PlexTvShows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DownloadWorkerTasksLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Message = table.Column<string>(type: "TEXT", nullable: true),
                    LogLevel = table.Column<string>(type: "TEXT", unicode: false, maxLength: 20, nullable: false),
                    DownloadWorkerTaskId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DownloadWorkerTasksLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DownloadWorkerTasksLogs_DownloadWorkerTasks_DownloadWorkerTaskId",
                        column: x => x.DownloadWorkerTaskId,
                        principalTable: "DownloadWorkerTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlexTvShowEpisodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Key = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    SortTitle = table.Column<string>(type: "TEXT", nullable: true),
                    Duration = table.Column<int>(type: "INTEGER", nullable: false),
                    MediaSize = table.Column<long>(type: "INTEGER", nullable: false),
                    MetaDataKey = table.Column<int>(type: "INTEGER", nullable: false),
                    Studio = table.Column<string>(type: "TEXT", nullable: true),
                    Summary = table.Column<string>(type: "TEXT", nullable: true),
                    ContentRating = table.Column<string>(type: "TEXT", nullable: true),
                    Rating = table.Column<double>(type: "REAL", nullable: false),
                    ChildCount = table.Column<int>(type: "INTEGER", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    OriginallyAvailableAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Index = table.Column<int>(type: "INTEGER", nullable: false),
                    HasThumb = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasArt = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasBanner = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasTheme = table.Column<bool>(type: "INTEGER", nullable: false),
                    FullTitle = table.Column<string>(type: "TEXT", nullable: true),
                    MediaData = table.Column<string>(type: "TEXT", nullable: true),
                    ParentKey = table.Column<int>(type: "INTEGER", nullable: false),
                    TvShowId = table.Column<int>(type: "INTEGER", nullable: false),
                    TvShowSeasonId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlexLibraryId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlexServerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlexTvShowEpisodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlexTvShowEpisodes_PlexLibraries_PlexLibraryId",
                        column: x => x.PlexLibraryId,
                        principalTable: "PlexLibraries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlexTvShowEpisodes_PlexServers_PlexServerId",
                        column: x => x.PlexServerId,
                        principalTable: "PlexServers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlexTvShowEpisodes_PlexTvShows_TvShowId",
                        column: x => x.TvShowId,
                        principalTable: "PlexTvShows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlexTvShowEpisodes_PlexTvShowSeason_TvShowSeasonId",
                        column: x => x.TvShowSeasonId,
                        principalTable: "PlexTvShowSeason",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "FolderPaths",
                columns: new[] { "Id", "DirectoryPath", "DisplayName", "FolderType", "MediaType" },
                values: new object[] { 1, "/Downloads", "Download Path", "DownloadFolder", "None" });

            migrationBuilder.InsertData(
                table: "FolderPaths",
                columns: new[] { "Id", "DirectoryPath", "DisplayName", "FolderType", "MediaType" },
                values: new object[] { 2, "/Movies", "Movie Destination Path", "MovieFolder", "Movie" });

            migrationBuilder.InsertData(
                table: "FolderPaths",
                columns: new[] { "Id", "DirectoryPath", "DisplayName", "FolderType", "MediaType" },
                values: new object[] { 3, "/TvShows", "Tv Show Destination Path", "TvShowFolder", "TvShow" });

            migrationBuilder.InsertData(
                table: "FolderPaths",
                columns: new[] { "Id", "DirectoryPath", "DisplayName", "FolderType", "MediaType" },
                values: new object[] { 4, "/Music", "Music Destination Path", "MusicFolder", "Music" });

            migrationBuilder.InsertData(
                table: "FolderPaths",
                columns: new[] { "Id", "DirectoryPath", "DisplayName", "FolderType", "MediaType" },
                values: new object[] { 5, "/Photos", "Photos Destination Path", "PhotosFolder", "Photos" });

            migrationBuilder.InsertData(
                table: "FolderPaths",
                columns: new[] { "Id", "DirectoryPath", "DisplayName", "FolderType", "MediaType" },
                values: new object[] { 6, "/Other", "Other Videos Destination Path", "OtherVideosFolder", "OtherVideos" });

            migrationBuilder.InsertData(
                table: "FolderPaths",
                columns: new[] { "Id", "DirectoryPath", "DisplayName", "FolderType", "MediaType" },
                values: new object[] { 7, "/Games", "Games Videos Destination Path", "GamesVideosFolder", "Games" });

            migrationBuilder.InsertData(
                table: "FolderPaths",
                columns: new[] { "Id", "DirectoryPath", "DisplayName", "FolderType", "MediaType" },
                values: new object[] { 8, "/", "Reserved #1 Destination Path", "None", "None" });

            migrationBuilder.InsertData(
                table: "FolderPaths",
                columns: new[] { "Id", "DirectoryPath", "DisplayName", "FolderType", "MediaType" },
                values: new object[] { 9, "/", "Reserved #2 Destination Path", "None", "None" });

            migrationBuilder.InsertData(
                table: "FolderPaths",
                columns: new[] { "Id", "DirectoryPath", "DisplayName", "FolderType", "MediaType" },
                values: new object[] { 10, "/", "Reserved #3 Destination Path", "None", "None" });

            migrationBuilder.CreateIndex(
                name: "IX_DownloadTasks_DestinationFolderId",
                table: "DownloadTasks",
                column: "DestinationFolderId");

            migrationBuilder.CreateIndex(
                name: "IX_DownloadTasks_DownloadFolderId",
                table: "DownloadTasks",
                column: "DownloadFolderId");

            migrationBuilder.CreateIndex(
                name: "IX_DownloadTasks_ParentId",
                table: "DownloadTasks",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_DownloadTasks_PlexLibraryId",
                table: "DownloadTasks",
                column: "PlexLibraryId");

            migrationBuilder.CreateIndex(
                name: "IX_DownloadTasks_PlexServerId",
                table: "DownloadTasks",
                column: "PlexServerId");

            migrationBuilder.CreateIndex(
                name: "IX_DownloadTasks_RootDownloadTaskId",
                table: "DownloadTasks",
                column: "RootDownloadTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_DownloadWorkerTasks_DownloadTaskId",
                table: "DownloadWorkerTasks",
                column: "DownloadTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_DownloadWorkerTasks_PlexServerId",
                table: "DownloadWorkerTasks",
                column: "PlexServerId");

            migrationBuilder.CreateIndex(
                name: "IX_DownloadWorkerTasksLogs_DownloadWorkerTaskId",
                table: "DownloadWorkerTasksLogs",
                column: "DownloadWorkerTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_FileTasks_DownloadTaskId",
                table: "FileTasks",
                column: "DownloadTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_PlexAccountLibraries_PlexLibraryId",
                table: "PlexAccountLibraries",
                column: "PlexLibraryId");

            migrationBuilder.CreateIndex(
                name: "IX_PlexAccountLibraries_PlexServerId",
                table: "PlexAccountLibraries",
                column: "PlexServerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlexAccountServers_PlexServerId",
                table: "PlexAccountServers",
                column: "PlexServerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlexLibraries_DefaultDestinationId",
                table: "PlexLibraries",
                column: "DefaultDestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_PlexLibraries_PlexServerId",
                table: "PlexLibraries",
                column: "PlexServerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlexMovie_PlexLibraryId",
                table: "PlexMovie",
                column: "PlexLibraryId");

            migrationBuilder.CreateIndex(
                name: "IX_PlexMovie_PlexServerId",
                table: "PlexMovie",
                column: "PlexServerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlexMovieGenres_PlexGenreId",
                table: "PlexMovieGenres",
                column: "PlexGenreId");

            migrationBuilder.CreateIndex(
                name: "IX_PlexMovieGenres_PlexMovieId",
                table: "PlexMovieGenres",
                column: "PlexMovieId");

            migrationBuilder.CreateIndex(
                name: "IX_PlexMovieRoles_PlexGenreId",
                table: "PlexMovieRoles",
                column: "PlexGenreId");

            migrationBuilder.CreateIndex(
                name: "IX_PlexMovieRoles_PlexMovieId",
                table: "PlexMovieRoles",
                column: "PlexMovieId");

            migrationBuilder.CreateIndex(
                name: "IX_PlexMovieRoles_PlexRoleId",
                table: "PlexMovieRoles",
                column: "PlexRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_PlexServerConnections_PlexServerId",
                table: "PlexServerConnections",
                column: "PlexServerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlexServerStatuses_PlexServerConnectionId",
                table: "PlexServerStatuses",
                column: "PlexServerConnectionId");

            migrationBuilder.CreateIndex(
                name: "IX_PlexServerStatuses_PlexServerId",
                table: "PlexServerStatuses",
                column: "PlexServerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlexTvShowEpisodes_PlexLibraryId",
                table: "PlexTvShowEpisodes",
                column: "PlexLibraryId");

            migrationBuilder.CreateIndex(
                name: "IX_PlexTvShowEpisodes_PlexServerId",
                table: "PlexTvShowEpisodes",
                column: "PlexServerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlexTvShowEpisodes_TvShowId",
                table: "PlexTvShowEpisodes",
                column: "TvShowId");

            migrationBuilder.CreateIndex(
                name: "IX_PlexTvShowEpisodes_TvShowSeasonId",
                table: "PlexTvShowEpisodes",
                column: "TvShowSeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_PlexTvShowGenre_PlexGenreId",
                table: "PlexTvShowGenre",
                column: "PlexGenreId");

            migrationBuilder.CreateIndex(
                name: "IX_PlexTvShowRole_PlexGenreId",
                table: "PlexTvShowRole",
                column: "PlexGenreId");

            migrationBuilder.CreateIndex(
                name: "IX_PlexTvShows_PlexLibraryId",
                table: "PlexTvShows",
                column: "PlexLibraryId");

            migrationBuilder.CreateIndex(
                name: "IX_PlexTvShows_PlexServerId",
                table: "PlexTvShows",
                column: "PlexServerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlexTvShowSeason_PlexLibraryId",
                table: "PlexTvShowSeason",
                column: "PlexLibraryId");

            migrationBuilder.CreateIndex(
                name: "IX_PlexTvShowSeason_PlexServerId",
                table: "PlexTvShowSeason",
                column: "PlexServerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlexTvShowSeason_TvShowId",
                table: "PlexTvShowSeason",
                column: "TvShowId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DownloadWorkerTasksLogs");

            migrationBuilder.DropTable(
                name: "FileTasks");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "PlexAccountLibraries");

            migrationBuilder.DropTable(
                name: "PlexAccountServers");

            migrationBuilder.DropTable(
                name: "PlexMovieGenres");

            migrationBuilder.DropTable(
                name: "PlexMovieRoles");

            migrationBuilder.DropTable(
                name: "PlexServerStatuses");

            migrationBuilder.DropTable(
                name: "PlexTvShowEpisodes");

            migrationBuilder.DropTable(
                name: "PlexTvShowGenre");

            migrationBuilder.DropTable(
                name: "PlexTvShowRole");

            migrationBuilder.DropTable(
                name: "DownloadWorkerTasks");

            migrationBuilder.DropTable(
                name: "PlexAccounts");

            migrationBuilder.DropTable(
                name: "PlexMovie");

            migrationBuilder.DropTable(
                name: "PlexRoles");

            migrationBuilder.DropTable(
                name: "PlexServerConnections");

            migrationBuilder.DropTable(
                name: "PlexTvShowSeason");

            migrationBuilder.DropTable(
                name: "PlexGenres");

            migrationBuilder.DropTable(
                name: "DownloadTasks");

            migrationBuilder.DropTable(
                name: "PlexTvShows");

            migrationBuilder.DropTable(
                name: "PlexLibraries");

            migrationBuilder.DropTable(
                name: "FolderPaths");

            migrationBuilder.DropTable(
                name: "PlexServers");
        }
    }
}
