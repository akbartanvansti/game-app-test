using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    // Nama scene yang mau dituju
    public string sceneName;
    
    // Objek AudioManager (reference)
    AudioManager audioManager; // Tidak perlu SerializeField karena dicari di Start()

    public void Start()
    {
        // 1. Cari objek dengan Tag "Audio"
        GameObject audioManagerObject = GameObject.FindWithTag("Audio");

        if (audioManagerObject != null)
        {
            // 2. Dapatkan komponen AudioManager dari objek tersebut
            audioManager = audioManagerObject.GetComponent<AudioManager>();
        }
        else
        {
            Debug.LogError("❌ Objek AudioManager dengan Tag 'Audio' tidak ditemukan di scene!");
        }
    }

    public void Go()
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("❌ Scene name belum diisi!");
            return;
        }

        // --- Tambahkan Logika Suara Tombol di Sini ---
        
        // Cek jika AudioManager sudah berhasil ditemukan
        if (audioManager != null)
        {
            // Panggil fungsi untuk memainkan SFX 'button'
            audioManager.sfxSource.PlayOneShot(audioManager.button);
            
            // Tambahkan jeda singkat (coroutine) jika SceneManager.LoadScene() terlalu cepat
            // Namun, untuk kesederhanaan, kita coba muat scene langsung:
        }
        
        // Muat Scene setelah suara dipicu (idealnya ada delay, tapi untuk tombol cepat kita langsung muat)
        SceneManager.LoadScene(sceneName);
    }
}