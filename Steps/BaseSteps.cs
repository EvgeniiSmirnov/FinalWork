using FinalWork.Pages;
using OpenQA.Selenium;

namespace FinalWork.Steps;

public class BaseSteps(IWebDriver driver)
{
    protected readonly IWebDriver Driver = driver;

    protected LoginPage? LoginPage { get; set; }

    //protected DashboardPage? DashboardPage { get; set; }
    //protected AddProjectPage? AddProjectPage { get; set; }
    //protected ProjectPage? ProjectPage { get;set; }
}