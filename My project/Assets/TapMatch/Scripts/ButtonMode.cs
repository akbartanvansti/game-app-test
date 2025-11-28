using UnityEngine;

public class ButtonMode : MonoBehaviour
{
    [Header("UI Panel")]
    public GameObject mainUI;
    public GameObject modeWindow;

    [Header("Gameplay Object")]
    public GameObject objectGameplay;

    [Header("Manager")]
    public GameObject gameManager;

    [Header("Mode Pilihan")]
    public bool isTimeMode = false; // normal=false, cepat=true

    // Tambahkan reference untuk AudioManager
    AudioManager audioManager; 

    public void Start()
    {
        // Cari objek AudioManager menggunakan Tag "Audio"
        GameObject audioManagerObject = GameObject.FindWithTag("Audio");

        if (audioManagerObject != null)
        {
            // Dapatkan komponen AudioManager
            audioManager = audioManagerObject.GetComponent<AudioManager>();
        }
        else
        {
            Debug.LogError("❌ Objek AudioManager dengan Tag 'Audio' tidak ditemukan di scene!");
        }
    }

    public void SelectMode()
    {
        // --- 1. Mainkan Suara Tombol ---
        if (audioManager != null)
        {
            // Panggil SFX 'button'
            audioManager.sfxSource.PlayOneShot(audioManager.button);
        }
        
        // --- 2. Logika Utama Seleksi Mode ---

        // aktifkan gameplay object
        objectGameplay.SetActive(true);

        // aktifkan script Gameplay di dalam objectGameplay
        Gameplay gp = gameManager.GetComponentInChildren<Gameplay>();
        if (gp != null)
        {
            gp.enabled = true;
            gp.isTimeMode = isTimeMode;     // set mode permainan
        }

        // aktifkan script falObject
        falObject fo = gameManager.GetComponentInChildren<falObject>();
        if (fo != null)
            fo.enabled = true;

        // tampilkan Main UI
        mainUI.SetActive(true);

        // sembunyikan mode window
        modeWindow.SetActive(false);

        Debug.Log("▶ Mode dipilih: " + (isTimeMode ? "Cepat" : "Normal"));
    }
}