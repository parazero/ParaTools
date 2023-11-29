using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using Curit.Module.RTX.Com;
using System.Runtime.InteropServices;
using ParaZero.SmartAirInfra;
using ParaZero.KalmanInfra;
using ParaZero.GeometryInfra;
using ParaZero.StatisticsInfra;
using static System.Windows.Forms.ListView;
using System.Windows.Forms.DataVisualization.Charting;

namespace GeneralTools
{
    public partial class Form1 : Form
    {
        string[] files;
        bool AutoMergeFlag = false;
        TextWriter tw;
        SerialPort _serialPort;
        List<string> ParamsThatCauseReset = new List<string> { "arm", "usd", "imu" };
        int SleepDurationAfterBoardInitMethod = 4500;
        bool IdentifiedText = false;
        bool WaitForInit = false;

        private bool EEFinished = false;
        private bool TestTimeout = false;
        string FullInitText = "";
        string FullTextSmartAir = "";
        private int SleepAfterWriteLineEvent = 3000;

        string shortFileName = "";
        string selectedFolder = "";

        int DoubleClickCounter = 0;
        int FirstXPos = 0;
        int SecondXPos = 0;

        List<string> AHRSPitch = new List<string>();
        List<string> AHRSRoll = new List<string>();
        List<string> AHRSYaw = new List<string>();

        AHRSIMUStruct LocalAHRSIMU = new AHRSIMUStruct();

        SmartAirLogRepresentaion LogFile = new SmartAirLogRepresentaion();

        public Form1()
        {
            InitializeComponent();
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.MouseMove += new MouseEventHandler(MouseHover_HitTest);
            chart1.MouseDoubleClick += new MouseEventHandler(MouseDoubleClick_Test);
            ConvertForDBcheckBox.CheckedChanged += new EventHandler(ConvertForDBCheckedChanged_Method);
            SelectAllcheckBox.CheckedChanged += new EventHandler(SelectAllCheckedChanged_Method);
            NumberOfColumnstoolTip.SetToolTip(NumberOfColumsQmarkspictureBox, "Number of columns in log file.\r\n" +
                "Lines with less columns are deleted.\r\n" +
                "Raw Files - 48, Non Raw Files - 23.\r\n" +
                "Some versions have other column numbers. If a merged file is small please check.");
            DirectoryNametoolTip.SetToolTip(BrowseQmarkpictureBox, "Directory name cannot contain '_' signs.");

            LocalAHRSIMU._q0 = 1;
            LocalAHRSIMU._q1 = 0;
            LocalAHRSIMU._q2 = 0;
            LocalAHRSIMU._q3 = 0;
            LocalAHRSIMU._beta = 0.3f;
            LocalAHRSIMU._sampleRate = 1.0f / 10;
            LocalAHRSIMU.Inclination = 3.5f;

            _serialPort = new SerialPort();
            // Allow the user to set the appropriate properties.
            foreach (string s in SerialPort.GetPortNames())
            {
                SmartAirPortcomboBox.Items.Add(s);
            }
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

                            for (i = 0; i < MaxIDSize - (FileIDEndIndex - FileIDStartIndex - 1); i++)
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
                        else if (Counter.Equals(FilesToMergecheckedListBox.CheckedItems.Count - 1))
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
                PrevFileTimeSpan = TimeSpan.Parse(PrevFileEndTime.Replace('-', ':'));
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
            return Index + 1;
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
            for (i = 0; i < FilesToMergecheckedListBox.Items.Count; i++)
            {
                FilesToMergecheckedListBox.SetItemChecked(i, SelectAllcheckBox.Checked);
            }
        }
        // Display Port values and prompt user to enter a port.
        public static string SetPortName(string defaultPortName)
        {
            string portName;

            Console.WriteLine("Available Ports:");
            foreach (string s in SerialPort.GetPortNames())
            {
                Console.WriteLine("   {0}", s);
            }

            Console.Write("Enter COM port value (Default: {0}): ", defaultPortName);
            portName = Console.ReadLine();

            if (portName == "" || !(portName.ToLower()).StartsWith("com"))
            {
                portName = defaultPortName;
            }
            return portName;
        }

        // Display BaudRate values and prompt user to enter a value.
        public static int SetPortBaudRate(int defaultPortBaudRate)
        {
            string baudRate;

            Console.Write("Baud Rate(default:{0}): ", defaultPortBaudRate);
            baudRate = Console.ReadLine();

            if (baudRate == "")
            {
                baudRate = defaultPortBaudRate.ToString();
            }

            return int.Parse(baudRate);
        }

        // Display PortParity values and prompt user to enter a value.
        public static Parity SetPortParity(Parity defaultPortParity)
        {
            string parity;

            Console.WriteLine("Available Parity options:");
            foreach (string s in Enum.GetNames(typeof(Parity)))
            {
                Console.WriteLine("   {0}", s);
            }

            Console.Write("Enter Parity value (Default: {0}):", defaultPortParity.ToString(), true);
            parity = Console.ReadLine();

            if (parity == "")
            {
                parity = defaultPortParity.ToString();
            }

            return (Parity)Enum.Parse(typeof(Parity), parity, true);
        }

        // Display DataBits values and prompt user to enter a value.
        public static int SetPortDataBits(int defaultPortDataBits)
        {
            string dataBits;

            Console.Write("Enter DataBits value (Default: {0}): ", defaultPortDataBits);
            dataBits = Console.ReadLine();

            if (dataBits == "")
            {
                dataBits = defaultPortDataBits.ToString();
            }

            return int.Parse(dataBits.ToUpperInvariant());
        }

        // Display StopBits values and prompt user to enter a value.
        public static StopBits SetPortStopBits(StopBits defaultPortStopBits)
        {
            string stopBits;

            Console.WriteLine("Available StopBits options:");
            foreach (string s in Enum.GetNames(typeof(StopBits)))
            {
                Console.WriteLine("   {0}", s);
            }

            Console.Write("Enter StopBits value (None is not supported and \n" +
             "raises an ArgumentOutOfRangeException. \n (Default: {0}):", defaultPortStopBits.ToString());
            stopBits = Console.ReadLine();

            if (stopBits == "")
            {
                stopBits = defaultPortStopBits.ToString();
            }

            return (StopBits)Enum.Parse(typeof(StopBits), stopBits, true);
        }

        public static Handshake SetPortHandshake(Handshake defaultPortHandshake)
        {
            string handshake;

            Console.WriteLine("Available Handshake options:");
            foreach (string s in Enum.GetNames(typeof(Handshake)))
            {
                Console.WriteLine("   {0}", s);
            }

            Console.Write("Enter Handshake value (Default: {0}):", defaultPortHandshake.ToString());
            handshake = Console.ReadLine();

            if (handshake == "")
            {
                handshake = defaultPortHandshake.ToString();
            }

            return (Handshake)Enum.Parse(typeof(Handshake), handshake, true);
        }

        private void Updatebutton_Click(object sender, EventArgs e)
        {
            string SelectedPort = "";
            string LocslSMAText = "";
            string EndCondition = "";
            string MCUID = "";
            string FileName = "";
            bool ContinueFlash = false;

            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "bin files (*.bin)|*.bin|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                    fileNameTextBox.Text = filePath;
                    shortFileName = openFileDialog.SafeFileName;

                    ////Read the contents of the file into a stream
                    //var fileStream = openFileDialog.OpenFile();

                    //using (StreamReader reader = new StreamReader(fileStream))
                    //{
                    //    fileContent = reader.ReadToEnd();
                    //}
                }
            }
            SelectedPort = SmartAirPortcomboBox.Text;
            _serialPort.PortName = SelectedPort;
            _serialPort.BaudRate = 921600;//115200;//
            _serialPort.Parity = 0;
            _serialPort.DataBits = 8;
            _serialPort.StopBits = (StopBits)1;
            _serialPort.Handshake = 0;
            _serialPort.ReadBufferSize = 500000;

            _serialPort.Open();
            FullTextSmartAir = "";
            long geoFenceFileLength = new System.IO.FileInfo(filePath).Length;

            WriteToSmartAir("del \"GeoFence.txt\"");
            Thread.Sleep(3500);

            WriteToSmartAir("LGF " + geoFenceFileLength.ToString());
            Thread.Sleep(3500);

            StreamReader reader = new StreamReader(filePath);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (!line.Equals(""))
                {
                    WriteToSmartAir(line.Replace("\t"," "));
                    //Thread.Sleep(3500);
                }
            }

                    //WriteToSmartAir("ee?");
                    ////byte[] aaa = new byte[11];
                    ////aaa[0] = 0xB5;
                    ////aaa[1] = 0x62;
                    ////aaa[2] = 0x06;
                    ////aaa[3] = 0x01;
                    ////aaa[4] = 0x03;
                    ////aaa[5] = 0x00;
                    ////aaa[6] = 0x01;
                    ////aaa[7] = 0x06;
                    ////aaa[8] = 0x01;

                    ////byte CK_A = 0, CK_B = 0;
                    ////for (int I = 2; I < 6; I++)
                    ////{
                    ////    CK_A = CK_A + aaa[I];
                    ////    CK_B = CK_B + CK_A;
                    ////}

                    ////aaa[9] = 0x16;
                    ////aaa[10] = 0x75;
                    ////_serialPort.Write(aaa, 0, 11);
                    ////WriteToSmartAir("$PUBX,41,1,0023,0001,115200,0*1C\r\n");
                    ////WriteToSmartAir("$JATT,NMEAHE,0\r\n$JASC,GPGGA,5\r\n$JASC,GPRMC,5\r\n$JASC,GPVTG,5\r\n$JASC,GPHDT,5\r\n$JMODE,SBASR,YES\r\n");
                    //FullTextSmartAir = "";
                    ////while (true)
                    ////{
                    ////    FullTextSmartAir += _serialPort.ReadExisting();
                    ////    Thread.Sleep(10);
                    ////    FullTextSmartAir = "";
                    ////}
                    //if (FullTextSmartAir.Contains("MCU ID.....................:"))
                    //{
                    //    MCUID = FullTextSmartAir.Substring(FullTextSmartAir.IndexOf("MCU ID.....................:") + 29, 26);
                    //    MCUID = MCUID.Replace(" ", "_");
                    //}

                    //WriteToSmartAir("trg 2");
                    //WriteToSmartAir("FWU");
                    //FullTextSmartAir = "";
                    //WriteToSmartAir("SMA");

                    ////_serialPort.DataReceived -= new SerialDataReceivedEventHandler(DataReceivedHandler);
                    //while (!FullTextSmartAir.Contains("Waiting for the SmartAir file to be sent"))
                    //{
                    //    FullTextSmartAir += _serialPort.ReadExisting();
                    //    //textBox1.Invoke(textBoxUpdateDelegate, new Object[] { textBox1, FullText, true });
                    //    Thread.Sleep(10);
                    //}

                    //LegacyCommunication a = new LegacyCommunication(_serialPort, true);
                    //EndCondition = "FW programming completed Successfully."; // return
                    //bool SMAFlashResult = a.sendBinaryFile(".\\NANO_OSR_256.bin", this, EndCondition);// return
                    ////bool SMAFlashResult = true;
                    //if (!SMAFlashResult)
                    //{
                    //    //textBox1.Invoke(textBoxUpdateDelegate, new Object[] { textBox1, "SMA Flash Failed.", true });
                    //    ContinueFlash = false;
                    //    SendToSMAChars("a");
                    //    //SMAFlashStatuspictureBox.Image = StatusIcons._1194989231691813435led_circle_red_svg_thumb;
                    //    MessageBox.Show("Firmware Did Not Update.", "Message");
                    //}
                    //else
                    //{
                    //    ContinueFlash = true;
                    //    FullTextSmartAir = "";
                    //    WriteToSmartAir("end");
                    //    //SMAFlashStatuspictureBox.Image = StatusIcons._11949892282132520602led_circle_green_svg_thumb;
                    //    //SMAStatus = "Passed";
                    //    //MessageBox.Show("Firmware Was Updated successfully.", "Message");
                    //}

                    //while (!FullTextSmartAir.Contains("!Initialization.............: Finished successfully."))
                    //{
                    //    WriteToSmartAir("end");
                    //    Thread.Sleep(5000);
                    //}
                    //FullTextSmartAir = "";
                    //WriteToSmartAir("eee");
                    //WriteToSmartAir("rst");
                    //while (!FullTextSmartAir.Contains("!Initialization.............: Finished successfully.") && (!FullTextSmartAir.Contains("!Incorrect orientation......:")))
                    //{
                    //    if ((_serialPort.BytesToRead > 0))
                    //    {
                    //        string indata = _serialPort.ReadExisting();
                    //        if (!indata.Equals(""))
                    //        {
                    //            //Console.WriteLine(indata);
                    //            FullTextSmartAir += indata;
                    //        }
                    //        else
                    //        {
                    //            Thread.Sleep(250);
                    //        }
                    //    }
                    //    Thread.Sleep(5000);
                    //}
                    //WriteToSmartAir("trg 2");
                    //WriteToSmartAir("atg");
                    //Thread.Sleep(5000);
                    //WriteToSmartAir("atg");
                    //WriteToSmartAir("trg 1");
                    //FullTextSmartAir = "";
                    //SetBaseValues();
                    //FullTextSmartAir = "";
                    //WriteToSmartAir("ee?");
                    //Thread.Sleep(3000);
                    //if (FullTextSmartAir.Contains("!IMU Configuration.......IMU: 23") &&
                    //       FullTextSmartAir.Contains("!Att ROLL Lim...............................[deg]ATTR: 35.0") &&
                    //       FullTextSmartAir.Contains("!Att PITCH Lim..............................[deg]ATTP: 35.0") &&
                    //       FullTextSmartAir.Contains("!Angular speed threshold for critical angle.[d/s]ATTS: 100") &&
                    //       FullTextSmartAir.Contains("!Angles Test Cycles...........................[n].ATC: 3") &&
                    //       FullTextSmartAir.Contains("Mode....................TRG: 1") &&
                    //       FullTextSmartAir.Contains("!Freefall duration...........................[ms].FFC: 300") &&
                    //       FullTextSmartAir.Contains("!Freefall limit...........................[m/s^2].FFL: 4.0") &&
                    //       FullTextSmartAir.Contains("!Height Test Cycles...........................[n].HTC: 5") &&
                    //       FullTextSmartAir.Contains("!Delta Height Thresh..........................[m].DHT: -0.110") &&
                    //       FullTextSmartAir.Contains("!Critical Delta Height Thresh.................[m]CDHT: -0.300") &&
                    //       FullTextSmartAir.Contains("!RC Configuration........RCE: 0") &&
                    //       FullTextSmartAir.Contains("!Attitude Rate Roll angle threshold.........[deg]ATRR: 10") &&
                    //       FullTextSmartAir.Contains("!Attitude Rate Pitch angle threshold........[deg]ATRP: 10") &&
                    //       FullTextSmartAir.Contains("!Angular Speed Roll/Pitch limit.........[deg/sec]ATRL: 150.0") &&
                    //       FullTextSmartAir.Contains("!Angular Speed Yaw limit................[deg/sec].YRL: 180.0") &&
                    //       FullTextSmartAir.Contains("!ARM/DIS 1 dur.  [100ms]ADFD: 40")
                    //       && FullTextSmartAir.Contains("!Roll calibr.value.[deg].RCV: 0.0")
                    //       && FullTextSmartAir.Contains("!Pitch calibr.value[deg].PCV: 0.0")
                    //       && FullTextSmartAir.Contains("!ARM height..........[m].ARH: 5.0")
                    //       && FullTextSmartAir.Contains("!Vibrations value.[m/s2].VIB: 0.08")
                    //       && FullTextSmartAir.Contains("!No vibrations time..[s].NVI: 5")
                    //       && FullTextSmartAir.Contains("!Vibrations time.....[s].VIT: 10")
                    //       && FullTextSmartAir.Contains("!Auto DISARM.....[1+2+4]DISE: 2")
                    //       && FullTextSmartAir.Contains("!Auto ARM..........[1+2]ARME: 3")
                    //       && FullTextSmartAir.Contains("Start ARM mode..........ARM: 2")
                    //       && FullTextSmartAir.Contains("!Trigger PWM on/off......PWM: 1")
                    //       && FullTextSmartAir.Contains("!Trigger Delay...........MTD: 1")
                    //       )

                    //{
                    //    MessageBox.Show("Configuration Update Ended Successfully.", "Message");
                    //    if (!MCUID.Equals(""))
                    //    {
                    //        //Directory.CreateDirectory(Sorucepath + "\\FlashLog\\"); //+ MCUID + "\\" + DateTime.UtcNow.Year.ToString() + "-" + DateTime.UtcNow.Month.ToString() + "-" + DateTime.UtcNow.Day.ToString() + "\\"
                    //        FileName = MCUID + ".txt";//Path.GetTempFileName();
                    //        FileStream fs = File.Open(FileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                    //        fs.Close();
                    //    }
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Configuration Did Not Update.", "Message");
                    //}


                    _serialPort.Close();
        }

        private void SetBaseValues()
        {
            string[] BaseParams = { "imu", "attr", "attp", "atts", "atc", "trg", "ffc", "ffl", "htc", "dht", "cdht", "rce",
                "atrr", "atrp", "atrl", "yrl", "adfd", "rcv","pcv", "acv", "arh", "vib", "nvi", "vit", "dise", "arme",
                "arm", "pwm", "mtd" };
            string[] BaseParamsValue = { "23", "35", "35", "100", "3", "1", "300", "5.8", "5", "-0.11", "-0.3", "0", "10",
                "10", "150", "180", "40", "0.0", "0.0", "1", "5", "0.08", "5.0", "10", "2", "3", "2", "1", "1" };
            InitBoardWithoutReset(BaseParams, BaseParamsValue);
            Thread.Sleep(SleepDurationAfterBoardInitMethod);
        }
        private void InitBoardWithoutReset(string[] Params, string[] ParamsValue)
        {
            //Thread.Sleep(1000);
            ConfigurationprogressBar.Minimum = 1;
            ConfigurationprogressBar.Maximum = Params.Length;
            if (!Params.Length.Equals(ParamsValue.Length))
            {
                return;
            }
            int Count = 0;
            foreach (string Param in Params)
            {
                ConfigurationprogressBar.Value = Math.Min(Count + 1, Params.Length);
                LooKForParameterinEESimplified(Param.ToUpperInvariant() + ": " + ParamsValue[Count]);
                if (!IdentifiedText)
                {
                    if (ParamsThatCauseReset.Contains(Param))
                    {
                        WaitForInit = true;
                        FullInitText = "";
                        Thread.Sleep(250);
                        WriteToSmartAir(Param + " " + ParamsValue[Count]);//+ "\r\n"                        
                    }
                    else
                    {
                        WriteToSmartAir(Param + " " + ParamsValue[Count]);

                    }
                }
                Count++;
                IdentifiedText = false;
            }
        }

        private void LooKForParameterinEESimplified(string str)
        {
            EEFinished = false;
            TestTimeout = false;
            IdentifiedText = false;
            //FullTextSmartAir = "";
            //Thread.Sleep(250);
            //WriteToSmartAir("EE?");
            //Thread.Sleep(850);
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            while (!EEFinished && !TestTimeout && !IdentifiedText)
            {
                TimeSpan ts = stopWatch.Elapsed;
                if (ts.TotalMilliseconds >= 3000)
                {
                    WriteToSmartAir("EE?");
                    //TestTimeout = true;
                }


                if (FullTextSmartAir.Contains(str))
                    IdentifiedText = true;
                if (FullTextSmartAir.Contains("!System.....................:") || FullTextSmartAir.Contains("!Main battery...............:"))
                    EEFinished = true;
            }
            EEFinished = false;
            TestTimeout = false;
        }

        private void WriteToSmartAir(string TextToSend)
        {
            //serialPort1.WriteLine("\r");
            //serialPort1.WriteLine(TextToSend + "\r");
            SendToSMAChars(TextToSend);
            Thread.Sleep(SleepAfterWriteLineEvent);
            if ((_serialPort.BytesToRead > 0) || TextToSend.Equals("dir"))
            {
                Thread.Sleep(SleepAfterWriteLineEvent);
                string indata = _serialPort.ReadExisting();
                if (!indata.Equals(""))
                {
                    //Console.WriteLine(indata);
                    FullTextSmartAir += indata;
                }
                else
                {
                    Thread.Sleep(250);
                }
            }
        }

        private void SendToSMAChars(string StringToSend)
        {
            _serialPort.Write(new byte[] { (byte)0x0A }, 0, 1);
            foreach (char C in StringToSend)
            {
                byte hex = Convert.ToByte(C);
                _serialPort.Write(new byte[] { hex }, 0, 1);
            }
            _serialPort.Write(new byte[] { (byte)0x0D }, 0, 1);
        }

        private void RefreshPortsbutton_Click(object sender, EventArgs e)
        {
            SmartAirPortcomboBox.Items.Clear();
            foreach (string s in SerialPort.GetPortNames())
            {
                SmartAirPortcomboBox.Items.Add(s);
            }
        }

        private void selectBinFile_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "bin files (*.bin)|*.bin|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                    fileNameTextBox.Text = filePath;
                    shortFileName = openFileDialog.SafeFileName;

                    ////Read the contents of the file into a stream
                    //var fileStream = openFileDialog.OpenFile();

                    //using (StreamReader reader = new StreamReader(fileStream))
                    //{
                    //    fileContent = reader.ReadToEnd();
                    //}
                }
            }

        }

        byte lrotate(byte val, int n)
        {
            uint t, i;
            t = val;
            for (i = 0; i < n; i++)
            {
                t = (t << 1);
                if ((t & 256) == 256)
                {
                    t = (t | 1);
                }
            }
            return (byte)t;
        }

        byte rrotate(byte val, int n)
        {
            uint t, i;
            t = val;
            t = (t << 8);
            for (i = 0; i < n; i++)
            {
                t = (t >> 1);
                if ((t & 128) == 128)
                {
                    t = (t | 32768);
                }
            }
            t = (t >> 8);
            return (byte)t;
        }

        private void encryptBinFile_Click(object sender, EventArgs e)
        {
            var fileArray = File.ReadAllBytes(fileNameTextBox.Text);

            Crc16Ccitt c = new Crc16Ccitt(InitialCrcValue.NonZero1);
            ushort crcValue = c.ComputeChecksum(fileArray);

            outputTextBox.AppendText(String.Format("Calculated CRC: 0x{0:X}\r\n", crcValue));
            outputTextBox.AppendText(String.Format("Calculated CRC: 0d{0}\r\n", crcValue));

            int totalNumberOfPackets = (fileArray.Length / 56);
            if (fileArray.Length % 56 != 0)
            {
                totalNumberOfPackets++;
            }
            byte[] encryptedArray = new byte[fileArray.Length];
            byte[] aplitEncryptedArray = new byte[fileArray.Length];

            byte[] key = { 0x30, 0xAB, 0xC2, 0x12, 0x09, 0xCE, 0x56, 0x9A, 0xF8, 0x81, 0xD4, 0xA9
            , 0x05, 0x67, 0x5E, 0x6F, 0x79, 0x3F, 0x96, 0xC9, 0x11, 0x2F, 0x37, 0x1C, 0x60, 0xE3, 0x04, 0x7E
            , 0xB6, 0x93, 0x70, 0xEF, 0x4A, 0x45, 0x02, 0x6A, 0x97, 0x12, 0x27, 0x42, 0xEC, 0x51, 0x55, 0x70
            , 0x00, 0xCB, 0xA8, 0x61, 0x22, 0xE1, 0xF1, 0x78, 0x53, 0x18, 0xF8, 0xDA, 0xC9, 0x40, 0x08, 0x5D
            , 0x3F, 0x96, 0x09, 0xC5, 0x9B, 0x08, 0x34, 0xBD, 0x9C, 0xFA, 0x9F, 0x1D, 0x5F, 0x61, 0x77, 0xA0
            , 0x16, 0xC5, 0xB1, 0x76, 0x00, 0xF7, 0x97, 0x9A, 0x04, 0x09, 0x95, 0x8C, 0x44, 0x5A, 0xF3, 0x1E
            , 0xCE, 0x96, 0x24, 0xF7, 0xE1, 0x2B, 0x20, 0x5A, 0x67, 0x89, 0xEa, 0x6C, 0xBA, 0x73, 0x0B, 0x19
            , 0xE6, 0xA3, 0xCD, 0xFC, 0xA1, 0xAD, 0x7E, 0x65, 0x67, 0x0C, 0x9B, 0x48, 0x5E, 0x6A, 0x53, 0xBD
            , 0xBB, 0x22, 0xBC, 0x85, 0x17, 0x85, 0x2A, 0x07, 0x9A, 0xC0, 0x18, 0xB6, 0xEF, 0x90, 0x3C, 0x0E
            , 0x8E, 0x19, 0x5E, 0xA1, 0xE0, 0x06, 0x18, 0xD0, 0x56, 0x36, 0x30, 0xA0, 0xA8, 0x60, 0x77, 0x31
            , 0x4C, 0xDA, 0x3B, 0x5E, 0x75, 0x9C, 0xD5, 0xB6, 0x97, 0x59, 0x56, 0x47, 0xF8, 0xEB, 0x40, 0xF9
            , 0x99, 0x2F, 0xFB, 0xFA, 0xFE, 0x88, 0xF3, 0x49, 0xD2, 0xC7, 0xD6, 0x54, 0x6A, 0xBB, 0x02, 0x52
            , 0xCB, 0xC1, 0xCA, 0xA1, 0x23, 0x6D, 0x7F, 0xC9, 0x4F, 0x6B, 0xDE, 0x2E, 0xF3, 0xB5, 0x4A, 0x30
            , 0x46, 0xE5, 0x08, 0xB9, 0xCD, 0x1F, 0x07, 0x12, 0x89, 0x04, 0xD3, 0x03, 0x72, 0x96, 0x50, 0x0D
            , 0xF3, 0xB9, 0xB1, 0xD6, 0xAE, 0xF9, 0xB4, 0x3A, 0x95, 0x78, 0xCE, 0x7D, 0xA4, 0xA0, 0x55, 0x4C
            , 0xDF, 0xC2, 0x5C, 0x5A, 0x79, 0x91, 0xD8, 0x4D, 0xC8, 0x62, 0x8E, 0x8D, 0xA0, 0xED, 0xF0, 0xE6
            , 0xED, 0x59, 0x63, 0xE2};

            uint Counter = 0;
            int Rotator = fileArray.Length % 8;
            if (Rotator.Equals(0))
            {
                Rotator = (fileArray.Length + 1) % 5;
            }
            if (Rotator.Equals(0))
            {
                Rotator = 4;
            }

            outputTextBox.AppendText(String.Format("File size in bytes: {0}\r\n", fileArray.Length));
            outputTextBox.AppendText(String.Format("Calculated Rotator: {0}\r\n", Rotator));

            for (int i = 0; i < fileArray.Length; i++)
            {
                byte firstLeftRotation = lrotate(fileArray[i], Rotator);
                byte afterKey = (byte)(firstLeftRotation ^ key[Counter]);
                byte secondLeftRotate = lrotate(afterKey, 8 - Rotator);

                byte firstLeftRotationAplit = lrotate(fileArray[i], 3);
                byte afterKeyAplit = (byte)(firstLeftRotationAplit ^ key[Counter]);
                byte secondLeftRotateAplit = lrotate(afterKeyAplit, 5);

                Counter++;
                encryptedArray[i] = secondLeftRotate;
                aplitEncryptedArray[i] = secondLeftRotateAplit;
                if (Counter > 255)
                {
                    Counter = 0;
                }
            }
            File.WriteAllBytes(fileNameTextBox.Text.Replace(shortFileName, "Encrypted" + shortFileName), encryptedArray);
            File.WriteAllBytes(fileNameTextBox.Text.Replace(shortFileName, "AplitEncrypted" + shortFileName), aplitEncryptedArray);
        }

        private void MLBrowse_Click(object sender, EventArgs e)
        {
            int Counter = 0;
            int FileIDStartIndex = 0;
            FilesToMergecheckedListBox.Items.Clear();
            AutoMergeFlag = AutoMergecheckBox.Checked;
            FolderBrowserDialog SelectedFolder = new FolderBrowserDialog();

            SelectedFolder.SelectedPath = "F:\\Windows\\Temp\\2023-08-16\\Mavic 3\\";

            DialogResult result = SelectedFolder.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(SelectedFolder.SelectedPath))
            {
                selectedFolder = SelectedFolder.SelectedPath;
                files = Directory.GetFiles(SelectedFolder.SelectedPath, "*.CSV", SearchOption.AllDirectories);
                MessageBox.Show("Files found: " + files.Length.ToString(), "Message");

                foreach (string file in files)
                {
                    FileIDStartIndex = file.LastIndexOf("\\");
                    long length = new System.IO.FileInfo(file).Length;
                    if (length <= 17000)
                    {
                        File.Delete(file);
                    }
                    else
                    {
                        string[] localIndex = { file.Substring(FileIDStartIndex + 1), length.ToString(), file.Substring(0, FileIDStartIndex + 1) };
                        ListViewItem viewItem = new ListViewItem(localIndex);
                        MLlistView.Items.Insert(Counter, viewItem);
                        Counter++;
                    }
                }

            }
        }

        private void Deletebutton_Click(object sender, EventArgs e)
        {
            int Counter = 0;
            int FileIDStartIndex = 0;
            SelectedListViewItemCollection localSelectedItems = MLlistView.SelectedItems;

            foreach (ListViewItem a in localSelectedItems)
            {
                string fileToDelete = a.SubItems[2].Text.ToString() + a.SubItems[0].Text.ToString();
                File.Delete(fileToDelete);
            }

            MLlistView.Items.Clear();
            files = Directory.GetFiles(selectedFolder, "*.CSV", SearchOption.AllDirectories);
            MessageBox.Show("Files found: " + files.Length.ToString(), "Message");

            foreach (string file in files)
            {
                FileIDStartIndex = file.LastIndexOf("\\");
                long length = new System.IO.FileInfo(file).Length;
                string[] localIndex = { file.Substring(FileIDStartIndex + 1), length.ToString(), file.Substring(0, FileIDStartIndex + 1) };
                ListViewItem viewItem = new ListViewItem(localIndex);
                MLlistView.Items.Insert(Counter, viewItem);
                Counter++;
            }

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            chart1.Series["MLBaroSeries"].Points.Clear();
            SelectedListViewItemCollection localSelectedItems = MLlistView.SelectedItems;
            if (localSelectedItems.Count != 1)
            {
                return;
            }
            else
            {
                foreach (ListViewItem a in localSelectedItems)
                {
                    LogFile.MaxColumnsInNonRawLog = Int32.Parse(NuOfColumsML.Text);
                    string fileToShow = a.SubItems[2].Text.ToString() + a.SubItems[0].Text.ToString();
                    if (RTOSFormatcheckBox.Checked)
                    {
                        LogFile.LoadRTOSLogFile(fileToShow);
                        LocalAHRSIMU._beta = 0.1f;
                        LocalAHRSIMU.RollPitchYawAngleFromIMUMeasurementList(ref AHRSPitch, ref AHRSRoll, ref AHRSYaw, LogFile, LocalAHRSIMU);
                    }
                    else
                    {
                        LogFile.LoadLogFile(fileToShow, false);
                        if (PreviousTimeFormatcheckBox.CheckState == CheckState.Checked)
                        {
                            double result = 0;
                            int Count = 0;
                            for (int i = 0; i < LogFile.TimeStamp.Count; i++)
                            {
                                result = TimeSpan.Parse(LogFile.TimeStamp[Count].Substring(11, 12)).TotalSeconds;
                                LogFile.TimeStamp[Count] = result.ToString();
                                Count++;
                            }
                        }
                    }
                }
                chart1.Series["MLBaroSeries"].Points.Clear();
                for (int i = 0; i < AHRSRoll.Count; i++)
                {
                    chart1.Series["MLBaroSeries"].Points.AddXY(LogFile.TimeStamp[i], AHRSRoll[i]);
                }

                SaveFileDialog LogFileToSave = new SaveFileDialog();

                LogFileToSave.Filter = "CSV file (*.CSV) | *.CSV | All files (*.*) | *.*";
                LogFileToSave.FilterIndex = 2;
                LogFileToSave.RestoreDirectory = false;
                if (LogFileToSave.ShowDialog() == DialogResult.OK)
                {

                    File.Create(LogFileToSave.FileName).Dispose();
                    var stream = new StreamWriter(LogFileToSave.FileName, true);

                    for (int i = 0; i < AHRSRoll.Count(); i++)
                    {
                        string csvRow = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14}",
                         LogFile.TimeStamp[i], LogFile.GyroX[i], LogFile.GyroY[i], LogFile.GyroZ[i],
                         LogFile.AccX[i], LogFile.AccY[i], LogFile.AccZ[i], LogFile.AbsAcc[i],
                         LogFile.MagX[i], LogFile.MagY[i], LogFile.MagZ[i], LogFile.BaroHeight[i],
                         AHRSPitch[i], AHRSRoll[i], AHRSYaw[i]);
                        //0     ,1        ,2        ,3          ,4      ,5      ,6          ,7      ,8      ,9          ,10         ,11     ,12         ,13
                        stream.WriteLine(csvRow);
                    }
                    stream.Close();

                }

            }
        }

        private void MouseHover_HitTest(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.DataVisualization.Charting.Chart cr = (System.Windows.Forms.DataVisualization.Charting.Chart)sender;
            HitTestResult result = cr.HitTest(e.X, e.Y);

            //if (result.ChartElementType == ChartElementType.DataPoint)
            //{
            //    LogTimetextBox.Text = LogFile.TimeStamp[result.PointIndex].Substring(11, 12).ToString();
            //    PointerXValuetextBox.Text = result.PointIndex.ToString();
            //    PointerYValuetextBox.Text = cr.Series[0].Points[result.PointIndex].YValues[0].ToString();

            //    System.Drawing.Image RollImage = RotateImage(PicturesResource._1479237685000_1298124, (float)Convert.ToDouble(LogFile.Roll[result.PointIndex]));
            //    RollpictureBox.Image = RollImage;

            //    System.Drawing.Image PitchImage = RotateImage(PicturesResource._1479237342000_IMG_708263, (float)Convert.ToDouble(LogFile.Pitch[result.PointIndex]));
            //    PitchpictureBox.Image = PitchImage;

            //    System.Drawing.Image YawImage = RotateImage(PicturesResource.TopViewPh4, (float)Convert.ToDouble(LogFile.Yaw[result.PointIndex]));
            //    YawpictureBox.Image = YawImage;
            //}
        }

        private void MouseDoubleClick_Test(object sender, MouseEventArgs e)
        {
            if (DoubleClickCounter >= 2)
            {
                DoubleClickCounter = 0;
                FirstXPos = 0;
                SecondXPos = 0;
            }
            if (DoubleClickCounter == 0)
            {
                System.Windows.Forms.DataVisualization.Charting.Chart cr = (System.Windows.Forms.DataVisualization.Charting.Chart)sender;
                HitTestResult result = cr.HitTest(e.X, e.Y);
                FirstXPos = result.PointIndex;

            }
            if (DoubleClickCounter == 1)
            {
                System.Windows.Forms.DataVisualization.Charting.Chart cr = (System.Windows.Forms.DataVisualization.Charting.Chart)sender;
                HitTestResult result = cr.HitTest(e.X, e.Y);
                SecondXPos = result.PointIndex;
            }
            DoubleClickCounter++;
            MLNumOfPointstextBox.Text = DoubleClickCounter.ToString();
        }

        //Takeoff
        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {
            //if (DoubleClickCounter == 2)
            //{

            //}
            //else
            //{
            //    MessageBox.Show("Not enough points selected");
            //}
            LogFile.SaveLogFile();
        }

        private void resetSelectedPointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoubleClickCounter = 0;
            FirstXPos = 0;
            SecondXPos = 0;
            MLNumOfPointstextBox.Text = DoubleClickCounter.ToString();
        }
    }
}
