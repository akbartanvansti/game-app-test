using UnityEngine;
using TMPro;

public class Gameplay : MonoBehaviour
{
    [Header("UI Skor")]
    public TextMeshProUGUI scoreText;

    [Header("Level & Kecepatan Jatuh")]
    public float[] levelSpeeds = { 1f, 1.5f, 2f, 2.5f, 3f };
    public int[] scoreThresholds = { 10, 25, 50, 80 };

    [Header("Data Permainan")]
    public int currentLevel = 0;
    public int currentScore = 0;

    public static Gameplay Instance { get; private set; }

    void Awake()
    {
        // Singleton agar mudah diakses dari mana pun
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        currentScore = 0;
        currentLevel = 0;
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        Debug.Log($"+{amount} poin! Total: {currentScore}");
        UpdateScoreUI();
        CheckLevelProgress();
    }

    void CheckLevelProgress()
    {
        if (currentLevel < scoreThresholds.Length &&
            currentScore >= scoreThresholds[currentLevel])
        {
            currentLevel++;
            Debug.Log($"🔥 Level Naik ke {currentLevel + 1}! Kecepatan {GetSpeedForCurrentLevel()}");
        }
    }

    public float GetSpeedForCurrentLevel()
    {
        if (levelSpeeds.Length == 0)
            return 1f;
        return currentLevel >= levelSpeeds.Length ? levelSpeeds[^1] : levelSpeeds[currentLevel];
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = $"{currentScore}";
    }
}
