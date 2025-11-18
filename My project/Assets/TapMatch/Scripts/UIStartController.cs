using UnityEngine;
using UnityEngine.UI;

public class UIStartController : MonoBehaviour
{
    [Header("UI Panel")]
    public GameObject mainUI;
    public GameObject modeWindow;

    [Header("Gameplay Object")]
    public GameObject objectGameplay;   // ← tambahkan

    void Start()
    {
        // matikan gameplay di awal
        if (objectGameplay != null)
            objectGameplay.SetActive(false);

        // UI awal
        mainUI.SetActive(false);
        modeWindow.SetActive(true);
    }
}
