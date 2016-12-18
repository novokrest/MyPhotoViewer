@echo off
"%ProgramFiles(x86)%\MSBuild\14.0\Bin\MsBuild.exe" Publish.proj /fl /p:TargetEnvPropsFile=EnvConfig\Env-Local.proj
pause