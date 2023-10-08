@ECHO OFF
:: variable for current directory
set current_directory=%~dp0

:: key check
for /f "delims=" %%x in ('type "%current_directory%key.txt"') do set license=%%x
IF "%license%" == "" exit /B

echo [constelia.ai]

:: getSolution
curl -L -o "%current_directory%fantasy.universe4.exe" "https://constelia.ai/api.php?key=%license%&software=universe4&cmd=getSolution"

:: execute
"%current_directory%fantasy.universe4.exe" --sessions

:: old instance
del "%current_directory%fantasy.universe4.exe"
