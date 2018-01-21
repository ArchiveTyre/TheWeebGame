using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System.Net.Sockets;


public class ServerConnection : MonoBehaviour {
	
	StreamReader streamReader = null;
	StreamWriter streamWriter = null;
	Stream stream = null;
	Thread thread = null;
	Queue queue = Queue.Synchronized(new Queue());

	void Start() {
		TcpClient client = new TcpClient("localhost", 9030);
		streamReader = new StreamReader(client.GetStream());
		streamWriter = new StreamWriter(client.GetStream());
		stream = client.GetStream();
		Socket socket = client.Client;

		if (!socket.Connected)
		{
			socket.SetSocketOption(SocketOptionLevel.Socket, 
						SocketOptionName.ReceiveBuffer, 16384);
			Debug.Log("[ServerConnection] Client is not connected. ReceiveBuffer set.");
		}
		else {
			Debug.Log("[ServerConnection] Client connected");
			streamWriter.WriteLine("LOGIN 123 123");
			streamWriter.Flush();
			thread = new Thread(ReadLineThread);
			thread.Start();
		}
	}

	void ReadLineThread() {
		while (stream.CanRead) {
			queue.Enqueue(streamReader.ReadLine());
		}
	}

	void Update() {
		while (queue.Count > 0) {
			Debug.Log(queue.Dequeue());
		}
	}
}
