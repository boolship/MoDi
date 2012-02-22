// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModeProvider.cs" company="boolship">
//   Copyright (c) 2012 boolship@gmail.com. All rights reserved.
// </copyright>
// <summary>
//   Defines the ModeProvider type.
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
	/// A DEPENDENCY that can provide Mode instances on request.
	/// </summary>
	public abstract class ModeProvider
	{
		/// <summary>
		/// Create the appropriate Mode object, e.g. one Mode implementation is DosMode.
		/// </summary>
		/// <returns>
		/// Return an abstract Mode implemention, e.g. DosMode for DOS console options.
		/// Optional parameters can be added to pick user preferences.
		/// </returns>
		public abstract Mode GetMode();
	}
}
