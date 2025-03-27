using System.Collections;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab;
    public float minDistance = 5f;
    public float maxDistance = 10f;
    public float minSize = 0.05f;
    public float maxSize = 0.3f;
    public Vector2 yRange = new Vector2(1f, 2f);

    private Transform cameraTransform;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        StartCoroutine(SpawnTargetsForDuration(30f, 1.2f)); // 30 seconds, new target every 1.2s
    }

    IEnumerator SpawnTargetsForDuration(float duration, float interval)
    {
        float timer = 0f;

        while (timer < duration)
        {
            SpawnSingleTarget();
            yield return new WaitForSeconds(interval);
            timer += interval;
        }
    }

    void SpawnSingleTarget()
    {
        // Distance and angle control
        float distance = Random.Range(minDistance, maxDistance);
        float angle = Random.Range(-30f, 30f); // Forward-facing spread
        float height = Random.Range(yRange.x, yRange.y);

        Vector3 spawnDirection = Quaternion.Euler(0, angle, 0) * cameraTransform.forward;
        Vector3 spawnPos = cameraTransform.position + spawnDirection.normalized * distance;
        spawnPos.y = height;

        // Create target
        GameObject target = Instantiate(targetPrefab, spawnPos, Quaternion.identity);

        // Set random size
        float size = Random.Range(minSize, maxSize);
        target.transform.localScale = Vector3.one * size;

        // Make target face player
        target.transform.LookAt(cameraTransform);
        target.transform.rotation = Quaternion.Euler(0, target.transform.eulerAngles.y + 180f, 0);

        // Pass data to target for logging
        Target targetScript = target.GetComponent<Target>();
        if (targetScript != null)
        {
            targetScript.distanceFromUser = distance;
            targetScript.targetSize = size;
        }
    }
}
