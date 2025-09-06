namespace Shlop.WindowsUtilsRevamped.SerializableConfiguration;

internal interface ISerializableConfigViewModel
{
    protected ISerializableConfiguration SerializableConfiguration { get; }

    void SetConfig();

    bool SaveConfig();
}
