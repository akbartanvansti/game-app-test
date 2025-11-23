using UnityEngine;

public class ButtonExit : MonoBehaviour
{
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

    public void ExitGame()
    {
        // --- 1. Mainkan Suara Tombol (Sangat Penting: Gunakan Coroutine jika ingin memastikan suara selesai) ---
        if (audioManager != null)
        {
            // Panggil SFX 'button'
            audioManager.sfxSource.PlayOneShot(audioManager.button);
        }
        
        Debug.Log("🚪 Keluar dari game...");

        // Karena Application.Quit() dijalankan SANGAT cepat, seringkali suara tidak sempat dimainkan.
        // Untuk memastikan suara sempat terdengar sebelum aplikasi ditutup (hanya perlu delay 0.1-0.2 detik):
        StartCoroutine(QuitAfterDelay(0.2f));
    }
    
    // Coroutine untuk menunggu sebentar sebelum keluar
    System.Collections.IEnumerator QuitAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        // Untuk aplikasi build (Android/Windows)
        Application.Quit();

        // Untuk editor biar tidak error
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}