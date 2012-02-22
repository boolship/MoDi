// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DosMode.cs" company="boolship">
//   Copyright (c) 2012 boolship@gmail.com. All rights reserved.
// </copyright>
// <summary>
//   Defines the DosMode type.
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
	using System;
	using System.Collections.Generic;
	using System.Collections.Specialized;
	using System.ComponentModel;
	using System.IO;
	using System.Runtime.InteropServices;

	using Boolship.Console.Domain;

	/// <summary>
	/// DOS Mode console options
	/// </summary>
	public partial class DosMode : Mode
	{
		#region Dos Mode fields

		/// <summary>
		/// To disable QuickEdit or Insert mode, use ENABLE_EXTENDED_FLAGS without other flag.
		/// </summary>
		private const int EnableExtendedFlags = 0x0080;

		/// <summary>
		/// To enable QuickEdit mode, use ENABLE_QUICK_EDIT_MODE | ENABLE_EXTENDED_FLAGS. 
		/// </summary>
		private const int EnableQuickEditMode = 0x0040;

		/// <summary>
		/// To enable Insert mode, use ENABLE_INSERT_MODE | ENABLE_EXTENDED_FLAGS. 
		/// </summary>
		private const int EnableInsertMode = 0x0020;

		/// <summary>
		/// Command console buffer columns
		/// </summary>
		private int bufferColumns = -1;

		/// <summary>
		/// Command console buffer lines
		/// </summary>
		private int bufferLines = -1;

		/// <summary>
		/// Command console window columns
		/// </summary>
		private int windowColumns = -1;

		/// <summary>
		/// Command console window lines
		/// </summary>
		private int windowLines = -1;

		/// <summary>
		/// Command console insert mode - ModeState values
		/// </summary>
		private int insertMode;
		
		/// <summary>
		/// Command console quick edit - ModeState values
		/// </summary>
		private int quickEdit;

		#endregion
		
		/// <summary>
		/// Gets BufferColumns.
		/// </summary>
		public override int BufferColumns
		{
			get
			{
				try
				{
					// current invalid value Console.[Property] throws ArgumentOutOfRangeException
					this.BufferColumns = Console.BufferWidth;
					return this.bufferColumns;
				}
				catch (ArgumentOutOfRangeException)
				{
					// current Console.[Property] exceeds minimum/maximum win col
					throw new InvalidOperationException();
				}
				catch (IOException)
				{
					// unit test Console.[Property] throws IOException.
					return DosUnitDefaults.BufferWidth;
				}
			}

			set
			{
				try
				{
					var dd = new DosDefaults();
					if (value < dd.WinColMin)
					{
						// minimum buff col is minimum win col
						this.bufferColumns = -2;
						throw new ArgumentOutOfRangeException("value", value, "err: minimum buf col " + dd.WinColMin);
					}

					if (value > dd.BufColMax)
					{
						this.bufferColumns = -3;
						throw new ArgumentOutOfRangeException("value", value, "err: maximum buf col " + dd.BufColMax);
					}
					
					if (value < this.WindowColumns)
					{
						// minimum buf col equals win col, e.g. no horizontal scroll bar
						this.bufferColumns = -4;
						throw new ArgumentOutOfRangeException("value", value, "err: minimum buf col equal to win col " + this.WindowColumns);
					}

					// set valid value, or getter sets value of Console.[Property]
					this.bufferColumns = Console.BufferWidth = value;
				}
				catch (IOException)
				{
					// unit test Console.[Property] throws IOException
				}
			}
		}

		/// <summary>
		/// Gets BufferLines.
		/// </summary>
		public override int BufferLines
		{
			get
			{
				try
				{
					// current invalid value Console.[Property] throws ArgumentOutOfRangeException
					this.BufferLines = Console.BufferHeight;
					return this.bufferLines;
				}
				catch (ArgumentOutOfRangeException)
				{
					// current Console.[Property] exceeds minimum/maximum win col
					throw new InvalidOperationException();
				}
				catch (IOException)
				{
					// unit test Console.[Property] throws IOException.
					return DosUnitDefaults.BufferHeight;
				}
			}

			set
			{
				try
				{
					var dd = new DosDefaults();
					if (value < dd.WinLinMin)
					{
						// minimum buff lin is minimum win lin, e.g. no vertical scroll bar
						this.bufferLines = -2;
						throw new ArgumentOutOfRangeException("value", value, "err: minimum buf lin " + dd.WinLinMin);
					}

					if (value > dd.BufLinMax)
					{
						this.bufferLines = -3;
						throw new ArgumentOutOfRangeException("value", value, "err: maximum buf lin " + dd.BufLinMax);
					}
					
					if (value < this.WindowLines)
					{
						// minimum buf lin equals win lin, e.g. no vertical scroll bar
						this.bufferLines = -4;
						throw new ArgumentOutOfRangeException("value", value, "err: minimum buf lin equal to win lin " + this.WindowLines);
					}

					// #WARNING# Only works if Console.Clear() called first.
					// set valid value, or getter sets value of Console.[Property]
					this.bufferLines = Console.BufferHeight = value;
				}
				catch (IOException)
				{
					// unit test Console.[Property] throws IOException
				}
			}
		}

		/// <summary>
		/// Gets WindowColumns.
		/// </summary>
		public override int WindowColumns
		{
			get
			{
				try
				{
					// current invalid value Console.[Property] throws ArgumentOutOfRangeException
					this.WindowColumns = Console.WindowWidth;
					return this.windowColumns;
				}
				catch (ArgumentOutOfRangeException)
				{
					// current Console.[Property] exceeds minimum/maximum win col
					throw new InvalidOperationException();
				}
				catch (IOException)
				{
					// unit test Console.[Property] throws IOException.
					return DosUnitDefaults.WindowWidth;
				}
			}

			set
			{
				try
				{
					var dd = new DosDefaults();
					if (value < dd.WinColMin)
					{
						this.windowColumns = -2;
						throw new ArgumentOutOfRangeException("value", value, "err: minimum win col " + dd.WinColMin);
					}

					if (value > dd.WinColMax)
					{
						this.windowColumns = -3;
						throw new ArgumentOutOfRangeException("value", value, "err: maximum win col " + dd.WinColMax);
					}

					// set valid value, or getter sets value of Console.[Property]
					this.windowColumns = Console.WindowWidth = value;
				}
				catch (IOException)
				{
					// unit test Console.[Property] throws IOException
				}
			}
		}

		/// <summary>
		/// Gets WindowLines.
		/// </summary>
		public override int WindowLines
		{
			get
			{
				try
				{
					// current invalid value Console.[Property] throws ArgumentOutOfRangeException
					this.WindowLines = Console.WindowHeight;
					return this.windowLines;
				}
				catch (ArgumentOutOfRangeException)
				{
					// current Console.[Property] exceeds minimum/maximum win lin
					throw new InvalidOperationException();
				}
				catch (IOException)
				{
					// unit test Console.[Property] throws IOException.
					return DosUnitDefaults.WindowHeight;
				}
			}

			set
			{
				try
				{
					var dd = new DosDefaults();
					if (value < dd.WinLinMin)
					{
						this.windowLines = -2;
						throw new ArgumentOutOfRangeException("value", value, "err: minimum win lin " + dd.WinLinMin);
					}

					if (value > dd.WinLinMax)
					{
						this.windowLines = -3;
						throw new ArgumentOutOfRangeException("value", value, "err: maximum win lin " + dd.WinLinMax);
					}

					// set valid value, or getter sets value of Console.[Property]
					this.windowLines = Console.WindowHeight = value;
				}
				catch (IOException)
				{
					// unit test Console.[Property] throws IOException
				}
			}
		}

		/// <summary>
		/// Gets or sets InsertMode.
		/// </summary>
		public override int InsertMode
		{
			get
			{
				this.InsertMode = (int)this.IsSet(EnableInsertMode);
				return this.insertMode;
			}

			set
			{
				try
				{
					if (value != (int)ModeFlag.States.Unknown)
					{
						const int StdInputHandle = -10;
						var handleConsole = GetStdHandle(StdInputHandle);
						if (String.Equals(handleConsole.ToString(), "0"))
						{
							// handleConsole not working in unit test
							const string Msg = "warning: GetStdHandle undefined ({0})";
							throw new Win32Exception(String.Format(Msg, handleConsole));
						}

						// writer not reader, Git Bash hack setup
						int currentMode;
						this.FixedGetConsoleMode(handleConsole, out currentMode, true);

						int simple = this.Presets.Contains(value) ? (int)ModeFlag.States.True : value;
						ModeFlag.States preValue = this.IsSet(EnableInsertMode);
						if (simple == (int)ModeFlag.States.True && preValue != ModeFlag.States.True)
						{
							int result = SetConsoleMode(handleConsole, currentMode | EnableInsertMode | EnableExtendedFlags);
							if (result == 0)
							{
								const string Msg = "warning: SetConsoleMode error ({0},{1})";
								throw new Win32Exception(Marshal.GetLastWin32Error(), string.Format(Msg, true, result));
							}
						}
						else if (simple == (int)ModeFlag.States.False && preValue != ModeFlag.States.False)
						{
							// EnableInsertMode (32)
							int currentModeOff = (currentMode | EnableInsertMode) == currentMode
							                     	? currentMode - EnableInsertMode
							                     	: currentMode;
							int result = SetConsoleMode(handleConsole, currentModeOff | EnableExtendedFlags);
							if (result == 0)
							{
								const string Msg = "warning: SetConsoleMode error ({0},{1})";
								throw new Win32Exception(Marshal.GetLastWin32Error(), string.Format(Msg, false, result));
							}
						}

						// set valid value, or getter sets value of IsSet([mode])
						this.insertMode = (int)this.IsSet(EnableInsertMode);
					}
				}
				catch (Win32Exception w)
				{
					// more info using: w.StackTrace, Exception e = w.GetBaseException();
					TextWriter ew = Console.Error;
					ew.WriteLine("win32 " + w.Message);
				}
			}
		}

		/// <summary>
		/// Gets or sets QuickEdit.
		/// </summary>
		public override int QuickEdit
		{
			get
			{
				this.QuickEdit = (int)this.IsSet(EnableQuickEditMode);
				return this.quickEdit;
			}

			set
			{
				try
				{
					if (value != (int)ModeFlag.States.Unknown)
					{
						const int StdInputHandle = -10;
						var handleConsole = GetStdHandle(StdInputHandle);
						if (String.Equals(handleConsole.ToString(), "0"))
						{
							// handleConsole not working in unit test
							const string Msg = "warning: GetStdHandle undefined ({0})";
							throw new Win32Exception(String.Format(Msg, handleConsole));
						}

						// writer not reader, Git Bash hack setup
						int currentMode;
						this.FixedGetConsoleMode(handleConsole, out currentMode, true);

						int simple = this.Presets.Contains(value) ? (int)ModeFlag.States.True : value;
						ModeFlag.States preValue = this.IsSet(EnableQuickEditMode);
						if (simple == (int)ModeFlag.States.True && preValue != ModeFlag.States.True)
						{
							int result = SetConsoleMode(handleConsole, currentMode | EnableQuickEditMode | EnableExtendedFlags);
							if (result == 0)
							{
								const string Msg = "warning: SetConsoleMode error ({0},{1})";
								throw new Win32Exception(Marshal.GetLastWin32Error(), string.Format(Msg, true, result));
							}
						}
						else if (simple == (int)ModeFlag.States.False && preValue != ModeFlag.States.False)
						{
							// EnableQuickEditMode (64)
							int currentModeOff = (currentMode | EnableQuickEditMode) == currentMode
							                     	? currentMode - EnableQuickEditMode
							                     	: currentMode;
							int result = SetConsoleMode(handleConsole, currentModeOff | EnableExtendedFlags);
							if (result == 0)
							{
								const string Msg = "warning: SetConsoleMode error ({0},{1})";
								throw new Win32Exception(Marshal.GetLastWin32Error(), string.Format(Msg, false, result));
							}
						}

						// set valid value, or getter sets value of IsSet([mode])
						this.quickEdit = (int)this.IsSet(EnableQuickEditMode);
					}
				}
				catch (Win32Exception w)
				{
					// more info using: w.StackTrace, Exception e = w.GetBaseException();
					TextWriter ew = Console.Error;
					ew.WriteLine("win32 " + w.Message);
				}
			}
		}

		/// <summary>
		/// Gets Presets.
		/// </summary>
		public override List<int> Presets
		{
			get
			{
				return new List<int> { 0, 1, 2, 3 };
			}
		}

		/// <summary>
		/// Assemble all prepared settings
		/// </summary>
		/// <returns>
		/// Collection of all prepared settings.
		/// </returns>
		public override IDictionary<string, int> GetCurrentSettings()
		{
			return new Dictionary<string, int>
				{
					{ "bc", this.BufferColumns },
					{ "bl", this.BufferLines },
					{ "wc", this.WindowColumns },
					{ "wl", this.WindowLines },
					{ "in", this.InsertMode },
					{ "qe", this.QuickEdit }
				};
		}

		/// <summary>
		/// Assemble detail command settings.
		/// </summary>
		/// <param name="option">
		/// The option.
		/// </param>
		/// <param name="value">
		/// The value.
		/// </param>
		/// <returns>
		/// One detail command option value pair.
		/// </returns>
		public override IDictionary<string, int> GetDetailCommandSetting(string option, string value)
		{
			int trueFalse;
			bool optionTrueFalse = this.OptionParse(value, out trueFalse);
			int optionValue;
			bool optionInt = int.TryParse(value, out optionValue);

			// (1) Detail command set one option using preset value, e.g. WL= | BL= | WC= | BC= | QE= | IN= with  0|1|2|3
			// (2) Detail command set one option using specific value, e.g. WL= | BL= | WC= | BC= | QE= | IN=
			if (optionInt || optionTrueFalse)
			{
				if (optionInt && this.Presets.Contains(optionValue))
				{
					// preset value
					var dd = new DosDefaults();
					switch (option.ToLower())
					{
						case "wl=":
							return new Dictionary<string, int> { { "wl", dd.WinLin[optionValue] } };
						case "bl=":
							return new Dictionary<string, int> { { "bl", dd.BufLin[optionValue] } };
						case "wc=":
							return new Dictionary<string, int> { { "wc", dd.WinCol[optionValue] } };
						case "bc=":
							return new Dictionary<string, int> { { "bc", dd.BufCol[optionValue] } };
						case "qe=":
							return new Dictionary<string, int> { { "qe", (int)ModeFlag.States.True } };
						case "in=":
							return new Dictionary<string, int> { { "in", (int)ModeFlag.States.True } };
					}
				}
				else
				{
					if (!optionTrueFalse)
					{
						// specific value
						switch (option.ToLower())
						{
							case "wl=":
								return new Dictionary<string, int> { { "wl", optionValue } };
							case "bl=":
								return new Dictionary<string, int> { { "bl", optionValue } };
							case "wc=":
								return new Dictionary<string, int> { { "wc", optionValue } };
							case "bc=":
								return new Dictionary<string, int> { { "bc", optionValue } };
						}
					}
					else
					{
						// specific value
						switch (option.ToLower())
						{
							case "qe=":
								return new Dictionary<string, int> { { "qe", trueFalse } };
							case "in=":
								return new Dictionary<string, int> { { "in", trueFalse } };
						}
					}
				}
			}

			return new Dictionary<string, int>();
		}

		/// <summary>
		/// Assemble multi command settings.
		/// </summary>
		/// <param name="tag">
		/// The option.
		/// </param>
		/// <param name="value">
		/// The value.
		/// </param>
		/// <returns>
		/// Collection of multi command option value pairs.
		/// </returns>
		public override IDictionary<string, int> GetMultiCommandSettings(string tag, string value)
		{
			// (1) option/value == ?? /{ 0, 1, 2, 3 }
			// (2) option/value == { W= | B= | L= | C= }/{ 0, 1, 2, 3 }
			// (3) option/value == { L= | C= }/{ nn } where (nn > ~25 or ~40)
			int optionValue;
			if (int.TryParse(value, out optionValue))
			{
				if (this.Presets.Contains(optionValue))
				{
					// preset value
					var dd = new DosDefaults();
					switch (tag.ToLower())
					{
						case "w=":
							return new Dictionary<string, int> { { "wl", dd.WinLin[optionValue] }, { "wc", dd.WinCol[optionValue] } };
						case "b=":
							return new Dictionary<string, int> { { "bl", dd.BufLin[optionValue] }, { "bc", dd.BufCol[optionValue] } };
						case "l=":
							return new Dictionary<string, int> { { "wl", dd.WinLin[optionValue] }, { "bl", dd.BufLin[optionValue] } };
						case "c=":
							return new Dictionary<string, int> { { "wc", dd.WinCol[optionValue] }, { "bc", dd.BufCol[optionValue] } };
					}

					if (string.IsNullOrEmpty(tag))
					{
						// preset command
						return this.GetPresetValues(optionValue);
					}
				}
				else
				{
					// specific value
					switch (tag.ToLower())
					{
						case "l=":
							return new Dictionary<string, int> { { "wl", optionValue }, { "bl", optionValue } };
						case "c=":
							return new Dictionary<string, int> { { "wc", optionValue }, { "bc", optionValue } };
					}
				}
			}

			return null;
		}

		/// <summary>
		/// Assemble all preset settings
		/// </summary>
		/// <param name="preset">
		/// The preset.
		/// </param>
		/// <returns>
		/// Collection of all preset settings.
		/// </returns>
		public override IDictionary<string, int> GetPresetValues(int preset)
		{
			if (!this.Presets.Contains(preset))
			{
				throw new ArgumentNullException("preset");
			}

			var dd = new DosDefaults();
			return new Dictionary<string, int>
				{
					{ "bc", dd.BufCol[preset] },
					{ "bl", dd.BufLin[preset] },
					{ "wc", dd.WinCol[preset] },
					{ "wl", dd.WinLin[preset] },
					{ "in", (int)ModeFlag.States.True },
					{ "qe", (int)ModeFlag.States.True }
				};
		}

		/// <summary>
		/// Update any settings from a collection
		/// </summary>
		/// <param name="commands">
		/// The commands.
		/// </param>
		public override void UpdateAnyCurrentSettings(IDictionary<string, int> commands)
		{
			if (commands.ContainsKey("bl") && commands.ContainsKey("wl"))
			{
				if (commands["bl"] < commands["wl"])
				{
					// #1 Rule quickfix: post buf lin < post win lin, so remove buf lin setting gives win lin priority
					commands.Remove("bl");
				}
			}

			if (commands.ContainsKey("bc") && commands.ContainsKey("wc"))
			{
				if (commands["bc"] < commands["wc"])
				{
					// #2 Rule quickfix: post buf col < post win col, so remove buf col setting gives win col priority
					commands.Remove("bc");
				}
			}

			var orderedCommands = new OrderedDictionary();
			if ((commands.ContainsKey("bl") && commands.ContainsKey("wl")) ||
				(commands.ContainsKey("bc") && commands.ContainsKey("wc")))
			{
				// #3 Rule priority: must order win lin before buf lin, e.g. post buf lin < pre win lin
				// #4 Rule priority: must order win col before buf col, e.g. post buf col < pre win col
				var commandPriority = new Dictionary<int, string> { { 0, "wl" }, { 1, "wc" }, { 2, "bl" }, { 3, "bc" } };
				for (int i = 0; i < commandPriority.Count; i++)
				{
					// append ordered dictionary pairs
					// e.g. { 0, "wl", commands["wl"] }, { 1, "bl", commands["bl"] } 
					string cmd = commandPriority[i];
					if (commands.ContainsKey(cmd))
					{
						orderedCommands.Add(cmd, commands[cmd]);
						commands.Remove(cmd);
					}
				}
			}

			if (orderedCommands.Contains("bl") || commands.ContainsKey("bl"))
			{
				// Rule workaround: Console clear before "Console.BufferHeight = value" sets window position = 0.
				this.ConsoleClear();
			}
			
			if (orderedCommands.Count > 0)
			{
				// use OrderedDictionary index order to retrieve keys (map index to key)
				var keys = new string[orderedCommands.Keys.Count];
				orderedCommands.Keys.CopyTo(keys, 0);

				foreach (var key in keys)
				{
					this.UpdateCurrentSettingFor(key, int.Parse(orderedCommands[key].ToString()));
				}
			}

			foreach (var command in commands)
			{
				this.UpdateCurrentSettingFor(command.Key, command.Value);
			}
		}

		/// <summary>
		/// Update one settings for option
		/// </summary>
		/// <param name="command">
		/// The command.
		/// </param>
		/// <param name="value">
		/// The value.
		/// </param>
		public override void UpdateCurrentSettingFor(string command, int value)
		{
			if (command == null)
			{
				return;
			}

			string msg;
			try
			{
				switch (command)
				{
					case "bc":
						this.BufferColumns = value;
						msg = "buf col set {0}";
						Console.WriteLine(string.Format(msg, value));
						break;
					case "bl":
						this.BufferLines = value;
						msg = "buf lin set {0}";
						Console.WriteLine(string.Format(msg, value));
						break;
					case "wc":
						this.WindowColumns = value;
						msg = "win col set {0}";
						Console.WriteLine(string.Format(msg, value));
						break;
					case "wl":
						this.WindowLines = value;
						msg = "win lin set {0}";
						Console.WriteLine(string.Format(msg, value));
						break;
					case "in":
						this.InsertMode = value;
						msg = "insert set {0}";
						Console.WriteLine(string.Format(msg, value == (int)ModeFlag.States.True));
						break;
					case "qe":
						this.QuickEdit = value;
						msg = "quick edit set {0}";
						Console.WriteLine(string.Format(msg, value == (int)ModeFlag.States.True));
						break;
				}
			}
			catch (Exception e)
			{
				msg = e.Message.Split('\r')[0];
				Console.WriteLine(msg);
			}
		}
	}

	/// <summary>
	/// update methods
	/// </summary>
	public partial class DosMode
	{
		/// <summary>
		/// Gets unit test workaround when Console.LargestWindowHeight == 0 (OPTIONAL).
		/// </summary>
		public static int ConsoleLargestWindowHeight
		{
			get
			{
				int def;
				try
				{
					def = Console.LargestWindowHeight > 0 ? Console.LargestWindowHeight : DosUnitDefaults.LargestWindowHeight;
				}
				catch (IOException)
				{
					def = DosUnitDefaults.LargestWindowHeight;
				}

				return def;
			}
		}

		/// <summary>
		/// Gets unit test workaround when Console.LargestWindowWidth == 0 (OPTIONAL).
		/// </summary>
		public static int ConsoleLargestWindowWidth
		{
			get
			{
				int def;
				try
				{
					def = Console.LargestWindowWidth > 0 ? Console.LargestWindowWidth : DosUnitDefaults.LargestWindowWidth;
				}
				catch (IOException)
				{
					def = DosUnitDefaults.LargestWindowWidth;
				}

				return def;
			}
		}

		/// <summary>
		/// Parse True and False to int result.
		/// </summary>
		/// <param name="value">
		/// Parse the strings "true" or "t" and "false" or "f", case insensitive.
		/// </param>
		/// <param name="result">
		/// Set result out equal to 1 if true, or equal to Int16.MaxValue if false.
		/// </param>
		/// <returns>
		/// Parsing "true" or "t" or "false" or "f" returns true, otherwise false.
		/// </returns>
		internal bool OptionParse(string value, out int result)
		{
			if (value.Equals("true", StringComparison.OrdinalIgnoreCase) ||
				value.Equals("t", StringComparison.OrdinalIgnoreCase))
			{
				result = (int)ModeFlag.States.True;
				return true;
			}

			result = (int)ModeFlag.States.False;
			if (value.Equals("false", StringComparison.OrdinalIgnoreCase) ||
				value.Equals("f", StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Console clear before "Console.BufferHeight = value" sets window position = 0.
		/// </summary>
		internal void ConsoleClear()
		{
			try
			{
				// set window position = 0
				Console.Clear();
			}
			catch (IOException)
			{
			}
		}

		/// <summary>
		/// Fixed *in-process* GetConsoleMode, e.g. Git Bash workaround
		/// </summary>
		/// <param name="consoleHandle">
		/// The console handle.
		/// </param>
		/// <param name="currentMode">
		/// The current mode.
		/// </param>
		/// <param name="writer">
		/// The writer.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.
		/// </returns>
		internal int FixedGetConsoleMode(IntPtr consoleHandle, out int currentMode, bool writer)
		{
			int rtn = GetConsoleMode(consoleHandle, out currentMode);
			if (writer && currentMode < EnableExtendedFlags)
			{
				// Git Bash hack: Enable *in-process* flag tracking (works writer only).
				// The first *in-process* execution sets currentMode == EnableExtendedFlags (128).
				// Thereafter, writing currentMode is cumulative using EnableExtendedFlags (128) baseline.
				// Do not use when reading because this will lead to all modes being reset.
				SetConsoleMode(consoleHandle, EnableExtendedFlags);
				rtn = GetConsoleMode(consoleHandle, out currentMode);
			}

			return rtn;
		}

		/// <summary>
		/// DOS kernel code.
		/// </summary>
		/// <param name="stdHandle">
		/// The std handle.
		/// </param>
		/// <returns>
		/// A handle value.
		/// </returns>
		[DllImport("kernel32.dll", SetLastError = true)]
		protected static extern IntPtr GetStdHandle(int stdHandle);

		/// <summary>
		/// DOS kernel code.
		/// </summary>
		/// <param name="consoleHandle">
		/// The console handle.
		/// </param>
		/// <param name="mode">
		/// The mode.
		/// </param>
		/// <returns>
		/// A console mode value.
		/// </returns>
		[DllImport("kernel32.dll", SetLastError = true)]
		protected static extern int GetConsoleMode(IntPtr consoleHandle, out int mode);

		/// <summary>
		/// DOS kernel code.
		/// </summary>
		/// <param name="consoleHandle">
		/// The console handle.
		/// </param>
		/// <param name="mode">
		/// The mode.
		/// </param>
		/// <returns>
		/// A console mode value.
		/// </returns>
		[DllImport("kernel32.dll", SetLastError = true)]
		protected static extern int SetConsoleMode(IntPtr consoleHandle, int mode);
		
		/// <summary>
		/// Check if flag set in DOS Console.
		/// Performs low level calls to kernel32.dll, hoping to get a valid result.
		/// Works great in DOS, but Git Bash is a wild horse. I spent way too much
		/// time testing these methods. Good luck.
		/// </summary>
		/// <param name="value">
		/// The value.
		/// </param>
		/// <returns>
		/// Returns ModeFlag.States status value set either True, False, or Unknown.
		/// </returns>
		/// <exception cref="Win32Exception">
		/// Various Win32Exception caught and handled.
		/// </exception>
		private ModeFlag.States IsSet(int value)
		{
			// initialized Unknown
			ModeFlag.States status = new ModeFlag().Status;
			try
			{
				const int StdInputHandle = -10;
				var handleConsole = GetStdHandle(StdInputHandle);
				if (string.Equals(handleConsole.ToString(), "0"))
				{
					// handleConsole not working in unit test
					const string Msg = "warning: GetStdHandle undefined ({0})";
					throw new Win32Exception(string.Format(Msg, handleConsole));
				}

				// reader not writer
				int currentMode;
				this.FixedGetConsoleMode(handleConsole, out currentMode, false);

				status = currentMode <= EnableExtendedFlags
							? ModeFlag.States.Unknown
							: (currentMode | value) == currentMode ? ModeFlag.States.True : ModeFlag.States.False;
			}
			catch (Win32Exception w)
			{
				// more info using: w.StackTrace, Exception e = w.GetBaseException();
				TextWriter ew = Console.Error;
				ew.WriteLine("win32 " + w.Message);
			}

			return status;
		}
	}
}
