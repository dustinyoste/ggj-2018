using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CameraBackground : MonoBehaviour
{
	public Camera m_MainCamera;
	private SpriteRenderer background;
	private GameController gameController;

	private void Awake()
	{
		background = GetComponent<SpriteRenderer>();
		background.transform.position = new Vector3(0, 0, 10);
	}

	private void Start()
	{
		GameController.TryGetInstance(out gameController);

     transform.localScale = new Vector3(1, 1, 1);

		var width = background.sprite.bounds.size.x;
		var height = background.sprite.bounds.size.y;

		var worldScreenHeight = Camera.main.orthographicSize * 2.0;
		var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

		transform.localScale = new Vector3((float)worldScreenWidth / width,
		                                   (float)worldScreenHeight / height, 0);
	}

	private void Update()
	{
		if (gameController) {
			background.sprite = gameController.CurrentBackgroundSprite;
		}
	}
}
