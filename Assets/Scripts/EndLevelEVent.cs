
using UnityEngine;

public class EndLevelEVent : MonoBehaviour
{
	public GameObject blocks;
	public GameObject levers;
	public GameObject players;
	public GameObject ground;
	public GameObject playerUI;
	public GameObject winUI;
	private bool didWin;

	protected void Update()
	{
		if (didWin) {
			if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Escape)) {
#if UNITY_EDITOR
				// Application.Quit() does not work in the editor so
				// UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
				UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
			blocks.SetActive(false);
			levers.SetActive(false);
			players.SetActive(false);
			ground.SetActive(false);
			playerUI.SetActive(false);
			winUI.SetActive(true);
			didWin = true;
		}
	}
}
