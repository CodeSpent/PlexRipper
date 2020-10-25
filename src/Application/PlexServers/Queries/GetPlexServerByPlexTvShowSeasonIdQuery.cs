﻿using FluentResults;
using MediatR;
using PlexRipper.Domain;

namespace PlexRipper.Application.PlexServers.Queries
{
    public class GetPlexServerByPlexTvShowSeasonIdQuery : IRequest<Result<PlexServer>>
    {
        public GetPlexServerByPlexTvShowSeasonIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}