using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginGUI : GUIBase {


	public void SetController(/*...*/) {

	}

	// Use this for initialization
	protected override void GUIStart () {
		Debug.Log("Hello From Login GUI!");
	}

	protected override bool OnButtonClick(string button) {
		switch(button) {
		case "LoginButton":
			// Do something here!
			return true;
		default:
			return false;
		}
	}

}
