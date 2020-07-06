﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {

	public GameObject gameLoseUI;
	public GameObject gameWinUI;
	bool gameIsOver;

	// Use this for initialization
	void Start () {
		Guard.onGuardHasSpottedPlayer += ShowGameLoseUI;
		FindObjectOfType<PlayerController> ().OnReachedEndLevel += ShowGameWinUI;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameIsOver) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				SceneManager.LoadScene (0);
			}
		}
	}

	void ShowGameWinUI(){
		OnGameOver (gameWinUI);
	}

	void ShowGameLoseUI(){
		OnGameOver (gameLoseUI);
	}

	void OnGameOver(GameObject gameOverUI){
		gameOverUI.SetActive (true);
		gameIsOver = true;
		Guard.onGuardHasSpottedPlayer -= ShowGameLoseUI;
		FindObjectOfType<PlayerController> ().OnReachedEndLevel -= ShowGameWinUI;
	}
}

