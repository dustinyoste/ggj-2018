using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeRenderer : MonoBehaviour
{
	private SpriteRenderer sprite;
    private float gameWidth = 20;
    private Transform camera;
    public int pictureLevel;

    const int totalPics = 6;

    void Awake() {
		sprite = GetComponent<SpriteRenderer>();
        
        // if convenient grab camera here, else it's public and optimize adding in editor, prob same with sprite ¯\_(ツ)_/¯
        //camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

	void Start() {
	}

    public void FixedUpdate() {
        Color tmp = sprite.color;

        var cameraPos = camera.transform.position.x;
        if (cameraPos <= 0) {
            if (pictureLevel == 1) {
                tmp.a = 1;
            } else {
                tmp.a = 0;
            }
        } else {
            var indexesToShow = (cameraPos / gameWidth) * totalPics;
            tmp.a = pictureLevel / indexesToShow;
        }

        sprite.color = tmp;
    }
}
