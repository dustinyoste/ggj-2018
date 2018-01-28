using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerRenderer : MonoBehaviour
{
	private SpriteRenderer[] sprites;
    public bool startHidden;

	void Start()
	{
		sprites = GetComponentsInChildren<SpriteRenderer>();
        if (startHidden) {
            ToggleLayer(false);
        }
	}

	public void ToggleLayer(bool show)
	{
		foreach (var item in sprites) {
			Color tmp = item.color;
			tmp.a = show ? 1 : 0f;
			item.color = tmp;
		}
	}
}
