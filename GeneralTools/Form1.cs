using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralTools
{
    public partial class Form1 : Form
    {
        string[] files;
        bool AutoMergeFlag = false;
        TextWriter tw;
        public Form1()
        {
            InitializeComponent();
            ConvertForDBcheckBox.CheckedChanged += new EventHandler(ConvertForDBCheckedChanged_Method);
            SelectAllcheckBox.CheckedChanged += new EventHandler(SelectAllCheckedChanged_Method);
            NumberOfColumnstoolTip.SetToolTip(NumberOfColumsQmarkspictureBox, "Number of columns in log file.\r\n" +
                "Lines with less columns are deleted.\r\n" +
                "Raw Files - 48, Non Raw Files - 23.\r\n" +
                "Some versions have other column numbers. If a merged file is small please check.");
            DirectoryNametoolTip.SetToolTip(BrowseQmarkpictureBox, "Directory name cannot contain '_' signs.");
        }

        private void Browsebutton_Click(object sender, EventArgs e)
        {
            int Counter = 0;
            int FileIDStartIndex = 0;
            int FileIDEndIndex = 0;
            int MaxIDSize = 0;
            int MinIDSize = 1;
            int i = 0;
            FilesToMergecheckedListBox.Items.Clear();
            AutoMergeFlag = AutoMergecheckBox.Checked;
            FolderBrowserDialog SelectedFolder = new FolderBrowserDialog();
            
            if (SelectedDirectorytextBox.Text.Equals(""))
            {
                SelectedFolder.SelectedPath = "C:\\";
            }
            else
            {
                SelectedFolder.SelectedPath = SelectedDirectorytextBox.Text.Substring(0, SelectedDirectorytextBox.Text.LastIndexOf('\\'));
            }

            DialogResult result = SelectedFolder.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(SelectedFolder.SelectedPath))
            {
                SelectedDirectorytextBox.Text = SelectedFolder.SelectedPath;
                files = Directory.GetFiles(SelectedFolder.SelectedPath, "*.CSV", SearchOption.AllDirectories);
                MessageBox.Show("Files found: " + files.Length.ToString(), "Message");

                foreach (string file in files)
                {
                    FileIDStartIndex = FindCharOccuranceInText(file, 1, '_');
                    FileIDEndIndex = FindCharOccuranceInText(file, 2, '_');
                    MaxIDSize = Math.Max(MaxIDSize, FileIDEndIndex - FileIDStartIndex - 1);
                    MinIDSize = Math.Min(MinIDSize, FileIDEndIndex - FileIDStartIndex - 1);
                }
                if (!MaxIDSize.Equals(MinIDSize))
                {
                    foreach (string file in files)
                    {
                        FileIDStartIndex = FindCharOccuranceInText(file, 1, '_');
                        FileIDEndIndex = FindCharOccuranceInText(file, 2, '_');
                        string ZeroPad = "";
                        if (!(FileIDEndIndex - FileIDStartIndex - 1).Equals(MaxIDSize))
                        {
                            string fileID = file.Substring(FileIDStartIndex, FileIDEndIndex - FileIDStartIndex - 1);
                            
                            for (i=0;i <MaxIDSize - (FileIDEndIndex - FileIDStartIndex-1 );i++)
                            {
                                ZeroPad += "0";
                            }
                            string NewFileName = file.Replace("_" + fileID + "_", "_" + ZeroPad + fileID + "_");
                            File.Move(file, NewFileName);
                        }
                    }
                }
                files = Directory.GetFiles(SelectedFolder.SelectedPath, "*.CSV", SearchOption.AllDirectories);

                foreach (string file in files)
                {
                    FilesToMergecheckedListBox.Items.Add(file); // .Substring(SelectedFolder.SelectedPath.Length+1)
                    if (AutoMergeFlag)
                    {
                        FilesToMergecheckedListBox.SetItemChecked(Counter, true);
                    }
                    Counter++;
                }
                
            }
        }

        private async void Mergebutton_Click(object sender, EventArgs e)
        {
            int Counter = 0;
            bool DiscardLine = false;
            string t = "";
            AutoMergeFlag = AutoMergecheckBox.Checked;
            if (!AutoMergeFlag)
            {
                SaveFileDialog FileToSave = new SaveFileDialog();

                if (SelectedDirectorytextBox.Text.Equals(""))
                {
                    FileToSave.InitialDirectory = "C:\\";
                }
                else
                {
                    FileToSave.InitialDirectory = SelectedDirectorytextBox.Text.Substring(0, SelectedDirectorytextBox.Text.LastIndexOf('\\'));
                }

                FileToSave.Filter = "CSV file (*.CSV) | *.CSV | All files (*.*) | *.*";
                FileToSave.FilterIndex = 2;
                FileToSave.RestoreDirectory = false;
                if (FileToSave.ShowDialog() == DialogResult.OK)
                {

                    File.Create(FileToSave.FileName).Dispose();
                    tw = new StreamWriter(FileToSave.FileName, true);
                    foreach (string f in FilesToMergecheckedListBox.CheckedItems)
                    {
                        StreamReader reader = new StreamReader(f);
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            if (!line.Equals(""))
                            {
                                var values = line.Split(',');
                                if (values.Count() > 2)
                                {
                                    if ((values[0].Contains("started") || values[1].Contains("Gyroscope data[deg/sec]")
                                    || (values[1].Contains("X") && values[2].Contains("Y"))
                                    || values[1].Contains("!Start log switch")) && (Counter > 0))
                                    {
                                        DiscardLine = true;
                                    }
                                    else
                                    {
                                        DiscardLine = false;
                                        if (values.Count() > 5)
                                        {
                                            t = "";
                                        }
                                        if (!values.Count().Equals(Convert.ToInt32(NumberOfColummstextBox.Text)))
                                        {
                                            DiscardLine = true;
                                        }
                                    }
                                }
                                else if (values.Count().Equals(2))
                                {
                                    if ((values[0].Contains("started") || values[1].Contains("Gyroscope data[deg/sec]")
                                    || (values[1].Contains("X"))
                                    || values[1].Contains("!Start log switch")) && (Counter > 0))
                                    {
                                        DiscardLine = true;
                                    }
                                    else
                                    {
                                        DiscardLine = false;
                                        if (ConvertForDBcheckBox.Checked)
                                        {
                                            int i = 0;
                                            t = "";
                                            for (i = 0; i <= Convert.ToInt32(NumberOfColummstextBox.Text); i++)
                                            {
                                                t += ",";
                                            }
                                        }
                                    }
                                }
                                else if (values.Count().Equals(1))
                                {
                                    if ((values[0].Contains("started")) && (Counter > 0))
                                    {
                                        DiscardLine = true;
                                    }
                                    else
                                    {
                                        DiscardLine = false;
                                    }
                                }
                                if (!DiscardLine)
                                {
                                    if (ConvertForDBcheckBox.Checked)
                                    {
                                        line = t + line;
                                    }
                                    await tw.WriteLineAsync(line);
                                }
                            }
                        }
                        reader.Close();
                        Counter++;
                    }
                }
                tw.Close();
            }
            else
            {
                bool[] AreConsecFilesArray = new bool[FilesToMergecheckedListBox.CheckedItems.Count];
                string PreviousFile = "";
                string CurrentFile = "";
                if (FilesToMergecheckedListBox.CheckedItems.Count > 1)
                {
                    
                    foreach (string f in FilesToMergecheckedListBox.CheckedItems)
                    {
                        CurrentFile = f;
                        if ((Counter <= FilesToMergecheckedListBox.CheckedItems.Count) && (Counter >= 1))
                        {
                            int PrevFileStart = PreviousFile.IndexOf("LOG_");
                            int CurrFileStart = PreviousFile.IndexOf("LOG_");
                            AreConsecFilesArray[Counter] = AreConsecutiveFiles(PreviousFile.Substring(PrevFileStart), CurrentFile.Substring(CurrFileStart));
                        }
                        Counter++;
                        PreviousFile = CurrentFile;
                    }
                }
                Counter = 0;
                foreach (bool b in AreConsecFilesArray)
                {
                    if (b.Equals(false)) // File is not consecutive generate new file
                    {
                        string TempFileName = FilesToMergecheckedListBox.CheckedItems[Counter].ToString();
                        TempFileName = TempFileName.Replace(".CSV", "_Merged.CSV");

                        File.Create(TempFileName).Dispose();
                        tw = new StreamWriter(TempFileName, true);
                        StreamReader reader = new StreamReader(FilesToMergecheckedListBox.CheckedItems[Counter].ToString());
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            if (!line.Equals(""))
                            {
                                var values = line.Split(',');
                                if (values.Count() > 2)
                                {
                                    if ((values[0].Contains("started") || values[1].Contains("Gyroscope data[deg/sec]")
                                    || (values[1].Contains("X") && values[2].Contains("Y"))
                                    || values[1].Contains("!Start log switch")) && (Counter > 0))
                                    {
                                        DiscardLine = true;
                                    }
                                    else
                                    {
                                        DiscardLine = false;
                                        if (values.Count() > 5)
                                        {
                                            t = "";
                                        }
                                        if (!values.Count().Equals(Convert.ToInt32(NumberOfColummstextBox.Text)))
                                        {
                                            DiscardLine = true;
                                        }
                                    }
                                }
                                else if (values.Count().Equals(2))
                                {
                                    if ((values[0].Contains("started") || values[1].Contains("Gyroscope data[deg/sec]")
                                    || (values[1].Contains("X"))
                                    || values[1].Contains("!Start log switch")) && (Counter > 0))
                                    {
                                        DiscardLine = true;
                                    }
                                    else
                                    {
                                        DiscardLine = false;
                                        if (ConvertForDBcheckBox.Checked)
                                        {
                                            int i = 0;
                                            t = "";
                                            for (i = 0; i <= Convert.ToInt32(NumberOfColummstextBox.Text); i++)
                                            {
                                                t += ",";
                                            }
                                        }
                                    }
                                }
                                else if (values.Count().Equals(1))
                                {
                                    if ((values[0].Contains("started")) && (Counter > 0))
                                    {
                                        DiscardLine = true;
                                    }
                                    else
                                    {
                                        DiscardLine = false;
                                    }
                                }
                                if (!DiscardLine)
                                {
                                    if (ConvertForDBcheckBox.Checked)
                                    {
                                        line = t + line;
                                    }
                                    await tw.WriteLineAsync(line);
                                }
                            }
                        }
                        reader.Close();
                        if ( (Counter < FilesToMergecheckedListBox.CheckedItems.Count-1))
                        {
                            if (AreConsecFilesArray[Counter + 1].Equals(true))//consecutive->Do not close File
                            {

                            }
                            else if (AreConsecFilesArray[Counter + 1].Equals(false)) //Not consecutive -> Close File
                            {
                                tw.Close();
                            }
                        }
                        else if (Counter.Equals(FilesToMergecheckedListBox.CheckedItems.Count))
                        {
                            //Last File -> Close
                            tw.Close();
                        }


                    }
                    else // File is consecutive -> Continue Old File
                    {
                        StreamReader reader = new StreamReader(FilesToMergecheckedListBox.CheckedItems[Counter].ToString());
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            if (!line.Equals(""))
                            {
                                var values = line.Split(',');
                                if (values.Count() > 2)
                                {
                                    if ((values[0].Contains("started") || values[1].Contains("Gyroscope data[deg/sec]")
                                    || (values[1].Contains("X") && values[2].Contains("Y"))
                                    || values[1].Contains("!Start log switch")) && (Counter > 0))
                                    {
                                        DiscardLine = true;
                                    }
                                    else
                                    {
                                        DiscardLine = false;
                                        if (values.Count() > 5)
                                        {
                                            t = "";
                                        }
                                        if (!values.Count().Equals(Convert.ToInt32(NumberOfColummstextBox.Text)))
                                        {
                                            DiscardLine = true;
                                        }
                                    }
                                }
                                else if (values.Count().Equals(2))
                                {
                                    if ((values[0].Contains("started") || values[1].Contains("Gyroscope data[deg/sec]")
                                    || (values[1].Contains("X"))
                                    || values[1].Contains("!Start log switch")) && (Counter > 0))
                                    {
                                        DiscardLine = true;
                                    }
                                    else
                                    {
                                        DiscardLine = false;
                                        if (ConvertForDBcheckBox.Checked)
                                        {
                                            int i = 0;
                                            t = "";
                                            for (i = 0; i <= Convert.ToInt32(NumberOfColummstextBox.Text); i++)
                                            {
                                                t += ",";
                                            }
                                        }
                                    }
                                }
                                else if (values.Count().Equals(1))
                                {
                                    if ((values[0].Contains("started")) && (Counter > 0))
                                    {
                                        DiscardLine = true;
                                    }
                                    else
                                    {
                                        DiscardLine = false;
                                    }
                                }
                                if (!DiscardLine)
                                {
                                    if (ConvertForDBcheckBox.Checked)
                                    {
                                        line = t + line;
                                    }
                                    await tw.WriteLineAsync(line);
                                }
                            }
                        }
                        reader.Close();
                        if ((Counter < FilesToMergecheckedListBox.CheckedItems.Count - 1))
                        {
                            if (AreConsecFilesArray[Counter + 1].Equals(true))//consecutive->Do not close File
                            {

                            }
                            else if (AreConsecFilesArray[Counter + 1].Equals(false)) //Not consecutive -> Close File
                            {
                                tw.Close();
                            }
                        }
                        else if (Counter.Equals(FilesToMergecheckedListBox.CheckedItems.Count-1))
                        {
                            //Last File -> Close
                            tw.Close();
                        }
                    }
                    Counter++;
                }
            }
            
            MessageBox.Show("Files Merged Successfully.", "Message");
        }
        private bool AreConsecutiveFiles(string PrevFile, string CurrFile)
        {
            int Pos = PrevFile.LastIndexOf("_");
            int PrevFileCSVPos = PrevFile.LastIndexOf(".CSV");
            int CurrFileCSVPos = CurrFile.LastIndexOf(".CSV");
            int CurrFileStartTimeIndex = 0;
            TimeSpan PrevFileTimeSpan = new TimeSpan();
            TimeSpan CurrFileTimeSpan = new TimeSpan();
            if (Pos >= 36)
            {
                //LOG_1_01-01-2000_00-00-15_01-01-2000_00-15-27.CSV
                string PrevFileEndTime = PrevFile.Substring(PrevFileCSVPos - 8, 8);
                CurrFileStartTimeIndex = FindCharOccuranceInText(CurrFile, 3, '_');
                string CurrFileStartTime = CurrFile.Substring(CurrFileStartTimeIndex, 8);
                PrevFileTimeSpan = TimeSpan.Parse(PrevFileEndTime.Replace('-',':'));
                CurrFileTimeSpan = TimeSpan.Parse(CurrFileStartTime.Replace('-', ':'));
                if (CurrFileTimeSpan.TotalSeconds - PrevFileTimeSpan.TotalSeconds < 8)
                {
                    return true;
                }
                else
                {
                    return false;
                }  
            }
            else
            {
                return false;
            }
            
        }
        private int FindCharOccuranceInText(string stringToSearchIn, int Occurance, char CharToSearch)
        {
            int Index = 0;
            int OccuranceCounter = 0;
            foreach (char c in stringToSearchIn)
            {
                if (c.Equals(CharToSearch)) 
                {
                    OccuranceCounter++;
                }
                if (OccuranceCounter.Equals(Occurance))
                {
                    break;
                }
                Index++;
            }
            return Index+1;
        }
        private void ConvertForDBCheckedChanged_Method(object sender, EventArgs e)
        {
            if (ConvertForDBcheckBox.Checked)
            {
                NumberOfColumnslabel.Visible = true;
                NumberOfColummstextBox.Visible = true;
            }
            else
            {
                //NumberOfColumnslabel.Visible = false;
                //NumberOfColummstextBox.Visible = false;
            }
        }

        private void SelectAllCheckedChanged_Method(object sender, EventArgs e)
        {
            int i = 0;
            for (i=0;i < FilesToMergecheckedListBox.Items.Count; i++)
            {
                FilesToMergecheckedListBox.SetItemChecked(i, SelectAllcheckBox.Checked);
            }
        }
    }
}
