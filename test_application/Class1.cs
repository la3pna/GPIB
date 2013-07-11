using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace GPIBlibrary
{
    public class GPIB
    {
        SerialPort sp = new SerialPort();
        
        public List <string> portlist()   
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

            // Error handling
         //   sp.DiscardNull = false;
         //   sp.ParityReplace = 0;

            if (timeout == 0)
            {
                timeout = 1000;
            }


            try
            {
                sp.Open();
                sp.Write("++mode 1" + "\r\n");  // sets mode to controller
                sp.Write("++read_tmo_ms" + timeout + "\r\n"); // sets timeout 
                sp.Write("++auto 0" + "\r\n"); 
                return true;

            }
            catch (Exception e)
            {
                System.Console.Write(e.Message);
                return false;
            }

        }
        public string read()
        {
            if (sp.IsOpen == true)
            {
                string y;
                try
                {
                    sp.DiscardInBuffer();
                    sp.Write("++read eoi" + "\r\n");
                    y = sp.ReadExisting();

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
        public Boolean write( string y)
        { // writes data
            if (sp.IsOpen == true)
            {
                try
                {
                sp.DiscardInBuffer();
              //  sp.Write("++addr " + address + "\r\n");
                sp.Write(y+"\r\n");
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
        public string writeread( string y)
        { // writes data, then reads

            if (sp.IsOpen == true)
            {
                try
                {
                    sp.DiscardInBuffer();

                    sp.Write(y + "\r\n");
                   string x = sp.ReadExisting();

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
        public double Multiplication(double x, double y)
        {
            try
            {
            }
            catch (Exception e)
            {

            }
            return x * y;
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
        public bool ver(int address)
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

    }
}


 