using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AGrid : MonoBehaviour
{
    // Declare public variables that can be set in the inspector
    public int gridWidth;
    public int gridHeight;
    public int gridDepth;
    public GameObject prefab;
    public float spaceX; // Space between prefabs on the x-axis
    public float spaceY; // Space between prefabs on the y-axis
    public float spaceZ; // Space between prefabs on the z-axis
    public float rotationSpeedX; // Rotation speed on the x-axis
    public float rotationSpeedY; // Rotation speed on the y-axis
    public float rotationSpeedZ; // Rotation speed on the z-axis
    public float pulseRange; // The range of the scale pulse, from the original scale
    public float pulseDuration; // The duration of the scale pulse in seconds

    private void Start()
    {
        // Loop through each position in the grid
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                for (int z = 0; z < gridDepth; z++)
                {
                    // Calculate the position for this grid cell
                    float posX = x * (prefab.transform.localScale.x + spaceX);
                    float posY = y * (prefab.transform.localScale.y + spaceY);
                    float posZ = z * (prefab.transform.localScale.z + spaceZ);
                    Vector3 pos = new Vector3(posX, posY, posZ);

                    // Instantiate the prefab at the calculated position
                    GameObject go = Instantiate(prefab, pos, Quaternion.identity);

                    // Set the instantiated object as the child of the parent object
                    go.transform.parent = transform;

                    // Set the rotation speeds for the prefab instance
                    go.GetComponent<Rotator>().rotationSpeedX = rotationSpeedX;
                    go.GetComponent<Rotator>().rotationSpeedY = rotationSpeedY;
                    go.GetComponent<Rotator>().rotationSpeedZ = rotationSpeedZ;

                    
                    // Start pulsing the scale of the prefab instance
                    StartCoroutine(PulseScale(go));
                }
            }
        }
    }

    IEnumerator PulseScale(GameObject go)
    {
        // Save the original scale of the prefab instance
        Vector3 originalScale = go.transform.localScale;

        while (true)
        {
            // Increase the scale of the prefab instance
            for (float t = 0; t < pulseDuration; t += Time.deltaTime)
            {
                go.transform.localScale =
                    originalScale + pulseRange * Vector3.one * Mathf.Sin(Mathf.PI * t / pulseDuration);
                yield return null;
            }

            // Decrease the scale of the prefab instance
            for (float t = pulseDuration; t > 0; t -= Time.deltaTime)
            {
                go.transform.localScale =
                    originalScale + pulseRange * Vector3.one * Mathf.Sin(Mathf.PI * t / pulseDuration);
                yield return null;
            }
            
        }
    }
}
