using UnityEngine;

public class ButtonSettings : MonoBehaviour
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

    public void OpenSettings()
    {
        // --- 1. Mainkan Suara Tombol ---
        if (audioManager != null)
        {
            // Pastikan AudioManager.sfxSource sudah diubah menjadi 'public'
            // agar bisa diakses tanpa error CS0122.
            audioManager.sfxSource.PlayOneShot(audioManager.button);
        }
        
        // --- 2. Logika Utama (Aksi Navigasi) ---
        
        // aktifkan object saat ini (mungkin ini maksudnya ObjectSettings)
        if (ObjectCurrent != null)
            ObjectCurrent.SetActive(true);
            
        // Menonaktifkan objek/UI lainnya
        if (background2 != null)
            background2.SetActive(false);

        if (mainUI != null)
            mainUI.SetActive(false);

        Debug.Log("ℹ Menampilkan halaman Settings");
    }
}