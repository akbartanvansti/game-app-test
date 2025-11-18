using UnityEngine;

public class ButtonExit : MonoBehaviour
{
    public void ExitGame()
    {
        Debug.Log("🚪 Keluar dari game...");

        // Untuk aplikasi build (Android/Windows)
        Application.Quit();

        // Untuk editor biar tidak error
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
