using Allure.Net.Commons;
using NUnit.Allure.Attributes;
using FinalWork.Models.UI;
using FinalWork.Pages;

namespace FinalWork.Tests.UI;

[AllureSuite("UI tests")]
public class InvalidDataTest: BaseTest
{
    [Test(Description = "Тест на использование некорректных данных")]
    [Category("Regression"), AllureSeverity(SeverityLevel.critical)]
    [AllureFeature("AFE")]
    public void CreateProjectWithInvalidDataTest()
    {
        string projectCode = "@#%";
        NavigationSteps.SuccessfulLogin(Admin!);

        Project project = new Project.Builder()
            .SetProjectName($"Project {DateTime.Now}")
            .SetProjectCode(projectCode)
            .SetDescription($"Description text")
            .SetCheckboxPublicProjectAccessType(true)
            .Build();

        ProjectsSteps.CreateProject(project);

        ProjectsPage projectsPage = new(Driver, false);

        Assert.Multiple(() =>
        {
            Assert.That(projectsPage.IsInvalidDataMessageDisplayed(), Is.EqualTo(true));
            Assert.That(projectsPage.IsFormatProjectCodeMessageDisplayed(), Is.EqualTo(true));
        });
    }

    [Test(Description = "Тест на ввод данных превышающих допустимые")]
    [Category("Regression"), AllureSeverity(SeverityLevel.critical)]
    [AllureFeature("AFE")]
    public void CreatProjectWithUpperBoundProjectCodeTest()
    {
        string projectCode = $"ABCDEFGHJ{new Random().Next(11, 99)}";

        NavigationSteps.SuccessfulLogin(Admin!);

        Project project = new Project.Builder()
            .SetProjectName($"Project {DateTime.Now}")
            .SetProjectCode(projectCode)
            .SetDescription($"Description text")
            .SetCheckboxPublicProjectAccessType(true)
            .Build();

        ProjectsSteps.CreateProject(project);

        ProjectsPage projectsPage = new(Driver, false);

        Assert.Multiple(() =>
        {
            Assert.That(projectsPage.IsInvalidDataMessageDisplayed(), Is.EqualTo(true));
            Assert.That(projectsPage.IsUpperBoundProjectCodeMessageDisplayed(), Is.EqualTo(true));
        });
    }
}