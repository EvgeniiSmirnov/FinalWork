using FinalWork.Elements;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;

namespace FinalWork.Pages;

public class ProjectPage : BasePage
{
    // Описание элементов
    private static readonly By CreateNewCaseButtonBy = By.Id("create-case-button");
    private static readonly By RepositoryNameTextBy = By.XPath("//h1[@class='pOpqJc']");
    private static readonly By SettingsButtonBy2 = By.XPath("//*[@class='VI0017' and text()='Settings']");
    private static readonly By CreateNewSuiteButtonBy = By.XPath("//*[text()='Create new suite']");
    private static readonly By SuiteNameInpitBy = By.Id("title"); //*[@id='title']
    private static readonly By CreateSuiteButtonBy = By.XPath("//button[@type='submit']");
    private static readonly By SuitesButtonBy = By.XPath("//*[@class='qgLedT' and text()='Suites']");

    // Инициализация класса
    public ProjectPage(IWebDriver driver, bool openPageByUrl) : base(driver, openPageByUrl) { }

    public ProjectPage(IWebDriver driver) : base(driver,false) { }

    // Реализация наследуемых методов
    public override bool IsPageOpened() => CreateNewCaseButton.Displayed;

    protected override string GetEndpoint() => "";

    // Поиск и ожидание элементов
    private IWebElement CreateNewCaseButton => WaitsHelper.WaitForExists(CreateNewCaseButtonBy);
    private IWebElement RepositoryNameText => WaitsHelper.WaitForExists(RepositoryNameTextBy);
    private IWebElement SettingsButton => WaitsHelper.WaitForExists(SettingsButtonBy2);
    private IWebElement CreateNewSuiteButton => WaitsHelper.WaitForExists(CreateNewSuiteButtonBy);
    private IWebElement SuiteNameInpit => WaitsHelper.WaitForExists(SuiteNameInpitBy);
    private IWebElement CreateSuiteButton => WaitsHelper.WaitForExists(CreateSuiteButtonBy);
    public IWebElement SuitesButton => WaitsHelper.WaitForExists(SuitesButtonBy);

    // Методы действий с элементами
    [AllureStep("Получаем название репозитория")]
    public string GetRepositoryNameText() => RepositoryNameText.Text;
    
    [AllureStep("Клик по кнопке Settings")]
    public ProjectPage ClickSettingsButton()
    {
        SettingsButton.Click();
        return new ProjectPage(Driver);
    }

    [AllureStep("Клик по кнопке Create new suite")]
    public ProjectPage ClickCreateNewSuiteButton()
    {
        CreateNewSuiteButton.Click();
        return new ProjectPage(Driver);
    }

    [AllureStep("Вводим название сьюта")]
    public ProjectPage InputSuiteNameValue(string value)
    {
        SuiteNameInpit.Clear();
        SuiteNameInpit.SendKeys(value);
        return this;
    }

    [AllureStep("Клик по кнопке Create")]
    public ProjectPage ClickCreateSuiteButton()
    {
        CreateSuiteButton.Click();
        return new ProjectPage(Driver);
    }
}