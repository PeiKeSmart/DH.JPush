using System.Text.Json;
using System.Text.Json.Serialization;

namespace Jiguang.JPush.Model;

public class PushPayload
{
    [JsonPropertyName("cid")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public String? CId { get; set; }

    /// <summary>
    /// 推送平台。可以为 "android" / "ios" / "all"。
    /// </summary>

    [JsonPropertyName("callback")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public CallBack? CallBack { get; set; }

    [JsonPropertyName("notification_3rd")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Notification3rd? Notification3rd { get; set; }

    [JsonPropertyName("platform")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object Platform { get; set; } = "all";

    [JsonPropertyName("audience")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Object Audience { get; set; } = "all";

    [JsonPropertyName("notification")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Notification? Notification { get; set; }

    [JsonPropertyName("message")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Message? Message { get; set; }

    [JsonPropertyName("sms_message")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public SmsMessage? SMSMessage { get; set; }

    [JsonPropertyName("options")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(OptionsJsonConvert))]
    public Options Options { get; set; } = new Options
    {
        IsApnsProduction = false
    };

    internal string GetJson()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = false
        });
    }

    public override string ToString()
    {
        return GetJson();
    }
}
