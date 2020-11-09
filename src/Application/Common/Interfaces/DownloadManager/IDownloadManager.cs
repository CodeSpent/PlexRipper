﻿using System.Collections.Generic;
using System.Threading.Tasks;
using FluentResults;
using PlexRipper.Domain;

namespace PlexRipper.Application.Common
{
    public interface IDownloadManager
    {
        Task<Result<bool>> StopDownloadAsync(int downloadTaskId);

        /// <summary>
        /// Adds a list of <see cref="DownloadTask"/>s to the download queue.
        /// </summary>
        /// <param name="downloadTasks">The list of <see cref="DownloadTask"/>s that will be checked and added.</param>
        /// <returns>Returns true if all downloadTasks were added successfully.</returns>
        Task<Result<bool>> AddToDownloadQueueAsync(List<DownloadTask> downloadTasks);

        Task<Result<bool>> RestartDownloadAsync(int downloadTaskId);

        Task<Result<bool>> ClearCompletedAsync();

        /// <summary>
        /// Starts a queued task immediately.
        /// </summary>
        /// <param name="downloadTaskId">The id of the <see cref="DownloadTask"/> to start.</param>
        /// <returns>Is successful.</returns>
        Task<Result<bool>> StartDownload(int downloadTaskId);

        /// <summary>
        /// Pause a currently downloading <see cref="DownloadTask"/>.
        /// </summary>
        /// <param name="downloadTaskId">The id of the <see cref="DownloadTask"/> to pause.</param>
        /// <returns>Is successful.</returns>
        Result<bool> PauseDownload(int downloadTaskId);

        /// <summary>
        /// Deletes the PlexDownloadClient, if active, from the _downloadList, executes its disposal and deletes from database.
        /// </summary>
        /// <param name="downloadTaskId">The id of PlexDownloadClient to delete,
        /// the <see cref="DownloadTask"/> id can be used as these are always the same.</param>
        /// <returns><see cref="Result"/> fails on error.</returns>
        Task<Result> DeleteDownloadClient(int downloadTaskId);

        /// <summary>
        /// Deletes multiple (active) PlexDownloadClients and <see cref="DownloadTask"/> from the database.
        /// </summary>
        /// <param name="downloadTaskIds">The list of <see cref="DownloadTask"/> to delete.</param>
        /// <returns><see cref="Result"/> fails on error.</returns>
        Task<Result> DeleteDownloadClients(IEnumerable<int> downloadTaskIds);
    }
}