@ECHO Cleaning directory %CD%

@REM Removing C# binaries.
@IF EXIST Bin RD /S /Q Bin
@IF EXIST Obj RD /S /Q Obj

@REM Removing C++ binaries.
@IF EXIST Debug RD /S /Q Debug
@IF EXIST Release RD /S /Q Release

@REM Removing temprorary user files.
@IF EXIST *.ncb DEL /F /Q *.ncb
@IF EXIST *.user DEL /F /Q *.user

@FOR /D %%i IN (.\*) DO @(
    @REM Run interative process by subfolders.
    CD "%%i"
    CALL "%SELFPATH%"
    CD ..
)