using FinalWork.Elements;

namespace FinalWork.Pages;

public class ProjectSettingsPage : BasePage
{
    // Описание элементов
    private static readonly By ProjectSettingsTitleBy = By.XPath("//h1[text()='Project settings']");
    private static readonly By DeleteProjectButtonBy = By.XPath("//span[text()=' Delete project']");
    private static readonly By DeleteProjectFinalButtonBy = By.XPath("//span[text()='Delete project']");
    private static readonly By ChangeLogoButtonBy = By.XPath("//label[text()='Change logo']");
    private static readonly By ChangeLogoInputBy = By.XPath("//input[@type='file']");
    private static readonly By MessageLogoInputBy = By.XPath("//span[text()='Project avatar was successfully updated!']");

    // Инициализация класса
    public ProjectSettingsPage(IWebDriver driver, bool openPageByUrl) : base(driver, openPageByUrl) { }

    public ProjectSettingsPage(IWebDriver driver) : base(driver, false) { }

    // Реализация наследуемых методов
    public override bool IsPageOpened() => ProjectSettingsTitle.Displayed;

    protected override string GetEndpoint() => "";

    // Поиск и ожидание элементов
    public UIElement ProjectSettingsTitle => new(Driver, ProjectSettingsTitleBy);
    public IWebElement ChangeLogoButton => new UIElement(Driver, ChangeLogoInputBy);
    private IWebElement DeleteProjectButton => WaitsHelper.WaitForExists(DeleteProjectButtonBy);
    private IWebElement DeleteProjectFinalButton => WaitsHelper.WaitForExists(DeleteProjectFinalButtonBy);
    public IWebElement MessageLogoInput => WaitsHelper.WaitForExists(MessageLogoInputBy);

    // Методы действий с элементами
    [AllureStep("Клик по кнопке Delete project")]
    public ProjectSettingsPage ClickDeleteProjectButton()
    {
        DeleteProjectButton.Click();
        return new ProjectSettingsPage(Driver);
    }

    [AllureStep("Клик по кнопке Delete")]
    public ProjectSettingsPage ClickDeleteProjectFinalButton()
    {
        DeleteProjectFinalButton.Click();
        return new ProjectSettingsPage(Driver);
    }

    [AllureStep("Проверка отображения сообщения")]
    public bool IsMessageLogoExist() => MessageLogoInput.Displayed;
}