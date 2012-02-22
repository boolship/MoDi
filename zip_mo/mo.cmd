@echo off
::
::  Run a program compiled to any .Net framework version
::
::  Copyright (c) 2011 boolship@gmail.com. All rights reserved.
::
::  This program is free software: you can redistribute it and/or modify
::  it under the terms of the GNU General Public License as published by
::  the Free Software Foundation, either version 3 of the License, or
::  (at your option) any later version.
::
::  This program is distributed in the hope that it will be useful,
::  but WITHOUT ANY WARRANTY; without even the implied warranty of
::  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
::  GNU General Public License for more details.
::
::  You should have received a copy of the GNU General Public License
::  along with this program.  If not, see <http://www.gnu.org/licenses/>.
::
if "%OS%" == "Windows_NT" goto WinNT
goto :done
:WinNT
setlocal

:: target framework versions (1.0, 1.1, 2.0, 3.0, 3.5, 4.0, 4.5)
set _frame10=v1.0.3705
set _frame11=v1.1.4322
set _frame20=v2.0.50727
set _frame30=v3.0
set _frame35=v3.5
set _frame40=v4.0.30319
set _frame45=v4.5.40805
:: service packs #TODO#
set _frame20sp1="v2.x"
set _frame20sp2="v2.y"
set _frame30sp1="v3.x"
set _frame30sp2="v3.y"
set _frame35sp1="Microsoft .NET Framework 3.5 SP1"

set _program_name=%0
set _program_type=.exe
set _arguments=%*
set _netpath=%SystemRoot%\Microsoft.NET\Framework
set _netpath64=%SystemRoot%\Microsoft.NET\Framework64
set _done=

if exist %_netpath% goto :exist
if exist %_netpath64% goto :exist
echo .Net must be installed, install and try again.
goto :done

:exist
:: manually set run1 or run2 method
goto :run2
goto :done

:run1  
  :: (1) run ordered by reverse directory sort
  ::   adv: simple, find framework version run framework program
  ::   dis: ordering assumptions, fails to run framework 3.5 sp1 before v1.1/v1.0
  ::
  :: find installed framework64 high-low, find framework32 high-low, run framework version
  ::
  if exist %_netpath64% for /F "tokens=*" %%f in ('dir /AD/O-N/B %_netpath64%\*') do call :find_framework %%f
  if exist %_netpath% for /F "tokens=*" %%f in ('dir /AD/O-N/B %_netpath%\*') do call :find_framework %%f
  if "%_done%" == "" call :not_found
  ::
  :: service packs test case "Microsoft .NET Framework 3.5 SP1"
  :: for /F "tokens=*" %%f in ('echo %_frame35sp1%') do call :find_framework %%f
  ::
  goto :done

:run2  
  :: (2) run ordered by framework targets
  ::   adv: explicit ordering
  ::   dis: maintain complex framework versions
  ::
  :: find framework target, verify CLR files, run framework version
  ::
  :: four key CLR files (4.0.30319, 2.0.50727, 1.1.4322, 1.0.3705)
  ::
  :: (1) %_netpath%\%_frame40%\mscorlib.dll
  set _nospace=%_frame45: =%
  if exist %_netpath64%\%_frame40%\mscorlib.dll call :find_framework %_nospace%
  if exist %_netpath%\%_frame40%\mscorlib.dll call :find_framework %_nospace%
  set _nospace=%_frame40: =%
  if exist %_netpath64%\%_frame40%\mscorlib.dll call :find_framework %_nospace%
  if exist %_netpath%\%_frame40%\mscorlib.dll call :find_framework %_nospace%
  ::
  :: (2) %_netpath%\%_frame20%\mscorlib.dll
  set _nospace=%_frame35sp1: =%
  if exist %_netpath%\%_frame20%\mscorlib.dll call :find_framework %_nospace%
  set _nospace=%_frame35: =%
  if exist %_netpath64%\%_frame20%\mscorlib.dll call :find_framework %_nospace%
  if exist %_netpath%\%_frame20%\mscorlib.dll call :find_framework %_nospace%
  set _nospace=%_frame30: =%
  if exist %_netpath64%\%_frame20%\mscorlib.dll call :find_framework %_nospace%
  if exist %_netpath%\%_frame20%\mscorlib.dll call :find_framework %_nospace%
  set _nospace=%_frame20: =%
  if exist %_netpath64%\%_frame20%\mscorlib.dll call :find_framework %_nospace%
  if exist %_netpath%\%_frame20%\mscorlib.dll call :find_framework %_nospace%
  ::
  :: (3) %_netpath%\%_frame11%\mscorlib.dll
  set _nospace=%_frame11: =%
  if exist %_netpath%\%_frame11%\mscorlib.dll call :find_framework %_nospace%
  ::
  :: (4) %_netpath%\%_frame10%\mscorlib.dll
  set _nospace=%_frame10: =%
  if exist %_netpath%\%_frame10%\mscorlib.dll call :find_framework %_nospace%
  ::
  if "%_done%" == "" call :not_found
  goto :done

:find_framework
  if "%_done%" NEQ "" goto :eof
  set _arg=%1
  set _nospace=%_arg: =%
  if "%_nospace%" == "" goto :eof
  if "%_nospace%" == "VJSharp" goto :eof
  if "%_nospace%" == "URTInstallPath_GAC" goto :eof
  ::
  :: find framework version run framework program
  call set _version=%%_nospace:~1,1%%%%_nospace:~3,1%%
  if "%_version%" == "10" call :not_supported v1.0
  if "%_version%" == "11" call :not_supported v1.1
  if "%_version%" == "20" call :run_program %_version%
  if "%_version%" == "30" call :run_program %_version%
  if "%_version%" == "35" call :run_program %_version%
  if "%_version%" == "40" call :run_program %_version%
  if "%_version%" == "45" call :run_program %_version%
  if "%_nospace%" == "%_frame35sp1: =%" call :run_program 35
  goto :eof  
  
:run_program
  if "%_done%" NEQ "" goto :eof
  set _arg=%1
  :: Runs a .Net program with the same name and version suffix. Convention requires a set of target executables.
  if exist %~dp0%_program_name%%_arg%%_program_type% (
    :: echo running %~dp0%_program_name%%_arg% %_arguments%
	%~dp0%_program_name%%_arg% %_arguments%
    set _done=true
  )
  goto :eof  
  
:not_supported
  echo Warning! %_program_name%??%_program_type% on .Net %1 is not supported
  goto :eof  

:not_found
  echo Error! Any %_program_name%??%_program_type% not found (with ?? framework version).
  goto :eof
  
:done
  endlocal
  @echo on
  
