// Assets/Scripts/ScoreManager.cs
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int score = 0;
    public Text scoreText; // assign in inspector (optional)

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddScore(int amount)
    {
        score += amount;
        if (scoreText != null) scoreText.text = score.ToString();
        Debug.Log("Score: " + score);
    }
}
