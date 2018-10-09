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

    public partial class MainForm : Form
    {
        private string inputFile = string.Empty;
        private string filePath = string.Empty;
        private int prevIP = 0;

        public MainForm()
        {
            InitializeComponent();
            KeyPreview = true;
            Program.mainForm = this;
            Program.compSim = new ComputerSim(this);
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
                filePath = openFileDialog1.FileName;
                inputFile = sr.ReadToEnd();
                sr.Close();
                reimportFileF4ToolStripMenuItem.Enabled = true;
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
                case Keys.F4:
                    reimportFileF4ToolStripMenuItem.PerformClick();
                    e.Handled = true;
                    break;
                case Keys.F5:
                    runFToolStripMenuItem.PerformClick();
                    e.Handled = true;
                    break;
                case Keys.F6:
                    stepF6ToolStripMenuItem.PerformClick();
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
            if ((isVerbose && verboseToggle.Checked) || !isVerbose)
            {
                if (InvokeRequired)
                {
                    this.Invoke(new Action<string, bool>(PrintOutput), new object[] {text, isVerbose});
                    return;
                }
                outputTextBox.AppendText(text);
            }
        }

        public void AddToDataGrid(int lineNumber, int value)
        {
            dataGridView.Rows[lineNumber].Cells[1].Value = "+" + value.ToString("D10");
        }

        public void AddToProgramGrid(string instruction, string op1, string op2, string op3)
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
            prevIP = 0;
            Program.Reset();
            Program.isLoaded = false;
        }

        public void ResetDataGrid()
        {
            for (int i = 0; i < 1000; ++i)
            {
                dataGridView.Rows.Add(i, string.Empty);
            }
        }

        public void SetMenuItem(string key, bool isEnabled)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string, bool>(SetMenuItem), new object[] { key, isEnabled });
                return;
            }

            switch (key)
            {
                case "F4":
                    reimportFileF4ToolStripMenuItem.Enabled = isEnabled;
                    break;
                case "F5":
                    runFToolStripMenuItem.Enabled = isEnabled;
                    break;
                case "F6":
                    stepF6ToolStripMenuItem.Enabled = isEnabled;
                    break;
                case "F7":
                    resetF7ToolStripMenuItem.Enabled = isEnabled;
                    break;
            }
        }

        public void ProgramLoaded()
        {
            programGridView.Rows[0].DefaultCellStyle.ForeColor = Color.Red;
            programGridView.Rows[0].DefaultCellStyle.SelectionForeColor = Color.Red;
            runFToolStripMenuItem.Enabled = true;
            stepF6ToolStripMenuItem.Enabled = true;
            resetF7ToolStripMenuItem.Enabled = true;
        }

        public void UpdateIP(int line)
        {
            if (line == -1)
            {
                programGridView.Rows[prevIP].DefaultCellStyle.ForeColor = SystemColors.ControlText;
                programGridView.Rows[prevIP].DefaultCellStyle.SelectionForeColor = SystemColors.ControlText;
                return;
            }

            programGridView.Rows[prevIP].DefaultCellStyle.ForeColor = SystemColors.ControlText;
            programGridView.Rows[prevIP].DefaultCellStyle.SelectionForeColor = SystemColors.ControlText;
            programGridView.Rows[line].DefaultCellStyle.ForeColor = Color.Red;
            programGridView.Rows[line].DefaultCellStyle.SelectionForeColor = Color.Red;
            prevIP = line;
        }

        public void DisplayDialogue(string text, string caption)
        {
            MessageBox.Show(text, caption);
        }

        private void resetF7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (inputFile != string.Empty)
                Program.LoadData(inputFile);
            else
                Reset();
        }

        private void runFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (inputFile != string.Empty)
            {
                resetF7ToolStripMenuItem.PerformClick();
                Program.BeginProgram(false);
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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void stepF6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (inputFile != string.Empty && !Program.isRunning && Program.isLoaded)
            {
                Program.BeginProgram(true);
            }
            else if (Program.isRunning)
            {
                Program.waitHandle.Set();
            }
        }

        private void versionBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            versionAToolStripMenuItem.Checked = false;
            versionBToolStripMenuItem.Checked = true;
            Program.langVersion = 'b';
        }

        private void versionAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            versionBToolStripMenuItem.Checked = false;
            versionAToolStripMenuItem.Checked = true;
            Program.langVersion = 'a';
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (versionAToolStripMenuItem.Checked)
            {
                Form aForm = new LanguageAForm();
                aForm.ShowDialog();
            }
            else if (versionBToolStripMenuItem.Checked)
            {
                Form bForm = new LanguageBForm();
                bForm.ShowDialog();
            }
        }

        private void reimportFileF4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(filePath);
            inputFile = sr.ReadToEnd();
            sr.Close();
            Program.LoadData(inputFile);
        }
    }
}
