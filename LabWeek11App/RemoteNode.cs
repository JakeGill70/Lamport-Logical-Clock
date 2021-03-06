﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace LabWeek11App
{
    class RemoteNode
    {
        private Socket _remoteSocket;
        public IPEndPoint RemoteEndPoint { get; set; }
        public ServerNode LocalServer { get; set; }

        public RemoteNode(ServerNode localServer) {
            LocalServer = localServer;
            _remoteSocket = null;
            RemoteEndPoint = null;
        }

        public void ConnectToRemoteEndPoint(IPAddress serverIpAddress, int serverPort) {
            RemoteEndPoint = new IPEndPoint(serverIpAddress, serverPort);
            _remoteSocket = new Socket(RemoteEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _remoteSocket.Connect(RemoteEndPoint);
            LocalServer.ReportMessage("Connected to: " + RemoteEndPoint.Address.ToString());

        }

        private byte[] EncodeRequest(string request) {
            request = request + "<EOF>";
            byte[] requestData = ASCIIEncoding.ASCII.GetBytes(request);
            return requestData;
        }

        public void SendRequest(string request) {
            LocalServer.ReportMessage("SENDING: " + request + "");
            byte[] requestData = EncodeRequest(request);
            _remoteSocket.Send(requestData);
        }

    }
}
