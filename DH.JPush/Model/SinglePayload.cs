using System.Text.Json;
using System.Text.Json.Serialization;

namespace Jiguang.JPush.Model;

public class SinglePayload
{
    /// <summary>
    /// 推送平台。可以为 "android" / "ios" / "all"。
    /// </summary>
    [JsonPropertyName("platform")]
    public Object Platform { get; set; } = "all";

    /// <summary>
    /// 推送设备指定。
    /// 如果是调用RegID方式批量单推接口（/v3/push/batch/regid/single），那此处就是指定regid值；
    /// 如果是调用Alias方式批量单推接口（/v3/push/batch/alias/single），那此处就是指定alias值。
    /// </summary>
    [JsonPropertyName("target")]
    public String? Target { get; set; }

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
    public Options Options { get; set; } = new Options
    {
        IsApnsProduction = false
    };

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
