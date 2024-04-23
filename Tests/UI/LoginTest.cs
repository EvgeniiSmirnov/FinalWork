using FinalWork.Pages;
using FinalWork.Steps;

namespace FinalWork.Tests.UI;

public class LoginTest : BaseUITest
{
    [Test(Description = "Тест воспроизводящий любой дефект")]
    [Category("Regression"), AllureSeverity(SeverityLevel.critical)]
    [AllureFeature("AFE")]
    public void IncorrectLoginFailureTest()
    {
        NavigationSteps.SuccessfulLogin(User!);

        ProjectsPage projectsPage = new(Driver, false);

        Assert.That(projectsPage.IsPageOpened);
    }

    [Ignore("Тест на доработке")]
    [Test(Description = "Тест на проверку всплывающего окна при неправильном логине")]
    [Category("Regression"), AllureSeverity(SeverityLevel.critical)]
    [AllureFeature("AFE")]
    public void IncorrectLoginTest()
    {
        NavigationSteps.IncorrectLogin(User!);
        LoginPage loginPage = new(Driver, false);
        
        Assert.Multiple(() =>
        {
            Assert.That(loginPage.IsInvalidDataMessageDisplayed(), Is.EqualTo(true));
            Assert.That(loginPage.InvalidDataMessage.Text.Trim(),
                Is.EqualTo("Successfully created"));
        });
    }
}