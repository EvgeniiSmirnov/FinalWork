using FinalWork.Clients;
using FinalWork.Services;

namespace FinalWork.Tests.API;

[AllureNUnit]
public class BaseApiTest
{
    protected ProjectService? ProjectService;

    [OneTimeSetUp]
    public void SetUpApi()
    {
        var restClient = new RestClientExtended();
        ProjectService = new ProjectService(restClient);
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        ProjectService.Dispose();
    }
}