// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DosDefaults.cs" company="boolship">
//   Copyright (c) 2012 boolship@gmail.com. All rights reserved.
// </copyright>
// <summary>
//   default console settings
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

namespace Boolship.Console.Dos
{
	using Boolship.Console.Domain;

	/// <summary>
	/// Default DOS console settings.
	/// </summary>
	public class DosDefaults : Defaults
	{
		#region DOS Default FIELDS

		/// <summary>
		/// buffer column defaults, low to high
		/// </summary>
		private readonly int[] bufCol;

		/// <summary>
		/// buffer line defaults, low to high
		/// </summary>
		private readonly int[] bufLin;

		/// <summary>
		/// window column defaults, low to high
		/// </summary>
		private readonly int[] winCol;

		/// <summary>
		/// window line defaults, low to high
		/// </summary>
		private readonly int[] winLin;

		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="DosDefaults"/> class.
		/// </summary>
		public DosDefaults()
		{
			this.winLin = new[]
				{
					(DosMode.ConsoleLargestWindowHeight / 3) - 4, (DosMode.ConsoleLargestWindowHeight / 2) - 4,
					(DosMode.ConsoleLargestWindowHeight * 2 / 3) - 4, DosMode.ConsoleLargestWindowHeight - 4
				};
			this.winCol = new[]
				{
					(DosMode.ConsoleLargestWindowWidth / 3) - 5, (DosMode.ConsoleLargestWindowWidth / 2) - 5,
					(DosMode.ConsoleLargestWindowWidth * 2 / 3) - 5, DosMode.ConsoleLargestWindowWidth - 5
				};
			this.bufLin = new[] { 600, 2400, 10000, short.MaxValue - 1 };
			this.bufCol = new[]
				{
					(DosMode.ConsoleLargestWindowWidth * 2 / 3) - 5, DosMode.ConsoleLargestWindowWidth - 5,
					DosMode.ConsoleLargestWindowWidth * 4 / 3, short.MaxValue - 1
				};
		}

		/// <summary>
		/// Gets WinLin.
		/// </summary>
		public override int[] WinLin
		{
			get
			{
				return this.winLin;
			}
		}

		/// <summary>
		/// Gets WinCol.
		/// </summary>
		public override int[] WinCol
		{
			get
			{
				return this.winCol;
			}
		}

		/// <summary>
		/// Gets BufLin.
		/// </summary>
		public override int[] BufLin
		{
			get
			{
				return this.bufLin;
			}
		}

		/// <summary>
		/// Gets BufCol.
		/// </summary>
		public override int[] BufCol
		{
			get
			{
				return this.bufCol;
			}
		}

		/// <summary>
		/// Gets WinLinMin.
		/// </summary>
		public int WinLinMin
		{
			get
			{
				return this.WinLin[0];
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
		/// Gets WinColMin.
		/// </summary>
		public int WinColMin
		{
			get
			{
				return this.WinCol[0];
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

		/// <summary>
		/// Gets BufLinMin.
		/// </summary>
		public int BufLinMin
		{
			get
			{
				return this.BufLin[0];
			}
		}

		/// <summary>
		/// Gets BufLinMax.
		/// </summary>
		public int BufLinMax
		{
			get
			{
				return this.BufLin[this.BufLin.Length - 1];
			}
		}

		/// <summary>
		/// Gets BufColMin.
		/// </summary>
		public int BufColMin
		{
			get
			{
				return this.BufCol[0];
			}
		}

		/// <summary>
		/// Gets BufColMax.
		/// </summary>
		public int BufColMax
		{
			get
			{
				return this.BufCol[this.BufCol.Length - 1];
			}
		}
	}
}
