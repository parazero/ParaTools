namespace GeneralTools
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.MaintabControl = new System.Windows.Forms.TabControl();
            this.FileMergertabPage = new System.Windows.Forms.TabPage();
            this.FilesToMergecheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.Mergebutton = new System.Windows.Forms.Button();
            this.SelectAllcheckBox = new System.Windows.Forms.CheckBox();
            this.NumberOfColumsQmarkspictureBox = new System.Windows.Forms.PictureBox();
            this.BrowseQmarkpictureBox = new System.Windows.Forms.PictureBox();
            this.Browsebutton = new System.Windows.Forms.Button();
            this.NumberOfColummstextBox = new System.Windows.Forms.TextBox();
            this.NumberOfColumnslabel = new System.Windows.Forms.Label();
            this.ConvertForDBcheckBox = new System.Windows.Forms.CheckBox();
            this.AutoMergecheckBox = new System.Windows.Forms.CheckBox();
            this.SelectedDirectorytextBox = new System.Windows.Forms.TextBox();
            this.SelectedDirectorylabel = new System.Windows.Forms.Label();
            this.UpdateConfigtabPage = new System.Windows.Forms.TabPage();
            this.RefreshPortsbutton = new System.Windows.Forms.Button();
            this.Progresslabel = new System.Windows.Forms.Label();
            this.ConfigurationprogressBar = new System.Windows.Forms.ProgressBar();
            this.Updatebutton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SmartAirPortcomboBox = new System.Windows.Forms.ComboBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.outputTextBox = new System.Windows.Forms.TextBox();
            this.encryptBinFile = new System.Windows.Forms.Button();
            this.selectBinFile = new System.Windows.Forms.Button();
            this.fileNameTextBox = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.MLNumOfPointstextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.PreviousTimeFormatcheckBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.NuOfColumsML = new System.Windows.Forms.TextBox();
            this.Deletebutton = new System.Windows.Forms.Button();
            this.MLBrowse = new System.Windows.Forms.Button();
            this.MLlistView = new System.Windows.Forms.ListView();
            this.FileNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FileSizeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FilePathHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.hoverToolStripMenuItem = new System.Windows.Forms.ToolStripTextBox();
            this.generalFlightToolStripMenuItem = new System.Windows.Forms.ToolStripTextBox();
            this.landingToolStripMenuItem = new System.Windows.Forms.ToolStripTextBox();
            this.resetSelectedPointsToolStripMenuItem = new System.Windows.Forms.ToolStripTextBox();
            this.NumberOfColumnstoolTip = new System.Windows.Forms.ToolTip(this.components);
            this.DirectoryNametoolTip = new System.Windows.Forms.ToolTip(this.components);
            this.RTOSFormatcheckBox = new System.Windows.Forms.CheckBox();
            this.MaintabControl.SuspendLayout();
            this.FileMergertabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumberOfColumsQmarkspictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BrowseQmarkpictureBox)).BeginInit();
            this.UpdateConfigtabPage.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MaintabControl
            // 
            this.MaintabControl.Controls.Add(this.FileMergertabPage);
            this.MaintabControl.Controls.Add(this.UpdateConfigtabPage);
            this.MaintabControl.Controls.Add(this.tabPage1);
            this.MaintabControl.Controls.Add(this.tabPage2);
            this.MaintabControl.Location = new System.Drawing.Point(38, 13);
            this.MaintabControl.Name = "MaintabControl";
            this.MaintabControl.SelectedIndex = 0;
            this.MaintabControl.Size = new System.Drawing.Size(1272, 547);
            this.MaintabControl.TabIndex = 0;
            // 
            // FileMergertabPage
            // 
            this.FileMergertabPage.BackColor = System.Drawing.SystemColors.Control;
            this.FileMergertabPage.Controls.Add(this.FilesToMergecheckedListBox);
            this.FileMergertabPage.Controls.Add(this.Mergebutton);
            this.FileMergertabPage.Controls.Add(this.SelectAllcheckBox);
            this.FileMergertabPage.Controls.Add(this.NumberOfColumsQmarkspictureBox);
            this.FileMergertabPage.Controls.Add(this.BrowseQmarkpictureBox);
            this.FileMergertabPage.Controls.Add(this.Browsebutton);
            this.FileMergertabPage.Controls.Add(this.NumberOfColummstextBox);
            this.FileMergertabPage.Controls.Add(this.NumberOfColumnslabel);
            this.FileMergertabPage.Controls.Add(this.ConvertForDBcheckBox);
            this.FileMergertabPage.Controls.Add(this.AutoMergecheckBox);
            this.FileMergertabPage.Controls.Add(this.SelectedDirectorytextBox);
            this.FileMergertabPage.Controls.Add(this.SelectedDirectorylabel);
            this.FileMergertabPage.Location = new System.Drawing.Point(4, 22);
            this.FileMergertabPage.Name = "FileMergertabPage";
            this.FileMergertabPage.Padding = new System.Windows.Forms.Padding(3);
            this.FileMergertabPage.Size = new System.Drawing.Size(1264, 521);
            this.FileMergertabPage.TabIndex = 0;
            this.FileMergertabPage.Text = "File Merger";
            // 
            // FilesToMergecheckedListBox
            // 
            this.FilesToMergecheckedListBox.CheckOnClick = true;
            this.FilesToMergecheckedListBox.FormattingEnabled = true;
            this.FilesToMergecheckedListBox.HorizontalScrollbar = true;
            this.FilesToMergecheckedListBox.Location = new System.Drawing.Point(10, 123);
            this.FilesToMergecheckedListBox.Name = "FilesToMergecheckedListBox";
            this.FilesToMergecheckedListBox.Size = new System.Drawing.Size(224, 94);
            this.FilesToMergecheckedListBox.Sorted = true;
            this.FilesToMergecheckedListBox.TabIndex = 1;
            // 
            // Mergebutton
            // 
            this.Mergebutton.Location = new System.Drawing.Point(240, 193);
            this.Mergebutton.Name = "Mergebutton";
            this.Mergebutton.Size = new System.Drawing.Size(75, 23);
            this.Mergebutton.TabIndex = 2;
            this.Mergebutton.Text = "Merge";
            this.Mergebutton.UseVisualStyleBackColor = true;
            this.Mergebutton.Click += new System.EventHandler(this.Mergebutton_Click);
            // 
            // SelectAllcheckBox
            // 
            this.SelectAllcheckBox.AutoSize = true;
            this.SelectAllcheckBox.Checked = true;
            this.SelectAllcheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SelectAllcheckBox.Location = new System.Drawing.Point(13, 101);
            this.SelectAllcheckBox.Name = "SelectAllcheckBox";
            this.SelectAllcheckBox.Size = new System.Drawing.Size(94, 17);
            this.SelectAllcheckBox.TabIndex = 9;
            this.SelectAllcheckBox.Text = "Select All Files";
            this.SelectAllcheckBox.UseVisualStyleBackColor = true;
            // 
            // NumberOfColumsQmarkspictureBox
            // 
            this.NumberOfColumsQmarkspictureBox.Image = ((System.Drawing.Image)(resources.GetObject("NumberOfColumsQmarkspictureBox.Image")));
            this.NumberOfColumsQmarkspictureBox.Location = new System.Drawing.Point(232, 74);
            this.NumberOfColumsQmarkspictureBox.Name = "NumberOfColumsQmarkspictureBox";
            this.NumberOfColumsQmarkspictureBox.Size = new System.Drawing.Size(22, 22);
            this.NumberOfColumsQmarkspictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.NumberOfColumsQmarkspictureBox.TabIndex = 11;
            this.NumberOfColumsQmarkspictureBox.TabStop = false;
            // 
            // BrowseQmarkpictureBox
            // 
            this.BrowseQmarkpictureBox.Image = ((System.Drawing.Image)(resources.GetObject("BrowseQmarkpictureBox.Image")));
            this.BrowseQmarkpictureBox.Location = new System.Drawing.Point(319, 24);
            this.BrowseQmarkpictureBox.Name = "BrowseQmarkpictureBox";
            this.BrowseQmarkpictureBox.Size = new System.Drawing.Size(22, 22);
            this.BrowseQmarkpictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BrowseQmarkpictureBox.TabIndex = 10;
            this.BrowseQmarkpictureBox.TabStop = false;
            // 
            // Browsebutton
            // 
            this.Browsebutton.Location = new System.Drawing.Point(241, 24);
            this.Browsebutton.Name = "Browsebutton";
            this.Browsebutton.Size = new System.Drawing.Size(75, 21);
            this.Browsebutton.TabIndex = 0;
            this.Browsebutton.Text = "Browse";
            this.Browsebutton.UseVisualStyleBackColor = true;
            this.Browsebutton.Click += new System.EventHandler(this.Browsebutton_Click);
            // 
            // NumberOfColummstextBox
            // 
            this.NumberOfColummstextBox.Location = new System.Drawing.Point(192, 74);
            this.NumberOfColummstextBox.Name = "NumberOfColummstextBox";
            this.NumberOfColummstextBox.Size = new System.Drawing.Size(34, 20);
            this.NumberOfColummstextBox.TabIndex = 8;
            this.NumberOfColummstextBox.Text = "48";
            // 
            // NumberOfColumnslabel
            // 
            this.NumberOfColumnslabel.AutoSize = true;
            this.NumberOfColumnslabel.Location = new System.Drawing.Point(117, 77);
            this.NumberOfColumnslabel.Name = "NumberOfColumnslabel";
            this.NumberOfColumnslabel.Size = new System.Drawing.Size(71, 13);
            this.NumberOfColumnslabel.TabIndex = 7;
            this.NumberOfColumnslabel.Text = "# of columns:";
            // 
            // ConvertForDBcheckBox
            // 
            this.ConvertForDBcheckBox.AutoSize = true;
            this.ConvertForDBcheckBox.Location = new System.Drawing.Point(13, 76);
            this.ConvertForDBcheckBox.Name = "ConvertForDBcheckBox";
            this.ConvertForDBcheckBox.Size = new System.Drawing.Size(99, 17);
            this.ConvertForDBcheckBox.TabIndex = 6;
            this.ConvertForDBcheckBox.Text = "Convert For DB";
            this.ConvertForDBcheckBox.UseVisualStyleBackColor = true;
            // 
            // AutoMergecheckBox
            // 
            this.AutoMergecheckBox.AutoSize = true;
            this.AutoMergecheckBox.Checked = true;
            this.AutoMergecheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AutoMergecheckBox.Location = new System.Drawing.Point(13, 52);
            this.AutoMergecheckBox.Name = "AutoMergecheckBox";
            this.AutoMergecheckBox.Size = new System.Drawing.Size(81, 17);
            this.AutoMergecheckBox.TabIndex = 5;
            this.AutoMergecheckBox.Text = "Auto Merge";
            this.AutoMergecheckBox.UseVisualStyleBackColor = true;
            // 
            // SelectedDirectorytextBox
            // 
            this.SelectedDirectorytextBox.Enabled = false;
            this.SelectedDirectorytextBox.Location = new System.Drawing.Point(11, 24);
            this.SelectedDirectorytextBox.Name = "SelectedDirectorytextBox";
            this.SelectedDirectorytextBox.Size = new System.Drawing.Size(234, 20);
            this.SelectedDirectorytextBox.TabIndex = 4;
            // 
            // SelectedDirectorylabel
            // 
            this.SelectedDirectorylabel.AutoSize = true;
            this.SelectedDirectorylabel.Location = new System.Drawing.Point(11, 7);
            this.SelectedDirectorylabel.Name = "SelectedDirectorylabel";
            this.SelectedDirectorylabel.Size = new System.Drawing.Size(97, 13);
            this.SelectedDirectorylabel.TabIndex = 3;
            this.SelectedDirectorylabel.Text = "Selected Directory:";
            // 
            // UpdateConfigtabPage
            // 
            this.UpdateConfigtabPage.BackColor = System.Drawing.SystemColors.Control;
            this.UpdateConfigtabPage.Controls.Add(this.RefreshPortsbutton);
            this.UpdateConfigtabPage.Controls.Add(this.Progresslabel);
            this.UpdateConfigtabPage.Controls.Add(this.ConfigurationprogressBar);
            this.UpdateConfigtabPage.Controls.Add(this.Updatebutton);
            this.UpdateConfigtabPage.Controls.Add(this.label1);
            this.UpdateConfigtabPage.Controls.Add(this.SmartAirPortcomboBox);
            this.UpdateConfigtabPage.Location = new System.Drawing.Point(4, 22);
            this.UpdateConfigtabPage.Name = "UpdateConfigtabPage";
            this.UpdateConfigtabPage.Padding = new System.Windows.Forms.Padding(3);
            this.UpdateConfigtabPage.Size = new System.Drawing.Size(1264, 521);
            this.UpdateConfigtabPage.TabIndex = 1;
            this.UpdateConfigtabPage.Text = "Update Configuration";
            // 
            // RefreshPortsbutton
            // 
            this.RefreshPortsbutton.Location = new System.Drawing.Point(359, 31);
            this.RefreshPortsbutton.Name = "RefreshPortsbutton";
            this.RefreshPortsbutton.Size = new System.Drawing.Size(75, 23);
            this.RefreshPortsbutton.TabIndex = 5;
            this.RefreshPortsbutton.Text = "Refresh Ports";
            this.RefreshPortsbutton.UseVisualStyleBackColor = true;
            this.RefreshPortsbutton.Click += new System.EventHandler(this.RefreshPortsbutton_Click);
            // 
            // Progresslabel
            // 
            this.Progresslabel.AutoSize = true;
            this.Progresslabel.Location = new System.Drawing.Point(28, 72);
            this.Progresslabel.Name = "Progresslabel";
            this.Progresslabel.Size = new System.Drawing.Size(51, 13);
            this.Progresslabel.TabIndex = 4;
            this.Progresslabel.Text = "Progress:";
            // 
            // ConfigurationprogressBar
            // 
            this.ConfigurationprogressBar.Location = new System.Drawing.Point(28, 91);
            this.ConfigurationprogressBar.Name = "ConfigurationprogressBar";
            this.ConfigurationprogressBar.Size = new System.Drawing.Size(299, 23);
            this.ConfigurationprogressBar.TabIndex = 3;
            // 
            // Updatebutton
            // 
            this.Updatebutton.Location = new System.Drawing.Point(252, 31);
            this.Updatebutton.Name = "Updatebutton";
            this.Updatebutton.Size = new System.Drawing.Size(75, 23);
            this.Updatebutton.TabIndex = 2;
            this.Updatebutton.Text = "Update";
            this.Updatebutton.UseVisualStyleBackColor = true;
            this.Updatebutton.Click += new System.EventHandler(this.Updatebutton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Com Port:";
            // 
            // SmartAirPortcomboBox
            // 
            this.SmartAirPortcomboBox.FormattingEnabled = true;
            this.SmartAirPortcomboBox.Location = new System.Drawing.Point(98, 33);
            this.SmartAirPortcomboBox.Name = "SmartAirPortcomboBox";
            this.SmartAirPortcomboBox.Size = new System.Drawing.Size(121, 21);
            this.SmartAirPortcomboBox.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.outputTextBox);
            this.tabPage1.Controls.Add(this.encryptBinFile);
            this.tabPage1.Controls.Add(this.selectBinFile);
            this.tabPage1.Controls.Add(this.fileNameTextBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1264, 521);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Version Encryptor";
            // 
            // outputTextBox
            // 
            this.outputTextBox.Location = new System.Drawing.Point(28, 168);
            this.outputTextBox.Multiline = true;
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.ReadOnly = true;
            this.outputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.outputTextBox.Size = new System.Drawing.Size(640, 160);
            this.outputTextBox.TabIndex = 3;
            // 
            // encryptBinFile
            // 
            this.encryptBinFile.Location = new System.Drawing.Point(160, 106);
            this.encryptBinFile.Name = "encryptBinFile";
            this.encryptBinFile.Size = new System.Drawing.Size(75, 23);
            this.encryptBinFile.TabIndex = 2;
            this.encryptBinFile.Text = "Encrypt";
            this.encryptBinFile.UseVisualStyleBackColor = true;
            this.encryptBinFile.Click += new System.EventHandler(this.encryptBinFile_Click);
            // 
            // selectBinFile
            // 
            this.selectBinFile.Location = new System.Drawing.Point(28, 107);
            this.selectBinFile.Name = "selectBinFile";
            this.selectBinFile.Size = new System.Drawing.Size(75, 23);
            this.selectBinFile.TabIndex = 1;
            this.selectBinFile.Text = "Select File";
            this.selectBinFile.UseVisualStyleBackColor = true;
            this.selectBinFile.Click += new System.EventHandler(this.selectBinFile_Click);
            // 
            // fileNameTextBox
            // 
            this.fileNameTextBox.Location = new System.Drawing.Point(28, 42);
            this.fileNameTextBox.Name = "fileNameTextBox";
            this.fileNameTextBox.ReadOnly = true;
            this.fileNameTextBox.Size = new System.Drawing.Size(640, 20);
            this.fileNameTextBox.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.RTOSFormatcheckBox);
            this.tabPage2.Controls.Add(this.MLNumOfPointstextBox);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.chart1);
            this.tabPage2.Controls.Add(this.PreviousTimeFormatcheckBox);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.NuOfColumsML);
            this.tabPage2.Controls.Add(this.Deletebutton);
            this.tabPage2.Controls.Add(this.MLBrowse);
            this.tabPage2.Controls.Add(this.MLlistView);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1264, 521);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "ML Filter";
            // 
            // MLNumOfPointstextBox
            // 
            this.MLNumOfPointstextBox.Location = new System.Drawing.Point(840, 21);
            this.MLNumOfPointstextBox.Name = "MLNumOfPointstextBox";
            this.MLNumOfPointstextBox.ReadOnly = true;
            this.MLNumOfPointstextBox.Size = new System.Drawing.Size(31, 20);
            this.MLNumOfPointstextBox.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(774, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "# Of Points:";
            // 
            // chart1
            // 
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            this.chart1.Location = new System.Drawing.Point(774, 70);
            this.chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Name = "MLBaroSeries";
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(484, 445);
            this.chart1.TabIndex = 7;
            this.chart1.Text = "MLBarochart";
            // 
            // PreviousTimeFormatcheckBox
            // 
            this.PreviousTimeFormatcheckBox.AutoSize = true;
            this.PreviousTimeFormatcheckBox.Location = new System.Drawing.Point(570, 38);
            this.PreviousTimeFormatcheckBox.Name = "PreviousTimeFormatcheckBox";
            this.PreviousTimeFormatcheckBox.Size = new System.Drawing.Size(121, 17);
            this.PreviousTimeFormatcheckBox.TabIndex = 6;
            this.PreviousTimeFormatcheckBox.Text = "Previous time format";
            this.PreviousTimeFormatcheckBox.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(567, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Columns:";
            // 
            // NuOfColumsML
            // 
            this.NuOfColumsML.Location = new System.Drawing.Point(623, 18);
            this.NuOfColumsML.Name = "NuOfColumsML";
            this.NuOfColumsML.Size = new System.Drawing.Size(96, 20);
            this.NuOfColumsML.TabIndex = 4;
            this.NuOfColumsML.Text = "28";
            // 
            // Deletebutton
            // 
            this.Deletebutton.Location = new System.Drawing.Point(119, 17);
            this.Deletebutton.Name = "Deletebutton";
            this.Deletebutton.Size = new System.Drawing.Size(75, 23);
            this.Deletebutton.TabIndex = 3;
            this.Deletebutton.Text = "Delete";
            this.Deletebutton.UseVisualStyleBackColor = true;
            this.Deletebutton.Click += new System.EventHandler(this.Deletebutton_Click);
            // 
            // MLBrowse
            // 
            this.MLBrowse.Location = new System.Drawing.Point(6, 18);
            this.MLBrowse.Name = "MLBrowse";
            this.MLBrowse.Size = new System.Drawing.Size(75, 23);
            this.MLBrowse.TabIndex = 2;
            this.MLBrowse.Text = "Browse";
            this.MLBrowse.UseVisualStyleBackColor = true;
            this.MLBrowse.Click += new System.EventHandler(this.MLBrowse_Click);
            // 
            // MLlistView
            // 
            this.MLlistView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FileNameHeader,
            this.FileSizeHeader,
            this.FilePathHeader});
            this.MLlistView.ContextMenuStrip = this.contextMenuStrip1;
            this.MLlistView.FullRowSelect = true;
            this.MLlistView.GridLines = true;
            this.MLlistView.HideSelection = false;
            this.MLlistView.Location = new System.Drawing.Point(6, 108);
            this.MLlistView.Name = "MLlistView";
            this.MLlistView.Size = new System.Drawing.Size(713, 203);
            this.MLlistView.TabIndex = 1;
            this.MLlistView.UseCompatibleStateImageBehavior = false;
            this.MLlistView.View = System.Windows.Forms.View.Details;
            // 
            // FileNameHeader
            // 
            this.FileNameHeader.Text = "File Name";
            this.FileNameHeader.Width = 197;
            // 
            // FileSizeHeader
            // 
            this.FileSizeHeader.Text = "File Size";
            this.FileSizeHeader.Width = 81;
            // 
            // FilePathHeader
            // 
            this.FilePathHeader.Text = "File Path";
            this.FilePathHeader.Width = 338;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.resetSelectedPointsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(301, 76);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.ReadOnly = true;
            this.toolStripMenuItem1.Size = new System.Drawing.Size(180, 23);
            this.toolStripMenuItem1.Text = "Show Graph";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1,
            this.hoverToolStripMenuItem,
            this.generalFlightToolStripMenuItem,
            this.landingToolStripMenuItem});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(300, 22);
            this.toolStripMenuItem2.Text = "Export";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.ReadOnly = true;
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBox1.Text = "TakeOff";
            this.toolStripTextBox1.Click += new System.EventHandler(this.toolStripTextBox1_Click);
            // 
            // hoverToolStripMenuItem
            // 
            this.hoverToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.hoverToolStripMenuItem.Name = "hoverToolStripMenuItem";
            this.hoverToolStripMenuItem.ReadOnly = true;
            this.hoverToolStripMenuItem.Size = new System.Drawing.Size(180, 23);
            this.hoverToolStripMenuItem.Text = "Hover";
            // 
            // generalFlightToolStripMenuItem
            // 
            this.generalFlightToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.generalFlightToolStripMenuItem.Name = "generalFlightToolStripMenuItem";
            this.generalFlightToolStripMenuItem.ReadOnly = true;
            this.generalFlightToolStripMenuItem.Size = new System.Drawing.Size(240, 23);
            this.generalFlightToolStripMenuItem.Text = "General Flight";
            // 
            // landingToolStripMenuItem
            // 
            this.landingToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.landingToolStripMenuItem.Name = "landingToolStripMenuItem";
            this.landingToolStripMenuItem.ReadOnly = true;
            this.landingToolStripMenuItem.Size = new System.Drawing.Size(300, 23);
            this.landingToolStripMenuItem.Text = "Landing";
            // 
            // resetSelectedPointsToolStripMenuItem
            // 
            this.resetSelectedPointsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.resetSelectedPointsToolStripMenuItem.Name = "resetSelectedPointsToolStripMenuItem";
            this.resetSelectedPointsToolStripMenuItem.ReadOnly = true;
            this.resetSelectedPointsToolStripMenuItem.Size = new System.Drawing.Size(240, 23);
            this.resetSelectedPointsToolStripMenuItem.Text = "Reset Selected Points";
            this.resetSelectedPointsToolStripMenuItem.Click += new System.EventHandler(this.resetSelectedPointsToolStripMenuItem_Click);
            // 
            // NumberOfColumnstoolTip
            // 
            this.NumberOfColumnstoolTip.ShowAlways = true;
            // 
            // RTOSFormatcheckBox
            // 
            this.RTOSFormatcheckBox.AutoSize = true;
            this.RTOSFormatcheckBox.Checked = true;
            this.RTOSFormatcheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RTOSFormatcheckBox.Location = new System.Drawing.Point(570, 62);
            this.RTOSFormatcheckBox.Name = "RTOSFormatcheckBox";
            this.RTOSFormatcheckBox.Size = new System.Drawing.Size(88, 17);
            this.RTOSFormatcheckBox.TabIndex = 10;
            this.RTOSFormatcheckBox.Text = "RTOS format";
            this.RTOSFormatcheckBox.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1322, 572);
            this.Controls.Add(this.MaintabControl);
            this.Name = "Form1";
            this.Text = "ParaTools V1.0.8";
            this.MaintabControl.ResumeLayout(false);
            this.FileMergertabPage.ResumeLayout(false);
            this.FileMergertabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumberOfColumsQmarkspictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BrowseQmarkpictureBox)).EndInit();
            this.UpdateConfigtabPage.ResumeLayout(false);
            this.UpdateConfigtabPage.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl MaintabControl;
        private System.Windows.Forms.TabPage FileMergertabPage;
        private System.Windows.Forms.TabPage UpdateConfigtabPage;
        private System.Windows.Forms.Button Browsebutton;
        private System.Windows.Forms.TextBox SelectedDirectorytextBox;
        private System.Windows.Forms.Label SelectedDirectorylabel;
        private System.Windows.Forms.CheckBox AutoMergecheckBox;
        private System.Windows.Forms.CheckBox ConvertForDBcheckBox;
        private System.Windows.Forms.TextBox NumberOfColummstextBox;
        private System.Windows.Forms.Label NumberOfColumnslabel;
        private System.Windows.Forms.ToolTip NumberOfColumnstoolTip;
        private System.Windows.Forms.ToolTip DirectoryNametoolTip;
        private System.Windows.Forms.PictureBox NumberOfColumsQmarkspictureBox;
        private System.Windows.Forms.PictureBox BrowseQmarkpictureBox;
        private System.Windows.Forms.CheckedListBox FilesToMergecheckedListBox;
        private System.Windows.Forms.Button Mergebutton;
        private System.Windows.Forms.CheckBox SelectAllcheckBox;
        private System.Windows.Forms.Button Updatebutton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox SmartAirPortcomboBox;
        private System.Windows.Forms.Label Progresslabel;
        private System.Windows.Forms.ProgressBar ConfigurationprogressBar;
        private System.Windows.Forms.Button RefreshPortsbutton;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button encryptBinFile;
        private System.Windows.Forms.Button selectBinFile;
        private System.Windows.Forms.TextBox fileNameTextBox;
        private System.Windows.Forms.TextBox outputTextBox;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button MLBrowse;
        private System.Windows.Forms.ListView MLlistView;
        private System.Windows.Forms.ColumnHeader FileNameHeader;
        private System.Windows.Forms.ColumnHeader FileSizeHeader;
        private System.Windows.Forms.ColumnHeader FilePathHeader;
        private System.Windows.Forms.Button Deletebutton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripTextBox toolStripMenuItem1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox NuOfColumsML;
        private System.Windows.Forms.CheckBox PreviousTimeFormatcheckBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripTextBox hoverToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox generalFlightToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox landingToolStripMenuItem;
        private System.Windows.Forms.TextBox MLNumOfPointstextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripTextBox resetSelectedPointsToolStripMenuItem;
        private System.Windows.Forms.CheckBox RTOSFormatcheckBox;
    }
}

