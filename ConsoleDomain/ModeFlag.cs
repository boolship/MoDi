// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModeFlag.cs" company="boolship">
//   Copyright (c) 2012 boolship@gmail.com. All rights reserved.
// </copyright>
// <summary>
//   Defines the ModeState type.
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

namespace Boolship.Console.Domain
{
	using System;

	/// <summary>
	/// Mode state for multi-value True/False/Unknown options
	/// </summary>
	public class ModeFlag
	{
		/// <summary>
		/// Mode state flag status.
		/// </summary>
		private States states = States.Unknown;

		/// <summary>
		/// Initializes a new instance of the <see cref="ModeFlag"/> class.
		/// </summary>
		public ModeFlag()
		{
			this.Status = States.Unknown;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ModeFlag"/> class.
		/// </summary>
		/// <param name="state">
		/// The state.
		/// </param>
		public ModeFlag(int state)
		{
			switch (state)
			{
				case (int)States.True:
					this.Status = States.True;
					break;
				case (int)States.False:
					this.Status = States.False;
					break;
				default:
					this.Status = States.Unknown;
					break;
			}
		}

		/// <summary>
		/// Multi value mode states
		/// </summary>
		public enum States
		{
			/// <summary>
			/// True value must be a Mode Presets value
			/// </summary>
			True = 1,

			/// <summary>
			/// mode set false
			/// </summary>
			False = Int32.MaxValue,

			/// <summary>
			/// mode setting unknown
			/// </summary>
			Unknown = -1
		}

		/// <summary>
		/// Gets or sets Status.
		/// </summary>
		public States Status
		{
			get
			{
				return this.states;
			}

			set
			{
				this.states = value;
			}
		}

		/// <summary>
		/// State text value.
		/// </summary>
		/// <returns>
		/// String with valid value or empty string
		/// </returns>
		public override string ToString()
		{
			return this.Status == States.True ? "True" : this.Status == States.False ? "False" : this.Status == States.Unknown ? "Unknown" : string.Empty;
		}
	}
}
