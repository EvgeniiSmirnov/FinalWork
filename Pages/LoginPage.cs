using OpenQA.Selenium;
using FinalWork.Elements;

namespace FinalWork.Pages;

public class LoginPage : BasePage
{
    private static readonly string END_POINT = "login";

    // Описание элементов
    private static readonly By EmailInputBy = By.XPath("//*[@class='wBsjbV' and @name='email']"); 
    private static readonly By PasswordInputBy = By.Name("password"); //*[@name='password']
    private static readonly By RememberMeCheckboxBy = By.CssSelector("input[name='remember']");
    private static readonly By SignInButtonBy = By.CssSelector("button[type = 'submit']");

    // Инициализация класса
    public LoginPage(IWebDriver driver, bool openPageByUrl) : base(driver, openPageByUrl) { }

    public LoginPage(IWebDriver driver) : base(driver, false) { }

    // Реализация наследуемых методов
    public override bool IsPageOpened() => SignInButton.Displayed && EmailInput.Displayed;

    protected override string GetEndpoint() => END_POINT;

    // Методы поиска элементов
    public IWebElement EmailInput => WaitsHelper.WaitForExists(EmailInputBy);
    public IWebElement PasswordInput => WaitsHelper.WaitForExists(PasswordInputBy);
    public IWebElement RememberMeCheckbox => WaitsHelper.WaitForExists(RememberMeCheckboxBy);
    public Button SignInButton => new Button(Driver, SignInButtonBy);

    // Методы действий с элементами
    public void ClickSignInButton() => SignInButton.Click();
}