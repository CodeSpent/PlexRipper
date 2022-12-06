﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlexRipper.Application;
using PlexRipper.WebAPI.Common.DTO;
using PlexRipper.WebAPI.Common.FluentResult;

namespace PlexRipper.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlexServerController : BaseController
{
    private readonly IPlexServerService _plexServerService;

    public PlexServerController(IPlexServerService plexServerService, IMapper mapper, INotificationsService notificationsService) : base(mapper,
        notificationsService)
    {
        _plexServerService = plexServerService;
    }

    // GET api/<PlexServerController>/
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDTO<List<PlexServerDTO>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResultDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResultDTO))]
    public async Task<IActionResult> GetAll()
    {
        return ToActionResult<List<PlexServer>, List<PlexServerDTO>>(await _plexServerService.GetAllPlexServersAsync(true));
    }

    // GET api/<PlexServerController>/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDTO<PlexServerDTO>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResultDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResultDTO))]
    public async Task<IActionResult> GetById(int id)
    {
        if (id <= 0)
            return BadRequestInvalidId();

        return ToActionResult<PlexServer, PlexServerDTO>(await _plexServerService.GetServerAsync(id));
    }

    // GET api/<PlexServerController>/5/inspect
    [HttpGet("inspect")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDTO<PlexServerDTO>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResultDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResultDTO))]
    public async Task<IActionResult> InspectServer(int id)
    {
        if (id <= 0)
            return BadRequestInvalidId();

        return ToActionResult<PlexServer, PlexServerDTO>(await _plexServerService.InspectPlexServerConnections(id));
    }

    // GET api/<PlexServerController>/5/sync
    [HttpGet("{id:int}/sync")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResultDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResultDTO))]
    public async Task<IActionResult> SyncServer(int id, [FromQuery] bool forceSync = false)
    {
        return ToActionResult(await _plexServerService.SyncPlexServer(id, forceSync));
    }
}