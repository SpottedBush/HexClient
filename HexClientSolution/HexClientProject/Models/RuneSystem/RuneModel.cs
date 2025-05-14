using System.Text.Json.Serialization;

namespace HexClientProject.Models.RuneSystem;

public class RuneModel
{
    public int Id { get; set; }
    public string Key { get; set; } = string.Empty;
    [JsonPropertyName("icon")]
    public string IconPath { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("shortDesc")]
    public string ShortDescription { get; set; } = string.Empty;
    [JsonPropertyName("longDesc")]
    public string LongDescription { get; set; } = string.Empty;
}
