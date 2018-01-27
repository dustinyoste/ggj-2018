﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

	public Button startButton;
	public string sceneName = "main_scene";

	// Use this for initialization
	void Start()
	{
		startButton.onClick.AddListener(PlayGame);
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void PlayGame()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
	}
}
