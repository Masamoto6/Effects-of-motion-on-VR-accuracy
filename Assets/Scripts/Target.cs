using UnityEngine;
using System.IO;

public class Target : MonoBehaviour
{
    [Header("Settings")]
    public int scoreValue = 10;
    public float destroyDelay = 0.5f;
    public Color hitColor = Color.red;

    [HideInInspector] public float targetSize;
    [HideInInspector] public float distanceFromUser;

    [Header("Effects")]
    public ParticleSystem hitEffect;
    public AudioClip hitSound;

    private Renderer targetRenderer;
    private AudioSource audioSource;
    private bool wasHit = false;
    private string logFilePath;

    void Start()
    {
        targetRenderer = GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();
        if (!audioSource) audioSource = gameObject.AddComponent<AudioSource>();

        // Set file path to Desktop
        logFilePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), "hit_log.csv");


        // Create CSV file with headers if it doesn't exist
        if (!File.Exists(logFilePath))
        {
            File.WriteAllText(logFilePath, "Time,TargetSize,DistanceFromCamera\n");
        }
    }

    public void OnHit()
    {
        if (wasHit) return;
        wasHit = true;

        Debug.Log($"[TARGET] Hit registered on {gameObject.name}");

        // Visual feedback
        if (targetRenderer) targetRenderer.material.color = hitColor;
        if (hitEffect) Instantiate(hitEffect, transform.position, Quaternion.identity);

        // Audio feedback
        if (hitSound) audioSource.PlayOneShot(hitSound);

        // Disable collider immediately
        if (TryGetComponent(out Collider col)) col.enabled = false;

        // Log data
        LogHitData();

        // Destroy after delay
        Destroy(gameObject, destroyDelay);
    }

    private void LogHitData()
    {
        string line = $"{System.DateTime.Now:HH:mm:ss},{targetSize:F2},{distanceFromUser:F2}\n";
        File.AppendAllText(logFilePath, line);
    }
}
