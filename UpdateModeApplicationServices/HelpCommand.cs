// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HelpCommand.cs" company="boolship">
//   Copyright (c) 2012 boolship@gmail.com. All rights reserved.
// </copyright>
// <summary>
//   Defines the HelpCommand type.
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
	/// Help with preset options.
	/// </summary>
	public class HelpCommand : ICommand
	{
		/// <summary>
		/// Abstract mode field.
		/// </summary>
		private readonly Defaults defaults;

		/// <summary>
		/// Program help message with format arguments 0-18
		/// </summary>
		private const string Usage =
			"Set Console Window & Buffer Sizes, QuickEdit, other options from command line." + "\n"
			+ "\n  {0} [0|1|2|3] [[WL|BL|WC|BC|QE|IN]=[0|1|2|3]] [[W|B|L|C]=[0|1|2|3]]"
			+ "\n\t[[L|C]=nn] [[WL|BL|WC|BC]=nn] [[QE|IN]=[t|true|f|false]]" + "\n"
			+ "\n  {0}       No arguement will display Console status."
			+ "\n  {0} 0     Small  window ~1/3 screen. W: {1,3}/{5,-3} B: {9,3}/{13,-3} col/lin"
			+ "\n  {0} 1     Medium window ~1/2 screen. W: {2,3}/{6,-3} B: {10,3}/{14,-3} col/lin"
			+ "\n  {0} 2     Large  window ~2/3 screen. W: {3,3}/{7,-3} B: {11,3}/{15,-3} col/lin"
			+ "\n  {0} 3     Max    window ~max screen. W: {4,3}/{8,-3} B: {12,3}/{16,-3} col/lin"
			+ "\n         Preset COMMAND of 0|1|2|3 also sets QE={17}, IN={18}." + "\n"
			+ "\n  {0} WL=n  Window Lines=n, where n is 0|1|2|3"
			+ "\n  {0} BL=n  Buffer Lines=n, where n is 0|1|2|3"
			+ "\n  {0} WC=n  Window Columns=n, where n is 0|1|2|3"
			+ "\n  {0} BC=n  Buffer Columns=n, where n is 0|1|2|3"
			+ "\n  {0} QE=n  QuickEdit Mode=n, where n is 0|1|2|3"
			+ "\n  {0} IN=n  Insert Mode=n, where n is 0|1|2|3"
			+ "\n         Preset VALUE n of 0|1|2|3 sets one COMMAND option." + "\n"
			+ "\n  {0} W=n   Window Lines=n and Columns=n, where n is 0|1|2|3"
			+ "\n  {0} B=n   Buffer Lines=n and Columns=n, where n is 0|1|2|3"
			+ "\n  {0} L=n   Window and Buffer Lines=n, where n is 0|1|2|3"
			+ "\n  {0} C=n   Window and Buffer Columns=n, where n is 0|1|2|3"
			+ "\n         Preset VALUE n of 0|1|2|3 sets two COMMAND options." + "\n"
			+ "\n  {0} L=nn  Window and Buffer Lines=nn, where nn is specified value"
			+ "\n  {0} C=nn  Window and Buffer Columns=nn, where nn is specified value"
			+ "\n         Matching Lines or Columns sets no scroll bar" + "\n"
			+ "\n  {0} WL=nn  Window Lines=nn, where nn is specified value"
			+ "\n  {0} BL=nn  Buffer Lines=nn, where nn is specified value"
			+ "\n  {0} WC=nn  Window Columns=nn, where nn is specified value"
			+ "\n  {0} BC=nn  Buffer Columns=nn, where nn is specified value"
			+ "\n          Specified VALUE nn sets option to specified value" + "\n"
			+ "\n  {0} QE=t|f  QuickEdit Mode=t or true, or f or false"
			+ "\n  {0} IN=t|f  Insert Mode=t or true, or f or false"
			+ "\n          Specified VALUE t or f sets option to specified value" + "\n"
			+ "\nCommand line (CLI) arguments are processed in order. This allows general"
			+ "\noptions, such as presets, to be overridden with subsequent details."
			+ "\nOverride QE and IN with preset COMMAND 0|1|2|3 using QE=false, IN=false."
			+ "\nThe actual buffer maximum size might be limited to less than 10000."
			+ "\nPreset sizes vary dynamically based on screen resolution." + "\n"
			+ "\nSimple usage examples include: \"mo\", \"mo 1\", \"mo 3 b=2\","
			+ "\n\"mo 1 b=2\", \"mo 2 qe=f in=f\", \"mo 1 c=100\", \"mo 1 c=100 l=40\","
			+ "\n\"mo 1 l=3\", \"mo 1 c=3\"";

		/// <summary>
		/// command line name
		/// </summary>
		private const string CliName = "mo";

		/// <summary>
		/// Initializes a new instance of the <see cref="HelpCommand"/> class.
		/// </summary>
		/// <param name="defaults">
		/// The defaults.
		/// </param>
		public HelpCommand(Defaults defaults)
		{
			if (defaults == null)
			{
				throw new ArgumentNullException("defaults");
			}

			this.defaults = defaults;
		}

		/// <summary>
		/// Gets Defaults.
		/// </summary>
		public Defaults Defaults
		{
			get { return this.defaults; }
		}

		#region ICommand Members

		/// <summary>
		/// Write USAGE message to console.
		/// </summary>
		public void Execute()
		{
			Console.WriteLine(
				string.Format(
					Usage,
					CliName,
					Defaults.WinCol[0],
					Defaults.WinCol[1],
					Defaults.WinCol[2],
					Defaults.WinCol[3],
					Defaults.WinLin[0],
					Defaults.WinLin[1],
					Defaults.WinLin[2],
					Defaults.WinLin[3],
					Defaults.BufCol[0],
					Defaults.BufCol[1],
					Defaults.BufCol[2],
					Defaults.BufCol[3],
					Defaults.BufLin[0],
					Defaults.BufLin[1],
					Defaults.BufLin[2],
					Defaults.BufLin[3],
					true,
					true));
		}

		#endregion
	}
}
