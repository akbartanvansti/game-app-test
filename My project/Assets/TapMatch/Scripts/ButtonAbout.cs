using UnityEngine;

public class ButtonAbout : MonoBehaviour
{
    public GameObject ObjectCurrent;
    public GameObject background2;
    public GameObject mainUI;

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

    public void OpenAbout()
    {
        // --- 1. Mainkan Suara Tombol ---
        if (audioManager != null)
        {
            // Panggil SFX 'button'
            audioManager.sfxSource.PlayOneShot(audioManager.button);
        }
        
        // --- 2. Logika Utama (Aksi Navigasi) ---
        
        // aktifkan object saat ini (halaman About/Tentang Kami)
        if (ObjectCurrent != null)
            ObjectCurrent.SetActive(true);
            
        // Menonaktifkan objek/UI lainnya (seperti Home Page)
        if (background2 != null)
            background2.SetActive(false);

        if (mainUI != null)
            mainUI.SetActive(false);

        Debug.Log("ℹ️ Menampilkan halaman About");
    }
}