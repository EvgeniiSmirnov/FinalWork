using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using FinalWork.Pages;
using FinalWork.Steps;
using FinalWork.Models.UI;
using Allure.Net.Commons;
using NUnit.Allure.Attributes;

namespace FinalWork.Tests.UI;

[AllureSuite("UI tests")]
public class HoverTest : BaseTest
{
    [Test(Description = "Тест на отображении подсказки при наведении курсора")]
    [Category("Regression"), AllureSeverity(SeverityLevel.normal)]
    [AllureFeature("NFE")]
    public void HoverSuiteTreeTest()
    {
        int projectCred = new Random().Next(1000, 9999);

        NavigationSteps.SuccessfulLogin(Admin!);

        Project project = new Project.Builder()
            .SetProjectName($"Project {projectCred}")
            .SetProjectCode($"{projectCred}")
            .SetDescription($"Description text {projectCred}")
            .SetCheckboxPublicProjectAccessType(true)
            .Build();

        ProjectsSteps.CreateProject(project);

        ProjectPage projectPage = new(Driver, false);

        Assert.Multiple(() =>
        {
            Assert.That(projectPage.IsPageOpened(), Is.EqualTo(true));
            Assert.That(projectPage.GetRepositoryNameText(), Does.Contain(projectCred.ToString()));
        });

        Suite suite = new Suite.SuiteBuilder()
            .SetSuiteName($"Suite {new Random().Next(111, 999)}")
            .Build();

        ProjectsSteps.CreateSuite(suite);

        AllureApi.Step("Наводим курсор на дерево сьютов");
        Actions actions = new(Driver);
        actions.MoveToElement(projectPage.SuitesButton, 1, 1).Perform();


        Assert.That(Driver.FindElement(By.XPath("//*[text()='Collapse suite tree']")).Text,
            Is.EqualTo("Collapse suite tree"));
        AllureApi.Step("Найдена всплывающая подсказка");
    }
}