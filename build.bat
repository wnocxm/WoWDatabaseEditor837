@echo off
rem Builds WoWDatabaseEditor for Windows into bin\wowdatabaseeditor-win
rem Requirements:
rem   - .NET 8 SDK (x64)        https://dotnet.microsoft.com/download/dotnet/8.0
rem   - Java 17 or newer        https://adoptium.net  (needed by the ANTLR code generators)
rem   - the repository cloned with submodules:
rem       git clone --recurse-submodules https://github.com/wnocxm/WoWDatabaseEditor837
rem The full output is written to build.log

setlocal
cd /d "%~dp0"
set OUT=bin\wowdatabaseeditor-win
set LOG=build.log

echo ==== Environment ==== > %LOG%
echo [.NET SDKs]
dotnet --list-sdks
dotnet --list-sdks >> %LOG% 2>&1
echo [selected SDK]
dotnet --version
dotnet --version >> %LOG% 2>&1
echo [Java]
java -version
java -version >> %LOG% 2>&1
if errorlevel 1 (
    echo.
    echo WARNING: Java was not found. Install Java 17+ from https://adoptium.net and run build.bat again.
    echo WARNING: Java was not found >> %LOG%
)

if not exist "AvaloniaEdit\src" (
    echo Submodules are missing, running: git submodule update --init --recursive
    git submodule update --init --recursive >> %LOG% 2>&1 || goto :error
)

echo.
echo Building, this can take a few minutes. Output: %LOG%
echo ==== LoaderAvalonia ==== >> %LOG%
dotnet publish -c Release --self-contained false -f net8.0 -r win-x64 -o %OUT% LoaderAvalonia\LoaderAvalonia.csproj -p:PublishSingleFile=true -p:PublishTrimmed=false >> %LOG% 2>&1 || goto :error
echo ==== CrashReport ==== >> %LOG%
dotnet publish -c Release --self-contained false -f net8.0 -r win-x64 -o %OUT% Modules\CrashReport\CrashReport.csproj -p:PublishSingleFile=true -p:PublishTrimmed=false >> %LOG% 2>&1 || goto :error
echo ==== Packet parser ==== >> %LOG%
dotnet publish -c Release --self-contained false -f net8.0 -r win-x64 -o %OUT%\parser WoWPacketParserLoader >> %LOG% 2>&1 || goto :error

echo.
echo Done. Start %OUT%\WoWDatabaseEditor.exe
pause
exit /b 0

:error
echo.
echo Build failed. First errors:
findstr /c:" error " %LOG% | findstr /v /c:"CS0006" > build-errors.txt
type build-errors.txt | more
echo.
echo Full output: %LOG%   Errors only: build-errors.txt
pause
exit /b 1
