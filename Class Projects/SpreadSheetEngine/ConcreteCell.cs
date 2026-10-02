// <copyright file="ConcreteCell.cs" company="Flavio Alvarez Penate">
// Copyright (c) Flavio Alvarez Penate. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SpreadSheetEngine
{
    /// <summary>
    /// Concrete class that inherits from an abstract class in order to handle events.
    /// </summary>/// <summary>
    /// Concrete class that inherits from an abstract class in order create an instance.
    /// </summary>
    internal class ConcreteCell : Cell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConcreteCell"/> class.
        /// </summary>
        /// <param name="rowIndex"> int type for row. </param>
        /// <param name="columnIndex"> int type for col. </param>
        /// <param name="text"> string type. </param>
        public ConcreteCell(int rowIndex, int columnIndex, string text)
            : base(rowIndex, columnIndex, text)
        {
        }
    }
}
