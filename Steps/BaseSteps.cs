using FinalWork.Pages;
using OpenQA.Selenium;

namespace FinalWork.Steps;

public class BaseSteps(IWebDriver driver)
{
    protected readonly IWebDriver Driver = driver;

    protected LoginPage? LoginPage { get; set; }
}