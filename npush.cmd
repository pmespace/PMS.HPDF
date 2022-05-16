@echo off
nuget push release\*.nupkg  -Source https://api.nuget.org/v3/index.json
pause