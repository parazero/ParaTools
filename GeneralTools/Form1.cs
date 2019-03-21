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
        public Form1()
        {
            InitializeComponent();
        }

        private void Browsebutton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog SelectedFolder = new FolderBrowserDialog();
            SelectedFolder.SelectedPath = "c:\\";
            DialogResult result = SelectedFolder.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(SelectedFolder.SelectedPath))
            {
                SelectedDirectorytextBox.Text = SelectedFolder.SelectedPath;
                files = Directory.GetFiles(SelectedFolder.SelectedPath, "*.CSV", SearchOption.AllDirectories);
                MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
                foreach(string file in files)
                {
                    FilesToMergecheckedListBox.Items.Add(file); // .Substring(SelectedFolder.SelectedPath.Length+1)
                }
            }
        }

        private async void Mergebutton_Click(object sender, EventArgs e)
        {
            int Counter = 0;
            bool DiscardLine = false;
            SaveFileDialog FileToSave = new SaveFileDialog();
            FileToSave.InitialDirectory = "C:\\";
            FileToSave.Filter = "CSV file (*.CSV) | *.CSV | All files (*.*) | *.*";
            FileToSave.FilterIndex = 1;
            FileToSave.RestoreDirectory = false;
            if (FileToSave.ShowDialog() == DialogResult.OK)
            {
                File.Create(FileToSave.FileName).Dispose();
                TextWriter tw = new StreamWriter(FileToSave.FileName,true);
                foreach (string f in FilesToMergecheckedListBox.CheckedItems)
                {
                    StreamReader reader = new StreamReader(f);
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (!line.Equals(""))
                        {
                            var values = line.Split(',');
                            if (values.Count()>2)
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
                tw.Close();
                MessageBox.Show("Files Merged Successfully.", "Message");
            }
        }
    }
}
