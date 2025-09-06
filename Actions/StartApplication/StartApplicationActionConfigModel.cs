using System.Text.Json;
using System.Text.Json.Serialization;
using Shlop.WindowsUtilsRevamped.SerializableConfiguration;

namespace Shlop.WindowsUtilsRevamped.Actions.StartApplication;

internal class StartApplicationActionConfigModel : ISerializableConfiguration
{
    [JsonPropertyName("path")] // To ensure backward compatibility with older versions of this plugin
    public string Path { get; set; } = "";

    [JsonPropertyName("arguments")] // To ensure backward compatibility with older versions of this plugin
    public string Arguments { get; set; } = "";
    public bool RunAsAdmin { get; set; } = false;
    // If true, the button state (on/off) will be synced with the application state (focus state)
    public bool SyncButtonState { get; set; } = false;
    // If true, the app icon will be derived from the application executable
    public bool DeriveAppIcon { get; set; } = false;
    public StartMethod StartMethod { get; set; } = StartMethod.Start;



    public string Serialize()
    {
        return JsonSerializer.Serialize(this);
    }
    public static StartApplicationActionConfigModel Deserialize(string config)
    {
        return ISerializableConfiguration.Deserialize<StartApplicationActionConfigModel>(config);
    }
}

public enum StartMethod
{
    Start,
    StartStop,
    StartFocus,
    ToggleUsingAHK,
}
