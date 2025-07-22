using System.Text.Json.Serialization;

namespace Jiguang.JPush.Model;

/// <summary>
/// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_push/#notification"/>
/// </summary>
public class Notification
{
    [JsonPropertyName("alert")]
    public String? Alert { get; set; }

    [JsonPropertyName("android")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Android? Android { get; set; }

    [JsonPropertyName("ios")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IOS? IOS { get; set; }

    [JsonPropertyName("hmos")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public HMOS? HMOS { get; set; }
}

public class Android
{
    /// <summary>
    /// 必填。
    /// </summary>
    [JsonPropertyName("alert")]
    public String? Alert { get; set; }

    [JsonPropertyName("title")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public String? Title { get; set; }

    [JsonPropertyName("builder_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Int32? BuilderId { get; set; }

    [JsonPropertyName("channel_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public String? ChannelId { get; set; }

    [JsonPropertyName("priority")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Int32? Priority { get; set; }

    [JsonPropertyName("category")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public String? Category { get; set; }

    [JsonPropertyName("style")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Int32? Style { get; set; }

    [JsonPropertyName("alert_type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Int32? AlertType { get; set; }

    [JsonPropertyName("big_text")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public String? BigText { get; set; }

    [JsonPropertyName("inbox")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<String, Object>? Inbox { get; set; }

    [JsonPropertyName("big_pic_path")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public String? BigPicturePath { get; set; }

    [JsonPropertyName("large_icon")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public String? LargeIcon { get; set; }

    [JsonPropertyName("intent")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<String, Object>? Indent { get; set; }

    [JsonPropertyName("extras")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<String, Object>? Extras { get; set; }

    /// <summary>
    /// (VIP only)指定开发者想要打开的 Activity，值为 <activity> 节点的 "android:name" 属性值。
    /// </summary>
    [JsonPropertyName("uri_activity")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public String? URIActivity { get; set; }

    /// <summary>
    /// (VIP only)指定打开 Activity 的方式，值为 Intent.java 中预定义的 "access flags" 的取值范围。
    /// </summary>
    [JsonPropertyName("uri_flag")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public String? URIFlag { get; set; }

    /// <summary>
    /// (VIP only)指定开发者想要打开的 Activity，值为 <activity> -> <intent-filter> -> <action> 节点中的 "android:name" 属性值。
    /// </summary>
    [JsonPropertyName("uri_action")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public String? URIAction { get; set; }
}

public class IOS
{
    /// <summary>
    /// 可以是 string，也可以是 Apple 官方定义的 alert payload 结构。
    /// <para><see ="https://developer.apple.com/library/content/documentation/NetworkingInternet/Conceptual/RemoteNotificationsPG/PayloadKeyReference.html#//apple_ref/doc/uid/TP40008194-CH17-SW5"/></para>
    /// </summary>
    [JsonPropertyName("alert")]
    public Object? Alert { get; set; }

    [JsonPropertyName("sound")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public String? Sound { get; set; }

    /// <summary>
    /// 默认角标 +1。
    /// </summary>
    [JsonPropertyName("badge")]
    public String Badge { get; set; } = "+1";

    [JsonPropertyName("content-available")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Boolean? ContentAvailable { get; set; }

    [JsonPropertyName("mutable-content")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Boolean? MutableContent { get; set; }

    [JsonPropertyName("category")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public String? Category { get; set; }

    [JsonPropertyName("extras")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<String, Object>? Extras { get; set; }

    [JsonPropertyName("thread-id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public String? ThreadId { get; set; }
}

public class HMOS
{
    /// <summary>
    /// 必填。
    /// </summary>
    [JsonPropertyName("alert")]
    public String? Alert { get; set; }

    [JsonPropertyName("title")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public String? Title { get; set; }

    [JsonPropertyName("category")]
    public String? Category { get; set; }

    [JsonPropertyName("large_icon")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public String? LargeIcon { get; set; }

    [JsonPropertyName("intent")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<String, Object>? Intent { get; set; }

    [JsonPropertyName("badge_add_num")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Int32? BadgeAddNum { get; set; }

    [JsonPropertyName("badge_set_num")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Int32? BadgeSetNum { get; set; }

    [JsonPropertyName("test_message")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Boolean? TestMessage { get; set; }

    [JsonPropertyName("receipt_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public String? ReceiptId { get; set; }

    [JsonPropertyName("extras")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<String, Object>? Extras { get; set; }

    [JsonPropertyName("style")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Int32? Style { get; set; }

    [JsonPropertyName("inbox")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<String, Object>? Inbox { get; set; }

    [JsonPropertyName("push_type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Int32? PushType { get; set; }

    [JsonPropertyName("extra_data")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public String? ExtraData { get; set; }
    
}
