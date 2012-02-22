// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShowCommand.cs" company="boolship">
//   Copyright (c) 2012 boolship@gmail.com. All rights reserved.
// </copyright>
// <summary>
//   Defines the ShowCommand type.
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

	using Boolship.Console.Domain;

	/// <summary>
	/// Show current window and buffer settings.
	/// </summary>
	public class ShowCommand : ICommand
	{
		/// <summary>
		/// Abstract mode field.
		/// </summary>
		private readonly Mode mode;

		/// <summary>
		/// Console status message with format arguments 0-5
		/// </summary>
		private const string Status =
			"Console Status:" + "\n---------------" + "\n\tWindow Size        = Lines: {0,4}, Columns: {1,4}"
			+ "\n\tScreen Buffer Size = Lines: {2,4}, Columns: {3,4}" + "\n" + "\nEdit Options:" + "\n-------------"
			+ "\n\tQuickEdit Mode: {4,3}, Insert Mode: {5,3}";

		/// <summary>
		/// Initializes a new instance of the <see cref="ShowCommand"/> class.
		/// </summary>
		/// <param name="mode">
		/// The mode.
		/// </param>
		public ShowCommand(Mode mode)
		{
			this.mode = mode;
		}

		#region ICommand Members

		/// <summary>
		/// Write STATUS message to console.
		/// </summary>
		public void Execute()
		{
			var setting = this.mode.GetCurrentSettings();
			Console.WriteLine(
				string.Format(
					Status,
					setting["wl"],
					setting["wc"],
					setting["bl"],
					setting["bc"],
					new ModeFlag(setting["qe"]),
					new ModeFlag(setting["in"])));
		}

		#endregion
	}
}
