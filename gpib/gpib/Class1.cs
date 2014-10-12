using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;

namespace GPIBlibrary
{
    public class GPIB
    {
        SerialPort sp = new SerialPort();

        public List<string> portlist()
        {
            List<String> tList = new List<String>();
            foreach (string s in System.IO.Ports.SerialPort.GetPortNames())
            {
                tList.Add(s);
            }

            tList.Sort();
            return tList;
        }
         public Boolean start(string port, int timeout)
        {
             // in order to start the procedure, and connect to the GPIB interface. 
             // Set port no. as string (com n) and timeout (ms).
            
            // COM port parameters
            sp.PortName = port;
            sp.BaudRate = 115200;
            sp.DataBits = 8;
            sp.Parity = Parity.None;
            sp.StopBits = StopBits.One;
            

            // RTS/CTS handshaking
            sp.Handshake = Handshake.RequestToSend;
            sp.DtrEnable = true;

           

            if (timeout == 0)
            {
                timeout = 1000;
            }


            try
            {
                sp.Open();
                sp.Write("++mode 1" + "\r\n");  // sets mode to controller
                sp.Write("++read_tmo_ms" + timeout + "\r\n"); // sets timeout 
                //sp.Write("++auto 0" + "\r\n"); 
                return true;

            }
            catch (Exception e)
            {
                System.Console.Write(e.Message);
                return false;
            }

        }

         public bool address(int address)
         {
             if (sp.IsOpen == true)
             {
                 try
                 {
                     sp.Write("++addr " + address + "\r\n");

                     return true;
                 }
                 catch (Exception e)
                 {
                     return false;
                 }
             }
             else
             {
                 return false;
             }
         }

        public string read()
        {
            if (sp.IsOpen == true)
            {
               string yread = "";
                try
                {
                    sp.DiscardInBuffer();
                    sp.Write("++read eoi" + "\r\n");
                    
                    DateTime lastRead = DateTime.Now;
                    TimeSpan elapsedTime = new TimeSpan();

                    //  timespan
                    TimeSpan TIMEOUT = new TimeSpan(0, 0, 10);

                    // Read from port until TIMEOUT time has elapsed since
                    // last successful read
                    while (TIMEOUT.CompareTo(elapsedTime) > 0)
                    {
                        string buffer = sp.ReadExisting();
                        //buffer = sp.ReadExisting();

                        if (buffer.Length > 0)
                        {
                            yread = buffer;
                           // Console.Write(buffer);
                            lastRead = DateTime.Now;
                        }
                        elapsedTime = DateTime.Now - lastRead;
                    }

                    return yread;
                    
                }
                catch (Exception e)
                {
                    return e.ToString();
                }
            }
            else
            {
                return "port not open";
            }
            
        }


        public Boolean write(int address, string y)
        { // writes data
            if (sp.IsOpen == true)
            {
                try
                {
                sp.Write("++addr " + address + "\r\n");
                sp.DiscardInBuffer();
               
                sp.Write(y + "\r\n");
                return true;
                     }
            catch (Exception e)
            {
                return false;
            }
            }
            else
            {
                return false;
            }

        }
        public string writeread(int address, string y)
        { // writes data, then reads
            string x="";

            if (sp.IsOpen == true)
            {
                try
                {
                    sp.Write("++addr " + address + "\r\n");
                    sp.DiscardInBuffer();
                    sp.Write(y + "\r\n");
                   
                   sp.DiscardInBuffer();
                   sp.Write("++read eoi" + "\r\n");
                   Thread.Sleep(500);
                   DateTime lastRead = DateTime.Now;
                   TimeSpan elapsedTime = new TimeSpan();

                   //  timespan
                   TimeSpan TIMEOUT = new TimeSpan(0, 0, 10);

                   // Read from port until TIMEOUT time has elapsed since
                   // last successful read
                   while (TIMEOUT.CompareTo(elapsedTime) > 0)
                   {
                       string buffer = sp.ReadExisting();
                       //buffer = sp.ReadExisting();

                       if (buffer.Length > 0)
                       {
                           x = buffer;
                           // Console.Write(buffer);
                           lastRead = DateTime.Now;
                       }
                       elapsedTime = DateTime.Now - lastRead;
                   }
                    return x;
                }
                catch (Exception e)
                {
                    return e.ToString();

                }
            }
            else
            {
                return "port not open";
            }  
        }

        public bool clear(int address)
        {
            if(sp.IsOpen == true)
            {
             
            try
            {
                sp.Write("++addr " + address + "\r\n");
                sp.Write("++clr");
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            }
            else{
            return false;
                 }
        }
        public bool eoi(int x)
        {
            if (sp.IsOpen == true)
            {
                try
            {
                    sp.DiscardInBuffer();
                    sp.Write("++eoi " + x + "\r\n");
                    return true;
            }
            catch (Exception e)
            {
                return false;
            }
            }
            else
            {
                return false;
            }
            
        }
        public Boolean ifc()
        {
           if (sp.IsOpen == true)
            {
                try
            {
                    sp.Write("++ifc" + "\r\n");
                    return true;
            }
            catch (Exception e)
            {
                return false;

            }
           }
               else 
               {
               return false;
               }
            
        }
        public bool loc(int address)
        {
            if (sp.IsOpen == true)
            {
                try
            {
                    sp.Write("++addr " + address + "\r\n");
                    sp.Write("++loc" + "\r\n");
                    return true;
            }
            catch (Exception e)
            {
                return false;
            }
            }
            else
            {
                return false;
            }
        }
        public Boolean mode(int y)
        {
            if (sp.IsOpen == true)
            {
                try
            {
                    sp.Write("++mode " + y + "\r\n");
                    return true;
            }
            catch (Exception e)
            {
                return false;
            }
            }
            else
            {
                return false;
            }
        }
        public bool rst()
        {
            if (sp.IsOpen == true)
            {
                try
            {
                    sp.Write("++rst" + "\r\n");
                    return true;
            }
            catch (Exception e)
            {
                return false;
            }
            }
            else 
            {
                return false;
            }
        }
        public string spoll(int x)
        {
            if (sp.IsOpen == true)
            {
                try
            {
                    sp.DiscardInBuffer();
                    sp.Write("++spoll "+ x + "\r\n");
                    string y = sp.ReadExisting();
                    return y;

            }
            catch (Exception e)
            {
                return e.ToString();
            }
                
            }
            else
            {
                return "port not open";
            }
        }
        public string[] buspoll()
        {
            if (sp.IsOpen == true)
            {
               var a  = new string [31];
                for (int i = 1; i < 30; i++)
                {
                    try
                    {
                       sp.Write("++spoll " + i + "\r\n");
                       a[i] = sp.ReadLine();
                       
                    }
                    catch (Exception e)
                    {
                        
                    }
                }
                return a;
            }
                else
                {
                var a = new string[1];
                    a[0] = "0";
                    return a;
                }
            
        }


        

        public string srq()
        {
            if (sp.IsOpen == true)
            {
                try
                {
                    sp.Write("++srq");
                    string y = sp.ReadLine();
                    return y;
                }
                catch (Exception e)
                {
                    return e.ToString();
                }
            }
            else
            {
                return "error";
            }
        }
        

    }
}


 