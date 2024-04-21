using OpenQA.Selenium;
using FinalWork.Core;
using FinalWork.Helpers;
using FinalWork.Helpers.Configuration;
using FinalWork.Steps;
using FinalWork.Models;
using NUnit.Allure.Core;

namespace FinalWork.Tests;

[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[AllureNUnit]
public class BaseTest
{
    protected IWebDriver Driver { get; private set; }
    protected WaitsHelper WaitsHelper { get; private set; }

    protected NavigationSteps NavigationSteps;
    protected ProjectsSteps ProjectsSteps;

    protected User? Admin { get; private set; }

    [SetUp]
    public void Setup()
    {
        Driver = new Browser().Driver;
        WaitsHelper = new WaitsHelper(Driver, TimeSpan.FromSeconds(Configurator.WaitsTimeout));

        // Инициализация Steps
        NavigationSteps = new NavigationSteps(Driver);
        ProjectsSteps = new ProjectsSteps(Driver);

        Admin = Configurator.Admin;

        Driver.Navigate().GoToUrl(Configurator.AppSettings.UI_URL);
    }

    [TearDown]
    public void TearDown()
    {
        Driver.Quit();
    }
}