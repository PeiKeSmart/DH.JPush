using System.Text.Json;
using System.Text.Json.Serialization;

namespace Jiguang.JPush.Model;

public class BatchPushPayload
{
    [JsonPropertyName("pushlist")]
    public Dictionary<String, SinglePayload>? Pushlist { get; set; }

    internal String GetJson()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = false
        });
    }

    public override String ToString() => GetJson();
}
