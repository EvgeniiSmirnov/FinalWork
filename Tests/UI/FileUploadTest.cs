using FinalWork.Models.UI;
using FinalWork.Pages;
using FinalWork.Steps;

namespace FinalWork.Tests.UI;

[AllureSuite("UI tests")]
public class FileUploadTest : BaseUITest
{
    [Test(Description = "Тест на загрузку изображения в профиль проекта")]
    [Category("Regression"), AllureSeverity(SeverityLevel.normal)]
    [AllureFeature("NFE")]
    public void ProjectLogoUploadTest()
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

        AllureApi.Step("Переходим на страницу настроек проекта");
        projectPage.ClickSettingsButton();

        ProjectSettingsPage projectSettingsPage = new(Driver, false);

        Assert.That(projectSettingsPage.IsPageOpened(), Is.EqualTo(true));

        projectSettingsPage.ChangeLogoButton.SendKeys(
            Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
            "Resources", "test_image.jpg"));

        Assert.That(projectSettingsPage.IsMessageLogoExist, Is.EqualTo(true));
        TakeScreenshot("Project avatar was successfully updated!");
        AllureApi.Step("Получено сообщение об успешном добавлении изображения");
    }
}