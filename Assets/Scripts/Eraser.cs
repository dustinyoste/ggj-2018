
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Hazard))]
public class Eraser : MonoBehaviour
{
	public float degreesPerSecond = 15.0f;
	public float amplitude = 4.5f;
	public float frequency = 1f;
	public float speedForward = 1f;

	Vector3 posOffset = new Vector3();
	Vector3 tempPos = new Vector3();

	void Update()
	{
		tempPos = posOffset;
		tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
		tempPos.x += transform.position.x + (speedForward/10 * Time.fixedTime);

		transform.position = tempPos;
	}
}
