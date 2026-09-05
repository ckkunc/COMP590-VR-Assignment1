using UnityEngine;
using UnityEngine.InputSystem;

// Sits on the Main Camera. A tap on the Cardboard button fires a ball
// along whatever direction the player is looking.
public class ButtonClick : MonoBehaviour
{
    public BallPrefab ballPrefab;

    // Tweak these in the Inspector until the ball reaches the target.
    public float minForce = 1500f;
    public float maxForce = 2000f;

    void Update()
    {
        // The round-over tap is the restart, so don't also spend it on a shot.
        if (ScoreManager.Instance != null && ScoreManager.Instance.IsGameOver)
        {
            return;
        }

        // One ball per tap. In the Editor this relies on touch simulation from the mouse.
        if (Touchscreen.current != null && Touchscreen.current.press.wasPressedThisFrame)
        {
            BallPrefab ball = Instantiate<BallPrefab>(ballPrefab);

            // Spawn a metre ahead so the ball doesn't flash across both eyes at birth.
            ball.transform.position = transform.position + Camera.main.transform.forward;
            ball.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward *
                UnityEngine.Random.Range(minForce, maxForce));
        }
    }
}
