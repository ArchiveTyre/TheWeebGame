using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PromptStringGUI : GUIBase {

	Text m_Title;

	public void SetTitle(string title) {
		m_Title.text = title;
	}
	protected override void GUIStart () {
		m_Title = GetComponentInChildren<Text>();
	}
	
	protected override bool OnButtonClick(string button) {
		if (button == "OkButton") {
			Hide();
			var str = GetTextInput("StringField");
			Game.Instance.Client.PerformReply(str);
			return true;
		}
		else {
			return false;
		}
	}
}
