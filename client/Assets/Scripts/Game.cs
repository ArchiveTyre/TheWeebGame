using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game {
	public static Game Instance = new Game();

	private ForkKikanClient m_Client = null;

	public ForkKikanClient Client {
		get {
			return m_Client;
		}
		set {
			if (m_Client == null) {
				m_Client = value;
			}
			else {
				throw new System.Exception("Can only set game.Client once!");
			}
		}
	}
}
