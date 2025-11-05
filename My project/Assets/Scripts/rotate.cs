using UnityEngine;
using System.Collections;

[ExecuteAlways]
[RequireComponent(typeof(BoxCollider2D))]
public class rotate : MonoBehaviour
{
    [Header("Pengaturan Rotasi")]
    public float rotationSpeed = 150f; // kecepatan rotasi smooth
    public Vector3 rotationAxis = new Vector3(0, 0, 1); // rotasi di sumbu Z
    public bool autoRotate = false;

    [Header("GameObject Warna (Hijau=Atas, Merah=Kiri, Kuning=Kanan, Biru=Bawah)")]
    public GameObject greenObj;
    public GameObject redObj;
    public GameObject yellowObj;
    public GameObject blueObj;

    private BoxCollider2D boxCol;
    private Vector3 pivotPoint;
    private bool isAnimating = false;
    private float currentRotation = 0f; // rotasi kumulatif

    void Start()
    {
        boxCol = GetComponent<BoxCollider2D>();
        if (boxCol == null)
        {
            Debug.LogWarning("❗ Object ini tidak memiliki BoxCollider2D, rotasi mungkin tidak akurat.");
            return;
        }
        else
        {
            // Aktifkan collider untuk hitung pivot
            boxCol.enabled = true;
        }

        UpdatePivotPoint();
        
        // Nonaktifkan collider dulu
        //boxCol.enabled = false;
        UpdateActiveCollider(); // default: hijau aktif

        if (autoRotate)
            StartRotation();
    }

    void Update()
    {
        if (boxCol == null)
            return;

        UpdatePivotPoint();

        if (autoRotate)
            transform.RotateAround(pivotPoint, rotationAxis, rotationSpeed * Time.deltaTime);
    }

    public void StartRotation() => autoRotate = true;
    public void StopRotation() => autoRotate = false;

    void UpdatePivotPoint()
    {
        pivotPoint = boxCol.bounds.center;
    }

    /// <summary>
    /// Fungsi rotasi smooth untuk tombol kiri/kanan
    /// </summary>
    public void RotateByDegrees(float degrees)
    {
        if (!isAnimating)
            StartCoroutine(RotateSmooth(degrees));
    }

    private IEnumerator RotateSmooth(float degrees)
    {
        if (boxCol == null)
            boxCol = GetComponent<BoxCollider2D>();

        UpdatePivotPoint();
        isAnimating = true;

        float rotated = 0f;
        float dir = Mathf.Sign(degrees);
        float target = Mathf.Abs(degrees);

        while (rotated < target)
        {
            float step = rotationSpeed * Time.deltaTime;
            if (rotated + step > target)
                step = target - rotated;

            transform.RotateAround(pivotPoint, rotationAxis, dir * step);
            rotated += step;
            yield return null;
        }

        currentRotation += degrees;
        currentRotation = NormalizeAngle(currentRotation);
        UpdateActiveCollider();

        isAnimating = false;
    }

    float NormalizeAngle(float angle)
    {
        angle %= 360f;
        if (angle < 0) angle += 360f;
        return angle;
    }

    /// <summary>
    /// Mengaktifkan hanya 1 PolygonCollider2D sesuai rotasi.
    /// </summary>
    void UpdateActiveCollider()
    {
        float angle = NormalizeAngle(currentRotation);

        // Nonaktifkan semua collider dulu
        DisableAllColliders();

        if (angle >= 315f || angle < 45f)
        {
            ActivateCollider(greenObj, "Hijau");
        }
        else if (angle >= 45f && angle < 135f)
        {
            ActivateCollider(yellowObj, "Kuning");
        }
        else if (angle >= 135f && angle < 225f)
        {
            ActivateCollider(blueObj, "Biru");
        }
        else if (angle >= 225f && angle < 315f)
        {
            ActivateCollider(redObj, "Merah");
        }
    }

    void ActivateCollider(GameObject obj, string name)
    {
        if (obj == null)
        {
            Debug.LogWarning($"⚠️ GameObject {name} belum di-assign!");
            return;
        }

        PolygonCollider2D col = obj.GetComponent<PolygonCollider2D>();
        if (col != null)
        {
            col.enabled = true;
            Debug.Log($"✅ Collider aktif: {name}");
        }
        else
        {
            Debug.LogWarning($"⚠️ GameObject {name} tidak punya PolygonCollider2D!");
        }
    }

    void DisableAllColliders()
    {
        DisableCollider(greenObj);
        DisableCollider(redObj);
        DisableCollider(yellowObj);
        DisableCollider(blueObj);
    }

    void DisableCollider(GameObject obj)
    {
        if (obj == null) return;
        PolygonCollider2D col = obj.GetComponent<PolygonCollider2D>();
        if (col != null)
            col.enabled = false;
    }

    void OnDrawGizmos()
    {
        if (boxCol == null)
            boxCol = GetComponent<BoxCollider2D>();

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(boxCol.bounds.center, 0.05f);
    }
}
