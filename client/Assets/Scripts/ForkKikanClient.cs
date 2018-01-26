using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkKikanClient : MonoBehaviour {

	private ServerConnection m_Connection;

	private bool m_Connected = false;

	void Start () {
		m_Connection = new ServerConnection("localhost", 8080, OnConnect, OnMessage);
	
		/*WordReader wr = new WordReader("\"how are you?\" \"hi\" Hello World Nice!");
		Debug.Log(wr.readString());
		Debug.Log(wr.readString());
		while (true) {
			var result = wr.readWord();
			if (result == null) {
				break;
			}
			else {
				Debug.Log(result);
			}
		}*/
	}

	void OnConnect(ServerConnection c) {
		// Tell the server what protocol we are using. //
		c.StreamWriter.WriteLine("rsb_game");
		c.StreamWriter.Flush();
	}

	bool AutoAnswer(string name) {
		switch(name) {
		case "game_support":
			m_Connection.StreamWriter.WriteLine("replyOk \"TheWeebGame\"");
			m_Connection.StreamWriter.Flush();
			return true;
		case "login_or_reg":
			var gui = GUIBase.GetGUI("LoginOrRegisterGUI");
			gui.Show();
			return true;
		default:
			return false;
		}
	}

	void PromptCommand(int id, string type, string name, WordReader rest) {
		if (!AutoAnswer(name)) {
			switch(type) {
			case "promptString":
				string str = rest.readRest();
				Debug.Log("Prompt " + name + ": " + str);
				break;
			default:
				Debug.LogError("Unsupported prompt type requested by server: " + type);
				break;
			}
		}
	}

	void OnMessage(string message) {
		if (m_Connected) {
			WordReader wr = new WordReader(message);
			string commandName = wr.readWord();

			switch(commandName) { 
			case "prompt":
				int    id   = wr.readInt();
				string type = wr.readWord();
				string name = wr.readWord();
				PromptCommand(id, type, name, wr);
				break;
			default:
				Debug.LogError("Unsupported command sent from server: " + commandName);
				break;
			}
			Debug.Log("Message: " + message);
		}
		else {
			m_Connection.StreamWriter.WriteLine("www.eit.se/rsb/0.9/client");
		    m_Connection.StreamWriter.Flush();
		    m_Connected = true;
		}
	}
	
	void Update () {
		m_Connection.Update();
	}
}
