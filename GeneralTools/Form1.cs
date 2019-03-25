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
            AutoMergeFlag = AutoMergecheckBox.Checked;
        }

        private void Browsebutton_Click(object sender, EventArgs e)
        {
            int Counter = 0;
            FilesToMergecheckedListBox.Items.Clear();
            FolderBrowserDialog SelectedFolder = new FolderBrowserDialog();
            SelectedFolder.SelectedPath = "C:\\";
            DialogResult result = SelectedFolder.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(SelectedFolder.SelectedPath))
            {
                SelectedDirectorytextBox.Text = SelectedFolder.SelectedPath;
                files = Directory.GetFiles(SelectedFolder.SelectedPath, "*.CSV", SearchOption.AllDirectories);
                MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
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
            
            if (!AutoMergeFlag)
            {
                SaveFileDialog FileToSave = new SaveFileDialog();
                FileToSave.InitialDirectory = "C:\\";
                FileToSave.Filter = "CSV file (*.CSV) | *.CSV | All files (*.*) | *.*";
                FileToSave.FilterIndex = 1;
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
                                    await tw.WriteLineAsync(line);
                                }
                            }
                        }
                        reader.Close();
                        if ( (Counter < FilesToMergecheckedListBox.CheckedItems.Count) && AreConsecFilesArray[Counter+1].Equals(true))
                        {
                            //consecutive->Do not close File
                        }
                        else if ((Counter < FilesToMergecheckedListBox.CheckedItems.Count) && AreConsecFilesArray[Counter + 1].Equals(false))
                        {
                            //Not consecutive -> Close File
                            tw.Close();
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
    }
}
