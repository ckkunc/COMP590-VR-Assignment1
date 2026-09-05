using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

// Runs the round: keeps score, counts down the clock, and handles the restart.
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public TMP_Text scoreText;

    // Length of a round in seconds. The clock is what turns a shooting toy into
    // a game: it gives the player something to win or lose against.
    public float roundSeconds = 60f;

    public bool IsGameOver { get; private set; }

    private int score;
    private float timeLeft;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (scoreText == null)
        {
            Debug.LogError("ScoreManager: no Score Text assigned in the Inspector.", this);
        }

        timeLeft = roundSeconds;
        UpdateText();
    }

    void Update()
    {
        if (IsGameOver)
        {
            // Tap once the round is over to play again.
            if (Touchscreen.current != null && Touchscreen.current.press.wasPressedThisFrame)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            return;
        }

        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0f)
        {
            timeLeft = 0f;
            IsGameOver = true;
        }

        UpdateText();
    }

    public void AddPoints(int points)
    {
        if (IsGameOver)
        {
            return;
        }

        score += points;
        UpdateText();
    }

    void UpdateText()
    {
        if (scoreText == null)
        {
            return;
        }

        if (IsGameOver)
        {
            scoreText.text = "Time!\nFinal Score: " + score + "\nTap to play again";
        }
        else
        {
            scoreText.text = "Score: " + score + "\nTime: " + Mathf.CeilToInt(timeLeft);
        }
    }
}
