@echo off

copy README.md WebApi.Hal\Readme.txt
.nuget\nuget.exe pack WebApi.Hal\WebApi.Hal.csproj -build -version 0.9.4.0
pause