using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ForkKikanClient : MonoBehaviour {

	private ServerConnection m_Connection;

	private bool m_Connected = false;

	public void PerformReply(string reply) {
		m_Connection.StreamWriter.WriteLine("replyOk \"" + reply + "\"");
		m_Connection.StreamWriter.Flush();
	}

	public void PerformLogin(string username, string password) {
		m_Connection.StreamWriter.WriteLine("replyOk \"" + username + "\"");
		m_Connection.StreamWriter.WriteLine("replyOk \"" + password + "\"");
		m_Connection.StreamWriter.Flush();
	}

	void Start () {
		Game.Instance.Client = this;
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

	private void OnConnect(ServerConnection c) {
		// Tell the server what protocol we are using. //
		c.StreamWriter.WriteLine("rsb_game");
		c.StreamWriter.Flush();
	}

	private bool AutoAnswer(string name) {
		switch(name) {
		case "game_support":
			m_Connection.StreamWriter.WriteLine("replyOk \"TheWeebGame\"");
			m_Connection.StreamWriter.Flush();
			return true;
		case "login_or_reg":
			GUIBase.GetGUI("LoginOrRegisterGUI").Show();
			return true;
		case "enter_player_pw":
			return true;
		case "enter_player_name":
			GUIBase.GetGUI("LoginGUI").Show();
			return true;
		default:
			return false;
		}
	}

	private void PromptCommand(int id, string type, string name, WordReader rest) {
		if (!AutoAnswer(name)) {
			switch(type) {
			case "buttonPrompt": {
				var prompt = GUIBase.GetGUI("PromptOptionGUI") as PromptOptionGUI;
				var question = rest.readString();
				var alternatives = rest.readInt();
				var options = new string[alternatives];
				for (int i = 0; i < alternatives; i++) {
					options[i] = rest.readString();
				}
				prompt.SetOptions(options);
				prompt.SetQuestion(question);
				prompt.Show();		
				break;
			}
			case "promptString": {
				string str = rest.readRest();
				Debug.Log("Prompt " + name + ": " + str);
				var prompt = GUIBase.GetGUI("PromptStringGUI") as PromptStringGUI;
				prompt.SetTitle(str);
				prompt.Show();
				break;
			}
			default:
				Debug.LogError("Unsupported prompt type requested by server: " + type);
				break;
			}
		}
	}

	private void OnMessage(string message) {
		if (m_Connected && message != null) {
			Debug.Log("Message: " + message);
			WordReader wr = new WordReader(message);
			string commandName = wr.readWord();

			switch(commandName) { 
			case "prompt":
				int    id   = wr.readInt();
				string type = wr.readWord();
				string name = wr.readWord();
				PromptCommand(id, type, name, wr);
				break;
			case "playerPreference":
				break;
			case "close":
				SceneManager.LoadScene("ConnectionClosed");
				break;
			default:
				Debug.LogError("Unsupported command sent from server: " + commandName);
				break;
			}
		}
		else {
			m_Connection.StreamWriter.WriteLine("www.eit.se/rsb/0.9/client");
		    m_Connection.StreamWriter.Flush();
		    m_Connected = true;
		}
	}
	
	void Update () {
		if (m_Connection != null) {
			m_Connection.Update();
		}
	}
}
