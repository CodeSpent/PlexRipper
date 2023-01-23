﻿using Application.Contracts;
using Newtonsoft.Json;

namespace PlexRipper.Application;

public class SyncServerProgress
{
    public SyncServerProgress() { }

    public SyncServerProgress(int serverId, List<LibraryProgress> libraryProgresses)
    {
        Id = serverId;
        LibraryProgresses = libraryProgresses;
        Percentage = DataFormat.GetPercentage(LibraryProgresses.Sum(x => x.Received), LibraryProgresses.Sum(x => x.Total));
    }

    [JsonProperty("id", Required = Required.Always)]
    public int Id { get; set; }

    [JsonProperty("percentage", Required = Required.Always)]
    public decimal Percentage { get; set; }

    [JsonProperty("libraryProgress", Required = Required.Always)]
    public List<LibraryProgress> LibraryProgresses { get; set; }
}