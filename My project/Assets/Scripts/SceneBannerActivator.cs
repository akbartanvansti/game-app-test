using UnityEngine;
using System.Collections;

public class SceneBannerActivator : MonoBehaviour
{
    void Start()
    {
        // Penting: Memastikan AdManager telah menyelesaikan Awake/Start-nya 
        // sebelum kita mencoba memanggil fungsinya.
        if (AdManager.Instance != null)
        {
            // Panggil ShowBanner() setelah jeda kecil (0.3 detik) 
            // untuk mengatasi masalah rendering UI Canvas di scene baru.
            StartCoroutine(DelayedShowBanner(0.3f));
        }
    }

    private IEnumerator DelayedShowBanner(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (AdManager.Instance != null)
        {
            AdManager.Instance.ShowBanner();
            // Anda mungkin ingin menambahkan logika untuk menggeser UI Anda
            // ke atas di sini jika tombol navigasi menutupi iklan.
        }
    }
}