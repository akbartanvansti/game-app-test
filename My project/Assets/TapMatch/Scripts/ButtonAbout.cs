using UnityEngine;

public class ButtonAbout : MonoBehaviour
{
    public GameObject ObjectCurrent;
    public GameObject background2;
    public GameObject mainUI;

    public void OpenAbout()
    {
        // aktifkan object saat ini
        if (ObjectCurrent != null)
            ObjectCurrent.SetActive(true);
            
        if (background2 != null)
            background2.SetActive(false);

        if (mainUI != null)
            mainUI.SetActive(false);

        Debug.Log("ℹ️ Menampilkan halaman About");
    }
}
