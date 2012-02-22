// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DosModeProvider.cs" company="boolship">
//   Copyright (c) 2012 boolship@gmail.com. All rights reserved.
// </copyright>
// <summary>
//   Defines the DosModeProvider type.
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
	using Domain;

	/// <summary>
	/// Provides mode communication with DOS, implementing abstract class.
	/// </summary>
	public class DosModeProvider : ModeProvider
	{
		/// <summary>
		/// Provides Mode implemtation for DOS.
		/// </summary>
		/// <returns>
		/// Return a Mode implementation extending Mode for DOS.
		/// Optional parameters can be added to pick user preferences.
		/// </returns>
		public override Mode GetMode()
		{
			return new DosMode();
		}
	}
}
