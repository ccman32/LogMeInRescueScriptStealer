using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LogMeInRescueScriptStealerGUI.Model
{
    class Installer
    {
        private const int MAX_PATH = 255;

        private static string localAppDataPath = getShortPath(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
        private static string installationFilePath = Path.Combine(localAppDataPath, "LMIRSS");
        private static string installationFileName32 = Path.Combine(installationFilePath, "LMIRSS32.dll");

        private static string installationIniFileName = Path.Combine(installationFilePath, "LMIRSS.ini");

        private static string getShortPath(string path)
        {
            StringBuilder shortPath = new StringBuilder(MAX_PATH);
            NativeMethods.GetShortPathName(path, shortPath, MAX_PATH);

            return shortPath.ToString();
        }

        private static RegistryKey getAppInitDllsKey32()
        {
            RegistryKey baseKey32 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
            return baseKey32.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Windows", true);
        }

        private static List<string> splitAppInitDllValue(RegistryKey appInitDllsKey)
        {
            return ((string)appInitDllsKey.GetValue("AppInit_DLLs")).Split(new Char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        private static void setAppInitDllsValue(RegistryKey appInitDllsKey, List<string> entries)
        {
            appInitDllsKey.SetValue("AppInit_DLLs", string.Join(",", entries), RegistryValueKind.String);
        }

        private static List<string> getAppInitDllsEntries32()
        {
            return splitAppInitDllValue(getAppInitDllsKey32());
        }

        private static bool checkInstallationFilesExist()
        {
            return File.Exists(installationFileName32);
        }

        private static bool checkAppInitDllsKeysExist()
        {
            return getAppInitDllsEntries32().Contains(installationFileName32);
        }

        private static void setAppInitDllsEntries32(List<string> entries)
        {
            setAppInitDllsValue(getAppInitDllsKey32(), entries);
        }

        private static bool checkLoadAppInitDllsValue(RegistryKey loadAppInitDllsKey)
        {
           return (int)loadAppInitDllsKey.GetValue("LoadAppInit_DLLs") == 1;
        }

        private static void setLoadAppInitDllsValue(RegistryKey loadAppInitDllsKey)
        {
            loadAppInitDllsKey.SetValue("LoadAppInit_DLLs", 1, RegistryValueKind.DWord);
        }

        public static bool IsInstalled()
        {
            bool installationFileExists = checkInstallationFilesExist();
            bool appInitDllEntryFound = checkAppInitDllsKeysExist();
            bool loadAppInitDllsEnabled32 = checkLoadAppInitDllsValue(getAppInitDllsKey32());

            if (installationFileExists && appInitDllEntryFound && loadAppInitDllsEnabled32)
            {
                return true;
            }

            return false;
        }

        public static void Install()
        {
            Directory.CreateDirectory(installationFilePath);
            File.WriteAllBytes(installationFileName32, LogMeInRescueScriptStealerGUI.Properties.Resources.LogMeInRescueScriptStealer_32);

            List<string> appInitDllsEntries32 = getAppInitDllsEntries32();
            appInitDllsEntries32.Add(installationFileName32);
            setAppInitDllsEntries32(appInitDllsEntries32);

            setLoadAppInitDllsValue(getAppInitDllsKey32());
        }

        public static bool Uninstall()
        {
            bool appInitDllEntryFound = checkAppInitDllsKeysExist();
            bool installationFileExists = checkInstallationFilesExist();
            bool systemRestartRequired = false;

            if (appInitDllEntryFound)
            {
                List<string> appInitDllsEntries32 = getAppInitDllsEntries32();
                appInitDllsEntries32.Remove(installationFileName32);
                setAppInitDllsEntries32(appInitDllsEntries32);
            }

            if (installationFileExists)
            {
                try
                {
                    Directory.Delete(installationFilePath, true);
                }
                catch (UnauthorizedAccessException)
                {
                    systemRestartRequired = true;
                    NativeMethods.MoveFileEx(installationIniFileName, null, MoveFileFlags.DelayUntilReboot);
                    NativeMethods.MoveFileEx(installationFileName32, null, MoveFileFlags.DelayUntilReboot);
                    NativeMethods.MoveFileEx(installationFilePath, null, MoveFileFlags.DelayUntilReboot);
                }
            }

            return systemRestartRequired;
        }

        public static string GetInstallationIniFileName()
        {
            return installationIniFileName;
        }

        public static void RestartComputer()
        {
            Process.Start("shutdown", "/r /t 00");
        }
    }
}
