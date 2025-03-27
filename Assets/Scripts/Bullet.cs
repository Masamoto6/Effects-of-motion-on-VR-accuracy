using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Settings")]
    public float lifetime = 3f;
    public float damage = 10f;
    public GameObject impactEffect;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"[BULLET] Collided with {collision.gameObject.name}");

        if (impactEffect)
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }

        if (collision.gameObject.TryGetComponent(out Target target))
        {
            target.OnHit();
        }

        Destroy(gameObject);
    }
}