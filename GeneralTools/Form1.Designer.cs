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
            this.MaintabControl = new System.Windows.Forms.TabControl();
            this.FileMergertabPage = new System.Windows.Forms.TabPage();
            this.SelectedDirectorytextBox = new System.Windows.Forms.TextBox();
            this.SelectedDirectorylabel = new System.Windows.Forms.Label();
            this.Browsebutton = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.AutoMergecheckBox = new System.Windows.Forms.CheckBox();
            this.ConvertForDBcheckBox = new System.Windows.Forms.CheckBox();
            this.NumberOfColumnslabel = new System.Windows.Forms.Label();
            this.NumberOfColummstextBox = new System.Windows.Forms.TextBox();
            this.NumberOfColumnstoolTip = new System.Windows.Forms.ToolTip(this.components);
            this.DirectoryNametoolTip = new System.Windows.Forms.ToolTip(this.components);
            this.BrowseQmarkpictureBox = new System.Windows.Forms.PictureBox();
            this.NumberOfColumsQmarkspictureBox = new System.Windows.Forms.PictureBox();
            this.SelectAllcheckBox = new System.Windows.Forms.CheckBox();
            this.Mergebutton = new System.Windows.Forms.Button();
            this.FilesToMergecheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.MaintabControl.SuspendLayout();
            this.FileMergertabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BrowseQmarkpictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumberOfColumsQmarkspictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // MaintabControl
            // 
            this.MaintabControl.Controls.Add(this.FileMergertabPage);
            this.MaintabControl.Controls.Add(this.tabPage2);
            this.MaintabControl.Location = new System.Drawing.Point(38, 13);
            this.MaintabControl.Name = "MaintabControl";
            this.MaintabControl.SelectedIndex = 0;
            this.MaintabControl.Size = new System.Drawing.Size(733, 399);
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
            this.FileMergertabPage.Size = new System.Drawing.Size(725, 373);
            this.FileMergertabPage.TabIndex = 0;
            this.FileMergertabPage.Text = "File Merger";
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
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(725, 373);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
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
            // NumberOfColumnslabel
            // 
            this.NumberOfColumnslabel.AutoSize = true;
            this.NumberOfColumnslabel.Location = new System.Drawing.Point(117, 77);
            this.NumberOfColumnslabel.Name = "NumberOfColumnslabel";
            this.NumberOfColumnslabel.Size = new System.Drawing.Size(71, 13);
            this.NumberOfColumnslabel.TabIndex = 7;
            this.NumberOfColumnslabel.Text = "# of columns:";
            // 
            // NumberOfColummstextBox
            // 
            this.NumberOfColummstextBox.Location = new System.Drawing.Point(192, 74);
            this.NumberOfColummstextBox.Name = "NumberOfColummstextBox";
            this.NumberOfColummstextBox.Size = new System.Drawing.Size(34, 20);
            this.NumberOfColummstextBox.TabIndex = 8;
            this.NumberOfColummstextBox.Text = "48";
            // 
            // NumberOfColumnstoolTip
            // 
            this.NumberOfColumnstoolTip.ShowAlways = true;
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.MaintabControl);
            this.Name = "Form1";
            this.Text = "ParaTools V1.0.5";
            this.MaintabControl.ResumeLayout(false);
            this.FileMergertabPage.ResumeLayout(false);
            this.FileMergertabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BrowseQmarkpictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumberOfColumsQmarkspictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl MaintabControl;
        private System.Windows.Forms.TabPage FileMergertabPage;
        private System.Windows.Forms.TabPage tabPage2;
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
    }
}

