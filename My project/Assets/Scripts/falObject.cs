using UnityEngine;

public class falObject : MonoBehaviour
{
    [Header("Objek sumber (di scene, nonaktif)")]
    public GameObject squareMerah;
    public GameObject squareBiru;
    public GameObject squareHijau;
    public GameObject squareKuning;

    [Header("Area tempat spawn (harus punya BoxCollider2D)")]
    public GameObject spawnAreaObject;

    [Header("Pengaturan Spawn")]
    public float spawnInterval = 1.5f;
    public float yOffset = 0f;

    private float timer;

    void Start()
    {
        timer = spawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnObject();
            timer = spawnInterval;
        }
    }

    void SpawnObject()
    {
        if (spawnAreaObject == null)
        {
            Debug.LogWarning("⚠️ spawnAreaObject belum diatur di Inspector!");
            return;
        }

        BoxCollider2D areaCollider = spawnAreaObject.GetComponent<BoxCollider2D>();
        if (areaCollider == null)
        {
            Debug.LogWarning("⚠️ spawnAreaObject tidak memiliki BoxCollider2D!");
            return;
        }

        // 🎯 Hitung posisi acak di area spawn
        float areaTopY = areaCollider.transform.position.y
                        + areaCollider.offset.y
                        + (areaCollider.size.y * areaCollider.transform.lossyScale.y / 2f);

        float halfWidth = (areaCollider.size.x * areaCollider.transform.lossyScale.x) / 2f;
        float areaCenterX = areaCollider.transform.position.x + areaCollider.offset.x;
        float randomX = Random.Range(areaCenterX - halfWidth, areaCenterX + halfWidth);
        float spawnY = areaTopY + yOffset;

        // 🎨 Pilih object sumber (inactive di scene)
        int rand = Random.Range(0, 4);
        GameObject sourceObject = null;
        string warna = "";

        switch (rand)
        {
            case 0: sourceObject = squareMerah; warna = "Merah"; break;
            case 1: sourceObject = squareBiru; warna = "Biru"; break;
            case 2: sourceObject = squareHijau; warna = "Hijau"; break;
            case 3: sourceObject = squareKuning; warna = "Kuning"; break;
        }

        if (sourceObject == null)
        {
            Debug.LogWarning($"⚠️ Objek sumber untuk warna {warna} belum diatur!");
            return;
        }

        // Ambil Z posisi dari object sumber
        float spawnZ = sourceObject.transform.position.z;
        Vector3 spawnPos = new Vector3(randomX, spawnY, spawnZ);

        // 🧱 Salin dari object scene (bukan prefab)
        GameObject newObj = Instantiate(sourceObject, spawnPos, sourceObject.transform.rotation);
        newObj.SetActive(true); // aktifkan hasil instansiasi

        // 🔽 Tambahkan Rigidbody kalau belum ada
        Rigidbody2D rb = newObj.GetComponent<Rigidbody2D>();
        if (rb == null)
            rb = newObj.AddComponent<Rigidbody2D>();

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = Gameplay.Instance.GetSpeedForCurrentLevel();

        // 🧠 Pastikan FallingObject ada
        FallingObject fo = newObj.GetComponent<FallingObject>();
        if (fo == null)
            fo = newObj.AddComponent<FallingObject>();

        fo.colorName = warna;

        Debug.Log($"🧩 Spawned '{warna}' pada posisi (X={spawnPos.x:F2}, Y={spawnPos.y:F2}, Z={spawnZ:F2})");
    }

    void OnDrawGizmosSelected()
    {
        if (spawnAreaObject == null) return;
        BoxCollider2D box = spawnAreaObject.GetComponent<BoxCollider2D>();
        if (box == null) return;

        Bounds b = box.bounds;
        Gizmos.color = new Color(0f, 1f, 1f, 0.25f);
        Gizmos.DrawCube(b.center, b.size);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(b.center, b.size);
    }
}
