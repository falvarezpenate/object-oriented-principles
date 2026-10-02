// <copyright file="Form1.cs" company="Flavio Alvarez Penate">
// Copyright (c) Flavio Alvarez Penate. All rights reserved.
// </copyright>

using System.IO;
using System.Text;

namespace HW3
{
    /// <summary>
    /// Form1 class that derives from Form.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// Constructor for Form1 class.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Method to load the Form with any attributes.
        /// </summary>
        /// <param name="sender"> object type parameter. </param>
        /// <param name="e"> EventArgs type parameter. </param>
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Method to manipulate text inside of a textBox type attribute.
        /// </summary>
        /// <param name="sender"> object type parameter. </param>
        /// <param name="e"> EventArgs type parameter. </param>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Load all text from a file into the textbox, clearing any previous text.
        /// </summary>
        /// <param name="sr"> TextReader object. </param>
        private void LoadText(TextReader sr)
        {
            // Note: removed the ability to print line numbers at the beginning.
            // For elaboration look at comment block under SaveToolStripMenuItem_Click() method.
            this.textBox1.Text = string.Empty; // clear the textbox
            string s = sr.ReadLine();

            // int index = 1;

            // loop until null is returned
            while (s != null)
            {
                // string input = index.ToString() + ". " + s; // has line number in front of line read.
                this.textBox1.AppendText(s);
                this.textBox1.AppendText(Environment.NewLine); // add a new line to replicate format of TextReader opened.
                s = sr.ReadLine(); // read next line

                // index++;
            }
        }

        /// <summary>
        /// Loads text from a file into the textbox.
        /// </summary>
        /// <param name="fileName"> string type that contains file name to open. </param>
        private void LoadFromFile(string fileName)
        {
            // uses LoadText() method by passing a StreamReader. Works since StreamReader inherits from TextReader.
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(fileName))
                {
                    this.LoadText(sr);
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                string errorMessage = "The file could not be read:" + e.Message;
                MessageBox.Show(errorMessage, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Manages any action involving the clicking of "Load File..." menu option.
        /// Look at https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.openfiledialog?view=windowsdesktop-7.0
        /// for more.
        /// </summary>
        /// <param name="sender"> Object type. </param>
        /// <param name="e"> EventArgs type. </param>
        private void LoadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the path of specified file
                    var filePath = openFileDialog.FileName;

                    this.LoadFromFile(filePath);
                }
            }
        }

        /// <summary>
        /// Manages any action involving the clicking of "Load Fibonacci Numbers ( first 50 )" menu option.
        /// </summary>
        /// <param name="sender"> Object type. </param>
        /// <param name="e"> EventArgs type. </param>
        private void LoadFibonacciNumbersFirst50ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FibonacciTextReader ftr = new FibonacciTextReader(50);
            this.LoadText(ftr);
        }

        /// <summary>
        /// Manages any action involving the clicking of "Load Fibonacci Numbers ( first 100 )" menu option.
        /// </summary>
        /// <param name="sender"> Object type. </param>
        /// <param name="e"> EventArgs type. </param>
        private void LoadFibonacciNumbersFirst100ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FibonacciTextReader ftr = new FibonacciTextReader(100);
            this.LoadText(ftr);
        }

        /// <summary>
        /// Manages any action involving the clicking of "Save..." menu option.
        /// </summary>
        /// <param name="sender"> Object type. </param>
        /// <param name="e"> EventArgs type. </param>
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    // Code to write the stream goes here.
                    // string[] splitLine;

                    // Take every line in the textbox, one by one.
                    // For each line, split it at the first space ' '.
                    // Write to the file we're saving to the contents of the second line
                    // in the split line array.
                    // Add a new line to the end of each of these in order to obtain the
                    // correct spacing.
                    /*
                    for (int i = 0; i < this.textBox1.Lines.Length - 1; i++)
                    {
                        string line = this.textBox1.Lines[i];
                        splitLine = line.Split(' ', 2);

                        var textBytes = Encoding.UTF8.GetBytes(splitLine[1] + Environment.NewLine);
                        myStream.Write(textBytes, 0, textBytes.Length);
                    }
                    // Decided to leave this out and remove line numbers
                    // They can cause problems because we are expecting every file that we try to save to have them.
                    // While the auto-generated files from Fibonacci do have them and everything we load does, as well,
                    // if the user starts typing a file and tries to save it without manually entering the
                    // line number every time a new line is started, the save function will crash.
                    */

                    var textBytes = Encoding.UTF8.GetBytes(this.textBox1.Text);
                    myStream.Write(textBytes, 0, textBytes.Length);

                    myStream.Close();
                }
            }
        }
    }
}