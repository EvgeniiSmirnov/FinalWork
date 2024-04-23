using FinalWork.Helpers.Configuration;

namespace FinalWork.Clients;

public class RestClientExtended
{
    private readonly RestClient _client;
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    public RestClientExtended()
    {
        var options = new RestClientOptions(Configurator.AppSettings.API_URL ?? throw new InvalidOperationException());

        _client = new RestClient(options);
        _client.AddDefaultHeaders(new Dictionary<string, string>
        {
            { "token", Configurator.Token! },
            { "accept", "application/json"},
            { "content-type", "application/json"}
        });
    }

    public void Dispose()
    {
        _client?.Dispose();
        GC.SuppressFinalize(this);
    }

    private void LogRequest(RestRequest request)
    {
        AllureApi.Step($"{request.Method} запрос: {request.Resource}");
        _logger.Debug($"{request.Method} запрос: {request.Resource}");

        var body = request.Parameters
            .FirstOrDefault(p => p.Type == ParameterType.RequestBody)?.Value;

        if (body != null)
        {
            AllureApi.Step($"Тело запроса:\n{JsonSerializer.Serialize(body)}");
            _logger.Debug($"Тело запроса:\n{JsonSerializer.Serialize(body)}");
        }
    }

    private void LogResponse(RestResponse response)
    {
        if (response.ErrorException != null)
        {
            _logger.Error(
                $"В ответе получена ошибка:\n{response.ErrorException.Message}");
        }

        AllureApi.Step($"Код ответа: {response.StatusCode}, {(int)response.StatusCode}");
        _logger.Debug($"Запрос завершился с кодом: {(int)response.StatusCode} = {response.StatusCode}");

        if (!string.IsNullOrWhiteSpace(response.Content))
        {
            AllureApi.Step($"Тело ответа: \n{response.Content}");
            _logger.Debug($"Тело ответа:\n{response.Content}");
        }
    }

    public async Task<RestResponse> ExecuteAsync(RestRequest request)
    {
        LogRequest(request);
        var response = await _client.ExecuteAsync(request);
        LogResponse(response);

        return response;
    }

    public async Task<T> ExecuteAsync<T>(RestRequest request)
    {
        LogRequest(request);
        var response = await _client.ExecuteAsync<T>(request);
        LogResponse(response);

        return response.Data ?? throw new InvalidOperationException();
    }
}