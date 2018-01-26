using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class GUIBase : MonoBehaviourWithStart {

	private Button[] m_Buttons;
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
		m_Buttons = GetComponentsInChildren<Button>();
		foreach (var button in m_Buttons) {
			button.onClick.AddListener(delegate() {
				if (!OnButtonClick(button.name)) {
					Debug.LogError("Unhandled button click: " + button.name);
				}
			});
		}
		m_GUIs.Add(gameObject.name, this);
		GUIStart();
	}

	public void Show() {

	}

	public void Hide() {

	}

	abstract protected bool OnButtonClick(string button);

	abstract protected void GUIStart();
}
