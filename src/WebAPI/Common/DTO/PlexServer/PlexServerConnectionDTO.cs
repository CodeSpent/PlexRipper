using Newtonsoft.Json;

namespace PlexRipper.WebAPI.Common.DTO;

public class PlexServerConnectionDTO
{
    #region Properties

    [JsonProperty("id", Required = Required.Always)]
    public int Id { get; set; }

    [JsonProperty("protocol", Required = Required.Always)]
    public string Protocol { get; set; }

    [JsonProperty("address", Required = Required.Always)]
    public string Address { get; set; }

    [JsonProperty("port", Required = Required.Always)]
    public int Port { get; set; }

    [JsonProperty("local", Required = Required.Always)]
    public bool Local { get; set; }

    [JsonProperty("relay", Required = Required.Always)]
    public bool Relay { get; set; }

    [JsonProperty("ipv6", Required = Required.Always)]
    public bool IPv6 { get; set; }

    [JsonProperty("plexServerId", Required = Required.Always)]
    public int PlexServerId { get; set; }

    [JsonProperty("url", Required = Required.Always)]
    public string Url { get; set; }

    #endregion
}