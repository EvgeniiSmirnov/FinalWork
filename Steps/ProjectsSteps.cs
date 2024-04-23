using FinalWork.Pages;
using OpenQA.Selenium;
using FinalWork.Models.UI;
using NUnit.Allure.Attributes;

namespace FinalWork.Steps;

public class ProjectsSteps(IWebDriver driver) : BaseSteps(driver)
{
    [AllureStep("Создаём проект")]
    public ProjectsPage CreateProject(Project project)
    {
        ProjectsPage projectsPage = new(Driver);

        return projectsPage
                .ClickCreateNewProjectButton()
                .InputProjectNameValue(project.ProjectName)
                .InputProjectCodeValue(project.ProjectCode)
                .InputDescription(project.Description)
                .SetCheckboxPublicType(project.IsPublicProjectAccessType)
                .CreateProjectButtonClick();
    }

    [AllureStep("Создаём сьют")]
    public ProjectPage CreateSuite(Suite suite)
    {
        ProjectPage projectPage = new(Driver);

        return projectPage
                .ClickCreateNewSuiteButton()
                .InputSuiteNameValue(suite.SuiteName)
                .ClickCreateSuiteButton();
    }
}