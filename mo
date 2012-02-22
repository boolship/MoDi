#!/bin/sh
#
#  Run a program compiled to any .Net framework version from Bash shell
#
#  Copyright (c) 2011 boolship@gmail.com. All rights reserved.
#
#  This program is free software: you can redistribute it and/or modify
#  it under the terms of the GNU General Public License as published by
#  the Free Software Foundation, either version 3 of the License, or
#  (at your option) any later version.
#
#  This program is distributed in the hope that it will be useful,
#  but WITHOUT ANY WARRANTY; without even the implied warranty of
#  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
#  GNU General Public License for more details.
#
#  You should have received a copy of the GNU General Public License
#  along with this program.  If not, see <http://www.gnu.org/licenses/>.
#

# target framework versions (1.0, 1.1, 2.0, 3.0, 3.5, 4.0, 4.5)
_frame10=v1.0.3705
_frame11=v1.1.4322
_frame20=v2.0.50727
_frame30=v3.0
_frame35=v3.5
_frame40=v4.0.30319
_frame45=v4.5.40805

_program_name=`basename "${BASH_SOURCE[0]}"`
_program_type=.exe
_arguments=$*
_netpath=$SYSTEMROOT/Microsoft.NET/Framework
_netpath64=$SYSTEMROOT/Microsoft.NET/Framework64
_done=

# set program directory, also safe with ". mo"
_program_dir="${BASH_SOURCE[0]}";
if ([ -h "${_program_dir}" ]) then
  while([ -h "${_program_dir}" ]) do _program_dir=`readlink "${_program_dir}"`; done
fi
pushd . > /dev/null
cd `dirname ${_program_dir}` > /dev/null
_program_dir=`pwd`;
popd  > /dev/null

# verify .Net
if ! [[ -d "$_netpath" && ! -L "$_netpath" ]] && 
   ! [[ -d "$_netpath64" && ! -L "$_netpath64" ]] ; then
  echo .Net must be installed, install and try again.
  exit
fi

function run1 {
  # (1) run ordered by reverse directory sort
  #   adv: simple, find framework version run framework program
  #   dis: ordering assumptions, fails to run framework 3.5 sp1 before v1.1/v1.0
  #
  # find installed framework64 high-low, find framework32 high-low, run framework version
  #
  if [[ -d "$_netpath64" && ! -L "$_netpath64" ]] ; then
    #echo path 64 $_netpath64
	for _fr in `ls -dr $_netpath64/*/` ; do find_framework $(echo $_fr | sed 's#.*/Framework64/\(.*\)/$# \1#') ; done
  fi 
  if [[ "$_done" ]] ; then return ; fi
  if [[ -d "$_netpath" && ! -L "$_netpath" ]] ; then
    #echo path 32 $_netpath
	for _fr in `ls -dr $_netpath/*/` ; do find_framework $(echo $_fr | sed 's#.*/Framework/\(.*\)/$# \1#') ; done
  fi
  if ! [[ "$_done" ]] ; then not_found ; fi
}

function run2 {
  # (2) run ordered by framework targets
  #   adv: explicit ordering
  #   dis: maintain complex framework versions
  #
  # find framework target, verify CLR files, run framework version
  #
  # four key CLR files (4.0.30319, 2.0.50727, 1.1.4322, 1.0.3705)
  #
  # (1) $_netpath/$_frame40/mscorlib.dll
  _nospace=$(echo $_frame45 | sed 's#[ ]*##g')
  if [[ -a "$_netpath64/$_frame40/mscorlib.dll" ]] ; then find_framework $_nospace ; fi
  if [[ -a "$_netpath/$_frame40/mscorlib.dll" ]] ; then find_framework $_nospace ; fi
  _nospace=$(echo $_frame40 | sed 's#[ ]*##g')
  if [[ -a "$_netpath64/$_frame40/mscorlib.dll" ]] ; then find_framework $_nospace ; fi
  if [[ -a "$_netpath/$_frame40/mscorlib.dll" ]] ; then find_framework $_nospace ; fi
  #
  # (2) $_netpath/$_frame20/mscorlib.dll
  _nospace=$(echo $_frame35 | sed 's#[ ]*##g')
  if [[ -a "$_netpath64/$_frame20/mscorlib.dll" ]] ; then find_framework $_nospace ; fi
  if [[ -a "$_netpath/$_frame20/mscorlib.dll" ]] ; then find_framework $_nospace ; fi
  _nospace=$(echo $_frame30 | sed 's#[ ]*##g')
  if [[ -a "$_netpath64/$_frame20/mscorlib.dll" ]] ; then find_framework $_nospace ; fi
  if [[ -a "$_netpath/$_frame20/mscorlib.dll" ]] ; then find_framework $_nospace ; fi
  _nospace=$(echo $_frame20 | sed 's#[ ]*##g')
  if [[ -a "$_netpath64/$_frame20/mscorlib.dll" ]] ; then find_framework $_nospace ; fi
  if [[ -a "$_netpath/$_frame20/mscorlib.dll" ]] ; then find_framework $_nospace ; fi  
  #
  # (3) $_netpath/$_frame11/mscorlib.dll
  _nospace=$(echo $_frame11 | sed 's#[ ]*##g')
  if [[ -a "$_netpath/$_frame11/mscorlib.dll" ]] ; then find_framework $_nospace ; fi
  #
  # (4) $_netpath/$_frame10/mscorlib.dll
  _nospace=$(echo $_frame10 | sed 's#[ ]*##g')
  if [[ -a "$_netpath/$_frame10/mscorlib.dll" ]] ; then find_framework $_nospace ; fi
  #
  if ! [[ "$_done" ]] ; then not_found ; fi
}

function find_framework {
  if [[ "$_done" ]] ; then return ; fi
  _arg=$1
  _nospace=$(echo $_arg | sed 's#[ ]*##g')
  if [[ "$_nospace" == "" ]] ; then return ; fi
  if [[ "$_nospace" == "VJSharp" ]] ; then return ; fi
  if [[ "$_nospace" == "URTInstallPath_GAC" ]] ; then return ; fi
  #
  # find framework version run framework program
  _version=$(echo $_nospace | sed 's#v\([0-9]\).\([0-9]\).*# \1\2#')
  if [[ $_version -eq 10 ]] ; then not_supported v1.0 ; fi
  if [[ $_version -eq 11 ]] ; then not_supported v1.1 ; fi
  if [[ $_version -eq 20 ]] ; then run_program $_version ; fi
  if [[ $_version -eq 30 ]] ; then run_program $_version ; fi
  if [[ $_version -eq 35 ]] ; then run_program $_version ; fi
  if [[ $_version -eq 40 ]] ; then run_program $_version ; fi
  if [[ $_version -eq 45 ]] ; then run_program $_version ; fi
}

function run_program {
  if [[ "$_done" ]] ; then return ; fi
  _arg=$1
  # Runs a .Net program with the same name and version suffix. Convention requires a set of target executables.
  if [[ -a "${_program_dir}/${_program_name}${_arg}${_program_type}" ]] ; then
    # echo ${_program_dir}/${_program_name}${_arg}${_program_type}
	${_program_name}${_arg}${_program_type} ${_arguments}
	_done=true
  fi	
}

function not_supported {
  echo Warning! ${_program_name}\?\?${_program_type} on .Net $1 is not supported
}

function not_found {
  echo Error! Any ${_program_name}\?\?${_program_type} not found \(with \?\? framework version\).
}

# manually set run1 or run2 method
run2

# done
