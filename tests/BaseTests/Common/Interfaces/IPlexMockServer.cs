﻿using System.Collections.Generic;
using WireMock.Server;

namespace PlexRipper.BaseTests
{
    public interface IPlexMockServer
    {
        string MockMovieMediaPath { get; }

        List<MockMediaData> GetMockMediaData();

        MockMediaData GetDefaultMovieMockMediaData();

        WireMockServer GetPlexMockServer();
    }
}