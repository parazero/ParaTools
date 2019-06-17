using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using System.IO.Ports;
using System.IO;
using System.Windows.Forms;
using GeneralTools;
using System.Diagnostics;

namespace Curit.Module.RTX.Com
{
    public enum SYSTEMSTATUS
    {
        STARTED = 0,
        STOPPED = 1
    }

    public class LegacyCommunication : IDisposable
    {
        private SerialPort sp;
        private SYSTEMSTATUS systemstatus;
        private Boolean loggedin = false;
        private byte[] inputdata = new byte[65535];
        private int bytesreceived = 0;
        private bool readbinary = false;
        public static bool _waitforack;
        private bool newline;
        private bool oplevel;
        private bool uslevel;
        private StringBuilder sb = new StringBuilder();
        private List<string> linehistory = new List<string>();
        public delegate void Progress(int curpos, int max);
        public delegate void NewLine(String s);
        private NewLine newLine;
        public Progress progressUpdate;
        private Form1 Source;
        private string EndCondition;
        static int NumOfPackets;
        static bool EndConditionIdentified = false;
        static bool ErrorIdentified = false;
        static bool FlashResult = false;

        public LegacyCommunication(String comport, NewLine newLineDelegate) : this(comport)
        {
            newLine += newLineDelegate;
        }

        public LegacyCommunication(String comport)
        {
            sp = new SerialPort(comport);
            newLine += new NewLine(errorCheck);
            try
            {
                sp.Open();
                sp.RtsEnable = true;
                sp.StopBits = System.IO.Ports.StopBits.One;
                sp.DataBits = 8;
                sp.BaudRate = 921600;
                sp.ReceivedBytesThreshold = 1;
                //sp.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
                systemstatus = SYSTEMSTATUS.STARTED;

                testSystemStatus();
            }
            catch (Exception ex)
            {
                throw new Exception("Poort con niet geopend worden.", ex);
            }
        }

        public LegacyCommunication(SerialPort port, bool OpenOrClose)
        {
            sp = port;
            if (OpenOrClose.Equals(true))
            {
                newLine += new NewLine(errorCheck);
                try
                {
                    systemstatus = SYSTEMSTATUS.STARTED;
                    sp.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
                }
                catch (Exception ex)
                {
                    throw new Exception("Poort con niet geopend worden.", ex);
                }
            }
            else
            {
                //sp.DataReceived -= new SerialDataReceivedEventHandler(sp_DataReceived);
            }
        }

        private void errorCheck(string templine)
        {
            if (templine.Contains("ERROR - Wrong login level or unknown command"))
            {
                throw new Exception("ERROR - Wrong login level or unknown command.");
            }
            else if (templine.Contains("ERROR - Already locked"))
            {
                throw new Exception("ERROR - Already locked.");
            }
            else if (templine.Contains("ERROR - Not locked"))
            {
                throw new Exception("ERROR - Not locked.");
            }
            else if (templine.Contains("ERROR SAVING DATA IN DATABASE"))
            {
                throw new Exception("ERROR SAVING DATA IN DATABASE");
            }
            else if (templine.Contains("Syntax error"))
            {
                throw new Exception("Syntax error");
            }
            else if (templine.Contains("not permitted"))
            {
                throw new Exception("Something likely a devicetype is not permitted");
            }
            else if (templine.Contains("ERROR - Only one generic Bluetooth device permitted"))
            {
                throw new Exception("ERROR - Only one generic Bluetooth device permitted");
            }
            else if (templine.Contains("ERROR - Only one IR device permitted"))
            {
                throw new Exception("ERROR - Only one IR device permitted");
            }
            else if (templine.Contains("ERROR - Only one RS232 device permitted"))
            {
                throw new Exception("ERROR - Only one RS232 device permitted");
            }
            else if (templine.Contains("DEVICE NOT IN DATABASE"))
            {
                throw new Exception("DEVICE NOT IN DATABASE");
            }
            else if (templine.Contains("ERROR - Names must be a maximum of 10 chars"))
            {
                throw new Exception("ERROR - Names must be a maximum of 10 chars");
            }
            else if (templine.Contains("ERROR - Not a valid mode"))
            {
                throw new Exception("ERROR - Not a valid mode.");
            }
            else if (templine.Contains("ERROR – String not accepted"))
            {
                throw new Exception("ERROR – String not accepted.");
            }
            else if (templine.Contains("ERROR – syntax error"))
            {
                throw new Exception("ERROR – syntax error");
            }
            else if (templine.Contains("Syntax error - Name must be 2 to 15 characters long"))
            {
                throw new Exception("Syntax error - Name must be 2 to 15 characters long.");
            }
            else if (templine.Contains("Syntax Error. Not a FQHN"))
            {
                throw new Exception("Syntax Error. Not a FQHN");
            }
        }

        private void testSystemStatus()
        {
            /*
            sendNewLine();
            waitforline();
            if (linehistory[0].Contains("Operator Level"))
            {
                loggedin = true;
                oplevel = true;
            }
            else if (linehistory[0].Contains("User Level"))
            {
                uslevel = true;
                loggedin = true;
            }
            else if (linehistory[0].Contains("User Name"))
            {
                loggedin = false;
            }
            else if (linehistory[0].Contains("Password"))
            {
                loggedin = false;
                sp.Write("goaway");
                sendNewLine();
            }
            return;
            */
            sp.Write("ee?\r");
            sp.Write("SMA\r");
        }

        void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (this.readbinary)
            {
                while (sp.BytesToRead != 0)
                {
                    if (bytesreceived == 65534)
                    {
                        inputdata = new byte[65535];
                        bytesreceived = 0;
                    }
                    sp.Read(inputdata, bytesreceived, 1);
                    //Source.textBox1.Invoke(Source.textBoxUpdateDelegate, new Object[] { Source.textBox1, Encoding.Default.GetString(inputdata, bytesreceived,1), true });
                    //Console.WriteLine(inputdata[bytesreceived]);

                    if (inputdata[bytesreceived] == (byte)0x06)
                    {
                        //Source.textBox1.Invoke(Source.textBoxUpdateDelegate, new Object[] { Source.textBox1, "\r\n" + "Bytes received " + bytesreceived.ToString(), true });
                        _waitforack = true;
                    }
                        
                    //else
                    //_waitforack = false;

                    if (_waitforack == true && inputdata[bytesreceived] == (byte)0x15)
                    {
                        //moet nog resend last package worden.
                        char[] tca = new char[1];
                        tca[0] = (char)0x18;
                        sp.Write(tca, 0, 1);
                        throw new Exception("Filesend failed!");
                    }
                    bytesreceived++;
                }
            }
            else
            {
                char[] tmpchar = new char[1];
                while (sp.BytesToRead != 0)
                {
                    sp.Read(tmpchar, 0, 1);
                    sb.Append(tmpchar[0]);
                    if ((tmpchar[0] == (char)0x3E))// || (tmpchar[0] == (char)0x0D))
                    {
                        newLine(sb.ToString());
                        linehistory.Add(sb.ToString());
                        sb = new StringBuilder();
                        newline = true;
                    }
                }
            }
        }

        public void sendConfig(String config)
        {
            sendConfig(config.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));
        }

        public void sendConfig(String[] config)
        {
            foreach (String s in config)
            {
                sp.Write(s);
                sendNewLine();
                waitforline();
            }
        }

        public bool sendBinaryFile(String Path, GeneralTools.Form1 OrgForm, String EndConditionText)
        {
            Source = OrgForm;
            EndCondition = EndConditionText;
            //OrgForm.textBox1.Invoke(OrgForm.textBoxUpdateDelegate, new Object[] { OrgForm.textBox1, "\r\n" + "Upload File: " + Path, true });
            //sp.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
            FileStream fs = new FileStream(Path, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);
            NumOfPackets = ((int)br.BaseStream.Length/1024)+1;
            byte[] bytes = new byte[br.BaseStream.Length];
            while (br.BaseStream.Position != br.BaseStream.Length)
            {
                bw.Write(br.ReadBytes(1024));
                bw.Flush();
            }
            fs.Close();
            br.Close();
            bytes = ms.ToArray();
            ms.Close();
            bw.Flush();
            bw.Close();
            sendBinaryFile(bytes, System.IO.Path.GetFileName(Path));
            //sp.DataReceived -= new SerialDataReceivedEventHandler(sp_DataReceived);
            return FlashResult;
        }

        public void sendBinaryFile(byte[] bytes, string filename)
        {
            readbinary = true;
            ErrorIdentified = false;
            FlashResult = false;
            //SendToSMAChars("AT+pBINARYUPLOAD");
            //sp.Write("AT+pBINARYUPLOAD");
            //sendNewLine();
            waitfor('C');
            ushort packetnum = 0;

            Packet initpacket = new Packet();
            initpacket.isinit = true;
            initpacket.packetnum = packetnum;
            initpacket.filename = filename;
            initpacket.filelength = bytes.Length;

            if (filename.Length > 125)
                initpacket.longpacket = true;
            else
                initpacket.longpacket = false;

            initpacket.createPacket();
            Thread.Sleep(250);
            sp.Write(initpacket.packet, 0, initpacket.packet.Length);
            Thread.Sleep(350);
            waitforack();
            
            _waitforack = false;
            waitfor('C');

            MemoryStream ms = new MemoryStream(bytes);
            byte[] temparr = new byte[1024];
            Packet sendPacket;
            long numpack = Math.Abs(((long)ms.Length) / ((long)1024));


            //long leftover = ms.Length % 1024;
            BinaryReader br = new BinaryReader(ms);
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            TimeSpan ts = stopWatch.Elapsed;
            while ((ms.Position != ms.Length) && ts.TotalMilliseconds<=60000)
            {
                ts = stopWatch.Elapsed;
                //progressUpdate(packetnum, Convert.ToInt32(numpack));
                _waitforack = false;
                packetnum++;
                temparr = br.ReadBytes(1024);
                sendPacket = new Packet();
                sendPacket.packetnum = packetnum;
                sendPacket.longpacket = true;
                sendPacket.isinit = false;
                sendPacket.data = temparr;
                sendPacket.createPacket();
                sp.Write(sendPacket.packet, 0, sendPacket.packet.Length);
                waitforack();
                //Source.textBox1.Invoke(Source.textBoxUpdateDelegate, new Object[] { Source.textBox1, "\r\n" + "Packet ID " + packetnum.ToString() + " Out of: " + NumOfPackets.ToString(), true });
                _waitforack = false;
                Thread.Sleep(20);
            }
            if (ts.TotalMilliseconds >= 60000)
                return;
            //sendEndOftransmision();
            while (!EndConditionIdentified && !ErrorIdentified)
            {
                if (Encoding.Default.GetString(inputdata).Contains(EndCondition))
                {
                    EndConditionIdentified = true;
                    FlashResult = true;
                }
                if (Encoding.Default.GetString(inputdata).Contains("No responce from SenseAir."))
                {
                    ErrorIdentified = true;
                    FlashResult = false;
                }
                Thread.Sleep(2000);
                sendEndOftransmision();
                /*while ((sp.BytesToRead < 100) && (!FlashResult) )
                {
                    Thread.Sleep(2000);
                    if ((!Encoding.Default.GetString(inputdata).Contains("Transmitting file to SenseAir")))
                    {
                        sendEndOftransmision();
                    }
                }*/

                while ((sp.BytesToRead>0))
                {
                    int NumOfChars = sp.BytesToRead;
                    sp.Read(inputdata, bytesreceived, NumOfChars);
                    bytesreceived += NumOfChars;
                    //Source.textBox1.Invoke(Source.textBoxUpdateDelegate, new Object[] { Source.textBox1, Encoding.Default.GetString(inputdata, bytesreceived - NumOfChars, NumOfChars), true });
                    
                }
            }
            //Thread.Sleep(15000);
            //waitforack();
            _waitforack = false;
            readbinary = false;
            //waitforline();
            sp.DataReceived -= new SerialDataReceivedEventHandler(sp_DataReceived);
            //Source.textBox1.Invoke(Source.textBoxUpdateDelegate, new Object[] { Source.textBox1, "\r\n" + "Upload File Ended.", true });
        }

        private void sendEndOftransmision()
        {
            sp.Write(new byte[] { (byte)0x04 }, 0, 1);
            Packet endpacket = new Packet();
            endpacket.isend = true;
            endpacket.longpacket = false;
            endpacket.packetnum = 0;
            endpacket.data = new byte[128];
            endpacket.createPacket();
            Thread.Sleep(500);
            sp.Write(endpacket.packet, 0, endpacket.packet.Length);
            Thread.Sleep(1500);
            return;
        }

        private void waitforack()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            TimeSpan ts = stopWatch.Elapsed;
            while (!_waitforack && ts.TotalMilliseconds<=15000)
            {
                //Application.DoEvents();
                if (inputdata[bytesreceived] == 0x6)
                {
                    _waitforack = true;
                }
                Thread.Sleep(10);
                ts = stopWatch.Elapsed;
            }
            return;
        }

        private void waitfor(char p)
        {
            if (bytesreceived != 0)
            {
                while ((char)inputdata[bytesreceived - 1] != p)
                    Application.DoEvents();
            }
            else
            {
                Application.DoEvents();
            }
            return;
        }

        public void login(string username, string password)
        {
            if (!loggedin)
            {
                sp.Write(username);
                sendNewLine();
                waitforline();

                if (linehistory[linehistory.Count - 1].Contains("Password>"))
                {
                    sp.Write(password);
                    sendNewLine();
                    waitforline();
                    if (linehistory[linehistory.Count - 1].Contains("User Level>"))
                    {
                        uslevel = true;
                        loggedin = true;
                    }
                    else if (linehistory[linehistory.Count - 1].Contains("Operator Level>"))
                    {
                        oplevel = true;
                        loggedin = true;
                    }
                    else
                    {
                        throw new Exception("Password fout");
                    }
                }
                else
                {
                    throw new Exception("Username fout");
                }
            }
        }

        private void waitforline()
        {
            while (!newline)
                Application.DoEvents();

            newline = false;
        }

        private void wait(bool p)
        {
            while (!p)
                Application.DoEvents();
        }

        public void stopSystem()
        {

            if (this.systemstatus == SYSTEMSTATUS.STARTED)
            {
                sp.Write("AT+pSTOP_SYSTEM");
                sendNewLine();
            }
            else
            {
                throw new Exception("System was al gestopt");
            }
        }

        private void sendNewLine()
        {
            sp.Write(new byte[] { (byte)0x0D }, 0, 1);
            return;
        }

        public void logout()
        {
            sp.Write("logout");
            sendNewLine();
            waitforline();
            waitforline();
        }

        public void startSystem()
        {
            if (this.systemstatus == SYSTEMSTATUS.STARTED)
            {
                sp.Write("AT+pSTART_SYSTEM");
                sendNewLine();
            }
            else
            {
                throw new Exception("System was al gestopt");
            }
        }
        private void SendToSMAChars(string StringToSend)
        {
            sp.Write(new byte[] { (byte)0x0A }, 0, 1);
            foreach (char C in StringToSend)
            {
                byte hex = Convert.ToByte(C);
                sp.Write(new byte[] { hex }, 0, 1);
            }
            sp.Write(new byte[] { (byte)0x0D }, 0, 1);
        }
        #region IDisposable Members

        public void Dispose()
        {
            if (systemstatus == SYSTEMSTATUS.STOPPED)
            {
                this.startSystem();
            }
            sp.WriteLine("logout");
            sp.Close();
        }

        #endregion
    }
}
