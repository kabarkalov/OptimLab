@SETLOCAL EnableDelayedExpansion

@SET SELFPATH=%CD%\_CountSourceCode.cmd
@SET /A TOTALCOUNT=0

@CALL "%SELFPATH%"

@ECHO -------------------------
@ECHO Total number of not empty lines is: %TOTALCOUNT%
@ECHO To calculate total number of lines multiply this number to 1.097
@ECHO -------------------------

@ENDLOCAL

@PAUSE