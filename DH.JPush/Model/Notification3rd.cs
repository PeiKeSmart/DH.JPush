using System.Text.Json.Serialization;

namespace Jiguang.JPush.Model;

/// <summary>
/// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_push/#notification_3rd"/>
/// </summary>
public class Notification3rd
{
    [JsonPropertyName("title")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public String? Url { get; set; }

    /// <summary>
    /// 必填。
    /// </summary>
    [JsonPropertyName("content")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public String? Content { get; set; }

    [JsonPropertyName("channel_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public String? ChannelId { get; set; }

    [JsonPropertyName("uri_activity")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public String? UriActivity { get; set; }

    [JsonPropertyName("uri_action")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public String? UriAction { get; set; }

    [JsonPropertyName("badge_add_num")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public String? BadgeAddNum { get; set; }

    [JsonPropertyName("badge_class")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public String? BadgeClass { get; set; }

    [JsonPropertyName("sound")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public String? Sound { get; set; }

    [JsonPropertyName("extras")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<String, Object>? Extras { get; set; }
}