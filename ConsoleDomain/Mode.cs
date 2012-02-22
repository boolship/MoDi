// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Mode.cs" company="boolship">
//   Copyright (c) 2012 boolship@gmail.com. All rights reserved.
// </copyright>
// <summary>
//   Defines the Mode type.
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
	using System.Collections.Generic;

	/// <summary>
	/// Mode setting options and actions, used for retrieval.
	/// </summary>
	public abstract class Mode
	{
		/// <summary>
		/// Gets or sets WindowLines.
		/// </summary>
		public abstract int WindowLines { get; set; }

		/// <summary>
		/// Gets or sets WindowColumns.
		/// </summary>
		public abstract int WindowColumns { get; set; }

		/// <summary>
		/// Gets or sets BufferLines.
		/// </summary>
		public abstract int BufferLines { get; set; }

		/// <summary>
		/// Gets or sets BufferColumns.
		/// </summary>
		public abstract int BufferColumns { get; set; }

		/// <summary>
		/// Gets or sets InsertMode.
		/// </summary>
		public abstract int InsertMode { get; set; }

		/// <summary>
		/// Gets or sets QuickEdit.
		/// </summary>
		public abstract int QuickEdit { get; set; }

		/// <summary>
		/// Gets Presets.
		/// </summary>
		public abstract List<int> Presets { get; }

		/// <summary>
		/// Gets all current setting option values.
		/// </summary>
		/// <returns>
		/// Collection of option-value pairs.
		/// </returns>
		public abstract IDictionary<string, int> GetCurrentSettings();

		/// <summary>
		/// Gets detail-command settings, e.g. WL= | BL= | WC= | BC= | QE= | IN= with  0|1|2|3 or {nn}
		/// </summary>
		/// <param name="tag">
		/// The option.
		/// </param>
		/// <param name="value">
		/// The value.
		/// </param>
		/// <returns>
		/// Collection of option value pairs.
		/// </returns>
		public abstract IDictionary<string, int> GetDetailCommandSetting(string tag, string value);

		/// <summary>
		/// Gets multi-command, e.g. preset 0|1|2|3, or W= B= L= C= with 0|1|2|3, or L= | C= with {nn}
		/// </summary>
		/// <param name="tag">
		/// The option.
		/// </param>
		/// <param name="value">
		/// The value.
		/// </param>
		/// <returns>
		/// Collection of option value pairs.
		/// </returns>
		public abstract IDictionary<string, int> GetMultiCommandSettings(string tag, string value);

		/// <summary>
		/// Gets all preset settings given preset.
		/// </summary>
		/// <param name="preset">
		/// The preset.
		/// </param>
		/// <returns>
		/// Collection of option value pairs.
		/// </returns>
		public abstract IDictionary<string, int> GetPresetValues(int preset);

		/// <summary>
		/// Sets any current setting option values.
		/// </summary>
		/// <param name="commands">
		/// The cmdValueDictionary.
		/// </param>
		public abstract void UpdateAnyCurrentSettings(IDictionary<string, int> commands);

		/// <summary>
		/// Set one current setting option value.
		/// </summary>
		/// <param name="option">
		/// The option.
		/// </param>
		/// <param name="value">
		/// The value.
		/// </param>
		public abstract void UpdateCurrentSettingFor(string option, int value);
	}
}
