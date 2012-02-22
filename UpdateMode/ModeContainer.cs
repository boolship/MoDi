// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModeContainer.cs" company="boolship">
//   Copyright (c) 2012 boolship@gmail.com. All rights reserved.
// </copyright>
// <summary>
//   Defines the ModeContainer type.
//   .
//   Copyright (c) 2012 boolship@gmail.com. All rights reserved.
//   .
//   This program is free software: you can redistribute it and/or modify
//   it under the terms of the GNU General Public License as published by
//   the Free Software Foundation, either version 3 of the License, or
//   (at your option) any later version.
//   .
//   This program is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//   GNU General Public License for more details.
//   You should have received a copy of the GNU General Public License
//   along with this program.  If not, see http://www.gnu.org/licenses/.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Boolship.Console.UpdateMode.CommandLine
{
	using ApplicationServices;

	using Domain;

	using Dos;

	/// <summary>
	/// Custom container with hard-wired dependency.
	/// </summary>
	public class ModeContainer
	{
		/// <summary>
		/// Custom container hard-wired for DOS mode provider.
		/// </summary>
		/// <returns>
		/// Parser for command-line args for DOS mode provider.
		/// </returns>
		public ModeParser ResolveModeParser()
		{
			ModeProvider provider = new DosModeProvider();
			DefaultsProvider defaults = new DosDefaultsProvider();
			return new ModeParser(provider, defaults);
		}
	}
}
