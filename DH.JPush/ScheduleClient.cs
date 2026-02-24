using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

using Jiguang.JPush.Model;

using NewLife;

namespace Jiguang.JPush;

public class ScheduleClient
{
    public const string BASE_URL_SCHEDULE_DEFAULT = "https://api.jpush.cn/v3/schedules";
    public const string BASE_URL_SCHEDULE_BEIJING = "https://bjapi.push.jiguang.cn/v3/push/schedules";

    private string BASE_URL = BASE_URL_SCHEDULE_DEFAULT;

    private JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    /// <summary>
    /// 设置 Schedule API 的调用地址。
    /// </summary>
    /// <param name="url"><see cref="BASE_URL_SCHEDULE_DEFAULT"/> or <see cref="BASE_URL_SCHEDULE_BEIJING"/></param>
    public void SetBaseURL(string url)
    {
        BASE_URL = url;
    }

    /// <summary>
    /// 创建定时任务。
    /// </summary>
    /// <param name="json">
    ///     自己构造的请求 json 字符串。
    ///     <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_push_schedule/#schedule"/>
    /// </param>
    public async Task<HttpResponse> CreateScheduleTaskAsync(String json)
    {
        if (json.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(json));

        HttpContent requestContent = new StringContent(json, Encoding.UTF8);
        var msg = await JPushClient.HttpClient.PostAsync(BASE_URL, requestContent).ConfigureAwait(false);
        var responseContent = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
        return new HttpResponse(msg.StatusCode, msg.Headers, responseContent);
    }

    /// <summary>
    /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_push_schedule/#_4"/>
    /// </summary>
    public async Task<HttpResponse> CreateSingleScheduleTaskAsync(string name, PushPayload pushPayload, string triggeringTime)
    {
        if (name.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(name));

        if (pushPayload == null)
            throw new ArgumentNullException(nameof(pushPayload));

        if (triggeringTime.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(triggeringTime));

        var requestJson = new JsonObject
        {
            ["name"] = name,
            ["enabled"] = true,
            ["push"] = JsonSerializer.SerializeToNode(pushPayload, jsonSerializerOptions),
            ["trigger"] = new JsonObject
            {
                ["single"] = new JsonObject
                {
                    ["time"] = triggeringTime
                }
            }
        };

        return await CreateScheduleTaskAsync(requestJson.ToString()).ConfigureAwait(false);
    }

    /// <summary>
    /// 创建单次定时任务。
    /// </summary>
    /// <param name="name">表示 schedule 任务的名字，由 schedule-api 在用户成功创建 schedule 任务后返回，不得超过 255 字节，由汉字、字母、数字、下划线组成。</param>
    /// <param name="pushPayload">推送对象</param>
    /// <param name="triggeringTime"></param>
    public HttpResponse CreateSingleScheduleTask(String name, PushPayload pushPayload, String triggeringTime)
    {
        var task = Task.Run(() => CreateSingleScheduleTaskAsync(name, pushPayload, triggeringTime));
        task.Wait();
        return task.Result;
    }

    /// <summary>
    /// <see cref="CreatePeriodicalScheduleTask(String, PushPayload, Trigger)"/>
    /// </summary>
    public async Task<HttpResponse> CreatePeriodicalScheduleTaskAsync(String name, PushPayload pushPayload, Trigger trigger)
    {
        if (name.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(name));

        if (pushPayload == null)
            throw new ArgumentNullException(nameof(pushPayload));

        if (trigger == null)
            throw new ArgumentNullException(nameof(trigger));

        var requestJson = new JsonObject
        {
            ["name"] = name,
            ["enabled"] = true,
            ["push"] = JsonSerializer.SerializeToNode(pushPayload, jsonSerializerOptions),
            ["trigger"] = new JsonObject
            {
                ["periodical"] = JsonSerializer.SerializeToNode(trigger, jsonSerializerOptions)
            }
        };

        return await CreateScheduleTaskAsync(requestJson.ToString()).ConfigureAwait(false);
    }

    /// <summary>
    /// 创建会在一段时间内重复执行的定期任务。
    /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_push_schedule/#_4"/>
    /// </summary>
    /// <param name="name">表示 schedule 任务的名字，由 schedule-api 在用户成功创建 schedule 任务后返回，不得超过 255 字节，由汉字、字母、数字、下划线组成。</param>
    /// <param name="pushPayload">推送对象</param>
    /// <param name="trigger">触发器</param>
    public HttpResponse CreatePeriodicalScheduleTask(String name, PushPayload pushPayload, Trigger trigger)
    {
        var task = Task.Run(() => CreatePeriodicalScheduleTaskAsync(name, pushPayload, trigger));
        task.Wait();
        return task.Result;
    }

    /// <summary>
    /// <see cref="GetValidScheduleTasks(Int32)"/>
    /// </summary>
    public async Task<HttpResponse> GetValidScheduleTasksAsync(Int32 page = 1)
    {
        if (page <= 0)
            throw new ArgumentNullException(nameof(page));

        var url = BASE_URL + "?page=" + page;
        var msg = await JPushClient.HttpClient.GetAsync(url).ConfigureAwait(false);
        var responseContent = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
        return new HttpResponse(msg.StatusCode, msg.Headers, responseContent);
    }

    /// <summary>
    /// 获取有效的定时任务列表。
    /// </summary>
    /// <param name="page">
    ///     <para>返回当前请求页的详细的 schedule-task 列表，如未指定 page 则 page 为 1。</para>
    ///     <para>排序规则：创建时间，由 schedule-service 完成。</para>
    ///     <para>如果请求页数大于总页数，则 page 为请求值，schedules 为空。</para>
    ///     <para>每页最多返回 50 个 task，如请求页实际的 task 的个数小于 50，则返回实际数量的 task。</para>
    /// </param>
    public HttpResponse GetValidScheduleTasks(Int32 page = 1)
    {
        var task = Task.Run(() => GetValidScheduleTasksAsync(page));
        task.Wait();
        return task.Result;
    }

    /// <summary>
    /// <see cref="GetScheduleTask(String)"/>
    /// </summary>
    public async Task<HttpResponse> GetScheduleTaskAsync(String scheduleId)
    {
        if (scheduleId.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(scheduleId));

        var url = BASE_URL + $"/{scheduleId}";
        var msg = await JPushClient.HttpClient.GetAsync(url).ConfigureAwait(false);
        var responseContent = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
        return new HttpResponse(msg.StatusCode, msg.Headers, responseContent);
    }

    /// <summary>
    /// 获取指定的定时任务。
    /// </summary>
    /// <param name="scheduleId">定时任务 ID。在创建定时任务时会返回。</param>
    public HttpResponse GetScheduleTask(String scheduleId)
    {
        var task = Task.Run(() => GetScheduleTaskAsync(scheduleId));
        task.Wait();
        return task.Result;
    }


    /// <summary>
    /// <see cref="GetScheduleTaskMsgId(String)"/>
    /// </summary>
    public async Task<HttpResponse> GetScheduleTaskMsgIdAsync(String scheduleId)
    {
        if (scheduleId.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(scheduleId));

        var url = BASE_URL + $"/{scheduleId}/msg_ids";
        var msg = await JPushClient.HttpClient.GetAsync(url).ConfigureAwait(false);
        var responseContent = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
        return new HttpResponse(msg.StatusCode, msg.Headers, responseContent);
    }

    /// <summary>
    /// 获取定时任务对应的所有 msg_id。
    /// </summary>
    /// <param name="scheduleId">定时任务 ID。在创建定时任务时会返回。</param>
    public HttpResponse GetScheduleTaskMsgId(String scheduleId)
    {
        var task = Task.Run(() => GetScheduleTaskMsgIdAsync(scheduleId));
        task.Wait();
        return task.Result;
    }

    public async Task<HttpResponse> UpdateScheduleTaskAsync(String scheduleId, String json)
    {
        if (scheduleId.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(scheduleId));

        if (json.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(json));

        var url = BASE_URL + $"/{scheduleId}";
        HttpContent requestContent = new StringContent(json, Encoding.UTF8);
        var msg = await JPushClient.HttpClient.PutAsync(url, requestContent).ConfigureAwait(false);
        var responseContent = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
        return new HttpResponse(msg.StatusCode, msg.Headers, responseContent);
    }

    /// <summary>
    /// <see cref="UpdateSingleScheduleTask(String, String, Boolean?, String, PushPayload)"/>
    /// </summary>
    public async Task<HttpResponse> UpdateSingleScheduleTaskAsync(String scheduleId, String name, Boolean? enabled,
        String triggeringTime, PushPayload pushPayload)
    {
        if (scheduleId.IsNullOrEmpty())
            throw new ArgumentNullException(scheduleId);

        var json = new JsonObject();

        if (!name.IsNullOrEmpty())
            json["name"] = name;

        if (enabled != null)
            json["enabled"] = enabled;

        if (triggeringTime != null)
        {
            json["trigger"] = new JsonObject
            {
                ["single"] = new JsonObject
                {
                    ["time"] = triggeringTime
                }
            };
        }

        if (pushPayload != null)
        {
            json["push"] = JsonSerializer.SerializeToNode(pushPayload, jsonSerializerOptions);
        }

        return await UpdateScheduleTaskAsync(scheduleId, json.ToString()).ConfigureAwait(false);
    }

    /// <summary>
    /// 更新单次定时任务。
    /// </summary>
    /// <param name="scheduleId">任务 ID</param>
    /// <param name="name">任务名称，为 null 表示不更新。</param>
    /// <param name="enabled">是否可用，为 null 表示不更新。</param>
    /// <param name="triggeringTime">触发时间，类似 "2017-08-03 12:00:00"，为 null 表示不更新。</param>
    /// <param name="pushPayload">推送内容，为 null 表示不更新。</param>
    public HttpResponse UpdateSingleScheduleTask(String scheduleId, String name, Boolean? enabled, String triggeringTime, PushPayload pushPayload)
    {
        var task = Task.Run(() => UpdateSingleScheduleTaskAsync(scheduleId, name, enabled, triggeringTime, pushPayload));
        task.Wait();
        return task.Result;
    }

    /// <summary>
    /// <see cref="UpdatePeriodicalScheduleTask(String, String, Boolean?, Trigger, PushPayload)"/>
    /// </summary>
    public async Task<HttpResponse> UpdatePeriodicalScheduleTaskAsync(String scheduleId, String name, Boolean? enabled,
        Trigger trigger, PushPayload pushPayload)
    {
        if (scheduleId.IsNullOrEmpty())
            throw new ArgumentNullException(scheduleId);

        var json = new JsonObject();

        if (!name.IsNullOrEmpty())
            json["name"] = name;

        if (enabled != null)
            json["enabled"] = enabled;

        if (trigger != null)
        {
            json["trigger"] = new JsonObject
            {
                ["periodical"] = JsonSerializer.SerializeToNode(trigger, jsonSerializerOptions)
            };
        }

        if (pushPayload != null)
        {
            json["push"] = JsonSerializer.SerializeToNode(pushPayload, jsonSerializerOptions);
        }

        return await UpdateScheduleTaskAsync(scheduleId, json.ToString()).ConfigureAwait(false);
    }

    /// <summary>
    /// 更新会重复执行的定时任务。
    /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_push_schedule/#schedule_2"/>
    /// </summary>
    /// <param name="scheduleId">任务 ID</param>
    /// <param name="name">任务名称，为 null 表示不更新。</param>
    /// <param name="enabled">是否可用，为 null 表示不更新。</param>
    /// <param name="trigger">触发器对象，为 null 表示不更新。</param>
    /// <param name="pushPayload">推送内容，为 null 表示不更新。</param>
    public HttpResponse UpdatePeriodicalScheduleTask(String scheduleId, String name, Boolean? enabled, Trigger trigger, PushPayload pushPayload)
    {
        var task = Task.Run(() => UpdatePeriodicalScheduleTaskAsync(scheduleId, name, enabled, trigger, pushPayload));
        task.Wait();
        return task.Result;
    }

    /// <summary>
    /// <see cref="DeleteScheduleTask(String)"/>
    /// </summary>
    public async Task<HttpResponse> DeleteScheduleTaskAsync(String scheduleId)
    {
        if (scheduleId.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(scheduleId));

        var url = BASE_URL + $"/{scheduleId}";
        var msg = await JPushClient.HttpClient.DeleteAsync(url).ConfigureAwait(false);
        var responseContent = await msg.Content.ReadAsStringAsync().ConfigureAwait(false);
        return new HttpResponse(msg.StatusCode, msg.Headers, responseContent);
    }

    /// <summary>
    /// 删除指定的定时任务。
    /// <see cref="https://docs.jiguang.cn/jpush/server/push/rest_api_push_schedule/#schedule_3"/>
    /// </summary>
    /// <param name="scheduleId">已创建的 schedule 任务的 id。如果 scheduleId 不合法，即不是有效的 uuid，则返回 404。</param>
    public HttpResponse DeleteScheduleTask(String scheduleId)
    {
        var task = Task.Run(() => DeleteScheduleTaskAsync(scheduleId));
        task.Wait();
        return task.Result;
    }
}
