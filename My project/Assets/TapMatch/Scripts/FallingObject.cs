using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [HideInInspector] public string colorName;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Gameplay.Instance != null && Gameplay.Instance.isGameOver)
        {
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.simulated = false; // ❗ stop total physics object
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (Gameplay.Instance.isGameOver) return;

        ColorArea area = collision.GetComponent<ColorArea>();
        if (area == null) return;

        if (area.areaName == colorName)
        {
            Gameplay.Instance.AddScore(1);
            Destroy(gameObject);
        }
        else
        {
            Gameplay.Instance.FailNormalMode(); // ❌ langsung STOP
        }
    }
}
