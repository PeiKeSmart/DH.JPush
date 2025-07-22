using System.Text.Json;
using System.Text.Json.Serialization;

namespace Jiguang.JPush.Model;

public class DevicePayload
{
    [JsonPropertyName("alias")]
    public String? Alias { get; set; }

    [JsonPropertyName("mobile")]
    public String? Mobile { get; set; }

    [JsonPropertyName("tags")]
    public Dictionary<String, Object>? Tags { get; set;}

    private String GetJson()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = false
        });
    }

    public override String ToString() => GetJson();
}
