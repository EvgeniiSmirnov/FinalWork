using Allure.Net.Commons;
using NUnit.Allure.Attributes;
using FinalWork.Models.UI;
using FinalWork.Pages;
using FinalWork.Steps;

namespace FinalWork.Tests.UI;

[AllureSuite("UI tests")]
public class BoundaryProjectTest : BaseUITest
{
    [Test(Description = "Создание проекта. Проверяем минимальное количество символов для кода проекта")]
    [Category("Regression"), Category("Smoke"), AllureSeverity(SeverityLevel.critical)]
    [AllureFeature("NFE")]
    public void LowerBoundProjectCodeTest()
    {        
        int projectCode = new Random().Next(11, 99);
        
        NavigationSteps.SuccessfulLogin(Admin!);

        Project project = new Project.Builder()
            .SetProjectName($"Project {DateTime.Now}")
            .SetProjectCode($"{projectCode}")
            .SetDescription($"Description text")
            .SetCheckboxPublicProjectAccessType(true)
            .Build();

        ProjectsSteps.CreateProject(project);

        ProjectPage projectPage = new(Driver, false);

        Assert.Multiple(() =>
        {
            Assert.That(projectPage.IsPageOpened(), Is.EqualTo(true));
            Assert.That(projectPage.GetRepositoryNameText(), Does.Contain(projectCode.ToString()));
        });
        TakeScreenshot($"Проект {projectCode}");
        AllureApi.Step($"Создан проект c ожидаемым значением {projectCode}");
    }

    [Test(Description = "Создание проекта. Проверяем максимальное количество символов для кода проекта")]
    [Category("Regression"), Category("Smoke"), AllureSeverity(SeverityLevel.critical)]
    [AllureFeature("NFE")]
    public void UpperBoundProjectCodeTest()
    {
        string projectCode = $"ABCDEFG{new Random().Next(111, 999)}";

        NavigationSteps.SuccessfulLogin(Admin!);

        Project project = new Project.Builder()
            .SetProjectName($"Project {DateTime.Now}")
            .SetProjectCode(projectCode)
            .SetDescription($"Description text")
            .SetCheckboxPublicProjectAccessType(true)
            .Build();

        ProjectsSteps.CreateProject(project);

        ProjectPage projectPage = new(Driver, false);

        Assert.Multiple(() =>
        {
            Assert.That(projectPage.IsPageOpened(), Is.EqualTo(true));
            Assert.That(projectPage.GetRepositoryNameText(), Does.Contain(projectCode));
        });
        TakeScreenshot($"Проект {projectCode}");
        AllureApi.Step($"Создан проект c ожидаемым значением {projectCode}");
    }
}