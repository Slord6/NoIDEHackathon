using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkTests
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress localAddress = GetLocalIPAddress();

            Console.WriteLine("Current IP = " + localAddress.ToString());

            SearchLocalNetVerbose(localAddress);

            
            PingAddressVerbose();
            
            Exit();
        }

        private static void SearchLocalNetVerbose(IPAddress localAddress)
        {
            Console.WriteLine("A to search host async, Y to search sync, anything else to not");
            string result = Console.ReadLine().ToLower();
            if (result == "y")
            {
                Console.WriteLine("Searching for pingable hosts");
                SearchLocalNet(localAddress).ForEach(address => Console.WriteLine(address.ToString()));
            }
            else if (result == "a")
            {
                Task<List<IPAddress>> asyncTask = Task<List<IPAddress>>.Run(() => { return SeachLocalNetAsync(localAddress);  });
                asyncTask.Wait();
                asyncTask.Result.ForEach((address) => Console.WriteLine(address));
            }
        }

        private static async Task<List<IPAddress>> SeachLocalNetAsync(IPAddress address)
        {
            string baseAddress = address.ToString();
            string[] addressSplit = baseAddress.Split('.');
            baseAddress = addressSplit[0] + "." + addressSplit[1] + "." + addressSplit[2];

            List<Task<IPAddress>> pingTasks = new List<Task<IPAddress>>();
            for (int i = 0; i < 255; i++)
            {
                int index = i;
                Task<IPAddress> pingTask = Task.Run<IPAddress>(() =>
                {
                    IPAddress currentAddress = IPAddress.Parse(baseAddress + "." + index);
                    PingReply reply = PingAddress(currentAddress, 200);
                    if (reply.Status == IPStatus.Success)
                    {
                        return currentAddress;
                    }
                    else
                    {
                        return null;
                    }
                });
                pingTasks.Add(pingTask);
            }

            Task.WaitAll(pingTasks.ToArray());
            List<IPAddress> validAddresses = new List<IPAddress>();
            foreach (Task<IPAddress> task in pingTasks)
            {
                if(task.Result != null)
                {
                    validAddresses.Add(task.Result);
                }
            }

            return validAddresses;
        }

        private static List<IPAddress> SearchLocalNet(IPAddress address)
        {
            string baseAddress = address.ToString();
            string[] addressSplit = baseAddress.Split('.');
            baseAddress = addressSplit[0] + "." + addressSplit[1] + "." + addressSplit[2];

            List<IPAddress> validAddresses = new List<IPAddress>();
            for (int i = 0; i < 255; i++)
            {
                IPAddress currentAddress = IPAddress.Parse(baseAddress + "." + i);
                PingReply reply = PingAddress(currentAddress, 200);
                if(reply.Status == IPStatus.Success)
                {
                    Console.Write(i + "[Y], ");
                    validAddresses.Add(currentAddress);
                }
                else
                {
                    Console.Write(i + "[N], ");
                }
            }

            Console.WriteLine();
            return validAddresses;
        }

        private static PingReply PingAddress(IPAddress address, int timeout = 1000)
        {
            try
            {
                return new Ping().Send(address, timeout);
            }
            catch (PingException ex)
            {
                Console.WriteLine("Ping failed - " + address.ToString() + ", " + ex.Message);
                return null;
            }
        }

        private static void PingAddressVerbose()
        {
            IPAddress pingAddress = null;
            do
            {
                Console.WriteLine("Enter IP to ping - ");
                string input = Console.ReadLine();
                try
                {
                    pingAddress = IPAddress.Parse(input);
                }
                catch (Exception)
                {
                    pingAddress = null;
                    Console.WriteLine("Invalid IP");
                }
            } while (pingAddress == null);

            Console.WriteLine("Sending ping...");
            

            Console.WriteLine(PingReplyInfo(PingAddress(pingAddress, 2000)));
        }

        private static string PingReplyInfo(PingReply reply)
        {
            string info = "Reply: ";
            info += reply.Address + Environment.NewLine;
            if (reply == null)
            {
                return "Empty reply";
            }
            else if (reply.Status == IPStatus.Success)
            {
                info += "Roundtrip = " + reply.RoundtripTime + "ms" + Environment.NewLine;
                info += "Status = " + reply.Status;
            }
            else
            {
                info += "Failed with status " + reply.Status;
            }

            return info;
        }

        private static void Exit()
        {
            Console.WriteLine("");
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
        
        public static IPAddress GetLocalIPAddress()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip;
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

    }
}
