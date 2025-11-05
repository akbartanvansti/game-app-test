using UnityEngine;

[ExecuteAlways] // jalan di Editor + Play Mode
[RequireComponent(typeof(SpriteRenderer))]
public class BackgroundFollower : MonoBehaviour
{
    [Header("Pilih kamera target (kosongkan untuk otomatis Main Camera)")]
    public Camera targetCamera;

    private SpriteRenderer sr;

    void OnEnable()
    {
        sr = GetComponent<SpriteRenderer>();
        UpdateCameraReference();
        FitToCamera();
    }

    void Update()
    {
        // Jalankan otomatis di editor & runtime
        UpdateCameraReference();
        FitToCamera();
    }

    void UpdateCameraReference()
    {
        if (targetCamera == null)
        {
#if UNITY_EDITOR
            // Coba ambil kamera utama
            targetCamera = Camera.main;

            // Kalau gak ada, ambil kamera pertama yang ditemukan
            if (targetCamera == null)
            {
#if UNITY_2023_1_OR_NEWER
                Camera[] allCameras = Object.FindObjectsByType<Camera>(FindObjectsSortMode.None);
#else
                Camera[] allCameras = Object.FindObjectsOfType<Camera>();
#endif
                if (allCameras.Length > 0)
                    targetCamera = allCameras[0];
            }
#else
            targetCamera = Camera.main;
#endif
        }
    }

    void FitToCamera()
    {
        if (targetCamera == null || sr == null || sr.sprite == null)
            return;

        float height = 2f * targetCamera.orthographicSize;
        float width = height * targetCamera.aspect;

        Vector2 spriteSize = sr.sprite.bounds.size;

        float scaleX = width / spriteSize.x;
        float scaleY = height / spriteSize.y;
        float finalScale = Mathf.Max(scaleX, scaleY);

       transform.position = new Vector3(
            targetCamera.transform.position.x,
            targetCamera.transform.position.y,
            targetCamera.transform.position.z + 5f // tetap di belakang kamera tapi masih terlihat
        );


        sr.drawMode = SpriteDrawMode.Sliced;
    }
}
