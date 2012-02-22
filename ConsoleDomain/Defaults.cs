// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Defaults.cs" company="boolship">
//   Copyright (c) 2012 boolship@gmail.com. All rights reserved.
// </copyright>
// <summary>
//   Defines the Defaults type.
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
	/// <summary>
	/// Defaults for console, used for retrieval.
	/// </summary>
	public abstract class Defaults
	{
		/// <summary>
		/// Gets BufCol.
		/// </summary>
		public abstract int[] BufCol { get; }

		/// <summary>
		/// Gets BufLin.
		/// </summary>
		public abstract int[] BufLin { get; }

		/// <summary>
		/// Gets WinCol.
		/// </summary>
		public abstract int[] WinCol { get; }

		/// <summary>
		/// Gets WinLin.
		/// </summary>
		public abstract int[] WinLin { get; }
	}
}
