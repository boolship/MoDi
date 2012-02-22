// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompositionRoot.cs" company="boolship">
//   Copyright (c) 2012 boolship@gmail.com. All rights reserved.
// </copyright>
// <summary>
//   Defines the CompositionRoot type.
//   Extended MOde (mo) console options. Program name "mo" pronounced "moe" as in "Moe Szyslak".
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
	/// <summary>
	/// Command line program and composition root.
	/// </summary>
	public class CompositionRoot
	{
		/// <summary>
		/// Main method sets up container and resolves the type.
		/// </summary>
		/// <param name="args">
		/// The program args.
		/// </param>
		public static void Main(string[] args)
		{
			var container = new ModeContainer();
			container.ResolveModeParser()
				.Parse(args)
				.Execute();
		}
	}
}
