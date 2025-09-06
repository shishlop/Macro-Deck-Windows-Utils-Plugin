using SuchByte.MacroDeck.Icons;
using SuchByte.MacroDeck.Logging;

namespace Shlop.WindowsUtilsRevamped.IconUtil;

public static class FileIconImport
{
    public static IconImportModel ImportIcon(string filePath)
    {
        string fileName = Path.GetFileNameWithoutExtension(filePath);
        string systemIconPackName = "System";
        SuchByte.MacroDeck.Icons.Icon existingIcon = IconManager.GetIconByString($"{systemIconPackName}.{fileName}");

        if (existingIcon != null)
        {
            return new IconImportModel()
            {
                IconPack = systemIconPackName,
                IconId = existingIcon.IconId,
            };
        }

        nint hIcon = ShellIcon.GetJumboIcon(ShellIcon.GetIconIndex(filePath));
        System.Drawing.Icon windowsIcon = (System.Drawing.Icon)System.Drawing.Icon.FromHandle(hIcon).Clone();
        {
            Image icon = windowsIcon.ToBitmap();
            if (icon == null) return null;

            try
            {
                IconPack systemIconPack = IconManager.GetIconPackByName(systemIconPackName) ?? IconManager.CreateIconPack(systemIconPackName, "Macro Deck", "0");

                SuchByte.MacroDeck.Icons.Icon macroDeckIcon = IconManager.AddIconImage(systemIconPack, icon, fileName);
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

public class IconImportModel
{
    public string IconPack { get; set; }

    public string IconId { get; set; }

    public string ToString()
    {
        return $"{IconPack}.{IconId}";
    }
}

