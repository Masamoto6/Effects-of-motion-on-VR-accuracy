using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class GunController : MonoBehaviour
{
    [Header("Core Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;

    [Header("Input")]
    public InputActionProperty shootAction;

    private void OnEnable()
    {
        shootAction.action.Enable();
        shootAction.action.performed += OnShoot;
    }

    private void OnDisable()
    {
        shootAction.action.performed -= OnShoot;
        shootAction.action.Disable();
    }



    void Start()
    {
        Debug.Log("Shoot Action enabled: " + shootAction.action.enabled);
        Debug.Log("Shoot Action bindings: " + string.Join(", ", shootAction.action.bindings));
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        Debug.Log("Trigger pressed - Value: " + context.ReadValue<float>());

        if (!bulletPrefab || !firePoint)
        {
            Debug.LogError("Missing references! BulletPrefab: " + bulletPrefab + " FirePoint: " + firePoint);
            return;
        }

        // Visual debug
        Debug.DrawRay(firePoint.position, firePoint.forward * 100f, Color.green, 2f);
        Debug.Log("[FIRE] Position: " + firePoint.position + " Forward: " + firePoint.forward);

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        if (bullet.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.linearVelocity = firePoint.forward * bulletSpeed;
            Debug.Log("[BULLET] Velocity: " + rb.linearVelocity);
        }
    }


    [Header("Alignment")]
    public Vector3 positionOffset = new Vector3(0.1f, -0.05f, 0.2f); // Adjust gun alignment
    public Vector3 rotationOffset = new Vector3(10f, 0f, 0f); // Tilt adjustment

    void Update()
    {
        if (controllerTransform == null)
        {
            // Automatically find the hand controller
            controllerTransform = GameObject.Find("RightHand Controller")?.transform;
            if (controllerTransform == null) return;
        }

        // Precise positioning
        transform.position = controllerTransform.TransformPoint(positionOffset);
        transform.rotation = controllerTransform.rotation * Quaternion.Euler(rotationOffset);
    }

    private Transform controllerTransform;
}