using Newtonsoft.Json;

namespace LcuApi.DataObjects;

public class RunePage
{
    [JsonProperty("formatVersion")]
    public int FormatVersion;

    [JsonProperty("current")]
    public bool IsCurrentPage { get; set; }

    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("isActive")]
    public bool IsActive { get; set; }

    [JsonProperty("isDeletable")]
    public bool IsDeletable { get; set; }

    [JsonProperty("isEditable")]
    public bool IsEditable { get; set; }

    [JsonProperty("isValid")]
    public bool IsValid { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("primaryStyleId")]
    public int PrimaryTreeId { get; set; }

    [JsonProperty("selectedPerkIds")]
    public int[] SelectedRunes { get; set; }

    [JsonProperty("subStyleId")]
    public int SecondaryTreeId { get; set; }

    public RunePage()
    {
        FormatVersion = 4;
        IsDeletable = true;
        IsEditable = true;
        IsValid = true;
    }
}