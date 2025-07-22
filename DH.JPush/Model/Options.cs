using System.Text.Json;
using System.Text.Json.Serialization;

namespace Jiguang.JPush.Model;

/// <summary>
/// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_push/#options"/>
/// </summary>
public class Options
{
    /// <summary>
    /// 推送序号。
    /// <para>用来作为 API 调用标识，API 返回时被原样返回，以方便 API 调用方匹配请求与返回。不能为 0。</para>
    /// </summary>
    [JsonPropertyName("sendno")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Int32? SendNo { get; set; }

    /// <summary>
    /// 离线消息保留时长(秒)。
    /// <para>推送当前用户不在线时，为该用户保留多长时间的离线消息，以便其上线时再次推送。默认 86400 （1 天），最长 10 天。设置为 0 表示不保留离线消息，只有推送当前在线的用户可以收到。</para>
    /// </summary>
    [JsonPropertyName("time_to_live")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Int32? TimeToLive { get; set; }

    /// <summary>
    /// 要覆盖的消息 ID。
    /// <para>如果当前的推送要覆盖之前的一条推送，这里填写前一条推送的 msg_id 就会产生覆盖效果。覆盖功能起作用的时限是：1 天。</para>
    /// </summary>
    [JsonPropertyName("override_msg_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Int64? OverrideMessageId { get; set; }

    /// <summary>
    /// iOS 推送是否为生产环境。默认为 false - 开发环境。
    /// <para>true: 生产环境；false: 开发环境。</para>
    /// </summary>
    [JsonPropertyName("apns_production")]
    public Boolean IsApnsProduction { get; set; } = false;

    /// <summary>
    /// 更新 iOS 通知的标识符。
    /// <para>APNs 新通知如果匹配到当前通知中心有相同 apns-collapse-id 字段的通知，则会用新通知内容来更新它，并使其置于通知中心首位。collapse id 长度不可超过 64 bytes。</para>
    /// </summary>
    [JsonPropertyName("apns_collapse_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public String? ApnsCollapseId { get; set; }

    /// <summary>
    /// 定速推送时长（分钟）。
    /// 又名缓慢推送。把原本尽可能快的推送速度，降低下来，给定的 n 分钟内，均匀地向这次推送的目标用户推送。最大值为 1400，未设置则不是定速推送。
    /// </summary>
    [JsonPropertyName("big_push_duration")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Int32? BigPushDuration { get; set; }

    /// <summary>
    /// 自定义参数
    /// </summary>
    public Dictionary<String, Object>? Dict { get; set; }

    public void Add(String key, Object value) {
        Dict ??= [];
        Dict.Add(key, value);
    }
}

public class OptionsJsonConvert : JsonConverter<Options>
{
    public override Options? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotSupportedException("Deserialization is not supported for Options.");
    }

    public override void Write(Utf8JsonWriter writer, Options value, JsonSerializerOptions options)
    {
        if (value == null)
        {
            writer.WriteNullValue();
            return;
        }

        writer.WriteStartObject();

        if (value.SendNo != null)
        {
            writer.WriteNumber("sendno", value.SendNo.Value);
        }

        if (value.TimeToLive != null)
        {
            writer.WriteNumber("time_to_live", value.TimeToLive.Value);
        }

        if (value.OverrideMessageId != null)
        {
            writer.WriteNumber("override_msg_id", value.OverrideMessageId.Value);
        }

        writer.WriteBoolean("apns_production", value.IsApnsProduction);

        if (value.ApnsCollapseId != null)
        {
            writer.WriteString("apns_collapse_id", value.ApnsCollapseId);
        }

        if (value.BigPushDuration != null)
        {
            writer.WriteNumber("big_push_duration", value.BigPushDuration.Value);
        }

        if (value.Dict != null)
        {
            foreach (var item in value.Dict)
            {
                writer.WritePropertyName(item.Key);
                JsonSerializer.Serialize(writer, item.Value, options);
            }
        }

        writer.WriteEndObject();
    }
}
