using UnityEngine;
using TMPro;

public class Gameplay : MonoBehaviour
{
    [Header("Mode Permainan")]
    public bool isTimeMode = false; // ✔ checklist untuk testing

    [Header("Gameplay Object")]
    public GameObject objectGameplay;

    [Header("UI Panel")]
    public GameObject mainUI;

    [Header("Manager")]
    public GameObject managerGameplay;

    [Header("UI Skor")]
    public TextMeshProUGUI scoreText;

    [Header("UI Skor Tertinggi")]
    public TextMeshProUGUI highScoreText;

    [Header("UI Time (khusus mode cepat)")]
    public GameObject timeUI;
    public TextMeshProUGUI timeText;
    public float fastModeDuration = 180f;

    [Header("UI Stop Game Window")]
    public GameObject stopGameWindow;
    public TextMeshProUGUI stopHeaderText;
    public TextMeshProUGUI stopScoreText;


    private float timer;

    [Header("Level & Kecepatan Jatuh")]
    public float[] levelSpeeds = { 1f, 1.5f, 2f, 2.5f, 3f };
    public int[] scoreThresholds = { 10, 25, 50, 80 };

    [Header("Data Permainan")]
    public int currentLevel = 0;
    public int currentScore = 0;
    public bool isGameOver = false;

    public static Gameplay Instance { get; private set; }

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {

        UpdateHighScoreUI();

        currentScore = 0;
        currentLevel = 0;

        if (isTimeMode)
            StartFastMode();
        else
            StartNormalMode();

        UpdateScoreUI();
    }

    // ----------------------------------------------------------
    // MODE NORMAL
    // ----------------------------------------------------------
    void StartNormalMode()
    {
        if (timeUI != null)
            timeUI.SetActive(false);
    }

    public void FailNormalMode()
    {
        if (isTimeMode) return; // ❗ jgn freeze kalau mode cepat
        if (isGameOver) return;

        isGameOver = true;

        DestroyAllFallingObjects(); // ✅ tambahkan ini

        Debug.Log("❌ Salah warna → GAME STOP (Mode Normal)");
        ShowStopGame("Permainan Selesai");
    }

    // ----------------------------------------------------------
    // MODE CEPAT
    // ----------------------------------------------------------
    void StartFastMode()
    {
        timer = fastModeDuration;
        if (timeUI != null)
            timeUI.SetActive(true);
    }

    void Update()
    {
        if (isGameOver) return;

        if (isTimeMode)
            UpdateTimer();
    }

    void UpdateTimer()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = 0;
            EndFastMode();
        }

        if (timeText != null)
            timeText.text = $"{timer:F1}";
    }

    void EndFastMode()
    {
        if (isGameOver) return;

        isGameOver = true;

        DestroyAllFallingObjects();

        Debug.Log("⏳ Waktu habis → GAME STOP (Mode Cepat)");
        ShowStopGame("Waktu Habis");

        // Nonaktifkan Script pada manager gameplay dan falObject
        if (managerGameplay != null)
        {
            Gameplay gp = managerGameplay.GetComponentInChildren<Gameplay>();
            if (gp != null)
                gp.enabled = false;

            falObject fo = managerGameplay.GetComponentInChildren<falObject>();
            if (fo != null)
                fo.enabled = false;
        }
    }

    // ----------------------------------------------------------
    // SKOR
    // ----------------------------------------------------------
    public void AddScore(int amount)
    {
        if (isGameOver) return;

        currentScore += amount;
        UpdateScoreUI();
        CheckLevelProgress();

        SavePlayerScore();
        UpdateHighScoreUI();
    }

    void CheckLevelProgress()
    {
        if (currentLevel < scoreThresholds.Length &&
            currentScore >= scoreThresholds[currentLevel])
        {
            currentLevel++;
            Debug.Log($"🔥 Level Naik ke {currentLevel + 1} | Speed {GetSpeedForCurrentLevel()}");
        }
    }

    public float GetSpeedForCurrentLevel()
    {
        if (levelSpeeds.Length == 0)
            return 1f;

        return currentLevel >= levelSpeeds.Length
            ? levelSpeeds[^1]
            : levelSpeeds[currentLevel];
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = $"{currentScore}";
    }

    void SavePlayerScore()
    {
        // Buat key berdasarkan waktu
        string timeKey = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string fullKey = "score_" + timeKey;

        // Simpan skor saat ini
        PlayerPrefs.SetInt(fullKey, currentScore);

        // Cek dan update skor tertinggi
        int high = PlayerPrefs.GetInt("highscore", 0);

        if (currentScore > high)
        {
            PlayerPrefs.SetInt("highscore", currentScore);
            Debug.Log($"🏆 Rekor baru! Skor tertinggi: {currentScore}");
        }
        else
        {
            Debug.Log($"Skor disimpan: {currentScore}");
        }

        PlayerPrefs.Save(); // flush data
    }

    void UpdateHighScoreUI()
    {
        if (highScoreText != null)
        {
            int high = PlayerPrefs.GetInt("highscore", 0);
            highScoreText.text = high.ToString();
        }
    }

    public void ShowStopGame(string header)
    {
        // Pastikan semua objek jatuh dihapus sebelum menampilkan layar berhenti
        DestroyAllFallingObjects();

        if (stopGameWindow == null)
        {
            Debug.LogError("❌ stopGameWindow belum diassign!");
            return;
        }

        // Nonaktifkan gameplay object
        objectGameplay.SetActive(false);

        // Nonaktifkan main UI
        mainUI.SetActive(false);

        // Nonaktifkan script pada manager gameplay dan falObject
        Gameplay gp = objectGameplay.GetComponentInChildren<Gameplay>();
        if (gp != null)
            gp.enabled = false;

        // Nonaktifkan script falObject
        falObject fo = objectGameplay.GetComponentInChildren<falObject>();
        if (fo != null)
            fo.enabled = false;

        stopGameWindow.SetActive(true);

        // update judul
        if (stopHeaderText != null)
            stopHeaderText.text = header;

        // update skor player
        if (stopScoreText != null)
            stopScoreText.text = currentScore.ToString();

        Debug.Log("📌 Menampilkan StopGame: " + header);
    }

   void DestroyAllFallingObjects()
{
    var fallingObjects = FindObjectsByType<FallingObject>(FindObjectsSortMode.None);

    foreach (var obj in fallingObjects)
    {
        Destroy(obj.gameObject);
    }

    Debug.Log($"🧹 Menghapus {fallingObjects.Length} objek jatuh dari scene");
}
}
