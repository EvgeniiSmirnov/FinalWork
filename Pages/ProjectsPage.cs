using OpenQA.Selenium;
using FinalWork.Elements;
using NUnit.Allure.Attributes;

namespace FinalWork.Pages;

public class ProjectsPage : BasePage
{
    private static readonly string END_POINT = "projects";

    // Описание элементов
    private static readonly By TitleTextBy = By.XPath("//h1[@class='uA6zAY' and text()='Projects']");
    private static readonly By CreateNewProjectButtonBy = By.Id("createButton"); //*[@id='createButton'] 
    private static readonly By CreateProjectButtonBy = By.XPath("//button[@type='submit']");

    private static readonly By ProjectNameBy = By.Id("project-name"); //*[@id='project-name']
    private static readonly By ProjectCodeBy = By.Id("project-code"); //*[@id='project-code']
    private static readonly By DescriptionBy = By.Id("description-area"); //*[@id='description-area']
    private static readonly By PublicTypeCheckboxBy = By.XPath("//input[@type='radio'and @value='public']");

    private static readonly By ChatButtonBy = By.XPath("//span[@aria-label='Chat']");
    private static readonly By InvalidDataMessageBy = By.XPath("//span[text()='Data is invalid.']");
    private static readonly By FormatProjectCodeMessageBy = By.XPath("//div[text()='The code format is invalid.']");
    private static readonly By UpperBoundProjectCodeMessageBy = By.XPath("//div[text()='The code may not be greater than 10 characters.']");

    // Инициализация класса
    public ProjectsPage(IWebDriver driver, bool openPageByUrl) : base(driver, openPageByUrl) { }

    public ProjectsPage(IWebDriver driver) : base(driver, false) { }

    // Реализация наследуемых методов
    public override bool IsPageOpened() => TitleText.Displayed;

    protected override string GetEndpoint() => END_POINT;

    // Поиск и ожидание элементов
    public UIElement TitleText => new UIElement(Driver, TitleTextBy);
    public UIElement ProjectNameInput => new(Driver, ProjectNameBy);
    public UIElement ProjectCodeInput => new(Driver, ProjectCodeBy);
    public UIElement DescriptionInput => new(Driver, DescriptionBy);
    public Checkbox PublicTypeCheckbox => new(Driver, PublicTypeCheckboxBy);
    private IWebElement CreateNewProjectButton => WaitsHelper.WaitForExists(CreateNewProjectButtonBy);
    private IWebElement CreateProjectButton => WaitsHelper.WaitForExists(CreateProjectButtonBy);
    private IWebElement InvalidDataMessage => WaitsHelper.WaitForExists(InvalidDataMessageBy);
    private IWebElement FormatProjectCodeMessage => WaitsHelper.WaitForExists(FormatProjectCodeMessageBy);
    private IWebElement UpperBoundProjectCodeMessage => WaitsHelper.WaitForExists(UpperBoundProjectCodeMessageBy);
    public IWebElement ChatButton => WaitsHelper.WaitForExists(ChatButtonBy);

    // Методы действий с элементами
    [AllureStep("Клик по кнопке Create new project")]
    public ProjectsPage ClickCreateNewProjectButton()
    {
        CreateNewProjectButton.Click();
        return new ProjectsPage(Driver);
    }

    [AllureStep("Вводим название проекта")]
    public ProjectsPage InputProjectNameValue(string value)
    {
        ProjectNameInput.Clear();
        ProjectNameInput.SendKeys(value);
        return this;
    }

    [AllureStep("Вводим код проекта")]
    public ProjectsPage InputProjectCodeValue(string value)
    {
        ProjectCodeInput.Clear();
        ProjectCodeInput.SendKeys(value);
        return this;
    }

    [AllureStep("Вводим описание проекта")]
    public ProjectsPage InputDescription(string value)
    {   
        DescriptionInput.Clear();
        DescriptionInput.SendKeys(value);
        return this;
    }

    [AllureStep("Отмечаем чекбок Public")]
    public ProjectsPage SetCheckboxPublicType(bool value)
    {
        PublicTypeCheckbox.UseCheckbox(value);
        return this;
    }

    [AllureStep("Клик по кнопке Create project")]
    public ProjectsPage CreateProjectButtonClick()
    {
        CreateProjectButton.Click();
        return new ProjectsPage(Driver);
    }

    [AllureStep("Проверяем всплывающее окно об ошибке")]
    public bool IsInvalidDataMessageDisplayed() => InvalidDataMessage.Displayed;

    [AllureStep("Проверяем отображение сообщения об ошибке")]
    public bool IsFormatProjectCodeMessageDisplayed() => FormatProjectCodeMessage.Displayed;

    [AllureStep("Проверяем отображение сообщения об ошибке")]
    public bool IsUpperBoundProjectCodeMessageDisplayed() => UpperBoundProjectCodeMessage.Displayed;
}