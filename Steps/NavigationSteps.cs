using FinalWork.Models;
using FinalWork.Pages;
using OpenQA.Selenium;
using NUnit.Allure.Attributes;

namespace FinalWork.Steps;

public class NavigationSteps(IWebDriver driver) : BaseSteps(driver)
{
    [AllureStep("Логинимся на сайт")]
    public ProjectsPage SuccessfulLogin(User user) => Login<ProjectsPage>(user);

    [AllureStep("Логинимся на сайт")]
    public LoginPage IncorrectLogin(User user) => Login<LoginPage>(user);

    private T Login<T>(User user) where T : BasePage
    {
        LoginPage = new LoginPage(Driver);
        LoginPage.EmailInput.SendKeys(user.Username);
        LoginPage.PasswordInput.SendKeys(user.Password);
        LoginPage.SignInButton.Click();

        return (T)Activator.CreateInstance(typeof(T), Driver, false);
    }
}