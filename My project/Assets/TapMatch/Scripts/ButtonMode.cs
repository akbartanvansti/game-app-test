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

    public void SelectMode()
    {
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
