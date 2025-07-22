using System.Text.Json.Serialization;

namespace Jiguang.JPush.Model;

/// <summary>
/// 短信补充。
/// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_push/#sms_message"/>
/// </summary>
public class SmsMessage
{
    [JsonPropertyName("delay_time")]
    public Int32 DelayTime { get; set; }

    [JsonPropertyName("signid")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Int32 Signid { get; set; }

    [JsonPropertyName("temp_id")]
    public Int64 TempId { get; set; }

    [JsonPropertyName("temp_para")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<String, Object> TempPara { get; set; }

    [JsonPropertyName("active_filter")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Boolean? ActiveFilter { get; set; }
}
