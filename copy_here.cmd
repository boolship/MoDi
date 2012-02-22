:: run at command line: copy_here .
copy /Y %1\mo %1\zip_mo
copy /Y %1\mo.cmd %1\zip_mo
copy /Y %1\UpdateMode\bin\Release\mo40.exe %1\zip_mo
copy /Y %1\ConsoleDomain\bin\Release\Boolship.Console.Domain.dll %1\zip_mo
copy /Y %1\ConsoleDosAccess\bin\Release\Boolship.Console.Dos.dll %1\zip_mo
copy /Y %1\UpdateModeApplicationServices\bin\Release\Boolship.Console.UpdateMode.ApplicationServices.dll %1\zip_mo
copy /Y %1\mo20\bin\Release\mo20.exe %1\zip_mo
copy /Y %1\mo20ConsoleDomain\bin\Release\mo20.Boolship.Console.Domain.dll %1\zip_mo
copy /Y %1\mo20ConsoleDosAccess\bin\Release\mo20.Boolship.Console.Dos.dll %1\zip_mo
copy /Y %1\mo20UpdateModeApplicationServices\bin\Release\mo20.Boolship.Console.UpdateMode.ApplicationServices.dll %1\zip_mo
cd %1\zip_mo
zip %1\mo.zip *.*
cd %1
