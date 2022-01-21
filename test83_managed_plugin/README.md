# test83_managed_plugin

```
> cd src
> dir /b
ManagedPluginTest.cs
_build.bat

> type _build.bat
set DOTNET_FRAMEWORK_PATH=C:\Windows\Microsoft.NET\Framework\v3.5
set UNITY_MANAGED_PATH="C:\Program Files\Unity\Hub\Editor\2020.3.24f1\Editor\Data\Managed"

%DOTNET_FRAMEWORK_PATH%\csc.exe  /target:library ^
  /reference:%UNITY_MANAGED_PATH%\UnityEngine.dll ^
  /reference:%UNITY_MANAGED_PATH%\UnityEditor.dll ^
  /out:ManagedPluginTest.dll ManagedPluginTest.cs 

> dir /b
ManagedPluginTest.cs
ManagedPluginTest.dll
_build.bat

> cp ManagedPluginTest.dll ..\Assets\Plugins\Editor
```
