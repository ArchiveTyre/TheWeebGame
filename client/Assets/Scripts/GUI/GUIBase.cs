using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class GUIBase : MonoBehaviourWithStart {

	private Dictionary<string, InputField> m_InputFields = new Dictionary<string, InputField>();
	private static Dictionary<string, GUIBase> m_GUIs = new Dictionary<string, GUIBase>();

	public static GUIBase GetGUI(string name) {
		GUIBase result;
		if (m_GUIs.TryGetValue(name, out result)) {
			return result;
		}
		else {
			Debug.LogError("Could not find GUI by the name of: " + name);
			return null;
		}
	}

	override protected sealed void Start () {
		foreach (var inputField in GetComponentsInChildren<InputField>()) {
			m_InputFields.Add(inputField.name, inputField);
		}
		foreach (var button in GetComponentsInChildren<Button>()) {
			button.onClick.AddListener(delegate() {
				if (!OnButtonClick(button.name)) {
					Debug.LogError("Unhandled button click: " + button.name);
				}
			});
		}
		m_GUIs.Add(gameObject.name, this);
		GUIStart();
		Hide();
	}

	public string GetTextInput(string fieldName) {
		InputField inputField;
		if (m_InputFields.TryGetValue(fieldName, out inputField)) {
			return inputField.text;
		}
		else {
			throw new System.Exception("Unknown field by the name: " + fieldName);
		}
	}

	private void SetVisibility(GameObject g, bool isVisible) {
		foreach (var graphic in g.GetComponentsInChildren<MaskableGraphic>()) {
			graphic.enabled = isVisible;
		}
	}

	public void Show() {
		SetVisibility(gameObject, true);
	}

	public void Hide() {
		SetVisibility(gameObject, false);
	}

	abstract protected bool OnButtonClick(string button);

	abstract protected void GUIStart();
}
