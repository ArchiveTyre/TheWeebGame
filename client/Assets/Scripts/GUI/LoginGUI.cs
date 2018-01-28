using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginGUI : GUIBase {


	public void SetController(/*...*/) {

	}

	// Use this for initialization
	protected override void GUIStart () {
	}

	protected override bool OnButtonClick(string button) {
		switch(button) {
		case "LoginButton":
			var username = GetTextInput("UsernameField");
			var password = GetTextInput("PasswordField");
			Game.Instance.Client.PerformLogin(username, password);
			Hide();
			return true;
		default:
			return false;
		}
	}

}
