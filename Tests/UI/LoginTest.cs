using Allure.Net.Commons;
using FinalWork.Helpers.Configuration;
using FinalWork.Models;
using FinalWork.Pages;
using NUnit.Allure.Attributes;

namespace FinalWork.Tests.UI;

public class LoginTest : BaseTest
{

    [Test(Description = "Тест на проверку всплывающего окна при неправильном логине")]
    [Category("Regression"), AllureSeverity(SeverityLevel.critical)]
    [AllureFeature("AFE")]
    [Ignore("Ignore attribute check")]
    public void IncorrectLoginTest()
    {
        NavigationSteps.IncorrectLogin(User!);
        LoginPage loginPage = new(Driver, false);
        Assert.That(loginPage.IsInvalidDataMessageDisplayed(), Is.EqualTo(true));
    }

    [Test(Description = "Тест воспроизводящий любой дефект")]
    public void IncorrectLoginFailureTest()
    {
        NavigationSteps.SuccessfulLogin(User!);

        ProjectsPage projectsPage = new(Driver, false);

        Assert.That(projectsPage.IsPageOpened);
    }

}