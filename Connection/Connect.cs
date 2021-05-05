using memory_game.Connection.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;

namespace memory_game.Connection
{
    class Client
    {
        public Thread thread;
        public TcpClient tcpClient;
    }
    public class Connect
    {
        private Hashtable clientsList = new Hashtable();
        private BinaryFormatter binaryFormatter = new BinaryFormatter();
        private TcpListener tcpListener;
        private Thread serverThread;
        private static long connectId = 1;
        private readonly int maxClients = 20;

        //
        //EventHandlers
        //
        public delegate void InitGameInfoReceivedEventsHandler(object sender, GameInfoEventArgs e);
        public event InitGameInfoReceivedEventsHandler initGameInfoReceived;

        public delegate void UnexpectedDisconnectionEventsHandler(object sender, UnexpectedDisconnectionEventArgs e);
        public event UnexpectedDisconnectionEventsHandler unexpectedDisconnection;

        public delegate void SuccessfullyConnectedEventsHandler(object sender, SuccessfullyConnectedEventArgs e);
        public event SuccessfullyConnectedEventsHandler successfullyConnected;

        public bool startServer(string address, int port) //zawsze true!
        {
            tcpListener = new TcpListener(IPAddress.Parse(address), port);
            tcpListener.Start();
            serverThread = new Thread(new ThreadStart(waitForTheClientsThread));
            serverThread.Name = "wątek serwera czekający na klientów id: " + 0;
            serverThread.IsBackground = false;
            serverThread.Start();
            return true;
        }

        public bool startClient(string adres, int port)
        {
            Client kli = new Client();
            IPAddress hostadd = IPAddress.Parse(adres);
            IPEndPoint EPhost = new IPEndPoint(hostadd, port);
            kli.tcpClient = new TcpClient();
            try
            {
                kli.tcpClient.Connect(EPhost);
                if (kli.tcpClient.Client.Connected)
                {
                    kli.thread = new Thread(new ParameterizedThreadStart(readFromSocketThread));
                    kli.thread.Name = "Wątek kllienta czytający z socketa id: 0";
                    clientsList.Add(0L, kli);
                    kli.thread.Start(0L);
                }
                else return false;
            }
            catch (Exception e1)
            {
                Console.WriteLine("Siakiś błąd: " + e1.Message);
                return false;
            }
            return true;
        }

        public void waitForTheClientsThread()
        {
            try
            {
                while (true)
                {
                    Client client = new Client();
                    client.tcpClient = tcpListener.AcceptTcpClientAbortable();
                    lock (clientsList)
                    {
                        if (connectId < long.MaxValue - 1)
                            Interlocked.Increment(ref connectId);
                        else
                            connectId = 1;
                        Console.WriteLine("połączono z: " + client.tcpClient.Client.RemoteEndPoint.ToString());
                        if (clientsList.Count < maxClients)
                        {
                            while (clientsList.Contains(connectId))
                            {
                                Interlocked.Increment(ref connectId);
                            }
                            client.thread = new Thread(new ParameterizedThreadStart(readFromSocketThread));
                            client.thread.Name = "wątek czytający z socketa id: " + connectId.ToString();
                            clientsList.Add(connectId, client);
                            client.thread.Start(connectId);
                            SuccessfullyConnectedEventArgs arg = new SuccessfullyConnectedEventArgs(connectId, client.tcpClient.Client.RemoteEndPoint.ToString());
                            if (successfullyConnected != null)
                                successfullyConnected(this, arg);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wątek czekający na klientów dostał wyjątkiem: " + ex.Message);
            }
        }

        public void readFromSocketThread(object id)
        {

            long realId = (long)id;
            TcpClient tcpclient = ((Client)clientsList[realId]).tcpClient;
            while (true)
            {
                if (tcpclient.Connected)
                {
                    try
                    {
                        GameInfo receivedMsg = (GameInfo)binaryFormatter.Deserialize(tcpclient.GetStream());
                        GameInfoEventArgs arg = new GameInfoEventArgs();
                        arg.initGameInfo = receivedMsg;
                        arg.connectionId = realId;
                        Console.WriteLine(receivedMsg);
                        if (initGameInfoReceived != null)
                            initGameInfoReceived(this, arg);
                    }
                    catch (SerializationException)
                    {
                        break;
                    }
                    catch (Exception)
                    {
                        if (!tcpclient.Connected)
                            break;
                    }
                }
            }
            lock (clientsList)
            {
                clientsList.Remove(realId);
            }
            UnexpectedDisconnectionEventArgs arg2 = new UnexpectedDisconnectionEventArgs(realId);
            if (unexpectedDisconnection != null)
                unexpectedDisconnection(this, arg2);
        }

        public void sendMessage(Object msg)
        {
            foreach (Client cli in clientsList.Values)
            {
                if (cli.tcpClient.Connected)
                    binaryFormatter.Serialize(cli.tcpClient.GetStream(), msg);
            }
        }

        public void fullDisconnect()
        {
            lock (clientsList)
            {
                foreach (Client kli in clientsList.Values)
                {
                    kli.tcpClient.Client.Disconnect(false);
                    kli.tcpClient.Close();
                    kli.thread.Abort();
                }
                clientsList.Clear();
                if (serverThread != null)
                    serverThread.Abort();
                if (tcpListener != null)
                    tcpListener.Server.Close();
            }
        }
    }
}
