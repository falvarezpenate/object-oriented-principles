// <copyright file="Form1.cs" company="Flavio Alvarez Penate">
// Copyright (c) Flavio Alvarez Penate. All rights reserved.
// </copyright>

using System.ComponentModel;
using System.Data.Common;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SpreadSheetEngine;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Spreadsheet_Flavio_Alvarez
{
    /// <summary>
    /// Form1 derives from Form.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Spreadsheet UI to handle cell values by events.
        /// </summary>
        private SpreadSheet spreadSheet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
            this.InitializeDataGrid();
            this.spreadSheet = new SpreadSheet(50, 26);
            this.spreadSheet.PropertyChanged += this.GridCellPropertyChanged;
            this.spreadSheet.ReferencePropertyChanged += this.GridCellPropertyChanged;
            this.dataGridView1.CellBeginEdit += this.DataGridView1_CellBeginEdit;
            this.dataGridView1.CellEndEdit += this.DataGridView1_CellEndEdit;
        }

        /// <summary>
        /// Handles the instance of when a value of a cell changes and updates the grid.
        /// </summary>
        /// <param name="sender"> object type. </param>
        /// <param name="e"> PropertyChangedEverntArgs type. </param>
        public void GridCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value")
            {
                int row = ((Cell)sender).RowIndex;
                int column = ((Cell)sender).ColumnIndex;
                this.dataGridView1.Rows[row].Cells[column].Value = ((Cell)sender).Value;
            }
            else if (e.PropertyName == "BGColor")
            {
                int row = ((Cell)sender).RowIndex;
                int column = ((Cell)sender).ColumnIndex;
                int color = unchecked((int)((Cell)sender).BGColor);
                this.dataGridView1.Rows[row].Cells[column].Style.BackColor = Color.FromArgb(color);
            }
        }

        /// <summary>
        /// Initializes the Data Grid with col A-Z and row 1-50.
        /// </summary>
        private void InitializeDataGrid()
        {
            this.dataGridView1.Columns.Clear(); // clear columns already made
            string columnName, headerText;

            // Iterate through A to Z adding these columns to Data Grid.
            for (char i = 'A'; i <= 'Z'; i++)
            {
                columnName = "Column" + i.ToString();
                headerText = i.ToString();
                this.dataGridView1.Columns.Add(columnName, headerText);
            }

            // Add 50 rows.
            this.dataGridView1.Rows.Add(50);

            int rowNumber = 1;
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }

                row.HeaderCell.Value = rowNumber.ToString();
                rowNumber = rowNumber + 1;
            }
        }

        /// <summary>
        /// Populates 50 random cells with text.
        /// Sets all B cells to "This is cell B#".
        /// Sets all A cells = to B cells.
        /// </summary>
        /// <param name="sender"> object. </param>
        /// <param name="e"> EventArgs. </param>
        private void PerformDemo_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int randRow = 0, randColumn = 0;

            // populate 50 random cells with text.
            for (int i = 0; i < 50; i++)
            {
                randRow = rnd.Next(50);
                randColumn = rnd.Next(26);

                this.spreadSheet.GetCell(randRow, randColumn).Text = "Hello World!";
                this.spreadSheet.GetCell(randRow, randColumn).BGColor = 0x0000FF;
            }

            for (int row = 0; row < 50; row++)
            {
                this.spreadSheet.GetCell(row, 1).Text = "This is B" + (row + 1).ToString();
            }

            for (int row = 0; row < 50; row++)
            {
                this.spreadSheet.GetCell(row, 0).Text = "=B" + (row + 1).ToString();
            }
        }

        /// <summary>
        /// Handles a user clicking on the changecolor menu option.
        /// </summary>
        /// <param name="sender"> object. </param>
        /// <param name="e"> EventArgs. </param>
        private void ChangeColor_OnClick(object sender, EventArgs e)
        {
            // Color dialog code from msdn altered to fit this program's circumstances
            ColorDialog myDialog = new ColorDialog();

            // Keeps the user from selecting a custom color.
            myDialog.AllowFullOpen = false;

            // Allows the user to get help. (The default is false.)
            myDialog.ShowHelp = true;

            // Sets the initial color select to the current text color.
            myDialog.Color = Color.White;

            // Update the text box color if the user clicks OK.
            if (myDialog.ShowDialog() == DialogResult.OK)
            {
                this.UpdateSelectedCellColor(myDialog.Color.ToArgb());
            }
        }

        /// <summary>
        /// Updates the color of all selected cells in the datagrid via the spreadsheet.
        /// </summary>
        /// <param name="color"> int argb value. </param>
        private void UpdateSelectedCellColor(int color)
        {
            Stack<uint> oldColors = new Stack<uint>();
            Stack<Cell> cells = new Stack<Cell>();

            // traverse the entire datagrid
            for (int row = 0; row < 50; row++)
            {
                for (int col = 0; col < 26; col++)
                {
                    // if cell is selected, update spreadsheet.
                    if (this.dataGridView1.Rows[row].Cells[col].Selected)
                    {
                        Cell selectedCell = this.spreadSheet.GetCell(row, col);
                        oldColors.Push(selectedCell.BGColor);
                        selectedCell.BGColor = (uint)color;
                        cells.Push(selectedCell);
                    }
                }
            }

            UndoRedoCellColor undo = new UndoRedoCellColor(cells, oldColors);
            this.spreadSheet.AddUndo(undo);
        }

        /// <summary>
        /// Grays out buttons if can't  be used for redo and undo.
        /// </summary>
        /// <param name="sender"> object. </param>
        /// <param name="e"> EventArgs. </param>
        private void EditToolStripMenu_OnClick(object sender, EventArgs e)
        {
            // stack is empty so disable button and gray it out.
            if (this.spreadSheet.UndoStackEmpty())
            {
                this.undoToolStripMenuItem.Enabled = false;

                // we cant undo anything specific so change text to default.
                this.undoToolStripMenuItem.Text = "Undo";
            }
            else
            {
                // enable button since undo stack has items.
                this.undoToolStripMenuItem.Enabled = true;

                // check if we're undoing the text or the color of a cell and adjust button text
                UndoRedoCellText temp = this.spreadSheet.UndoPeek() as UndoRedoCellText;
                if (temp != null)
                {
                    this.undoToolStripMenuItem.Text = "Undo cell text";
                }

                // only one other thing it could be: object of UndoRedoCellColor type.
                else
                {
                    this.undoToolStripMenuItem.Text = "Undo cell color";
                }
            }

            // same as above but for redo button.
            if (this.spreadSheet.RedoStackEmpty())
            {
                this.redoToolStripMenuItem.Enabled = false;
                this.redoToolStripMenuItem.Text = "Redo";
            }
            else
            {
                this.redoToolStripMenuItem.Enabled = true;
                UndoRedoCellText temp = this.spreadSheet.RedoPeek() as UndoRedoCellText;
                if (temp != null)
                {
                    this.redoToolStripMenuItem.Text = "Redo cell text";
                }
                else
                {
                    this.redoToolStripMenuItem.Text = "Redo cell color";
                }
            }
        }

        /// <summary>
        /// Handles the event that the undo button option is clicked on the menu.
        /// </summary>
        /// <param name="sender"> object. </param>
        /// <param name="e"> EventArgs. </param>
        private void Undo_OnClick(object sender, EventArgs e)
        {
            UndoRedoCollection undo = this.spreadSheet.RemoveUndo();
            undo.ExecuteCommand();
            this.spreadSheet.AddRedo(undo);
        }

        /// <summary>
        /// Handles the event that the redo button is clicked on the menu.
        /// </summary>
        /// <param name="sender"> object. </param>
        /// <param name="e"> EventArgs. </param>
        private void Redo_OnClick(object sender, EventArgs e)
        {
            UndoRedoCollection redo = this.spreadSheet.RemoveRedo();
            redo.ExecuteCommand();
            this.spreadSheet.AddUndo(redo);
        }

        /// <summary>
        /// Opens a file dialog for opening a file and passes it to spreadsheet.load().
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> EventArgs. </param>
        private void LoadSpreadSheet_OnClick(object sender, EventArgs e)
        {
            Stream myStream;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\Documents"; // set init dir at documents for convenience in demo.
                openFileDialog.Filter = "XML Files (*.xml)|*.xml"; // set it to only accept .xml files.
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    myStream = openFileDialog.OpenFile();
                    this.spreadSheet.LoadCells(myStream);
                    myStream.Close();
                }
            }
        }

        /// <summary>
        /// Opens a save file dialog and passes it to spreadsheet.save().
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> EventArgs. </param>
        private void SaveSpreadSheet_OnClick(object sender, EventArgs e)
        {
            Stream myStream;
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = "c:\\Documents";
                saveFileDialog.Filter = "XML Files (*.xml)|*.xml";
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    myStream = saveFileDialog.OpenFile();
                    this.spreadSheet.SaveCells(myStream);
                    myStream.Close();
                }
            }
        }

        /// <summary>
        /// Built-in Form1_Load() method.
        /// </summary>
        /// <param name="sender"> object. </param>
        /// <param name="e"> EventArgs. </param>
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the event that a cell is opened for editing.
        /// </summary>
        /// <param name="sender"> object. </param>
        /// <param name="e"> DataGridCellCancelEventArgs. </param>
        private void DataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            Cell currCell = this.spreadSheet.GetCell(e.RowIndex, e.ColumnIndex);
            this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = currCell.Text; // when editing text should be displayed.
        }

        /// <summary>
        /// Handles the event that a cell is closed after editing.
        /// </summary>
        /// <param name="sender"> object. </param>
        /// <param name="e"> DataGridViewCellEventArgs. </param>
        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Cell currCell = this.spreadSheet.GetCell(e.RowIndex, e.ColumnIndex);

            // if a change was actually made, we need to push an undo execution to the undo stack.
            if (currCell.Value != Convert.ToString(this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
            {
                string old = currCell.Text;
                UndoRedoCollection undo = new UndoRedoCellText(currCell, old);
                currCell.Text = Convert.ToString(this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value); // if user has changed text, it has to be updated in SpreadSheet
                this.spreadSheet.AddUndo(undo);
            }

            this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = currCell.Value; // finished editing so back to displaying value. Value will be updated through events if user changed text.
        }
    }
}