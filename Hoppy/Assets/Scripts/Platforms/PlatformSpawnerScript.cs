using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.UIElements;

public class PlatformSpawnerScript : MonoBehaviour
{
    private const float MAX_DISTANCE = 25.0f;
    private const float SPAWN_DISTANCE_FROM_BUNNY = 15.0f;
    private const int INITIAL_PLATFORM_COUNT = 5;

    [SerializeField] private GameObject m_platformPrefab;
    [SerializeField] private Transform m_bunnyTransform;
    [SerializeField] private Camera m_camera;
    [Tooltip("Minimum Y that platforms can spawn at")]
    [SerializeField] private float m_minY;
    [Tooltip("Maximum Y that platforms can spawn at")]
    [SerializeField] private float m_maxY;
    [Tooltip("A platform will be spawned every this amount of units in the Y axis")]
    [SerializeField] private float m_platformSpawnDistance = 1.0f;
    private float m_minX;
    private float m_maxX;
    private float m_lastSpawnedY;

    private List<GameObject> instantiatedObjects = new List<GameObject>();

    void Start()
    {
        Assert.IsTrue(m_minY < m_maxY, "Min Y must be less than Max Y");

        m_minX = m_camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        m_maxX = m_camera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        Assert.IsTrue(m_minX < m_maxX, "Min X must be less than Max X");

        m_lastSpawnedY = m_bunnyTransform.position.y;

        for (int i = 0; i < INITIAL_PLATFORM_COUNT; ++i)
        {
            if (CanSpawnPlatform())
            {
                SpawnRandomisedPlatform();
            }
        }
    }

    void Update()
    {
        if (CanSpawnPlatform())
        {
            SpawnRandomisedPlatform();
        }

        RemoveDistantPlatforms();
    }

    private void SpawnRandomisedPlatform()
    {
        InstantiatePlatform(m_platformPrefab, GetRandomPlatformPosition());
    }

    private bool CanSpawnPlatform()
    {
        bool bunnyHighEnough = m_bunnyTransform.position.y + SPAWN_DISTANCE_FROM_BUNNY > m_lastSpawnedY + m_platformSpawnDistance;
        bool bunnyInRange = m_bunnyTransform.position.y < m_maxY && m_bunnyTransform.position.y > m_minY;
        return bunnyHighEnough && bunnyInRange;
    }

    private Vector3 GetRandomPlatformPosition()
    {
        float x = Random.Range(m_minX, m_maxX);
        return new Vector3(x, m_lastSpawnedY + m_platformSpawnDistance, transform.position.z);
    }

    private void InstantiatePlatform(GameObject prefab, Vector3 pos)
    {
        m_lastSpawnedY = pos.y;
        GameObject newObject = Instantiate(prefab, pos, Quaternion.identity);
        instantiatedObjects.Add(newObject);
    }

    void RemoveDistantPlatforms()
    {
        for (int i = instantiatedObjects.Count - 1; i >= 0; i--)
        {
            GameObject obj = instantiatedObjects[i];
            if (obj == null)
            {
                instantiatedObjects.RemoveAt(i);
            }
            else if (Mathf.Abs(obj.transform.position.y - m_bunnyTransform.position.y) > MAX_DISTANCE)
            {
                instantiatedObjects.RemoveAt(i);
                Destroy(obj);
            }
        }
    }
}
