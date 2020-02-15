﻿
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenP2P
{
    class Program
    {
        

        public static string connectToAddress = "127.0.0.1";

        static void Main(string[] args)
        {
            NetworkTime.Start();

            bool isServer = false;
          
            for (int i = 0; i < args.Length; i++)
            {
                if( args[i].ToLower().Equals("--server") )
                {
                    isServer = true;
                }
                if( args[i].ToLower().StartsWith("--connectto") )
                {
                    if( i < args.Length - 1 )
                    {
                        //connectToAddress = args[++i];
                    }
                }
                Console.WriteLine("Arg[{0}] = [{1}]", i, args[i]);
            }

            /*
            if (isServer)
            {
                RunServer();
            }
            else
            {
                RunClient();
            }
            */
            
        
            //NetworkRSAEncryption enc = new NetworkRSAEncryption();
            //enc.Test();

            RunServer();
            RunClient();
            //Thread t = new Thread(Test1);
            //t.Start();
            //t = new Thread(Test2);
            //t.Start();

            Thread.Sleep(10000);
        }
        
        public static void RunServer()
        {
            //string localIP = NetworkConfig.GetPublicIP();
            //Console.WriteLine("IPAddress = " + localIP);
            
            NetworkServer server = new NetworkServer(9000, true);
        }

        public static void RunClient()
        {
            List<NetworkClient> clients = new List<NetworkClient>();
            NetworkClient client = null;// new NetworkClient("127.0.0.1", 9000, 9002);
            NetworkConfig.ProfileEnable();
            for (int i=0; i< NetworkConfig.MAXCLIENTS; i++)
            {
                client = new NetworkClient();

                clients.Add(client);
                

                //client.ConnectToSTUN();
            }

            for(int i=0; i< NetworkConfig.MAXSEND; i++)
            {
                clients[0].Connect("127.0.0.1", 9000, "JoeOfTexas" + i);
            }
            
            
            
           
            Thread.Sleep(4000);
            for(int i=0; i< NetworkConfig.MAXCLIENTS; i++)
            {
                Console.WriteLine("Client PacketPool Count = " + clients[i].socket.thread.PACKETPOOL.packetCount);
                //Console.WriteLine("Server PacketPool Count = " + server.protocol.socket.thread.PACKETPOOL.packetCount);
                Console.WriteLine("Client Receive Cnt: " + clients[i].receiveCnt);
                Console.WriteLine("Client bandwidth sent: " + clients[i].socket.thread.sentBufferSize);
            }
            
        }

        static void Test1()
        {
            int c = 1000000;
            var sw = new Stopwatch();
            sw.Start();
            long sum = 0;
            for (var i = 0; i < c; i++)
            {
                sum += Environment.TickCount;
            }
            sw.Stop();
            Console.WriteLine("Test1 " + c + ": " + sw.ElapsedMilliseconds + "   (ignore: " + sum + ")");
        }

        static void Test2()
        {
            int c = 1000000;
            var sw = new Stopwatch();
            sw.Start();
            long sum = 0;
            for (var i = 0; i < c; i++)
            {
                sum += DateTime.Now.Ticks;
            }
            sw.Stop();
            Console.WriteLine("Test2 " + c + ": " + sw.ElapsedMilliseconds + "   (ignore: " + sum + ")");
        }
    }
}
