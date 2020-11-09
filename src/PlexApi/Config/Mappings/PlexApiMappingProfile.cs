﻿using System.Linq;
using AutoMapper;
using PlexRipper.Domain;
using PlexRipper.Domain.AutoMapper.ValueConverters;
using PlexRipper.PlexApi.Models;
using PlexRipper.PlexApi.Models.Server;

namespace PlexRipper.PlexApi.Config.Mappings
{
    public class PlexApiMappingProfile : Profile
    {
        public PlexApiMappingProfile()
        {
            // PlexUser -> PlexAccount
            CreateMap<PlexUserDTO, PlexAccount>(MemberList.None)
                .ForMember(dest => dest.PlexAccountServers, opt => opt.Ignore())
                .ForMember(dest => dest.PlexId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            // Server <-> PlexServer
            CreateMap<Server, PlexServer>(MemberList.Destination)
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PlexLibraries, opt => opt.Ignore())
                .ForMember(dest => dest.ServerStatus, opt => opt.Ignore())
                .ForMember(dest => dest.PlexAccountServers, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt,
                    opt => opt.ConvertUsing(new UnixLongStringToDateTimeUTC()))
                .ForMember(dest => dest.UpdatedAt,
                    opt => opt.ConvertUsing(new UnixLongStringToDateTimeUTC()))
                .ReverseMap();

            // MediaContainer -> PlexLibrary
            CreateMap<MediaContainer, PlexLibrary>(MemberList.None)
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Type, opt => opt.MapFrom(x => x.ViewGroup))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(x => x.Title1));

            // Directory -> PlexLibrary
            CreateMap<Directory, PlexLibrary>(MemberList.Destination)
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PlexServer, opt => opt.Ignore())
                .ForMember(dest => dest.PlexServerId, opt => opt.Ignore())
                .ForMember(dest => dest.Movies, opt => opt.Ignore())
                .ForMember(dest => dest.TvShows, opt => opt.Ignore())
                .ForMember(dest => dest.DownloadTasks, opt => opt.Ignore())
                .ForMember(dest => dest.CheckedAt, opt => opt.Ignore())
                .ForMember(dest => dest.PlexAccountLibraries, opt => opt.Ignore())
                .ForMember(dest => dest.MediaType, opt => opt.Ignore())
                .ForMember(dest => dest.LibraryLocationId,
                    opt => opt.MapFrom(src => src.Location.First().Id))

                // Location[0].Path -> LibraryLocationPath
                .ForMember(dest => dest.LibraryLocationPath,
                    opt => opt.MapFrom(src => src.Location.First().Path));

            // Metadata -> PlexTvShow
            CreateMap<Metadata, PlexTvShow>(MemberList.None)
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PlexTvShowGenres, opt => opt.Ignore())
                .ForMember(dest => dest.PlexTvShowRoles, opt => opt.Ignore())
                .ForMember(dest => dest.PlexLibrary, opt => opt.Ignore())
                .ForMember(dest => dest.PlexLibraryId, opt => opt.Ignore())
                .ForMember(dest => dest.Seasons, opt => opt.Ignore())
                .ForMember(dest => dest.OriginallyAvailableAt, opt => opt.ConvertUsing(new StringToDateTimeUTC()));

            // Metadata -> PlexTvShowSeason
            CreateMap<Metadata, PlexTvShowSeason>(MemberList.None)
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.TvShow, opt => opt.Ignore())
                .ForMember(dest => dest.TvShowId, opt => opt.Ignore())
                .ForMember(dest => dest.PlexLibrary, opt => opt.Ignore())
                .ForMember(dest => dest.PlexLibraryId, opt => opt.Ignore())
                .ForMember(dest => dest.Episodes, opt => opt.Ignore());

            // Metadata -> PlexTvShowEpisode
            CreateMap<Metadata, PlexTvShowEpisode>(MemberList.None)
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.TvShowSeason, opt => opt.Ignore())
                .ForMember(dest => dest.TvShowSeasonId, opt => opt.Ignore())
                .ForMember(dest => dest.PlexLibrary, opt => opt.Ignore())
                .ForMember(dest => dest.PlexLibraryId, opt => opt.Ignore());

            // PlexMediaContainer -> PlexMediaMetaData
            CreateMap<PlexMediaContainer, PlexMediaMetaData>(MemberList.Destination)
                .ConvertUsing<PlexMediaMetaDataDTOPlexMediaMetaData>();

            // Medium -> PlexMediaData
            CreateMap<Medium, PlexMediaData>(MemberList.Destination)
                .ForMember(dest => dest.MediaFormat, opt => opt.MapFrom(src => src.Container))
                .ForMember(dest => dest.Parts, opt => opt.MapFrom(src => src.Part.ToList()));

            // Part -> PlexMediaDataPart
            CreateMap<Part, PlexMediaDataPart>(MemberList.Destination)
                .ForMember(dest => dest.ObfuscatedFilePath, opt => opt.MapFrom(x => x.Key));

            PlexMovieMappings();
        }

        private void PlexMovieMappings()
        {
            // Metadata -> PlexMovie
            CreateMap<Metadata, PlexMovie>(MemberList.Destination)
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PlexMovieGenres, opt => opt.Ignore())
                .ForMember(dest => dest.PlexMovieRoles, opt => opt.Ignore())
                .ForMember(dest => dest.PlexLibrary, opt => opt.Ignore())
                .ForMember(dest => dest.PlexLibraryId, opt => opt.Ignore())
                .ForMember(dest => dest.PlexMovieDatas, opt => opt.MapFrom(x => x.Media))
                .ForMember(dest => dest.GetParts, opt => opt.Ignore())
                .ForMember(dest => dest.OriginallyAvailableAt, opt => opt.ConvertUsing(new StringToDateTimeUTC()));

            // Medium -> PlexMovieData
            CreateMap<Medium, PlexMovieData>(MemberList.Destination)
                .ConvertUsing<MediumToPlexMovieData>();

            // Part -> PlexMovieDataPart
            CreateMap<Part, PlexMovieDataPart>(MemberList.Destination)
                .ConvertUsing<PartToPlexMovieDataPart>();
        }
    }
}