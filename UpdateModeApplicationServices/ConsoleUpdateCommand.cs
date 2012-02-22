// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleUpdateCommand.cs" company="boolship">
//   Copyright (c) 2012 boolship@gmail.com. All rights reserved.
// </copyright>
// <summary>
//   Defines the ConsoleUpdateCommand type.
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

namespace Boolship.Console.UpdateMode.ApplicationServices
{
	using System;
	using System.Collections.Generic;

	using Domain;

	/// <summary>
	/// Command to update console.
	/// </summary>
	public class ConsoleUpdateCommand : ICommand
	{
		/// <summary>
		/// Abstract mode field.
		/// </summary>
		private readonly Mode mode;

		/// <summary>
		/// Command collection.
		/// </summary>
		private readonly IDictionary<string, int> commands;

		/// <summary>
		/// Initializes a new instance of the <see cref="ConsoleUpdateCommand"/> class.
		/// </summary>
		/// <param name="mode">
		/// The mode.
		/// </param>
		/// <param name="commands">
		/// The commands.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// Guard clauses for mode, commands.
		/// </exception>
		public ConsoleUpdateCommand(Mode mode, IDictionary<string, int> commands)
		{
			if (mode == null)
			{
				throw new ArgumentNullException("mode");
			}

			if (commands == null)
			{
				throw new ArgumentNullException("commands");
			}

			this.mode = mode;
			this.commands = commands;
		}

		/// <summary>
		/// Gets Mode.
		/// </summary>
		public Mode Mode
		{
			get { return this.mode; }
		}

		/// <summary>
		/// Gets Commands.
		/// </summary>
		public IDictionary<string, int> Commands
		{
			get { return this.commands; }
		}

		#region ICommand Members

		/// <summary>
		/// Command execution.
		/// </summary>
		public void Execute()
		{
			this.Mode.UpdateAnyCurrentSettings(this.commands);
			////Console.WriteLine("Updated console.");
		}

		#endregion
	}
}
