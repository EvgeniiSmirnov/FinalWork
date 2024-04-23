using FinalWork.Core;
using FinalWork.Helpers;
using FinalWork.Helpers.Configuration;
using FinalWork.Steps;
using FinalWork.Models;

namespace FinalWork.Tests;

//[Parallelizable(scope: ParallelScope.All)]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[AllureNUnit]
public class BaseUITest
{
    protected IWebDriver Driver { get; private set; }
    protected WaitsHelper WaitsHelper { get; private set; }

    protected NavigationSteps NavigationSteps;
    protected ProjectsSteps ProjectsSteps;

    protected User? Admin { get; private set; }
    protected User? User { get; private set; }

    public void TakeScreenshot(string name)
    {
        Screenshot screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
        byte[] screenshotBytes = screenshot.AsByteArray;

        AllureApi.Step(name);
        AllureApi.AddAttachment($"{name}", "image/png", screenshotBytes);
    }

    [SetUp]
    public void Setup()
    {
        Driver = new Browser().Driver;
        WaitsHelper = new WaitsHelper(Driver, TimeSpan.FromSeconds(Configurator.WaitsTimeout));

        // Инициализация Steps
        NavigationSteps = new NavigationSteps(Driver);
        ProjectsSteps = new ProjectsSteps(Driver);

        Admin = Configurator.Admin;
        User = Configurator.User;

        Driver.Navigate().GoToUrl(Configurator.AppSettings.UI_URL);
    }

    [TearDown]
    public void TearDown()
    {
        try
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                Screenshot screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
                byte[] screenshotBytes = screenshot.AsByteArray;

                AllureApi.AddAttachment("Screenshot", "image/png", screenshotBytes);
                AllureApi.AddAttachment("error.txt", "text/plain", Encoding.UTF8.GetBytes(TestContext.CurrentContext.Result.Message));
            }
        }

        finally
        {
            Driver.Quit();
        }
    }
}