using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginOrRegisterGUI : GUIBase {
	
	override protected void GUIStart() {}

	override protected bool OnButtonClick(string buttonName) {
		switch (buttonName) {
		case "LoginButton":
			Game.Instance.Client.PerformReply("Login");
			Hide();
			return true;
		case "RegisterButton":
			Game.Instance.Client.PerformReply("Reg new account");
			Hide();
			return true;
		default:
			return false;
		}
	}
}
