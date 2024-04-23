namespace FinalWork.Elements;

public class Checkbox(IWebDriver driver, By by)
{
    private readonly UIElement _uiElement = new(driver, by);

    private void Click() => _uiElement.Click();

    public void UseCheckbox(bool set)
    {
        if (IsSet() != set) Click();
    }

    public bool IsSet() => _uiElement.Selected;
}