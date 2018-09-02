using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace VisualInterpreterV2
{

    public partial class Form1 : Form
    {
        private string inputFile = string.Empty;

        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;
            Program.mainForm = this;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName);
                inputFile = sr.ReadToEnd();
                Program.LoadData(inputFile);
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    helpF1ToolStripMenuItem.PerformClick();
                    e.Handled = true;
                    break;
                case Keys.F5:
                    runFToolStripMenuItem.PerformClick();
                    e.Handled = true;
                    break;
                case Keys.F6:
                    //if (this.menuStep.Enabled)
                    //    this.step();
                    e.Handled = true;
                    break;
                case Keys.F7:
                    resetF7ToolStripMenuItem.PerformClick();
                    e.Handled = true;
                    break;
            }
        }

        private void helpF1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form helpForm = new HelpForm();
            helpForm.ShowDialog();
        }

        public void PrintOutput(string text, bool isVerbose)
        {
            if (isVerbose == verboseToggle.Checked)
            {
                outputTextBox.AppendText(text);
            }
        }

        public void AddToDataGrid(int lineNumber, string value)
        {
            dataGridView.Rows[lineNumber].Cells[1].Value = value;
        }

        public void AddToProgramGrid(string instruction, int op1, int op2, int op3)
        {
            programGridView.Rows.Add(programGridView.RowCount, instruction, op1, op2, op3);
        }

        public void AddToInputGrid(string value)
        {
            inputGridView.Rows.Add(inputGridView.RowCount, value);
        }

        public void Reset()
        {
            dataGridView.Rows.Clear();
            programGridView.Rows.Clear();
            inputGridView.Rows.Clear();

            outputTextBox.Text = string.Empty;
        }

        public void ResetDataGrid()
        {
            for (int i = 0; i < 1000; ++i)
            {
                dataGridView.Rows.Add(i, "0");
            }
        }

        private void resetF7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reset();
            if (inputFile != string.Empty)
                Program.LoadData(inputFile);
        }

        private void runFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (inputFile != string.Empty)
            {
                Console.WriteLine("beginning");
                Program.BeginProgram();
            }
        }

        private void outputTextBox_TextChanged(object sender, EventArgs e)
        {
            if (outputTextBox.Text.Length > 0)
                saveOutputToolStripMenuItem.Enabled = true;
            else
            {
                saveOutputToolStripMenuItem.Enabled = false;
            }
        }

        private void saveOutputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialogResult = saveFileDialog1.ShowDialog();

            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.File.WriteAllText(saveFileDialog1.FileName, outputTextBox.Text);
            }
        }
    }
}
