﻿using OpenQA.Selenium;
using FinalWork.Elements;
using NUnit.Allure.Attributes;

namespace FinalWork.Pages;

public class ProjectSettingsPage : BasePage
{
    private static readonly By ProjectSettingsTitleBy = By.XPath("//h1[text()='Project settings']");
    private static readonly By DeleteProjectButtonBy = By.XPath("//span[text()=' Delete project']");
    private static readonly By DeleteProjectFinalButtonBy = By.XPath("//span[text()='Delete project']");
    private static readonly By ChangeLogoButtonBy = By.XPath("//label[text()='Change logo']");
    private static readonly By ChangeLogoInputBy = By.XPath("//input[@type='file']");
    private static readonly By MessageLogoInputBy = By.XPath("//span[text()='Project avatar was successfully updated!']"); 

    public ProjectSettingsPage(IWebDriver driver, bool openPageByUrl) : base(driver, openPageByUrl) { }

    public ProjectSettingsPage(IWebDriver driver) : base(driver, false) { }

    public override bool IsPageOpened() => ProjectSettingsTitle.Displayed;

    protected override string GetEndpoint() => "";

    // Поиск и ожидание элементов
    public UIElement ProjectSettingsTitle => new(Driver, ProjectSettingsTitleBy);
    public IWebElement ChangeLogoButton => new UIElement(Driver, ChangeLogoInputBy);
    private IWebElement DeleteProjectButton => WaitsHelper.WaitForExists(DeleteProjectButtonBy);
    private IWebElement DeleteProjectFinalButton => WaitsHelper.WaitForExists(DeleteProjectFinalButtonBy);
    public IWebElement MessageLogoInput => WaitsHelper.WaitForExists(MessageLogoInputBy);
    
    [AllureStep("Клик по кнопке Delete project")]
    public ProjectSettingsPage ClickDeleteProjectButton()
    {
        DeleteProjectButton.Click();
        return new ProjectSettingsPage(Driver);
    }

    [AllureStep("Клик по кнопке Delete project")]
    public ProjectSettingsPage ClickDeleteProjectFinalButton()
    {
        DeleteProjectFinalButton.Click();
        return new ProjectSettingsPage(Driver);
    }

    //public ProjectSettingsPage ClickChangeLogoButton1()
    //{
    //    ChangeLogoButton.Click();
    //    return new ProjectSettingsPage(Driver);
    //}

    public bool IsMessageLogoExist() => MessageLogoInput.Displayed;
}