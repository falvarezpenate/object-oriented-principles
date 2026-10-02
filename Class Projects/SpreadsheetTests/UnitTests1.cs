// <copyright file="UnitTests1.cs" company="Flavio Alvarez Penate">
// Copyright (c) Flavio Alvarez Penate. All rights reserved.
// </copyright>

using System.Xml;
using NUnit.Framework.Constraints;
using SpreadSheetEngine;

namespace SpreadsheetTests
{
    /// <summary>
    /// UnitTests class for testing HW4.
    /// </summary>
    public class UnitTests1
    {
        /// <summary>
        /// Built in Setup() method.
        /// </summary>
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Tests the GetCell() method with a normal input.
        /// </summary>
        [Test]
        public void TestSpreadSheetGetCellNormal()
        {
            SpreadSheet spreadSheet = new SpreadSheet(4, 4);

            spreadSheet.GetCell(1, 1).Text = "test";

            string result = spreadSheet.GetCell(1, 1).Text;

            Assert.That(result, Is.EqualTo("test"));
        }

        /// <summary>
        /// Tests the GetCell() method with a normal input.
        /// </summary>
        [Test]
        public void TestSpreadSheetGetCellByStringNormal()
        {
            SpreadSheet spreadSheet = new SpreadSheet(4, 4);

            spreadSheet.GetCell("A1").Text = "test";

            string result = spreadSheet.GetCell(0, 0).Text;

            Assert.That(result, Is.EqualTo("test"));
        }

        /// <summary>
        /// Tests the GetCell() method on a cell that has no text.
        /// </summary>
        [Test]
        public void TestSpreadSheetGetCellEdge()
        {
            SpreadSheet spreadSheet = new SpreadSheet(4, 4);

            string result = spreadSheet.GetCell(3, 3).Text;

            Assert.That(result, Is.EqualTo(string.Empty));
        }

        /// <summary>
        /// Tests that GetCell() method returns a null value on an out of bounds request for the index.
        /// </summary>
        [Test]
        public void TestSpreadSheetGetCellException()
        {
            SpreadSheet spreadSheet = new SpreadSheet(4, 4);

            Cell result = spreadSheet.GetCell(5, 5);

            Assert.That(result, Is.EqualTo(null));
        }

        /// <summary>
        /// Tests GetCellName method.
        /// </summary>
        [Test]
        public void TestSpreadSheetGetCellNameNormal()
        {
            SpreadSheet spreadSheet = new SpreadSheet(4, 4);

            string result = spreadSheet.GetCellName(0, 0);

            Assert.That(result, Is.EqualTo("A1"));
        }

        /// <summary>
        /// Tests setting a cell equal to another.
        /// </summary>
        [Test]
        public void TestSpreadSheetFormulaNormal()
        {
            SpreadSheet spreadSheet = new SpreadSheet(4, 4);

            spreadSheet.GetCell(1, 1).Text = "34";

            spreadSheet.GetCell(2, 2).Text = "=B2";
            string result = spreadSheet.GetCell(2, 2).Value;

            Assert.That(result, Is.EqualTo("34"));
        }

        /// <summary>
        /// Test getting the value of a cell that is not using any formulas as text.
        /// </summary>
        [Test]
        public void TestSpreadSheetFormulaException()
        {
            SpreadSheet spreadSheet = new SpreadSheet(4, 4);

            spreadSheet.GetCell(1, 1).Text = "test";

            string result = spreadSheet.GetCell(1, 1).Value;

            Assert.That(result, Is.EqualTo("test"));
        }

        /// <summary>
        /// Tests update formula update when a referenced cell changes.
        /// </summary>
        [Test]
        public void TestSpreadSheetFormulaUpdateNormal()
        {
            SpreadSheet spreadSheet = new SpreadSheet(4, 4);

            spreadSheet.GetCell(1, 1).Text = "34";

            spreadSheet.GetCell(2, 2).Text = "=B2";

            spreadSheet.GetCell(1, 1).Text = "56";

            string result = spreadSheet.GetCell(2, 2).Value;

            Assert.That(result, Is.EqualTo("56"));
        }

        /// <summary>
        /// Tests a slightly complex formula that references other cells.
        /// </summary>
        [Test]
        public void TestSpreadSheetFormulaArithmeticNormal()
        {
            SpreadSheet spreadSheet = new SpreadSheet(4, 4);

            spreadSheet.GetCell(1, 1).Text = "5";

            spreadSheet.GetCell(2, 2).Text = "4";

            spreadSheet.GetCell(3, 3).Text = "=(B2+C3)/3";

            string result = spreadSheet.GetCell(3, 3).Value;

            Assert.That(result, Is.EqualTo("3"));
        }

        /// <summary>
        /// Tests a formula with wrong syntax.
        /// </summary>
        [Test]
        public void TestSpreadSheetFormulaArithmeticException()
        {
            SpreadSheet spreadSheet = new SpreadSheet(4, 4);

            spreadSheet.GetCell(1, 1).Text = "5";

            spreadSheet.GetCell(2, 2).Text = "cat";

            spreadSheet.GetCell(3, 3).Text = "=(B2+C3)/3";

            string result = spreadSheet.GetCell(3, 3).Value;

            Assert.That(result, Is.EqualTo("Wrong Format!"));
        }

        /// <summary>
        /// Tests that every cell has default color white.
        /// </summary>
        [Test]
        public void TestSpreadSheetColorDefaultNormal()
        {
            SpreadSheet spreadSheet = new SpreadSheet(4, 4);

            uint result = spreadSheet.GetCell(1, 1).BGColor;

            // check that default is white.
            Assert.That(result, Is.EqualTo(0xFFFFFFFF));
        }

        /// <summary>
        /// Tests that we can set the color of a cell to something else effectively.
        /// </summary>
        [Test]
        public void TestSpreadSheetColorChangeNormal()
        {
            SpreadSheet spreadSheet = new SpreadSheet(4, 4);

            // setting color to uint black
            spreadSheet.GetCell(1, 1).BGColor = 0x000000;

            uint result = spreadSheet.GetCell(1, 1).BGColor;

            Assert.That(result, Is.EqualTo(0x000000));
        }

        /// <summary>
        /// Tests undo'ing a text change of a cell.
        /// </summary>
        [Test]
        public void TestSpreadSheetTextUndoNormal()
        {
            SpreadSheet spreadSheet = new SpreadSheet(4, 4);

            spreadSheet.GetCell(1, 1).Text = "5";

            UndoRedoCellText undo = new UndoRedoCellText(spreadSheet.GetCell(1, 1), string.Empty);

            spreadSheet.AddUndo(undo);

            spreadSheet.RemoveUndo().ExecuteCommand();

            string result = spreadSheet.GetCell(1, 1).Text;

            // check that text got changed back to empty string.
            Assert.That(result, Is.EqualTo(string.Empty));
        }

        /// <summary>
        /// Tests undo'ing and then redo'ing a text change of a cell resulting in the initial change.
        /// </summary>
        [Test]
        public void TestSpreadSheetTextRedoNormal()
        {
            SpreadSheet spreadSheet = new SpreadSheet(4, 4);

            spreadSheet.GetCell(1, 1).Text = "5";

            UndoRedoCellText undo = new UndoRedoCellText(spreadSheet.GetCell(1, 1), string.Empty);

            spreadSheet.AddUndo(undo);

            spreadSheet.RemoveUndo().ExecuteCommand();

            spreadSheet.AddRedo(undo);

            spreadSheet.RemoveRedo().ExecuteCommand();

            string result = spreadSheet.GetCell(1, 1).Text;

            // check that text got changed back to empty string.
            Assert.That(result, Is.EqualTo("5"));
        }

        /// <summary>
        /// Test the undo'ing of a color change for a cell.
        /// </summary>
        [Test]
        public void TestSpreadSheetColorUndoNormal()
        {
            SpreadSheet spreadSheet = new SpreadSheet(4, 4);

            // need stacks for UndoRedoCellColor even if its just one cell we're changing.
            Stack<Cell> cells = new Stack<Cell>();
            Stack<uint> colors = new Stack<uint>();

            colors.Push(spreadSheet.GetCell(1, 1).BGColor);

            spreadSheet.GetCell(1, 1).BGColor = 0x000000;

            cells.Push(spreadSheet.GetCell(1, 1));

            UndoRedoCellColor undo = new UndoRedoCellColor(cells, colors);

            spreadSheet.AddUndo(undo);

            spreadSheet.RemoveUndo().ExecuteCommand();

            uint result = spreadSheet.GetCell(1, 1).BGColor;

            // check that color is back at white.
            Assert.That(result, Is.EqualTo(0xFFFFFFFF));
        }

        /// <summary>
        /// Tests changing the color of a cell to black, undo'ing back to white, and redo'ing back to black.
        /// </summary>
        [Test]
        public void TestSpreadSheetColorRedoNormal()
        {
            SpreadSheet spreadSheet = new SpreadSheet(4, 4);
            Stack<Cell> cells = new Stack<Cell>();
            Stack<uint> colors = new Stack<uint>();

            colors.Push(spreadSheet.GetCell(1, 1).BGColor);

            spreadSheet.GetCell(1, 1).BGColor = 0x000000;

            cells.Push(spreadSheet.GetCell(1, 1));

            UndoRedoCellColor undo = new UndoRedoCellColor(cells, colors);

            spreadSheet.AddUndo(undo);

            spreadSheet.RemoveUndo().ExecuteCommand();

            spreadSheet.AddRedo(undo);

            spreadSheet.RemoveRedo().ExecuteCommand();

            uint result = spreadSheet.GetCell(1, 1).BGColor;

            // check that color is back at black.
            Assert.That(result, Is.EqualTo(0x000000));
        }

        /// <summary>
        /// Tests loading some text to a cell in a spreadsheet.
        /// </summary>
        [Test]
        public void TestSpreadSheetLoadTextNormal()
        {
            FileStream fs = File.Open("loadTextTest.xml", FileMode.Create, FileAccess.Write);

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            XmlWriter writer = XmlWriter.Create(fs, settings);
            writer.WriteStartElement("spreadsheet");

            writer.WriteStartElement("cell"); // <cell>
            writer.WriteAttributeString("name", "A1"); // <cell name = A1>

            writer.WriteStartElement("text");
            writer.WriteString("hello world");
            writer.WriteEndElement(); // <text>Text<text>

            writer.WriteEndElement(); // </cell>

            writer.WriteEndElement(); // </spreadsheet>

            writer.WriteEndDocument();

            writer.Close();
            fs.Close();

            SpreadSheet spreadSheet = new SpreadSheet(4, 4);

            fs = File.Open("loadTextTest.xml", FileMode.Open, FileAccess.Read);
            spreadSheet.LoadCells(fs);

            Assert.That(spreadSheet.GetCell("A1").Text, Is.EqualTo("hello world"));
        }

        /// <summary>
        /// Tests loading a cell color to the spreadsheet.
        /// </summary>
        [Test]
        public void TestSpreadSheetLoadColorNormal()
        {
            FileStream fs = File.Open("loadColorTest.xml", FileMode.Create, FileAccess.Write);

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            XmlWriter writer = XmlWriter.Create(fs, settings);
            writer.WriteStartElement("spreadsheet");

            writer.WriteStartElement("cell"); // <cell>
            writer.WriteAttributeString("name", "A1"); // <cell name = A1>

            writer.WriteStartElement("bgcolor");
            writer.WriteString("4286595136"); // some shade of brown.
            writer.WriteEndElement(); // <bgcolor>color<bgcolor>

            writer.WriteEndElement(); // </cell>

            writer.WriteEndElement(); // </spreadsheet>

            writer.WriteEndDocument();

            writer.Close();
            fs.Close();

            SpreadSheet spreadSheet = new SpreadSheet(4, 4);

            fs = File.Open("loadColorTest.xml", FileMode.Open, FileAccess.Read);
            spreadSheet.LoadCells(fs);

            Assert.That(spreadSheet.GetCell("A1").BGColor, Is.EqualTo(4286595136));
        }

        /// <summary>
        /// Tests that saving a spreadsheet correctly sets text.
        /// </summary>
        [Test]
        public void TestSpreadSheetColorSaveTextNormal()
        {
            SpreadSheet spreadSheet = new SpreadSheet(4, 4);
            spreadSheet.GetCell(0, 0).Text = "23";
            FileStream fs = File.Open("saveTextTest.xml", FileMode.Create, FileAccess.Write);
            spreadSheet.SaveCells(fs);
            fs.Close();

            fs = File.Open("saveTextTest.xml", FileMode.Open, FileAccess.Read);
            XmlReader reader = XmlReader.Create(fs);

            string result = string.Empty;
            while (reader.Read())
            {
                if (reader.IsStartElement("text"))
                {
                    result = reader.ReadString();
                }
            }

            Assert.That(result.Equals("23"));
        }

        /// <summary>
        /// Tests that saving spreadsheet saves color correctly.
        /// </summary>
        [Test]
        public void TestSpreadSheetColorSaveColorNormal()
        {
            SpreadSheet spreadSheet = new SpreadSheet(4, 4);
            spreadSheet.GetCell(0, 0).BGColor = 0x00ffff; // Aqua color uint.
            FileStream fs = File.Open("saveColorTest.xml", FileMode.Create, FileAccess.Write);
            spreadSheet.SaveCells(fs);
            fs.Close();

            fs = File.Open("saveColorTest.xml", FileMode.Open, FileAccess.Read);
            XmlReader reader = XmlReader.Create(fs);

            uint result = 0;
            while (reader.Read())
            {
                if (reader.IsStartElement("bgcolor"))
                {
                    result = Convert.ToUInt32(reader.ReadString());
                }
            }

            Assert.That(result.Equals(0x00ffff));
        }

        /// <summary>
        /// Tests referencing a cell that's empty.
        /// </summary>
        [Test]
        public void TestSpreadSheetReferenceOutOfBounds()
        {
            SpreadSheet spreadSheet = new SpreadSheet(4, 4);
            spreadSheet.GetCell(0, 0).Text = "=cell3";
            Assert.That(spreadSheet.GetCell(0, 0).Value, Is.EqualTo("Cell doesn't exist!"));
        }

        /// <summary>
        /// Tests referencing a cell that doesn't exist.
        /// </summary>
        [Test]
        public void TestSpreadSheetReferenceEmptyCellNormal()
        {
            SpreadSheet spreadSheet = new SpreadSheet(4, 4);
            spreadSheet.GetCell(0, 0).Text = "=B1";
            Assert.That(spreadSheet.GetCell(0, 0).Value, Is.EqualTo("0"));
        }

        /// <summary>
        /// Tests circular reference.
        /// </summary>
        [Test]
        public void TestSpreadSheetCircularReferenceNormal()
        {
            SpreadSheet spreadSheet = new SpreadSheet(4, 4);
            spreadSheet.GetCell(0, 0).Text = "=B1";
            spreadSheet.GetCell(0, 1).Text = "=A1";
            Assert.That(spreadSheet.GetCell(0, 1).Value, Is.EqualTo("Circular reference!"));
        }

        /// <summary>
        /// Tests circular reference result of other cells.
        /// </summary>
        [Test]
        public void TestSpreadSheetCircularReferenceEdge()
        {
            SpreadSheet spreadSheet = new SpreadSheet(4, 4);
            spreadSheet.GetCell(0, 0).Text = "=B1";
            spreadSheet.GetCell(0, 1).Text = "=A1";
            Assert.That(spreadSheet.GetCell(0, 0).Value, Is.EqualTo("Wrong Format!"));
        }
    }
}