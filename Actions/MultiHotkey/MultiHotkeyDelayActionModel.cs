namespace Shlop.WindowsUtilsRevamped.Actions.MultiHotkey;

public class MultiHotkeyDelayActionModel : IMultiHotkeyAction
{
    public int DelayLengthMs { get; set; } = 100;

    public void Execute()
    {
        Thread.Sleep(DelayLengthMs);
    }
}
