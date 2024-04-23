using FinalWork.Steps;
using FinalWork.Pages;
using FinalWork.Models.UI;

namespace FinalWork.Tests;

[AllureSuite("UI tests")]
public class ProjectTest : BaseUITest
{
    [Test(Description = "Тест на создание и удаление сущности")]
    [Category("Regression"), Category("Smoke"), AllureSeverity(SeverityLevel.critical)]
    [AllureFeature("NFE")]
    public void CreateAndDeleteProjectTest()
    {        
        string projectCred = new Random().Next(1000, 9999).ToString();

        NavigationSteps.SuccessfulLogin(Admin!);

        Project project = new Project.Builder()
            .SetProjectName($"Project {projectCred}")
            .SetProjectCode(projectCred)
            .SetDescription($"Description text {projectCred}")
            .SetCheckboxPublicProjectAccessType(true)
            .Build();

        ProjectsSteps.CreateProject(project);

        ProjectPage projectPage = new(Driver, false);
        Assert.Multiple(() =>
        {
            Assert.That(projectPage.IsPageOpened(), Is.EqualTo(true));
            Assert.That(projectPage.GetRepositoryNameText(), Does.Contain(projectCred));
        });
        AllureApi.Step($"Создан проект c кодом {projectCred}");
        
        AllureApi.Step($"Переход в настройки для удаления проекта по коду {projectCred}");
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