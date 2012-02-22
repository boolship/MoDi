// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DosUnitDefaults.cs" company="boolship">
//   Copyright (c) 2012 boolship@gmail.com. All rights reserved.
// </copyright>
// <summary>
//   Defines the DosUnitDefaults type.
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
//   along with this program.  If not, see http://www.gnu.org/licenses/.//
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Boolship.Console.Dos
{
	using System.Collections.Generic;

	using Boolship.Console.Domain;

	/// <summary>
	/// Default DOS Unit Test values.
	/// </summary>
	public partial class DosUnitDefaults : Defaults
	{
		/// <summary>
		/// Unit test default settings.
		/// </summary>
		private static readonly Dictionary<string, int> UnitTestValues = new Dictionary<string, int>
			{
				{ "WindowHeight", 50 },
				{ "LargestWindowHeight", 75 },
				{ "WindowWidth", 100 },
				{ "LargestWindowWidth", 240 },
				{ "BufferHeight", 500 },
				{ "BufferWidth", 100 }
			};

		/// <summary>
		/// Gets WinLin.
		/// </summary>
		public override int[] WinLin
		{
			get
			{
				return new[] { UnitTestValues["WindowHeight"], UnitTestValues["LargestWindowHeight"] };
			}
		}

		/// <summary>
		/// Gets WinCol.
		/// </summary>
		public override int[] WinCol
		{
			get
			{
				return new[] { UnitTestValues["WindowWidth"], UnitTestValues["LargestWindowWidth"] };
			}
		}

		/// <summary>
		/// Gets BufLin.
		/// </summary>
		public override int[] BufLin
		{
			get
			{
				return new[] { UnitTestValues["BufferHeight"] };
			}
		}

		/// <summary>
		/// Gets BufCol.
		/// </summary>
		public override int[] BufCol
		{
			get
			{
				return new[] { UnitTestValues["BufferWidth"] };
			}
		}

		/// <summary>
		/// Gets WinLinMax.
		/// </summary>
		public int WinLinMax
		{
			get
			{
				return this.WinLin[this.WinLin.Length - 1];
			}
		}

		/// <summary>
		/// Gets WinColMax.
		/// </summary>
		public int WinColMax
		{
			get
			{
				return this.WinCol[this.WinCol.Length - 1];
			}
		}
	}

	/// <summary>
	/// Unit test values when Console.[Property] throws IOException
	/// </summary>
	public partial class DosUnitDefaults
	{
		/// <summary>
		/// Gets default WindowHeight, unit test Console.[Property] throws IOException.
		/// </summary>
		public static int WindowHeight
		{
			get
			{
				return UnitTestValues["WindowHeight"];
			}
		}

		/// <summary>
		/// Gets LargestWindowHeight, unit test workaround when Console.LargestWindowHeight == 0.
		/// </summary>
		public static int LargestWindowHeight
		{
			get
			{
				return UnitTestValues["LargestWindowHeight"];
			}
		}

		/// <summary>
		/// Gets default WindowWidth, unit test Console.[Property] throws IOException.
		/// </summary>
		public static int WindowWidth
		{
			get
			{
				return UnitTestValues["WindowWidth"];
			}
		}

		/// <summary>
		/// Gets LargestWindowWidth, unit test workaround when Console.LargestWindowWidth == 0.
		/// </summary>
		public static int LargestWindowWidth
		{
			get
			{
				return UnitTestValues["LargestWindowWidth"];
			}
		}

		/// <summary>
		/// Gets default BufferHeight, unit test Console.[Property] throws IOException.
		/// </summary>
		public static int BufferHeight
		{
			get
			{
				return UnitTestValues["BufferHeight"];
			}
		}

		/// <summary>
		/// Gets default BufferWidth, unit test Console.[Property] throws IOException.
		/// </summary>
		public static int BufferWidth
		{
			get
			{
				return UnitTestValues["BufferWidth"];
			}
		}
	}
}
