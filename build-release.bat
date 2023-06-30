"%ProgramFiles%\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\msbuild" "WebModule.MCNV\WebModule.MCNV.csproj" /p:Configuration=Release
xcopy WebModule.MCNV\bin\Web*.dll dist\ /Y
PAUSE
EXIT