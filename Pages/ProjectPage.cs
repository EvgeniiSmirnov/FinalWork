using FinalWork.Elements;
using OpenQA.Selenium;

namespace FinalWork.Pages;

public class ProjectPage : BasePage
{
    private static readonly By CreateNewCaseButtonBy = By.Id("create-case-button");
    private static readonly By RepositoryNameTextBy = By.XPath("//h1[@class='pOpqJc']");
    private static readonly By SettingsButtonBy2 = By.XPath("//*[@class='VI0017' and text()='Settings']");
    private static readonly By CreateNewSuiteButtonBy = By.XPath("//*[text()='Create new suite']");
    private static readonly By SuiteNameInpitBy = By.Id("title"); //*[@id='title']
    private static readonly By CreateSuiteButtonBy = By.XPath("//button[@type='submit']");
    private static readonly By SuitesButtonBy = By.XPath("//*[@class='qgLedT' and text()='Suites']");

    public ProjectPage(IWebDriver driver, bool openPageByUrl) : base(driver, openPageByUrl) { }

    public ProjectPage(IWebDriver driver) : base(driver,false) { }

    public override bool IsPageOpened() => CreateNewCaseButton.Displayed;

    protected override string GetEndpoint() => "";

    private IWebElement CreateNewCaseButton => WaitsHelper.WaitForExists(CreateNewCaseButtonBy);
    private IWebElement RepositoryNameText => WaitsHelper.WaitForExists(RepositoryNameTextBy);
    private IWebElement SettingsButton => WaitsHelper.WaitForExists(SettingsButtonBy2);
    private IWebElement CreateNewSuiteButton => WaitsHelper.WaitForExists(CreateNewSuiteButtonBy);
    private IWebElement SuiteNameInpit => WaitsHelper.WaitForExists(SuiteNameInpitBy);
    private IWebElement CreateSuiteButton => WaitsHelper.WaitForExists(CreateSuiteButtonBy);
    public IWebElement SuitesButton => WaitsHelper.WaitForExists(SuitesButtonBy);

    public string GetRepositoryNameText() => RepositoryNameText.Text;    

    public ProjectPage ClickSettingsButton()
    {
        SettingsButton.Click();
        return new ProjectPage(Driver);
    }
    public ProjectPage ClickCreateNewSuiteButton()
    {
        CreateNewSuiteButton.Click();
        return new ProjectPage(Driver);
    }

    public ProjectPage InputSuiteNameValue(string value)
    {
        SuiteNameInpit.Clear();
        SuiteNameInpit.SendKeys(value);
        return this;
    }

    public ProjectPage ClickCreateSuiteButton()
    {
        CreateSuiteButton.Click();
        return new ProjectPage(Driver);
    }
}