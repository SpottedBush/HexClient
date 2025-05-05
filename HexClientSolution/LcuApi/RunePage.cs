using Newtonsoft.Json;

namespace LcuApi;

public class RunePage
{
    [JsonProperty("formatVersion")]
    public int FormatVersion = 4;

    [JsonProperty("current")]
    public bool IsCurrentPage { get; set; }

    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("isActive")]
    public bool IsActive { get; set; }

    [JsonProperty("isDeletable")]
    public bool IsDeletable { get; set; } = true;

    [JsonProperty("isEditable")]
    public bool IsEditable { get; set; } = true;

    [JsonProperty("isValid")]
    public bool IsValid { get; set; } = true;

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("primaryStyleId")]
    public int PrimaryTreeId { get; set; }

    [JsonProperty("selectedPerkIds")]
    public int[]? SelectedRunes { get; set; }

    [JsonProperty("subStyleId")]
    public int SecondaryTreeId { get; set; }
}