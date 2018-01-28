using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
[RequireComponent(typeof(Hazard))]
public class Eraser : MonoBehaviour
{
	public float degreesPerSecond = 15.0f;
	public float amplitude = 4.5f;
	public float frequency = 1f;
	public float speedForward = 1f;
    
    private float gameOverTimer = 0;
    private bool gameover = false;
    public float gameOverTimerCountDown = 3; //seconds

	Vector3 posOffset = new Vector3();
	Vector3 tempPos = new Vector3();

	void Update()
	{
        if (gameover) {
            gameOverTimer += Time.deltaTime;
            return;
        }

		tempPos = posOffset;
		tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
		tempPos.x += transform.position.x + (speedForward/10 * Time.fixedTime);

		transform.position = tempPos;
	}

	void FixedUpdate() {
        if (gameOverTimer > gameOverTimerCountDown) {
            SceneManager.LoadScene("main_menu");
        }
    }

	private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player") {
            GameOver();
        }
    }

	private void OnTriggerEnter2D(Collider2D other)
	{
        if (other.gameObject.tag == "Player") {
            GameOver();
        }
    }

    private void GameOver() {
        GameObject.Find("GameOverCanvas").GetComponent<LayerRenderer>().ToggleLayer(true);
        GameObject.Find("Canvas").SetActive(false);
        gameover = true;
    }
}
