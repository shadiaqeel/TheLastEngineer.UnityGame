﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager {

	private GameObject gameObject;

	private static GameManager m_Instance;
	public static GameManager Instance {
		get { 
			if (m_Instance == null) {
				m_Instance = new GameManager ();
				m_Instance.gameObject = new GameObject ("_gameManager");
				m_Instance.gameObject.AddComponent <Timer> ();
		
			}
			return m_Instance;
		}
	}

	
	private Timer m_Timer;
	public Timer Timer {
		get {
			if (m_Timer == null)
				m_Timer = gameObject.GetComponent<Timer> ();
			return m_Timer;
		}
	}


}
