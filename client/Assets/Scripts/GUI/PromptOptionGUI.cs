using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PromptOptionGUI : GUIBase {

	public GameObject OptionsPrefab;
	private GameObject m_OptionsContainer;
	private Text m_QuestionText;

	public void SetOptions(string[] options) {
		m_OptionsContainer.transform.DetachChildren();
		foreach (var option in options) {
			// Add this options to the options container. //
			GameObject optionObject = GameObject.Instantiate(OptionsPrefab, Vector3.zero, Quaternion.identity, m_OptionsContainer.transform);
			var text = optionObject.GetComponentInChildren<Text>();
			text.text = option;
			var button = optionObject.GetComponentInChildren<Button>();
			button.onClick.AddListener(delegate {
				Game.Instance.Client.PerformReply(option);
			});
		}
	}

	public void SetQuestion(string question) {
		m_QuestionText.text = question;
	}

	protected override void GUIStart () {
		m_OptionsContainer = transform.Find("Panel/Container").gameObject;
		m_QuestionText = GetComponentInChildren<Text>();
	}

	protected override bool OnButtonClick (string buttonName) {
		return false;
	}
}
