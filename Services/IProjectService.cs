using FinalWork.Models.API;

namespace FinalWork.Services;

public interface IProjectService
{
    Task<RestResponse> CreateProject(Project project);
    Task<RestResponse> GetProject(string projectCode);
    Task<RestResponse> GetAllProjects();
    HttpStatusCode DeleteProject(string projectCode);
}