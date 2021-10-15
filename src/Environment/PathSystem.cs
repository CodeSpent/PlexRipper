﻿using System.IO;
using System.Reflection;

namespace Environment
{
    public class PathSystem : IPathSystem
    {
        #region Properties

        public static string ConfigDirectory => Path.Combine(RootDirectory, "config");

        public static string ConfigFileLocation => Path.Join(ConfigDirectory, ConfigFileName);

        public static string ConfigFileName => "PlexRipperSettings.json";

        public static string DatabaseBackupDirectory => Path.Combine(ConfigDirectory, "Database BackUp");

        public static string DatabaseName => EnvironmentExtensions.IsIntegrationTestMode() ? "PlexRipperDB_Tests.db" : "PlexRipperDB.db";

        public static string DatabasePath => Path.Combine(ConfigDirectory, DatabaseName);

        public static string LogsDirectory => Path.Combine(RootDirectory, "config", "logs");

        public static string RootDirectory
        {
            get
            {
                switch (OsInfo.CurrentOS)
                {
                    case OperatingSystemPlatform.Linux:
                    case OperatingSystemPlatform.Osx:
                        return "/";
                    case OperatingSystemPlatform.Windows:
                        return Path.GetPathRoot(Assembly.GetExecutingAssembly().Location) ?? @"C:\";
                    default:
                        return "/";
                }
            }
        }

        #region Interface Implementations

        string IPathSystem.RootDirectory => RootDirectory;

        string IPathSystem.ConfigFileLocation => ConfigFileLocation;

        string IPathSystem.ConfigFileName => ConfigFileName;

        string IPathSystem.DatabaseBackupDirectory => DatabaseBackupDirectory;

        string IPathSystem.DatabaseName => DatabaseName;

        string IPathSystem.DatabasePath => DatabasePath;

        string IPathSystem.LogsDirectory => LogsDirectory;

        string IPathSystem.ConfigDirectory => ConfigDirectory;

        #endregion

        #endregion
    }
}