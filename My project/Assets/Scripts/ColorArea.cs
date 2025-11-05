using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class ColorArea : MonoBehaviour
{
    [Header("Nama area warna (isi manual, misal: Merah, Biru, Hijau, Kuning)")]
    public string areaName = "TidakDikenal";

    void Start()
    {
        PolygonCollider2D poly = GetComponent<PolygonCollider2D>();
        poly.isTrigger = true;

        // Debug info agar tahu area mana yang aktif
        Debug.Log($"🎨 Area aktif: {areaName} ({gameObject.name}) siap mendeteksi warna yang cocok.");
    }
}
