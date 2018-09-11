namespace VisualInterpreterV2
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.programGridView = new System.Windows.Forms.DataGridView();
            this.LineNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Instruction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Operand1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Operand2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Operand3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inputGridView = new System.Windows.Forms.DataGridView();
            this.Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.outputTextBox = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveOutputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.interpreterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stepF6ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetF7ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.versionAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.versionBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpF1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.verboseToggle = new System.Windows.Forms.CheckBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.reimportFileF4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.programGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputGridView)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridView, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.programGridView, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.inputGridView, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(13, 50);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(775, 240);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Index,
            this.Value});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(3, 3);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowTemplate.ReadOnly = true;
            this.dataGridView.Size = new System.Drawing.Size(252, 234);
            this.dataGridView.TabIndex = 0;
            // 
            // Index
            // 
            this.Index.HeaderText = "#";
            this.Index.Name = "Index";
            this.Index.ReadOnly = true;
            this.Index.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Index.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Index.Width = 50;
            // 
            // Value
            // 
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            this.Value.ReadOnly = true;
            this.Value.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Value.Width = 180;
            // 
            // programGridView
            // 
            this.programGridView.AllowUserToAddRows = false;
            this.programGridView.AllowUserToDeleteRows = false;
            this.programGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            this.programGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.programGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.programGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LineNumber,
            this.Instruction,
            this.Operand1,
            this.Operand2,
            this.Operand3});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.programGridView.DefaultCellStyle = dataGridViewCellStyle4;
            this.programGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.programGridView.Location = new System.Drawing.Point(261, 3);
            this.programGridView.Name = "programGridView";
            this.programGridView.ReadOnly = true;
            this.programGridView.RowHeadersVisible = false;
            this.programGridView.RowTemplate.ReadOnly = true;
            this.programGridView.Size = new System.Drawing.Size(252, 234);
            this.programGridView.TabIndex = 1;
            // 
            // LineNumber
            // 
            this.LineNumber.HeaderText = "Line";
            this.LineNumber.Name = "LineNumber";
            this.LineNumber.ReadOnly = true;
            this.LineNumber.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.LineNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LineNumber.Width = 50;
            // 
            // Instruction
            // 
            this.Instruction.HeaderText = "Intr";
            this.Instruction.MaxInputLength = 2;
            this.Instruction.Name = "Instruction";
            this.Instruction.ReadOnly = true;
            this.Instruction.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Instruction.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Instruction.Width = 48;
            // 
            // Operand1
            // 
            this.Operand1.HeaderText = "Op1";
            this.Operand1.MaxInputLength = 3;
            this.Operand1.Name = "Operand1";
            this.Operand1.ReadOnly = true;
            this.Operand1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Operand1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Operand1.Width = 48;
            // 
            // Operand2
            // 
            this.Operand2.HeaderText = "Op2";
            this.Operand2.MaxInputLength = 3;
            this.Operand2.Name = "Operand2";
            this.Operand2.ReadOnly = true;
            this.Operand2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Operand2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Operand2.Width = 48;
            // 
            // Operand3
            // 
            this.Operand3.HeaderText = "Op3";
            this.Operand3.MaxInputLength = 3;
            this.Operand3.Name = "Operand3";
            this.Operand3.ReadOnly = true;
            this.Operand3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Operand3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Operand3.Width = 48;
            // 
            // inputGridView
            // 
            this.inputGridView.AllowUserToAddRows = false;
            this.inputGridView.AllowUserToDeleteRows = false;
            this.inputGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            this.inputGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.inputGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.inputGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Number,
            this.Value2});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.inputGridView.DefaultCellStyle = dataGridViewCellStyle6;
            this.inputGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputGridView.Location = new System.Drawing.Point(519, 3);
            this.inputGridView.Name = "inputGridView";
            this.inputGridView.ReadOnly = true;
            this.inputGridView.RowHeadersVisible = false;
            this.inputGridView.RowTemplate.ReadOnly = true;
            this.inputGridView.Size = new System.Drawing.Size(253, 234);
            this.inputGridView.TabIndex = 2;
            // 
            // Number
            // 
            this.Number.HeaderText = "#";
            this.Number.Name = "Number";
            this.Number.ReadOnly = true;
            this.Number.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Number.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Number.Width = 50;
            // 
            // Value2
            // 
            this.Value2.HeaderText = "Value";
            this.Value2.Name = "Value2";
            this.Value2.ReadOnly = true;
            this.Value2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Value2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Value2.Width = 180;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.outputTextBox);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(13, 315);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(775, 158);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // outputTextBox
            // 
            this.outputTextBox.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputTextBox.Location = new System.Drawing.Point(3, 3);
            this.outputTextBox.Multiline = true;
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.ReadOnly = true;
            this.outputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.outputTextBox.Size = new System.Drawing.Size(769, 155);
            this.outputTextBox.TabIndex = 0;
            this.outputTextBox.TextChanged += new System.EventHandler(this.outputTextBox_TextChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.interpreterToolStripMenuItem,
            this.languageToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.reimportFileF4ToolStripMenuItem,
            this.saveOutputToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveOutputToolStripMenuItem
            // 
            this.saveOutputToolStripMenuItem.Enabled = false;
            this.saveOutputToolStripMenuItem.Name = "saveOutputToolStripMenuItem";
            this.saveOutputToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveOutputToolStripMenuItem.Text = "Save Output";
            this.saveOutputToolStripMenuItem.Click += new System.EventHandler(this.saveOutputToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // interpreterToolStripMenuItem
            // 
            this.interpreterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runFToolStripMenuItem,
            this.stepF6ToolStripMenuItem,
            this.resetF7ToolStripMenuItem});
            this.interpreterToolStripMenuItem.Name = "interpreterToolStripMenuItem";
            this.interpreterToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.interpreterToolStripMenuItem.Text = "Interpreter";
            // 
            // runFToolStripMenuItem
            // 
            this.runFToolStripMenuItem.Enabled = false;
            this.runFToolStripMenuItem.Name = "runFToolStripMenuItem";
            this.runFToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.runFToolStripMenuItem.Text = "Run    F5";
            this.runFToolStripMenuItem.Click += new System.EventHandler(this.runFToolStripMenuItem_Click);
            // 
            // stepF6ToolStripMenuItem
            // 
            this.stepF6ToolStripMenuItem.Enabled = false;
            this.stepF6ToolStripMenuItem.Name = "stepF6ToolStripMenuItem";
            this.stepF6ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.stepF6ToolStripMenuItem.Text = "Step   F6";
            this.stepF6ToolStripMenuItem.Click += new System.EventHandler(this.stepF6ToolStripMenuItem_Click);
            // 
            // resetF7ToolStripMenuItem
            // 
            this.resetF7ToolStripMenuItem.Enabled = false;
            this.resetF7ToolStripMenuItem.Name = "resetF7ToolStripMenuItem";
            this.resetF7ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.resetF7ToolStripMenuItem.Text = "Reset F7";
            this.resetF7ToolStripMenuItem.Click += new System.EventHandler(this.resetF7ToolStripMenuItem_Click);
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.versionAToolStripMenuItem,
            this.versionBToolStripMenuItem,
            this.toolStripSeparator3,
            this.viewToolStripMenuItem});
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            this.languageToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.languageToolStripMenuItem.Text = "Language";
            // 
            // versionAToolStripMenuItem
            // 
            this.versionAToolStripMenuItem.Checked = true;
            this.versionAToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.versionAToolStripMenuItem.Name = "versionAToolStripMenuItem";
            this.versionAToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.versionAToolStripMenuItem.Text = "Version A";
            this.versionAToolStripMenuItem.Click += new System.EventHandler(this.versionAToolStripMenuItem_Click);
            // 
            // versionBToolStripMenuItem
            // 
            this.versionBToolStripMenuItem.Name = "versionBToolStripMenuItem";
            this.versionBToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.versionBToolStripMenuItem.Text = "Version B";
            this.versionBToolStripMenuItem.Click += new System.EventHandler(this.versionBToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(120, 6);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.viewToolStripMenuItem.Text = "View";
            this.viewToolStripMenuItem.Click += new System.EventHandler(this.viewToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpF1ToolStripMenuItem,
            this.toolStripSeparator2,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // helpF1ToolStripMenuItem
            // 
            this.helpF1ToolStripMenuItem.Name = "helpF1ToolStripMenuItem";
            this.helpF1ToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.helpF1ToolStripMenuItem.Text = "Help F1";
            this.helpF1ToolStripMenuItem.Click += new System.EventHandler(this.helpF1ToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(111, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Data:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(274, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Program:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(532, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Input:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 299);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Output:";
            // 
            // verboseToggle
            // 
            this.verboseToggle.AutoSize = true;
            this.verboseToggle.Location = new System.Drawing.Point(723, 298);
            this.verboseToggle.Name = "verboseToggle";
            this.verboseToggle.Size = new System.Drawing.Size(65, 17);
            this.verboseToggle.TabIndex = 7;
            this.verboseToggle.Text = "Verbose";
            this.verboseToggle.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.verboseToggle.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "p2sol.dat";
            this.openFileDialog1.Filter = "DAT files|*.dat|Text files|*.txt|All files|*.*";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "dat";
            this.saveFileDialog1.Filter = "DAT files|*.dat|Text files|*.txt|All files|*.*";
            // 
            // reimportFileF4ToolStripMenuItem
            // 
            this.reimportFileF4ToolStripMenuItem.Enabled = false;
            this.reimportFileF4ToolStripMenuItem.Name = "reimportFileF4ToolStripMenuItem";
            this.reimportFileF4ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.reimportFileF4ToolStripMenuItem.Text = "Reimport File    F4";
            this.reimportFileF4ToolStripMenuItem.Click += new System.EventHandler(this.reimportFileF4ToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 485);
            this.Controls.Add(this.verboseToggle);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Visual Interpreter V2";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.programGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputGridView)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveOutputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem interpreterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runFToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem stepF6ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetF7ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpF1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridView programGridView;
        private System.Windows.Forms.DataGridView inputGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn Number;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox verboseToggle;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox outputTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn LineNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Instruction;
        private System.Windows.Forms.DataGridViewTextBoxColumn Operand1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Operand2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Operand3;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem versionAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem versionBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reimportFileF4ToolStripMenuItem;
    }
}