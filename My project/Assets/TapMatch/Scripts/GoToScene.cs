using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    // Nama scene yang mau dituju
    public string sceneName;

    public void Go()
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("❌ Scene name belum diisi!");
            return;
        }

        SceneManager.LoadScene(sceneName);
    }
}
