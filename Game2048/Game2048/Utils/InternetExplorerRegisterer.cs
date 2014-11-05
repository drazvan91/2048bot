using System;
using Microsoft.Win32;

namespace Game2048.Utils
{
    public static class InternetExplorerRegisterer
    {
        public static void RegisterIE11()
        {
            string executablePath = Environment.GetCommandLineArgs()[0];
            string executableName = System.IO.Path.GetFileName(executablePath);

            RegistryKey registrybrowser = Registry.CurrentUser.OpenSubKey
                (@"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", true);

            if (registrybrowser == null)
            {
                RegistryKey registryFolder = Registry.CurrentUser.OpenSubKey
                    (@"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl", true);
                registrybrowser = registryFolder.CreateSubKey("FEATURE_BROWSER_EMULATION");
            }
            registrybrowser.SetValue(executableName, 0x02710, RegistryValueKind.DWord);
            registrybrowser.Close();
        }
    }
}