// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModeParser.cs" company="boolship">
//   Copyright (c) 2012 boolship@gmail.com. All rights reserved.
// </copyright>
// <summary>
//   Defines the ModeParser type.
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
	/// Configures console by parsing options.
	/// </summary>
	public class ModeParser
	{
		/// <summary>
		/// Abstract mode provider.
		/// </summary>
		private readonly ModeProvider modeProvider;

		/// <summary>
		/// Abstract defaults provider.
		/// </summary>
		private readonly DefaultsProvider defaultsProvider;

		/// <summary>
		/// Possible multi-command, e.g. W= B= L= C= with 0|1|2|3, or L= | C= with {nn}
		/// </summary>
		private readonly List<string> multiCommand = new List<string> { "w=", "b=", "l=", "c=" };

		/// <summary>
		/// Possible detail-command, e.g. WL= | BL= | WC= | BC= | QE= | IN= with  0|1|2|3 or {nn}
		/// </summary>
		private readonly List<string> detailCommand = new List<string> { "wc=", "bc=", "wl=", "bl=", "qe=", "in=" };

		/// <summary>
		/// Initializes a new instance of the <see cref="ModeParser"/> class.
		/// </summary>
		/// <param name="modeProvider">
		/// The mode provider.
		/// </param>
		/// <param name="defaultsProvider">
		/// The defaults Provider.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// Guard clause for argument. 
		/// </exception>
		public ModeParser(ModeProvider modeProvider, DefaultsProvider defaultsProvider)
		{
			if (modeProvider == null)
			{
				throw new ArgumentNullException("modeProvider");
			}

			if (defaultsProvider == null)
			{
				throw new ArgumentNullException("defaultsProvider");
			}

			this.modeProvider = modeProvider;
			this.defaultsProvider = defaultsProvider;
		}

		/// <summary>
		/// Simple parse command-line (cli) arguments.
		/// </summary>
		/// <param name="args">
		/// The args.
		/// </param>
		/// <returns>
		/// ConsoleUpdateCommand class with commands.
		/// </returns>
		public ICommand Parse(string[] args)
		{
			var mode = this.modeProvider.GetMode();
			var defaults = this.defaultsProvider.GetDefaults();
			if (args == null || args.Length == 0)
			{
				return new ShowCommand(mode);
			}

			var commands = new Dictionary<string, int>();
			foreach (var cliArg in args)
			{
				int length = cliArg.Contains("=") ? cliArg.IndexOf("=", StringComparison.Ordinal) + 1 : 0;
				string tag = cliArg.Substring(0, length).ToLower();
				string value = cliArg.Substring(length);
				bool found = false;
				int option;
				bool presetCommand = string.IsNullOrEmpty(tag) && int.TryParse(value, out option)
				                     && mode.Presets.Contains(option);

				// multi-command, e.g. preset 0|1|2|3, or W= B= L= C= with 0|1|2|3, or L= | C= with {nn}
				if (presetCommand || this.multiCommand.Contains(tag))
				{
					found = this.AddSettings(mode.GetMultiCommandSettings(tag, value), commands);
				}
				else if (this.detailCommand.Contains(tag))
				{
					// detail-command, e.g. WL= | BL= | WC= | BC= | QE= | IN= with  0|1|2|3 or {nn}
					found = this.AddSettings(mode.GetDetailCommandSetting(tag, value), commands);
				}

				if (!found)
				{
					return new HelpCommand(defaults);
				}
			}

			return new ConsoleUpdateCommand(mode, commands);
		}

		/// <summary>
		/// Add command settings to result commands.
		/// </summary>
		/// <param name="settings">
		/// The settings.
		/// </param>
		/// <param name="commands">
		/// The commands.
		/// </param>
		/// <returns>
		/// True if found else false.
		/// </returns>
		private bool AddSettings(IEnumerable<KeyValuePair<string, int>> settings, IDictionary<string, int> commands)
		{
			if (settings == null || commands == null)
			{
				return false;
			}

			bool found = false;
			foreach (var setting in settings)
			{
				string key = setting.Key.ToLower();
				if (!commands.ContainsKey(key))
				{
					commands.Add(key, setting.Value);
				}
				else
				{
					commands[key] = setting.Value;
				}

				found = true;
			}

			return found;
		}
	}
}
