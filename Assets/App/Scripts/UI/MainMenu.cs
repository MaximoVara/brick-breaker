﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	public void OnPlay() {
		SceneManager.LoadScene("Game");
	}
}
