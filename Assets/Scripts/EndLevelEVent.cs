
using UnityEngine;

public class EndLevelEVent : MonoBehaviour
{
	public GameObject level;
	public GameObject playerUI;
	public GameObject winUI;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
			level.SetActive(false);
			playerUI.SetActive(false);
			winUI.SetActive(true);
		}
	}
}
