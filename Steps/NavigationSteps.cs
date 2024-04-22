using FinalWork.Models;
using FinalWork.Pages;
using OpenQA.Selenium;
using NUnit.Allure.Attributes;

namespace FinalWork.Steps;

public class NavigationSteps(IWebDriver driver) : BaseSteps(driver)
{
    //public LoginPage NavigateToLoginPage() => new LoginPage(Driver, true);
    //public ProjectPage NavigateToProjectPage() => new ProjectPage(Driver, true);

    [AllureStep("Логинимся на сайт")]
    public ProjectsPage SuccessfulLogin(User user) => Login<ProjectsPage>(user);


    //public LoginPage IncorrectLogin(User user) => Login<LoginPage>(user);

    private T Login<T>(User user) where T : BasePage
    {
        LoginPage = new LoginPage(Driver);
        LoginPage.EmailInput.SendKeys(user.Username);
        LoginPage.PasswordInput.SendKeys(user.Password);
        LoginPage.SignInButton.Click();

        return (T)Activator.CreateInstance(typeof(T), Driver, false);
    }
}