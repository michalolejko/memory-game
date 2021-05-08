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
        private Dictionary<long, Client> clientsList = new Dictionary<long, Client>();
        private BinaryFormatter binaryFormatter = new BinaryFormatter();
        private TcpListener tcpListener;
        private Thread serverThread;
        private static long connectId = 1;
        private readonly int maxClients = 20;
        public GameInfo gameInfo;
        //
        //EventHandlers
        //
        public delegate void GameInfoReceivedEventsHandler(object sender, GameInfoEventArgs e);
        public event GameInfoReceivedEventsHandler GameInfoReceived;

        public delegate void UnexpectedDisconnectionEventsHandler(object sender, UnexpectedDisconnectionEventArgs e);
        public event UnexpectedDisconnectionEventsHandler unexpectedDisconnection;

        public delegate void SuccessfullyConnectedEventsHandler(object sender, SuccessfullyConnectedEventArgs e);
        public event SuccessfullyConnectedEventsHandler successfullyConnected;

        public bool StartServer(string address, int port, GameInfo gameInfo) //zawsze true!
        {
            this.gameInfo = gameInfo;
            tcpListener = new TcpListener(IPAddress.Parse(address), port);
            tcpListener.Start();
            serverThread = new Thread(new ThreadStart(WaitForTheClientsThread));
            serverThread.Name = "wątek serwera czekający na klientów id: " + 0;
            serverThread.IsBackground = false;
            serverThread.Start();
            return true;
        }

        public bool StartClient(string adres, int port)
        {
            Client cli = new Client();
            IPAddress hostadd = IPAddress.Parse(adres);
            IPEndPoint EPhost = new IPEndPoint(hostadd, port);
            cli.tcpClient = new TcpClient();
            try
            {
                cli.tcpClient.Connect(EPhost);
                if (cli.tcpClient.Client.Connected)
                {
                    cli.thread = new Thread(new ParameterizedThreadStart(ReadFromSocketThread));
                    cli.thread.Name = "Wątek kllienta czytający z socketa id: 0";
                    clientsList.Add(0L, cli);
                    cli.thread.Start(0L);
                }
                else return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Błąd przy tworzeniu klienta: " + e.Message);
                return false;
            }
            return true;
        }

        public void WaitForTheClientsThread()
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
                            while (clientsList.ContainsKey(connectId))
                                Interlocked.Increment(ref connectId);
                            client.thread = new Thread(new ParameterizedThreadStart(ReadFromSocketThread));
                            client.thread.Name = "wątek czytający z socketa id: " + connectId.ToString();
                            clientsList.Add(connectId, client);
                            client.thread.Start(connectId);
                            SuccessfullyConnectedEventArgs arg = new SuccessfullyConnectedEventArgs(connectId, client.tcpClient.Client.RemoteEndPoint.ToString());
                            successfullyConnected?.Invoke(this, arg);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wątek czekający na klientów dostał wyjątkiem: " + ex.Message);
            }
        }

        public void ReadFromSocketThread(object id)
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
                        GameInfoEventArgs arg = new GameInfoEventArgs
                        {
                            gameInfo = receivedMsg,
                            connectionId = realId
                        };
                        //Console.WriteLine(receivedMsg);
                        GameInfoReceived?.Invoke(this, arg);
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
            unexpectedDisconnection?.Invoke(this, arg2);
        }

        public void FullDisconnect()
        {
            lock (clientsList)
            {
                foreach (Client cli in clientsList.Values)
                {
                    cli.tcpClient.Client.Disconnect(false);
                    cli.tcpClient.Close();
                    cli.thread.Abort();
                }
                clientsList.Clear();
                if (serverThread != null)
                    serverThread.Abort();
                if (tcpListener != null)
                    tcpListener.Server.Close();
            }
        }
        public void SendGameInfoToAllClients(GameInfo msg)
        {
            foreach (Client cli in clientsList.Values)
                if (cli.tcpClient.Connected)
                    binaryFormatter.Serialize(cli.tcpClient.GetStream(), msg);
        }

        public void SendGameInfoToAllClients()
        {
            SendGameInfoToAllClients(gameInfo);
        }
        public void SendGameInfoToPlayerById(GameInfo msg, int id)
        {
            msg.currentPlayerConnectId = id;
            SendGameInfoToPlayerById(msg);
        }

        public void SendGameInfoToPlayerById(GameInfo msg)
        {
            binaryFormatter.Serialize(((Client)clientsList[(long)msg.currentPlayerConnectId]).tcpClient.GetStream(), msg);
        }
        public void SendGameInfoToPlayerById()
        {
            binaryFormatter.Serialize(((Client)clientsList[(long)gameInfo.currentPlayerConnectId]).tcpClient.GetStream(), gameInfo);
        }
        //returns the id of player, who takes the turn
        public int TryStartGameAsServer(GameInfo msg)
        {
            msg.currentPlayerConnectId = 0;
            SendGameInfoToAllClients(msg);
            //tutaj powinno byc czekaj na odpowiedz i kontynuuj
            Console.WriteLine("Podłączonych klientów: " + clientsList.Count);
            int randomClientId = new Random().Next(1, clientsList.Count + 2);
            if (randomClientId > 1)
                SendGameInfoToPlayerById(msg, randomClientId);
            return randomClientId;
        }

        public int NextTurn(GameInfo msg)
        {
            if (msg == null)
                return 0;
            ++msg.currentPlayerConnectId;
            if (msg.currentPlayerConnectId > clientsList.Count + 1)
                msg.currentPlayerConnectId = 1;
            /*else
                SendGameInfoToPlayerById(msg);*/
            if (msg.currentPlayerConnectId > 1)
            {
                SendGameInfoToPlayerById(msg);
                Console.WriteLine("Teraz tura gracza o ID: " + msg.currentPlayerConnectId + " - klient");
            }
            else
                Console.WriteLine("Teraz tura gracza o ID: " + msg.currentPlayerConnectId + " - serwer");

            foreach (KeyValuePair<long, Client> kvp in clientsList)
                Console.WriteLine(kvp.Key);
            return msg.currentPlayerConnectId;
        }
    }
}
