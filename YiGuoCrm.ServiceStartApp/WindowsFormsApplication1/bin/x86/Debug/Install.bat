@echo off
C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe %~dp0\YiGuoCrm.ServiceStartApp.exe
echo Ready to Start,please waiting...
net start YiGuoCrm.ServiceStartApp
pause