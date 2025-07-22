using System.Collections;
using System.Text.Json.Serialization;

namespace Jiguang.JPush.Model;

/// <summary>
/// 自定义消息。
/// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_push/#message"/>
/// </summary>
public class Message
{
    /// <summary>
    /// 消息内容本身（必填）。
    /// </summary>
    [JsonPropertyName("msg_content")]
    public String? Content { get; set; }

    [JsonPropertyName("title")]
    public String? Title { get; set; }

    [JsonPropertyName("content_type")]
    public String? ContentType { get; set; }

    [JsonPropertyName("extras")]
    public IDictionary? Extras { get; set; }
}
