using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [HideInInspector] public string colorName;

    void OnTriggerEnter2D(Collider2D collision)
    {
        ColorArea area = collision.GetComponent<ColorArea>();
        if (area == null) return;

        if (Gameplay.Instance == null)
        {
            Debug.LogError("❌ Gameplay.Instance tidak ditemukan di scene!");
            return;
        }

        // Cek warna
        if (area.areaName == colorName)
        {
            Gameplay.Instance.AddScore(1);
            Debug.Log($"✅ {colorName} cocok dengan area {area.areaName} → +1 poin!");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log($"❌ {colorName} menyentuh area {area.areaName} → tidak cocok.");
        }
    }
}
