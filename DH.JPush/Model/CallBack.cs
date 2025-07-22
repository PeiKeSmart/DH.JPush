using System.Text.Json.Serialization;

namespace Jiguang.JPush.Model;

/// <summary>
/// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_push/#callback"/>
/// </summary>
public class CallBack
{
    [JsonPropertyName("url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public String? Url { get; set; }

    [JsonPropertyName("params")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<String, Object>? Params { get; set; }

    [JsonPropertyName("type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Int32 Type { get; set; }
}