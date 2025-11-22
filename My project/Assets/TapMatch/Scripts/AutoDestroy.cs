using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float destroyY = -10f;

    void Update()
    {
        if (transform.position.y < destroyY)
        {
            Destroy(gameObject);
        }
    }
}
