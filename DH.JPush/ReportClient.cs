using System.Text;
using System.Text.Json;

using Jiguang.JPush.Model;

namespace Jiguang.JPush;

public class ReportClient
{
    public const string BASE_URL_REPORT_DEFAULT = "https://report.jpush.cn/v3";
    public const string BASE_URL_REPORT_BEIJING = "https://bjapi.push.jiguang.cn/v3/report";

    private string BASE_URL = BASE_URL_REPORT_DEFAULT;

    /// <summary>
    /// 设置 Report API 的调用地址。
    /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_report/"/>
    /// </summary>
    /// <param name="url"><see cref="BASE_URL_REPORT_DEFAULT"/> or <see cref="BASE_URL_REPORT_BEIJING"/></param>
    public void SetBaseURL(String url)
    {
        BASE_URL = url;
    }

    /// <summary>
    /// <see cref="GetMessageReport(List{String})"/>
    /// </summary>
    public async Task<HttpResponse> GetMessageReportAsync(List<String> msgIdList)
    {
        if (msgIdList == null)
            throw new ArgumentNullException(nameof(msgIdList));

        var msgIds = String.Join(",", msgIdList);
        var url = BASE_URL + "/received?msg_ids=" + msgIds;
        var msg = await JPushClient.HttpClient.GetAsync(url).ConfigureAwait(false);
        var content = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
        return new HttpResponse(msg.StatusCode, msg.Headers, content);
    } 

    /// <summary>
    /// 获取指定 msg_id 的消息送达统计数据。
    /// </summary>
    /// <param name="msgIdList">消息的 msg_id 列表，每次最多支持 100 个。</param>
    public HttpResponse GetMessageReport(List<String> msgIdList)
    {
        var task = Task.Run(() => GetMessageReportAsync(msgIdList));
        task.Wait();
        return task.Result;
    }

    /// <summary>
    /// <see cref="GetReceivedDetailReport(List{String})"/>
    /// </summary>
    public async Task<HttpResponse> GetReceivedDetailReportAsync(List<String> msgIdList)
    {
        if (msgIdList == null)
            throw new ArgumentNullException(nameof(msgIdList));

        var msgIds = String.Join(",", msgIdList);
        var url = BASE_URL + "/received/detail?msg_ids=" + msgIds;
        var msg = await JPushClient.HttpClient.GetAsync(url).ConfigureAwait(false);
        var content = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
        return new HttpResponse(msg.StatusCode, msg.Headers, content);
    }

    /// <summary>
    /// 送达统计详情（新）
    /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_report/#_7"/>
    /// </summary>
    /// <param name="msgIdList">消息的 msg_id 列表，每次最多支持 100 个。</param>
    public HttpResponse GetReceivedDetailReport(List<String> msgIdList)
    {
        var task = Task.Run(() => GetReceivedDetailReportAsync(msgIdList));
        task.Wait();
        return task.Result;
    }

    /// <summary>
    /// <see cref="GetMessageSendStatus(String, List{String}, String)"/>
    /// </summary>
    public async Task<HttpResponse> GetMessageSendStatusAsync(String msgId, List<String> registrationIdList, String data)
    {
        if (string.IsNullOrEmpty(msgId))
            throw new ArgumentNullException(nameof(msgId));

        if (registrationIdList == null)
            throw new ArgumentNullException(nameof(registrationIdList));

        var body = new Dictionary<String, Object>
        {
            { "msg_id", Int64.Parse(msgId) },
            { "registration_ids", registrationIdList }
        };

        if (!String.IsNullOrEmpty(data))
            body.Add("data", data);

        var jsonBody = JsonSerializer.Serialize(body);
        var url = BASE_URL + "/status/message";
        var httpContent = new StringContent(jsonBody, Encoding.UTF8);

        var msg = await JPushClient.HttpClient.PostAsync(url, httpContent).ConfigureAwait(false);
        var content = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
        return new HttpResponse(msg.StatusCode, msg.Headers, content);
    }

    /// <summary>
    /// 查询指定消息的送达状态。
    /// <para><see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_report/#_7"/></para>
    /// </summary>
    /// <param name="msgId">待查询消息的 Message Id。</param>
    /// <param name="registrationIdList">收到消息设备的 Registration Id 列表。</param>
    /// <param name="data">待查询日期，格式为 yyyy-MM-dd。如果传 null，则默认为当天。</param>
    public HttpResponse GetMessageSendStatus(String msgId, List<String> registrationIdList, String data)
    {
        var task = Task.Run(() => GetMessageSendStatusAsync(msgId, registrationIdList, data));
        task.Wait();
        return task.Result;
    }

    /// <summary>
    /// <see cref="GetMessageDetailReport(List{String})"/>
    /// </summary>
    public async Task<HttpResponse> GetMessageDetailReportAsync(List<String> msgIdList)
    {
        if (msgIdList == null)
            throw new ArgumentNullException(nameof(msgIdList));

        var msgIds = String.Join(",", msgIdList);
        var url = BASE_URL + "/messages?msg_ids=" + msgIds;
        var msg = await JPushClient.HttpClient.GetAsync(url).ConfigureAwait(false);
        var content = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
        return new HttpResponse(msg.StatusCode, msg.Headers, content);
    }

    /// <summary>
    /// 消息统计（VIP 专属接口，旧）
    /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_report/#vip"/>
    /// </summary>
    /// <param name="msgIdList">消息的 msg_id 列表，每次最多支持 100 个。</param>
    public HttpResponse GetMessageDetailReport(List<String> msgIdList)
    {
        var task = Task.Run(() => GetMessageDetailReportAsync(msgIdList));
        task.Wait();
        return task.Result;
    }

    /// <summary>
    /// <see cref="GetMessagesDetailReport(List{string})"/>
    /// </summary>
    public async Task<HttpResponse> GetMessagesDetailReportAsync(List<String> msgIdList)
    {
        if (msgIdList == null)
            throw new ArgumentNullException(nameof(msgIdList));

        var msgIds = String.Join(",", msgIdList);
        var url = BASE_URL + "/messages/detail?msg_ids=" + msgIds;
        var msg = await JPushClient.HttpClient.GetAsync(url).ConfigureAwait(false);
        var content = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
        return new HttpResponse(msg.StatusCode, msg.Headers, content);
    }

    /// <summary>
    /// 消息统计详情（VIP 专属接口，新）
    /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_report/#vip_1"/>
    /// </summary>
    /// <param name="msgIdList">消息的 msg_id 列表，每次最多支持 100 个。</param>
    public HttpResponse GetMessagesDetailReport(List<String> msgIdList)
    {
        var task = Task.Run(() => GetMessagesDetailReportAsync(msgIdList));
        task.Wait();
        return task.Result;
    }

    /// <summary>
    /// <see cref="GetUserReport(String, String, Int32)"/>
    /// </summary>
    public async Task<HttpResponse> GetUserReportAsync(String timeUnit, String startTime, Int32 duration)
    {
        if (String.IsNullOrEmpty(timeUnit))
            throw new ArgumentNullException(nameof(timeUnit));

        if (startTime == null)
            throw new ArgumentNullException(nameof(startTime));

        if (duration <= 0)
            throw new ArgumentOutOfRangeException(nameof(duration));

        var url = BASE_URL + "/users?time_unit=" + timeUnit + "&start=" + startTime + "&duration=" + duration;
        var msg = await JPushClient.HttpClient.GetAsync(url).ConfigureAwait(false);
        var content = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
        return new HttpResponse(msg.StatusCode, msg.Headers, content);
    }

    /// <summary>
    /// 提供近2个月内某时间段的用户相关统计数据：新增用户、在线用户、活跃用户（VIP only）。
    /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_v3_report/#vip_1"/>
    /// </summary>
    /// <param name="timeUnit">时间单位。支持 "HOUR", "DAY" 或 "MOUNTH"</param>
    /// <param name="startTime">
    ///     起始时间。
    ///     <para>如果单位是小时，则起始时间是小时（包含天），格式例：2014-06-11 09</para>
    ///     <para>如果单位是天，则起始时间是日期（天），格式例：2014-06-11</para>
    ///     <para>如果单位是月，则起始时间是日期（月），格式例：2014-06</para>
    /// </param>
    /// <param name="duration">
    ///     持续时长。
    ///     <para>如果时间单位（timeUnit）是天，则是持续的天数，其他时间单位以此类推。</para>
    ///     <para>只支持查询 60 天以内的用户信息。如果 timeUnit 为 HOUR，则只会输出当天的统计结果。</para>
    /// </param>
    public HttpResponse GetUserReport(String timeUnit, String startTime, Int32 duration)
    {
        var task = Task.Run(() => GetUserReportAsync(timeUnit, startTime, duration));
        task.Wait();
        return task.Result;
    }
}
