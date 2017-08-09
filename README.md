LogMeIn Rescue Script Stealer
=================================

OVERVIEW
-----
LogMeIn Rescue Script Stealer allows you to steal scripts and programs that are remotely executed on your computer by the program "LogMeIn Rescue".
It uses the AppInit_DLLs registry value to inject a dll into the "Support-LogMeInRescue" program. The loaded dll then checks if the technician attempts to execute any scripts on your computer and will copy such scripts to a directory of your choice.

INSTALLATION
-----
- Download the latest release from https://github.com/ccman32/LogMeInRescueScriptStealer/releases
- Extract all files from the .rar archive
- Run the LogMeInRescueScriptStealerGUI.exe file
- Specify the "Output Path" - Whenever a technician attempts to execute a script, this path is where the script will be copied to
- Click the "Install" button to install LogMeIn Rescue Script Stealer

HOW TO USE
-----
LogMeIn Rescue Script Stealer runs fully automatic after installing it.