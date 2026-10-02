// <copyright file="SpreadSheet.cs" company="Flavio Alvarez Penate">
// Copyright (c) Flavio Alvarez Penate. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using SpreadSheetEngine;

namespace SpreadSheetEngine
{
    /// <summary>
    /// Class that acts as a factory of cells.
    /// </summary>
    public class SpreadSheet
    {
        /// <summary>
        /// keeps track of number of rows.
        /// </summary>
        private int rowCount;

        /// <summary>
        /// keeps track of number of columns.
        /// </summary>
        private int columnCount;

        /// <summary>
        /// keeps track of a 2D array of cells.
        /// </summary>
        private Cell[,] cellArray;

        /// <summary>
        /// stack that keeps track of undo commands.
        /// </summary>
        private Stack<UndoRedoCollection> undoStack = new Stack<UndoRedoCollection>();

        /// <summary>
        /// stack that keeps track of redo commands.
        /// </summary>
        private Stack<UndoRedoCollection> redoStack = new Stack<UndoRedoCollection>();

        /// <summary>
        /// Dictionary for circular references.
        /// </summary>
        private Dictionary<string, string[]> referenceDic = new Dictionary<string, string[]>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SpreadSheet"/> class.
        /// </summary>
        /// <param name="numRows"> int type number of rows. </param>
        /// <param name="numCols"> int type number of columns. </param>
        public SpreadSheet(int numRows, int numCols)
        {
            this.rowCount = numRows;
            this.columnCount = numCols;
            this.cellArray = new Cell[this.rowCount, this.columnCount];
            for (int row = 0; row < this.rowCount; row++)
            {
                for (int col = 0; col < this.columnCount; col++)
                {
                    this.cellArray[row, col] = new ConcreteCell(row, col, string.Empty);
                    this.cellArray[row, col].PropertyChanged += this.CellPropertyChanged;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary>
        /// Creates event for when reference cell is changed.
        /// </summary>
        public event PropertyChangedEventHandler ReferencePropertyChanged = delegate { };

        /// <summary>
        /// Gets row count.
        /// </summary>
        public int RowCount { get => this.rowCount; }

        /// <summary>
        /// Gets column count.
        /// </summary>
        public int ColumnCount { get => this.columnCount; }

        /// <summary>
        /// Gets a cell in the cell array.
        /// </summary>
        /// <param name="row"> int type row index. </param>
        /// <param name="column"> int type col index. </param>
        /// <returns> returns null if cell doesn't exist. Otherwise returns cell. </returns>
        public Cell GetCell(int row, int column)
        {
            if (row > this.RowCount || column > this.ColumnCount)
            {
                return null;
            }

            return this.cellArray[row, column];
        }

        /// <summary>
        /// Gets cell based on name (i.e. cell at "B2" is [1,1]).
        /// </summary>
        /// <param name="cellName"> string type. </param>
        /// <returns> Cell type. </returns>
        public Cell GetCell(string cellName)
        {
            // have to handle setting a cell equal to another.
            // for a value such as "=A3", column will be value[1] = 'A' and row will be value[2] = 3
            int column = Convert.ToInt32(cellName[0]);

            cellName = cellName.Remove(0, 1); // get rid of the column.
            int row = Convert.ToInt32(cellName);

            // call get cell to get the cell we're referencing.
            return this.GetCell(row - 1, column - 65); // have to correct for rows starting at 1 and  ASCII value of column 'A' starting at 65
        }

        /// <summary>
        /// Handles the case that a cell changes its text and determines whether the value needs to be changed.
        /// </summary>
        /// <param name="sender"> sender type. </param>
        /// <param name="e"> PropertyChangedEventArgs type. </param>
        public void CellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            string cellText = string.Empty;

            // Do something useful here.
            if (e.PropertyName == "Text")
            {
                int status = 0; // 0 = success, 1 = cell not in spreadsheet, 2 = wrong format

                cellText = ((Cell)sender).Text;

                if (cellText.StartsWith('='))
                {
                    cellText = cellText.Remove(0, 1); // get rid of =.

                    // create an expression tree with the cell text as input.
                    ExpressionTree expressionTree = new ExpressionTree(cellText);

                    // get sender cell's name
                    string cellName = this.GetCellName(((Cell)sender).RowIndex, ((Cell)sender).ColumnIndex);

                    // add reference to the dictionary
                    this.referenceDic[cellName] = expressionTree.GetVariableNames();

                    if (this.IsCircularReference(cellName, this.referenceDic[cellName]))
                    {
                        status = 4;
                    }
                    else
                    {
                        foreach (string varName in expressionTree.GetVariableNames())
                        {
                            if (!this.DoesCellExist(varName))
                            {
                                status = 1;
                                break;
                            }

                            // get current cell from current variable name in array.
                            Cell currCell = this.GetCell(varName);

                            double value;

                            // tryparse the value of the current cell as a double
                            try
                            {
                                // if cell does exist, and value is empty, then default to 0.
                                if (currCell.Text == string.Empty)
                                {
                                    value = 0;
                                }

                                // if cell does exist, and value is not empty, make sure to check if a double.
                                else
                                {
                                    value = double.Parse(currCell.Value);
                                }

                                expressionTree.SetVariable(varName, value);
                                currCell.PropertyChanged += this.ReferenceCellChanged;
                            }
                            catch (FormatException)
                            {
                                currCell.PropertyChanged += this.ReferenceCellChanged;
                                status = 2;
                            }
                        }
                    }

                    // 0 = success, 1 = cell referenced not in spreadsheet, 2 = cell referenced wasn't a double type value.
                    if (status == 0)
                    {
                        ((Cell)sender).Value = Convert.ToString(expressionTree.Evaluate());
                    }
                    else if (status == 1)
                    {
                        ((Cell)sender).Value = "Cell doesn't exist!";
                    }
                    else if (status == 2)
                    {
                        ((Cell)sender).Value = "Wrong Format!";
                    }
                    else
                    {
                        ((Cell)sender).Value = "Circular reference!";
                    }
                }
                else
                {
                    // no longer referencing anything for this cell in the text
                    string cellName = this.GetCellName(((Cell)sender).RowIndex, ((Cell)sender).ColumnIndex);

                    if (this.referenceDic.ContainsKey(cellName))
                    {
                        this.referenceDic.Remove(cellName);
                    }

                    ((Cell)sender).Value = ((Cell)sender).Text;
                }

                this.PropertyChanged(sender, new PropertyChangedEventArgs("Value"));
            }
            else if (e.PropertyName == "BGColor")
            {
                this.PropertyChanged(sender, new PropertyChangedEventArgs("BGColor"));
            }
        }

        /// <summary>
        /// New event to handle a cell referenced by a formula in a different cell changing.
        /// </summary>
        /// <param name="sender"> object. </param>
        /// <param name="e"> PropertyChangedEventArgs. </param>
        public void ReferenceCellChanged(object sender, PropertyChangedEventArgs e)
        {
            int row = ((Cell)sender).RowIndex;
            int col = ((Cell)sender).ColumnIndex;

            string editedCellName = this.GetCellName(row, col);

            for (int i = 0; i < this.RowCount; i++)
            {
                for (int j = 0; j < this.ColumnCount; j++)
                {
                    Cell currCell = this.cellArray[i, j];

                    // if the current index cell contains the sender cell in it's text as a reference
                    if (currCell.Text.Contains(editedCellName))
                    {
                        int status = 0;

                        string cellText = currCell.Text;

                        cellText = cellText.Remove(0, 1); // get rid of =.

                        // create an expression tree with the cell text as input.
                        ExpressionTree expressionTree = new ExpressionTree(cellText);

                        foreach (string varName in expressionTree.GetVariableNames())
                        {
                            if (!this.DoesCellExist(varName))
                            {
                                status = 1;
                                break;
                            }

                            // get current cell from current variable name in array.
                            Cell dictCell = this.GetCell(varName);

                            double value;

                            // tryparse the value of the current cell as a double
                            try
                            {
                                if (dictCell.Text == string.Empty)
                                {
                                    value = 0;
                                }
                                else
                                {
                                    value = double.Parse(dictCell.Value);
                                }

                                expressionTree.SetVariable(varName, value);
                            }
                            catch (FormatException)
                            {
                                status = 2;
                                break;
                            }
                        }

                        // 0 = success, 1 = cell doesnt exist, 2 = wrong format of referenced cell
                        if (status == 0)
                        {
                            currCell.Value = Convert.ToString(expressionTree.Evaluate());
                        }
                        else if (status == 1)
                        {
                            currCell.Value = "Cell doesn't exist!";
                        }
                        else
                        {
                            currCell.Value = "Wrong Format!";
                        }

                        this.ReferencePropertyChanged(currCell, new PropertyChangedEventArgs("Value"));
                    }
                }
            }
        }

        /// <summary>
        /// Gets the cell name of a cell based on index (i.e. [0,0] is "A1").
        /// </summary>
        /// <param name="row"> int row index. </param>
        /// <param name="column"> int col index. </param>
        /// <returns> string name. </returns>
        public string GetCellName(int row, int column)
        {
            return Convert.ToChar(column + 65) + (row + 1).ToString();
        }

        /// <summary>
        /// Adds and undo command to the undo stack.
        /// </summary>
        /// <param name="newUndo"> UndoRedoCollection. </param>
        public void AddUndo(UndoRedoCollection newUndo)
        {
            this.undoStack.Push(newUndo);
        }

        /// <summary>
        /// Removed an undo command from the undo stack after its been executed.
        /// </summary>
        /// <returns> UndoRedoCollection. </returns>
        public UndoRedoCollection RemoveUndo()
        {
            return this.undoStack.Pop();
        }

        /// <summary>
        /// Adds a redo command to the redo stack.
        /// </summary>
        /// <param name="newRedo"> UndoRedoCollection. </param>
        public void AddRedo(UndoRedoCollection newRedo)
        {
            this.redoStack.Push(newRedo);
        }

        /// <summary>
        /// Pops a redo command from the redo stack after its been executed.
        /// </summary>
        /// <returns> UndoRedoCollection. </returns>
        public UndoRedoCollection RemoveRedo()
        {
            return this.redoStack.Pop();
        }

        /// <summary>
        /// Checks if undo stack is empty.
        /// </summary>
        /// <returns> true if empty, false if not. </returns>
        public bool UndoStackEmpty()
        {
            return this.undoStack.Count == 0;
        }

        /// <summary>
        /// Checks if the redo stack is empty.
        /// </summary>
        /// <returns> true if redo stack is empty, false if not. </returns>
        public bool RedoStackEmpty()
        {
            return this.redoStack.Count == 0;
        }

        /// <summary>
        /// Allows outside of this project to peek at the undo stack to determine button names.
        /// </summary>
        /// <returns> UndoRedoCollection. </returns>
        public UndoRedoCollection UndoPeek()
        {
            return this.undoStack.Peek();
        }

        /// <summary>
        /// Allows outside of this project to peek at the redo stack to determine button names.
        /// </summary>
        /// <returns> UndoRedoCollection. </returns>
        public UndoRedoCollection RedoPeek()
        {
            return this.redoStack.Peek();
        }

        /// <summary>
        /// Saves any used cell into a xml document for loading later.
        /// </summary>
        /// <param name="stream"> Stream object. </param>
        public void SaveCells(Stream stream)
        {
            // set indent to true for easier readability, otherwise everything is in one line.
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            XmlWriter writer = XmlWriter.Create(stream, settings);
            writer.WriteStartDocument();
            writer.WriteStartElement("spreadsheet");

            foreach (Cell cell in this.cellArray)
            {
                // if text is not empty or color is not white
                if (cell.Text != string.Empty || cell.BGColor != 0xFFFFFFFF)
                {
                    writer.WriteStartElement("cell"); // <cell>
                    writer.WriteAttributeString("name", this.GetCellName(cell.RowIndex, cell.ColumnIndex)); // <cell name = A1>

                    // check if text is one of the changed attributes checked in the if statement above.
                    if (cell.Text != string.Empty)
                    {
                        writer.WriteStartElement("text");
                        writer.WriteString(cell.Text);
                        writer.WriteEndElement(); // <text>Text<text>
                    }

                    // now check for bgcolor.
                    if (cell.BGColor != 0xFFFFFFFF)
                    {
                        writer.WriteStartElement("bgcolor");
                        writer.WriteString(cell.BGColor.ToString());
                        writer.WriteEndElement(); // <bgcolor>0x000000<bgcolor>
                    }

                    // after this line:
                    // <spreadsheet>
                    //    <cell name=A1>
                    //          <text>hello world<text>
                    //          <color>0x0000000<color>
                    //    <cell>
                    writer.WriteEndElement();
                }
            }

            // after this line:
            // <spreadsheet>
            //    <cell name=A1>
            //          <text>hello world<text>
            //          <color>0xFFFFF<color>
            //    <cell>
            //    .
            //    .
            //    .
            // <spreadsheet>
            writer.WriteEndElement();

            writer.WriteEndDocument();
            writer.Close();
        }

        /// <summary>
        /// Loads a spreadsheet from a xml file.
        /// </summary>
        /// <param name="stream"> Stream type object. </param>
        /// <exception> any xml file that doesn't have a name attribute for cell elements. </exception>"
        public void LoadCells(Stream stream)
        {
            // clear all cells. Its a load not a merge.
            this.ClearCells();

            XmlReader reader = XmlReader.Create(stream);

            // have to assign an empty value to the cell var or there's an error.
            Cell currentCell = new ConcreteCell(0, 0, string.Empty);

            // check that the xml file is for a spreadsheet.
            if (reader.IsStartElement("spreadsheet"))
            {
                while (reader.Read())
                {
                    // if it's a cell element, update current cell to match the cell being loaded.
                    if (reader.IsStartElement("cell"))
                    {
                        currentCell = this.GetCell(reader.GetAttribute("name"));
                    }

                    // else the XmlReader is inside a cell already and it just needs to find any text/color changes to load.
                    else if (reader.IsStartElement("text"))
                    {
                        currentCell.Text = reader.ReadString();
                    }
                    else if (reader.IsStartElement("bgcolor"))
                    {
                        currentCell.BGColor = Convert.ToUInt32(reader.ReadString());
                    }

                    // not an element that the program cares about for loading, ignore/skip.
                    else
                    {
                        reader.Skip();
                    }
                }
            }

            reader.Close();

            // after loading, clear undo/redo stacks
            this.ClearUndoRedoStacks();
        }

        /// <summary>
        /// Clears the spreadsheet before loading a new xml file.
        /// </summary>
        private void ClearCells()
        {
            foreach (Cell cell in this.cellArray)
            {
                cell.Text = string.Empty; // text to empty
                cell.BGColor = 0xFFFFFFFF; // color to white
            }
        }

        /// <summary>
        /// Clears stacks after loading a xml file.
        /// </summary>
        private void ClearUndoRedoStacks()
        {
            this.undoStack.Clear();
            this.redoStack.Clear();
        }

        /// <summary>
        /// Determines wether a cell exists in the spreadsheet.
        /// </summary>
        /// <param name="cellName"> string cell name. </param>
        /// <returns> a bool. True if exists, false if not. </returns>
        private bool DoesCellExist(string cellName)
        {
            if (cellName[0] - 65 <= this.columnCount)
            {
                cellName = cellName.Remove(0, 1); // get rid of the letter.
                if (Convert.ToInt32(cellName) < this.rowCount)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Takes a cell name and a list of references and checks if its embedded in any of the cells referenced in the list.
        /// </summary>
        /// <param name="name"> string. </param>
        /// <param name="list"> string array. </param>
        /// <returns> true if it's circular, flase if not. </returns>
        private bool IsCircularReference(string name, string[] list)
        {
            if (list.Contains(name))
            {
                return true;
            }

            bool result = false;
            foreach (var variable in list)
            {
                if (this.referenceDic.ContainsKey(variable))
                {
                    result = this.IsCircularReference(name, this.referenceDic[variable]);
                }
            }

            return result;
        }
    }
}