@echo off
rem Builds WoWDatabaseEditor for Windows into bin\wowdatabaseeditor-win
rem Requirements: .NET 8 SDK, the repository cloned with submodules:
rem   git clone --recurse-submodules https://github.com/wnocxm/WoWDatabaseEditor837
rem   (or in an existing clone: git submodule update --init --recursive)

setlocal
set OUT=bin\wowdatabaseeditor-win

if not exist "AvaloniaEdit\src" (
    echo Submodules are missing, running: git submodule update --init --recursive
    git submodule update --init --recursive || goto :error
)

dotnet publish -c Release --self-contained false -f net8.0 -r win-x64 -o %OUT% LoaderAvalonia\LoaderAvalonia.csproj -p:PublishSingleFile=true -p:PublishTrimmed=false || goto :error
dotnet publish -c Release --self-contained false -f net8.0 -r win-x64 -o %OUT% Modules\CrashReport\CrashReport.csproj -p:PublishSingleFile=true -p:PublishTrimmed=false || goto :error
dotnet publish -c Release --self-contained false -f net8.0 -r win-x64 -o %OUT%\parser WoWPacketParserLoader || goto :error

echo.
echo Done. Start %OUT%\WoWDatabaseEditor.exe
exit /b 0

:error
echo.
echo Build failed.
exit /b 1
