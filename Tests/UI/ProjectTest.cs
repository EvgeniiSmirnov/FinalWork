using FinalWork.Steps;
using FinalWork.Pages;
using FinalWork.Models.UI;
using Allure.Net.Commons;
using NUnit.Allure.Attributes;

namespace FinalWork.Tests;

[AllureSuite("UI tests")]
public class ProjectTest : BaseTest
{
    [Test(Description = "Тест на создание и удаление сущности")]
    [Category("Regression"), AllureSeverity(SeverityLevel.critical)]
    public void CreateAndDeleteProjectTest()
    {        
        string projectCred = new Random().Next(1000, 9999).ToString();

        AllureApi.Step("Логинимся на сайт");
        NavigationSteps.SuccessfulLogin(Admin!);

        Project project = new Project.Builder()
            .SetProjectName($"Project {projectCred}")
            .SetProjectCode(projectCred)
            .SetDescription($"Description text {projectCred}")
            .SetCheckboxPublicProjectAccessType(true)
            .Build();

        AllureApi.Step($"Создаём проект c кодом {projectCred}");
        ProjectsSteps.CreateProject(project);

        ProjectPage projectPage = new(Driver, false);
        Assert.Multiple(() =>
        {
            Assert.That(projectPage.IsPageOpened(), Is.EqualTo(true));
            Assert.That(projectPage.GetRepositoryNameText(), Does.Contain(projectCred));
        });
        AllureApi.Step($"Создан проект c кодом {projectCred}");

        AllureApi.Step($"Создан проект по коду {projectCred}");
        projectPage.ClickSettingsButton();

        ProjectSettingsPage projectSettingsPage = new(Driver, false);

        Assert.That(projectSettingsPage.IsPageOpened(), Is.EqualTo(true));
        
        projectSettingsPage
            .ClickDeleteProjectButton()
            .ClickDeleteProjectFinalButton();

        ProjectsPage projectsPage = new(Driver, false);

        Assert.That(projectsPage.IsPageOpened);
        AllureApi.Step($"Проект удалён");
    }
}