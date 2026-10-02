// <copyright file="UndoRedoCellText.cs" company="Flavio Alvarez Penate">
// Copyright (c) Flavio Alvarez Penate. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadSheetEngine
{
    /// <summary>
    /// Undo's or Redo's a cell's text property.
    /// </summary>
    public class UndoRedoCellText : UndoRedoCollection
    {
        /// <summary>
        /// holds cell that's being changed.
        /// </summary>
        private Cell cell;

        /// <summary>
        /// holds previous text contained within cell.
        /// </summary>
        private string cellText;

        /// <summary>
        /// Initializes a new instance of the <see cref="UndoRedoCellText"/> class.
        /// </summary>
        /// <param name="editedCell"> Cell type. </param>
        /// <param name="newText"> string type. </param>
        public UndoRedoCellText(Cell editedCell, string newText)
        {
            this.cell = editedCell;
            this.cellText = newText;
        }

        /// <summary>
        /// Executes undo/redo command. Uses a temp to update values for when we need to unexecute it.
        /// </summary>
        public override void ExecuteCommand()
        {
            // cell's current text becomes the string passed into constructor.
            string temp = this.cell.Text;

            this.cell.Text = this.cellText;

            this.cellText = temp;
        }
    }
}
