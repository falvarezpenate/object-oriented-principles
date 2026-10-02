// <copyright file="Cell.cs" company="Flavio Alvarez Penate">
// Copyright (c) Flavio Alvarez Penate. All rights reserved.
// </copyright>

using System.ComponentModel;
using System.Xml.Linq;

namespace SpreadSheetEngine
{
    /// <summary>
    /// This will be a class.
    /// </summary>
    public abstract class Cell : INotifyPropertyChanged
    {
        /// <summary>
        /// contains the row index of this cell.
        /// </summary>
        protected int rowIndex;

        /// <summary>
        /// contains the column index of this cell.
        /// </summary>
        protected int columnIndex;

        /// <summary>
        /// contains the text of this cell.
        /// </summary>
        protected string text;

        /// <summary>
        /// contains the value of this cell.
        /// </summary>
        protected string value;

        /// <summary>
        /// Determines color of the cell.
        /// </summary>
        protected uint bgColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="rowIndex"> int type row index. </param>
        /// <param name="columnIndex"> int type column index. </param>
        /// <param name="text"> string type text. </param>
        public Cell(int rowIndex, int columnIndex, string text)
        {
            this.rowIndex = rowIndex;
            this.columnIndex = columnIndex;
            this.text = text;
            this.value = text;
            this.bgColor = 0xFFFFFFFF; // white bg color
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary>
        /// Gets rowIndex value. Read only.
        /// </summary>
        public int RowIndex { get => this.rowIndex; }

        /// <summary>
        /// Gets columnIndex value. Read only.
        /// </summary>
        public int ColumnIndex { get => this.columnIndex; }

        /// <summary>
        /// Gets or sets text value.
        /// </summary>
        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                // property changed notif
                if (value == this.text)
                {
                    return;
                }

                this.text = value;

                // The event gets called as if it were one function (in terms of syntax)
                // Calls everything (all subscribed delegates) in the internal list
                this.PropertyChanged(this, new PropertyChangedEventArgs(nameof(this.Text)));
            }
        }

        /// <summary>
        /// Gets or sets value.
        /// </summary>
        public string Value
        {
            get
            {
                return this.value;
            }

            protected internal set
            {
                if (this.value == value)
                {
                    return;
                }

                this.value = value;
                this.PropertyChanged(this, new PropertyChangedEventArgs(nameof(this.Value)));
            }
        }

        /// <summary>
        /// Gets or sets background color.
        /// </summary>
        public uint BGColor
        {
            get
            {
                return this.bgColor;
            }

            set
            {
                if (this.bgColor == value)
                {
                    return;
                }

                this.bgColor = value;
                this.PropertyChanged(this, new PropertyChangedEventArgs(nameof(this.BGColor)));
            }
        }
    }
}