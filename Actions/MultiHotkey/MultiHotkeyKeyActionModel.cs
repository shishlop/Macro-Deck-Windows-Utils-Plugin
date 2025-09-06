using WindowsInput;

namespace Shlop.WindowsUtilsRevamped.Actions.MultiHotkey;

public class MultiHotkeyKeyActionModel : IMultiHotkeyAction
{

    public MultiHotkeyKeyMethod MultiHotkeyKeyMethod { get; set; } = MultiHotkeyKeyMethod.Down;

    public VirtualKeyCode KeyCode;


    public void Execute()
    {
        switch (MultiHotkeyKeyMethod)
        {
            case MultiHotkeyKeyMethod.Down:
                Main.Instance.InputSimulator.Keyboard.KeyDown(KeyCode);
                break;
            case MultiHotkeyKeyMethod.Up:
                Main.Instance.InputSimulator.Keyboard.KeyUp(KeyCode);
                break;
        }
    }
}


public enum MultiHotkeyKeyMethod
{
    Down,
    Up,
}
