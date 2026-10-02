// <copyright file="UndoRedoCellColor.cs" company="Flavio Alvarez Penate">
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
    /// Class inheriting from UndoRedoCollection that implements undo and redo for cell color.
    /// </summary>
    public class UndoRedoCellColor : UndoRedoCollection
    {
        /// <summary>
        /// Holds a stack of all cells that were selected at a single time for editing color.
        /// </summary>
        private Stack<Cell> cells = new Stack<Cell>();

        /// <summary>
        /// Holds the corresponding cell old colors at the same index as the stack above.
        /// </summary>
        private Stack<uint> cellColors = new Stack<uint>();

        /// <summary>
        /// Count of stack items.
        /// </summary>
        private int count;

        /// <summary>
        /// Initializes a new instance of the <see cref="UndoRedoCellColor"/> class.
        /// </summary>
        /// <param name="editedCell"> Cell stack. </param>
        /// <param name="newCellColor"> uint stack. </param>
        public UndoRedoCellColor(Stack<Cell> editedCell, Stack<uint> newCellColor)
        {
            this.cells = editedCell;
            this.cellColors = newCellColor;
            this.count = this.cells.Count;
        }

        /// <summary>
        /// Executes an undo/redo command and sets the unexecute to its opposite.
        /// </summary>
        public override void ExecuteCommand()
        {
            // define two new stacks for unexecuting this command.
            Stack<Cell> tempCells = new Stack<Cell>();
            Stack<uint> tempColors = new Stack<uint>();

            // stacks have same size, so while stacks not empty:
            while (this.count > 0)
            {
                // pop cell from stack
                Cell currentCell = this.cells.Pop();

                // add its color to the tempColors stack for if we want to undo this execution.
                tempColors.Push(currentCell.BGColor);

                // set the cell's color the old color stack's corresponding value.
                currentCell.BGColor = this.cellColors.Pop();

                // push this now edited cell into the tempCells stack for when we have to unexecute this command.
                tempCells.Push(currentCell);

                // decrease count
                this.count -= 1;
            }

            // set stacks to new updated temps.
            this.cells = tempCells;
            this.cellColors = tempColors;

            // reset count for unexecute command.
            this.count = this.cells.Count;
        }
    }
}
