using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System.Net.Sockets;


public class ServerConnection {
	public delegate void OnConnect(ServerConnection c);

	public delegate void OnMessage(string message);

	private OnMessage m_OnMessage;

	private StreamReader m_StreamReader;

	private StreamWriter m_StreamWriter;

	private Stream m_Stream;

	private Thread m_Thread;

	private Queue<string> m_Queue = new Queue<string>();

	public StreamWriter StreamWriter {
		get {
			return m_StreamWriter;
		}
	}

	public ServerConnection(string host, int port, OnConnect onConnect, OnMessage onMessage) {
		TcpClient client = new TcpClient(host, port);
		Socket socket = client.Client;
		m_OnMessage = onMessage;	
		m_StreamReader = new StreamReader(client.GetStream());
		m_StreamWriter = new StreamWriter(client.GetStream());
		m_Stream = client.GetStream();
		
		m_Thread = new Thread(ReadLineThread);

		if (!socket.Connected)
		{
			socket.SetSocketOption(SocketOptionLevel.Socket, 
						SocketOptionName.ReceiveBuffer, 16384);
			Debug.Log("[ServerConnection] Client is not connected. ReceiveBuffer set.");
		}
		else {
			Debug.Log("[ServerConnection] Client connected");		
			m_StreamWriter.Flush();
			m_Thread.Start();
			onConnect(this);
		}
	}

	public void Update() {
		if (m_Queue.Count > 0) {
			m_OnMessage(m_Queue.Dequeue() as string);
		}
	}

	private void ReadLineThread() {
		while (m_Stream.CanRead) {
			m_Queue.Enqueue(m_StreamReader.ReadLine());
		}
	}
}
