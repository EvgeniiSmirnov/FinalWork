using OpenQA.Selenium;

namespace FinalWork.Elements;

public class Button
{
    private readonly UIElement _uiElement;

    public Button(IWebDriver webDriver, By by)
    {
        _uiElement = new UIElement(webDriver, by);
    }

    public Button(IWebDriver webDriver, IWebElement webElement)
    {
        _uiElement = new UIElement(webDriver, webElement);
    }

    public void Click()
    {
        _uiElement.Click();
    }

    public void Submit()
    {
        _uiElement.Submit();
    }

    public string Text => _uiElement.Text;
    public bool Displayed => _uiElement.Displayed;
}