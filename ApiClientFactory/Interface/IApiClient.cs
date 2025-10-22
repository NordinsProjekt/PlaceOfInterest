namespace ApiClientFactory.Interface;

public interface IApiClient
{
    Task<T> GetAsync<T>(string endpoint, string query);
    Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest data);
    IApiClient SetBaseUrl(string baseUrl);
    IApiClient AddDefaultHeader(string name, string value);
    IApiClient Take(int count);
    IApiClient Skip(int count);
}