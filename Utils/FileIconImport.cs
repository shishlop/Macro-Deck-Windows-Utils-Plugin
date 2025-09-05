using SuchByte.MacroDeck.Icons;
using SuchByte.WindowsUtils.Models;
using SuchByte.MacroDeck.Logging;

namespace SuchByte.WindowsUtils.Utils;

public static class FileIconImport
{
    public static IconImportModel ImportIcon(string filePath)
    {
        string fileName = Path.GetFileNameWithoutExtension(filePath);
        string systemIconPackName = "System";
        MacroDeck.Icons.Icon existingIcon = IconManager.GetIconByString($"{systemIconPackName}.{fileName}");
        
        if (existingIcon != null)
        {
            return new IconImportModel()
            {
                IconPack = systemIconPackName,
                IconId = existingIcon.IconId,
            };
        }

        IntPtr hIcon = ShellIcon.GetJumboIcon(ShellIcon.GetIconIndex(filePath));
        System.Drawing.Icon windowsIcon = (System.Drawing.Icon)System.Drawing.Icon.FromHandle(hIcon).Clone();
        {
            Image icon = windowsIcon.ToBitmap();
            if (icon == null) return null;

            try
            {
                IconPack systemIconPack = IconManager.GetIconPackByName(systemIconPackName) ?? IconManager.CreateIconPack(systemIconPackName, "Macro Deck", "0");

                MacroDeck.Icons.Icon macroDeckIcon = IconManager.AddIconImage(systemIconPack, icon, fileName);
                return new IconImportModel()
                {
                    IconPack = systemIconPack.Name,
                    IconId = macroDeckIcon.IconId,
                };
            }
            catch (Exception e)
            {
                MacroDeckLogger.Error(Main.Instance, $"{e}");
            }
        }
        return null;
    }
}

