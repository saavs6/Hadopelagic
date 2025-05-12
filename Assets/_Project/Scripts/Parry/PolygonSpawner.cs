using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class PolygonSpawner : MonoBehaviour
{
    public Transform playerCamera;
    public GameObject boss;
    public GameObject rock;
    public float spawnDistance = 2f;
    public float polygonRadius = 0.6f;
    public List<GameObject> polygons;

    private int size = 0;

    void Update()
    {
        Debug.Log(Success());
    }

    public void SpawnPolygon(int vertexCount)
    { 
        size = vertexCount;
        polygons = new List<GameObject>();
        Vector3 center = playerCamera.transform.position + Vector3.Normalize(boss.transform.position - playerCamera.position) * spawnDistance;
        Vector3 forward = Vector3.Normalize(boss.transform.position - playerCamera.position);
        Vector3 up = playerCamera.transform.up;
        Vector3 right = Vector3.Cross(up, forward).normalized;

        
        for (int i = 0; i < vertexCount; i++)
        {
            float angle = i * Mathf.PI * 2 / vertexCount;
            Vector3 offset = (right * Mathf.Cos(angle) + up * Mathf.Sin(angle)) * polygonRadius;
            Vector3 vertexPosition = center + offset;

            GameObject NextParry = Instantiate(rock, vertexPosition, Quaternion.identity);
            NextParry.transform.localScale *= 0.3f;
            Rigidbody nrrb = NextParry.GetComponent<Rigidbody>();
            polygons.Add(NextParry);
        }
        Debug.Log("Spawned Polygon");
    }

    public bool Success()
    {
        for (int i = 0; i < size; i++)
        {
            if (polygons[i] != null)
            {
                return false;
            }
        }
        return true;
    }

    public void DestroyAll()
    {
        for (int i = 0; i < size; i++)
        {
            if (polygons[i] != null)
            {
                Destroy(polygons[i]);
            }
        }
    }
}