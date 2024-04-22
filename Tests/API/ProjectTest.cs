﻿using Allure.Net.Commons;
using Bogus;
using Newtonsoft.Json;
using NUnit.Allure.Attributes;
using FinalWork.Fakers;
using FinalWork.Models.API;

namespace FinalWork.Tests.API;

[AllureSuite("API tests")]
public class ProjectTest : BaseApiTest
{
    private Project? _project;
    private CreateProjectAnswer? _createdProject;
    private int totalProjectCounty;

    private static Faker<Project> Project => new ProjectFaker();

    [Test(Description = "Создание проекта")]
    [Category("Regression"), Category("Smoke"), AllureSeverity(SeverityLevel.critical)]
    [Category("NFE")]
    [Order(1)]
    public void CreateTest()
    {
        _project = Project.Generate();

        var actual = ProjectService!.CreateProject(_project);

        _createdProject = JsonConvert
            .DeserializeObject<CreateProjectAnswer>(actual.Result.Content!);

        Assert.Multiple(() =>
        {
            Assert.That(_createdProject!.Status, Is.EqualTo(true));
            Assert.That(_project.Code, Is.EqualTo(_createdProject!.Result!.Code));
        });
    }

    [Test(Description = "Получаем данные о проекта по коду проекта")]
    [Category("Regression"), Category("Smoke"), AllureSeverity(SeverityLevel.normal)]
    [Category("NFE")]
    [Order(2)]
    public void GetProjectTest()
    {
        var projectFromAPI = ProjectService!.GetProject(_createdProject!.Result.Code);
        GetProjectAnswer? deserializedProjectFromAPI = JsonConvert
            .DeserializeObject<GetProjectAnswer>(projectFromAPI.Result.Content!);

        Assert.Multiple(() =>
        {
            Assert.That(_createdProject.Status, Is.EqualTo(true));
            Assert.That(deserializedProjectFromAPI?.Result?.Code, Is.EqualTo(_project?.Code));
        });

    }

    [Test(Description = "Получаем данные о всех проектах")]
    [Category("Regression"), Category("Smoke"), AllureSeverity(SeverityLevel.normal)]
    [Category("NFE")]
    [Order(3)]
    public void GetAllProjectsTest()
    {
        var projectsInfoFromAPI = ProjectService!.GetAllProjects();

        GetAllProjectsAnswer? deserializedProjectInfoFromAPI = JsonConvert
            .DeserializeObject<GetAllProjectsAnswer>(projectsInfoFromAPI.Result.Content!);

        Assert.Multiple(() =>
        {
            Assert.That(deserializedProjectInfoFromAPI!.Status, Is.EqualTo(true));
            Assert.That(deserializedProjectInfoFromAPI!.Result!.Total, Is.GreaterThan(0));
        });
        totalProjectCounty = deserializedProjectInfoFromAPI!.Result!.Total;
    }

    [Test(Description = "Удаляем проект по коду")]
    [Category("Regression"), Category("Smoke"), AllureSeverity(SeverityLevel.normal)]
    [Category("NFE")]
    [Order(4)]
    public void DeleteProjectTest()
    {
        Debug.Assert(ProjectService != null, nameof(ProjectService) + " != null");
        Assert.That(ProjectService.DeleteProject(_createdProject!.Result.Code),
            Is.EqualTo(HttpStatusCode.OK));

        var projectsInfoFromAPI = ProjectService!.GetAllProjects();

        GetAllProjectsAnswer? deserializedProjectInfoFromAPI = JsonConvert
            .DeserializeObject<GetAllProjectsAnswer>(projectsInfoFromAPI.Result.Content!);

        Assert.Multiple(() =>
        {
            Assert.That(deserializedProjectInfoFromAPI!.Status!, Is.EqualTo(true));
            Assert.That(deserializedProjectInfoFromAPI!.Result!.Total, Is.EqualTo(totalProjectCounty - 1));
        });
    }

    [Test(Description = "Ошибка при создании проеката с полем code длинной более 10 символов")]
    [Category("Regression"), AllureSeverity(SeverityLevel.normal)]
    [Category("AFE")]
    [Order(5)]
    public void AddProjectNegativeTest()
    {
        Project project = Project.Generate();
        project.Code += "A";

        var actual = ProjectService!.CreateProject(project);

        CreateProjectError? _getAnswer = JsonConvert
            .DeserializeObject<CreateProjectError>(actual.Result.Content!);

        Assert.Multiple(() =>
        {
            // проверка статус кода
            Assert.That(actual.Result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

            // проверки данных в ответе
            Assert.That(_getAnswer.Status, Is.EqualTo(false));
            Assert.That(_getAnswer.ErrorMessage, Is.EqualTo("Data is invalid."));
            Assert.That(_getAnswer.ErrorFields[0].Error, Is.EqualTo("Project code may not be greater than 10 characters."));
        });
    }

    // проверка обязательного поля Title
    [Test(Description = "Проверяем, что поле title обязательно для заполнения")]
    [Category("Regression"), AllureSeverity(SeverityLevel.normal)]
    [Category("AFE")]
    [Order(6)]
    public void AddProjectNegativeTest2()
    {
        Project project = Project.Generate();
        project.Title = null;

        var actual = ProjectService!.CreateProject(project);

        CreateProjectError? _getAnswer = JsonConvert
            .DeserializeObject<CreateProjectError>(actual.Result.Content!);

        Assert.Multiple(() =>
        {
            // проверка статус кода
            Assert.That(actual.Result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

            // проверки данных в ответе
            Assert.That(_getAnswer.Status, Is.EqualTo(false));
            Assert.That(_getAnswer.ErrorMessage, Is.EqualTo("Data is invalid."));
            Assert.That(_getAnswer.ErrorFields[0].Error, Is.EqualTo("Title is required."));
        });
    }

    [Test(Description = "Проверяем, что возвращается ошибку если не правильно указан код проекта")]
    [Category("Regression"), AllureSeverity(SeverityLevel.normal)]
    [Category("AFE")]
    [Order(7)]
    public void GetProjectByIncorrectCode()
    {
        var actual = ProjectService!.GetProject("example");

        GetProjectAnswer? _getAnswer = JsonConvert
            .DeserializeObject<GetProjectAnswer>(actual.Result.Content!);

        Assert.Multiple(() =>
        {
            // проверка статус кода
            Assert.That(actual.Result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));

            // проверки данных в ответе
            Assert.That(_getAnswer.Status, Is.EqualTo(false));
        });
    }
}