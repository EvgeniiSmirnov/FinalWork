using Allure.Net.Commons;
using NUnit.Allure.Attributes;
using FinalWork.Models.UI;
using FinalWork.Pages;

namespace Test4.Tests.UI;

public class InvalidDataTest: BaseTest
{
    [Test(Description = "Тест на отображении подсказки при наведении курсора")]
    [Category("Regression"), AllureSeverity(SeverityLevel.critical)]
    public void CreateProjectWithInvalidDataTest()
    {
        string projectCode = "@#%";
        AllureApi.Step("Логинимся на сайт");
        NavigationSteps.SuccessfulLogin(Admin!);

        Project project = new Project.Builder()
            .SetProjectName($"Project {DateTime.Now}")
            .SetProjectCode(projectCode)
            .SetDescription($"Description text")
            .SetCheckboxPublicProjectAccessType(true)
            .Build();

        AllureApi.Step($"Создаём проект c кодом {projectCode}");
        ProjectsSteps.CreateProject(project);

        ProjectsPage projectsPage = new(Driver, false);

        Assert.Multiple(() =>
        {
            Assert.That(projectsPage.IsInvalidDataMessageDisplayed(), Is.EqualTo(true));
            Assert.That(projectsPage.IsFormatProjectCodeMessageDisplayed(), Is.EqualTo(true));
        });
        AllureApi.Step("Отображается информация об ошибке");
    }

    [Test]
    public void CreatProjectWithUpperBoundProjectCodeTest()
    {
        string projectCode = $"ABCDEFGHJ{new Random().Next(11, 99)}";
        AllureApi.Step("Логинимся на сайт");
        NavigationSteps.SuccessfulLogin(Admin!);

        Project project = new Project.Builder()
            .SetProjectName($"Project {DateTime.Now}")
            .SetProjectCode(projectCode)
            .SetDescription($"Description text")
            .SetCheckboxPublicProjectAccessType(true)
            .Build();

        AllureApi.Step($"Создаём проект c кодом {projectCode}");
        ProjectsSteps.CreateProject(project);

        ProjectsPage projectsPage = new(Driver, false);

        Assert.Multiple(() =>
        {
            Assert.That(projectsPage.IsInvalidDataMessageDisplayed(), Is.EqualTo(true));
            Assert.That(projectsPage.IsUpperBoundProjectCodeMessageDisplayed(), Is.EqualTo(true));
        });
        AllureApi.Step("Отображается информация об ошибке");
    }
}