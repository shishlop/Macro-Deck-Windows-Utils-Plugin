using Shlop.WindowsUtilsRevamped.Language;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;

namespace Shlop.WindowsUtilsRevamped.Actions.StartApplication;

public class StartApplicationAction : PluginAction
{
    public override string Name => PluginLanguageManager.PluginStrings.ActionStartApplication;

    public override string Description => PluginLanguageManager.PluginStrings.ActionStartApplicationDescription;

    public override bool CanConfigure => true;

    public override void Trigger(string clientId, ActionButton actionButton)
    {
        var configModel = StartApplicationActionConfigModel.Deserialize(Configuration);
        if (configModel == null) return;

        MacroDeckLogger.Info(PluginInstance.Main, $"Triggered StartApplicationAction with method {configModel.StartMethod}");

        switch (configModel.StartMethod)
        {
            case StartMethod.Start:
                ApplicationLauncher.StartApplication(configModel.Path, configModel.Arguments, configModel.RunAsAdmin);
                UpdateState();
                break;
            case StartMethod.StartStop:
                if (ApplicationLauncher.IsRunning(configModel.Path))
                {
                    ApplicationLauncher.KillApplication(configModel.Path);
                }
                else
                {
                    ApplicationLauncher.StartApplication(configModel.Path, configModel.Arguments, configModel.RunAsAdmin);
                }
                UpdateState();
                break;
            case StartMethod.StartFocus:
                if (!ApplicationLauncher.IsRunning(configModel.Path))
                {
                    ApplicationLauncher.StartApplication(configModel.Path, configModel.Arguments, configModel.RunAsAdmin);
                    UpdateState();
                }
                else
                {
                    ApplicationLauncher.BringToForeground(configModel.Path);
                    UpdateState();
                }
                break;
            case StartMethod.ToggleUsingAHK:
                ToggleAppResult toggleResult = AutoHotkeyRunner.ToggleApp(configModel.Path);
                switch (toggleResult)
                {
                    case ToggleAppResult.NothingHappened:
                        MacroDeckLogger.Info(PluginInstance.Main, $"App not running and was not launched. ({configModel.Path})");
                        break;
                    case ToggleAppResult.ApplicationStarted:
                        MacroDeckLogger.Info(PluginInstance.Main, $"Successfully started the application. ({configModel.Path})");
                        ActionButton.State = true;
                        break;
                    case ToggleAppResult.ApplicationMinimized:
                        MacroDeckLogger.Info(PluginInstance.Main, $"Successfully minimized the application. ({configModel.Path})");
                        ActionButton.State = false;
                        break;
                    case ToggleAppResult.ApplicationFocused:
                        MacroDeckLogger.Info(PluginInstance.Main, $"Successfully brought the application to the foreground. ({configModel.Path})");
                        ActionButton.State = true;
                        break;
                    case ToggleAppResult.Error:
                        MacroDeckLogger.Error(PluginInstance.Main, $"An error occurred during the AHK script execution. ({configModel.Path})");
                        break;
                }
                break;
        }
    }

    public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
    {
        return new StartApplicationActionConfigView(this);
    }

    public override void OnActionButtonLoaded()
    {
        var configModel = StartApplicationActionConfigModel.Deserialize(Configuration);
        if (configModel == null || !configModel.SyncButtonState) return;

        // Subscribe to the global hook's event
        WindowFocusMonitor.WindowFocusChanged += OnWindowFocusChanged;
    }

    public override void OnActionButtonDelete()
    {
        var configModel = StartApplicationActionConfigModel.Deserialize(Configuration);
        if (configModel == null || !configModel.SyncButtonState) return;

        // Unsubscribe from the event
        WindowFocusMonitor.WindowFocusChanged -= OnWindowFocusChanged;
    }

    private void OnWindowFocusChanged(string title, uint processId)
    {
        // Run this in a background task to not block the UI thread
        Task.Run(() => UpdateState());
    }

    // private void UpdateState(bool optionalForeState = false)
    private void UpdateState()
    {
        if (ActionButton == null) return;
        var configModel = StartApplicationActionConfigModel.Deserialize(Configuration);
        if (configModel == null || !configModel.SyncButtonState || string.IsNullOrWhiteSpace(configModel.Path)) return;

        MacroDeckLogger.Info(PluginInstance.Main, $"Updating button state for method {configModel.StartMethod}");

        switch (configModel?.StartMethod)
        {
            case StartMethod.StartStop:
                ActionButton.State = ApplicationLauncher.IsRunning(configModel.Path);
                break;
            case StartMethod.StartFocus:
            case StartMethod.ToggleUsingAHK:
                ActionButton.State = ApplicationLauncher.IsFocused(configModel.Path);
                break;
            default:
                return;
        }
    }
}
