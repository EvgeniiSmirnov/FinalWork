namespace FinalWork.Core;

public class Browser
{
    public IWebDriver Driver { get; }

    public Browser()
    {
        Driver = new DriverFactory().GetChromeDriver()
            ?? throw new InvalidOperationException("Browser is not supported.");

        Driver.Manage().Window.Maximize();
        Driver.Manage().Cookies.DeleteAllCookies();
        Driver!.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
    }
}