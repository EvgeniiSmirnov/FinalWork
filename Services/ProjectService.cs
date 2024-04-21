using FinalWork.Clients;
using FinalWork.Models.API;

namespace FinalWork.Services;

public class ProjectService : IProjectService, IDisposable
{
    private readonly RestClientExtended _client;

    public ProjectService(RestClientExtended client)
    {
        _client = client;
    }

    /// <summary>
    /// This method is used to create a new project through API.
    /// </summary>
    /// <param name="project"></param>
    /// <returns></returns>
    public Task<RestResponse> CreateProject(Project project)
    {
        var request = new RestRequest("v1/project", Method.Post)
            .AddJsonBody(project);

        return _client.ExecuteAsync(request);
    }

    /// <summary>
    /// This method allows to retrieve a specific project.
    /// </summary>
    /// <param name="projectCode">project code</param>
    /// <returns></returns>
    public Task<RestResponse> GetProject(string projectCode)
    {
        var request = new RestRequest("v1/project/{code}", Method.Get)
            .AddUrlSegment("code", projectCode);


        return _client.ExecuteAsync(request);
    }

    /// <summary>
    /// This method allows to retrieve all projects available for your account.
    /// </summary>
    /// <returns></returns>
    public Task<RestResponse> GetAllProjects()
    {
        //https://api.qase.io/v1/project?limit=1
        var request = new RestRequest("v1/project?limit=1", Method.Get);
            
        return _client.ExecuteAsync(request);
    }

    /// <summary>
    /// This method allows to delete a specific project.
    /// </summary>
    /// <param name="projectCode">project code</param>
    /// <returns></returns>
    public HttpStatusCode DeleteProject(string projectCode)
    {
        var request = new RestRequest("v1/project/{code}", Method.Delete)
            .AddUrlSegment("code", projectCode);

        return _client.ExecuteAsync(request).Result.StatusCode;
    }

    public void Dispose()
    {
        _client?.Dispose();
        GC.SuppressFinalize(this);
    }
}