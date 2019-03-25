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
            this.MaintabControl = new System.Windows.Forms.TabControl();
            this.FileMergertabPage = new System.Windows.Forms.TabPage();
            this.SelectedDirectorytextBox = new System.Windows.Forms.TextBox();
            this.SelectedDirectorylabel = new System.Windows.Forms.Label();
            this.Mergebutton = new System.Windows.Forms.Button();
            this.FilesToMergecheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.Browsebutton = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.AutoMergecheckBox = new System.Windows.Forms.CheckBox();
            this.MaintabControl.SuspendLayout();
            this.FileMergertabPage.SuspendLayout();
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
            this.FileMergertabPage.Controls.Add(this.AutoMergecheckBox);
            this.FileMergertabPage.Controls.Add(this.SelectedDirectorytextBox);
            this.FileMergertabPage.Controls.Add(this.SelectedDirectorylabel);
            this.FileMergertabPage.Controls.Add(this.Mergebutton);
            this.FileMergertabPage.Controls.Add(this.FilesToMergecheckedListBox);
            this.FileMergertabPage.Controls.Add(this.Browsebutton);
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
            this.SelectedDirectorytextBox.Location = new System.Drawing.Point(7, 24);
            this.SelectedDirectorytextBox.Name = "SelectedDirectorytextBox";
            this.SelectedDirectorytextBox.Size = new System.Drawing.Size(224, 20);
            this.SelectedDirectorytextBox.TabIndex = 4;
            // 
            // SelectedDirectorylabel
            // 
            this.SelectedDirectorylabel.AutoSize = true;
            this.SelectedDirectorylabel.Location = new System.Drawing.Point(7, 7);
            this.SelectedDirectorylabel.Name = "SelectedDirectorylabel";
            this.SelectedDirectorylabel.Size = new System.Drawing.Size(97, 13);
            this.SelectedDirectorylabel.TabIndex = 3;
            this.SelectedDirectorylabel.Text = "Selected Directory:";
            // 
            // Mergebutton
            // 
            this.Mergebutton.Location = new System.Drawing.Point(237, 133);
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
            this.FilesToMergecheckedListBox.Location = new System.Drawing.Point(7, 63);
            this.FilesToMergecheckedListBox.Name = "FilesToMergecheckedListBox";
            this.FilesToMergecheckedListBox.Size = new System.Drawing.Size(224, 94);
            this.FilesToMergecheckedListBox.TabIndex = 1;
            // 
            // Browsebutton
            // 
            this.Browsebutton.Location = new System.Drawing.Point(227, 24);
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
            this.AutoMergecheckBox.Location = new System.Drawing.Point(309, 26);
            this.AutoMergecheckBox.Name = "AutoMergecheckBox";
            this.AutoMergecheckBox.Size = new System.Drawing.Size(81, 17);
            this.AutoMergecheckBox.TabIndex = 5;
            this.AutoMergecheckBox.Text = "Auto Merge";
            this.AutoMergecheckBox.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.MaintabControl);
            this.Name = "Form1";
            this.Text = "Form1";
            this.MaintabControl.ResumeLayout(false);
            this.FileMergertabPage.ResumeLayout(false);
            this.FileMergertabPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl MaintabControl;
        private System.Windows.Forms.TabPage FileMergertabPage;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button Mergebutton;
        private System.Windows.Forms.CheckedListBox FilesToMergecheckedListBox;
        private System.Windows.Forms.Button Browsebutton;
        private System.Windows.Forms.TextBox SelectedDirectorytextBox;
        private System.Windows.Forms.Label SelectedDirectorylabel;
        private System.Windows.Forms.CheckBox AutoMergecheckBox;
    }
}

