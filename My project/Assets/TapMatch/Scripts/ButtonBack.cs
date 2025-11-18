using UnityEngine;

public class ButtonBack : MonoBehaviour
{
    public GameObject ObjectCurrent;
    public GameObject background2;
    public GameObject mainUI;

    public void Back()
    {
        // Nonaktifkan object saat ini
        if (ObjectCurrent != null)
            ObjectCurrent.SetActive(false);

        if (background2 != null)
            background2.SetActive(true);

        if (mainUI != null)
            mainUI.SetActive(true);

        Debug.Log("↩️ Kembali ke Main UI");
    }
}
