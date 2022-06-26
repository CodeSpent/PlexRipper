﻿using System.Collections.Generic;
using FluentResults;
using MediatR;

namespace PlexRipper.Application
{
    public class ClearCompletedDownloadTasksCommand : IRequest<Result>
    {
        public List<int> DownloadTaskIds { get; }

        public ClearCompletedDownloadTasksCommand(List<int> downloadTaskIds = null)
        {
            DownloadTaskIds = downloadTaskIds;
        }
    }
}