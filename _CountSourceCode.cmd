@REM Calculate number of lines in source files in current directory.
@FOR %%P IN (*.bat *.c *.config *.cpp *.cs *.h *.js *.pl *.sql *.vb *.vbs *.xaml) DO @(
    @SET /A LINESCOUNT=0
    @FOR /F "eol=" %%L IN (%%P) DO @(
        @SET /A LINESCOUNT=!LINESCOUNT!+1
    )
    @SET /A TOTALCOUNT=!TOTALCOUNT!+!LINESCOUNT!
    @ECHO Source file "%%P" contains !LINESCOUNT! lines // Total lines: !TOTALCOUNT!
)

@REM Run interative process by subfolders.
@FOR /D %%i IN (.\*) DO @(
    IF NOT "%%i"==".\" (
        CD "%%i"
        CALL "%SELFPATH%"
        CD ..
    )
)