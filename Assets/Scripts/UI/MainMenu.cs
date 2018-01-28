using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[DisallowMultipleComponent]
public class MainMenu : MonoBehaviour
{
	public VideoPlayer videoplayer;
	public Image background;
	public string sceneName = "main_scene";

	// Use this for initialization
	void Start()
	{
		background.enabled = true;
		videoplayer.started += (VideoPlayer source) =>
		{
			background.enabled = !source.isPlaying;
		};
	}

	// Update is called once per frame
	void Update()
	{
		var hitEnterKey = Input.GetKey(KeyCode.KeypadEnter)
			|| Input.GetKey(KeyCode.Return)
							   || Input.GetKey((KeyCode.Space));

		var hitLetter = false;
		foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode))) {
			if (kcode >= KeyCode.A && kcode <= KeyCode.Z && Input.GetKeyDown(kcode)) {
				Debug.Log("KeyCode down: " + kcode);
				hitLetter = true;
			}
		}

		if (hitEnterKey || hitLetter) {
			PlayGame();
		} else if (Input.GetKey(KeyCode.Escape)) {
			ExitGame();
		}
	}

	protected void OnMouseDown()
	{
		PlayGame();
	}

	private void PlayGame()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
	}

	private void ExitGame()
	{
#if UNITY_EDITOR
		// Application.Quit() does not work in the editor so
		// UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}
}
