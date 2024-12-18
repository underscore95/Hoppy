
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ObjectSpawnerScript : MonoBehaviour
{
    private const float MAX_DISTANCE = 25.0f;
    private const float SPAWN_DISTANCE_FROM_BUNNY = 15.0f;

    [SerializeField] private bool m_enabled = true;
    [Tooltip("Prefabs to spawn and their weights")]
    [SerializeField] private List<KeyValuePair<GameObject, float>> m_prefabs;
    [SerializeField] private Transform m_bunnyTransform;
    [SerializeField] private Camera m_camera;
    [Tooltip("Minimum Y that objects can spawn at")]
    [SerializeField] private float m_minY;
    [Tooltip("Maximum Y that objects can spawn at")]
    [SerializeField] private float m_maxY;
    [Tooltip("An object will be spawned every this amount of units in the Y axis")]
    [SerializeField] private float m_objectSpawnDistance = 1.0f;
    [SerializeField] private int m_initialObjectCount = 5;

    private float m_minX;
    private float m_maxX;
    private float m_lastSpawnedY;
    private float m_totalWeight;

    private List<GameObject> instantiatedObjects = new List<GameObject>();

    void Start()
    {
        if (!m_enabled)
        {
            Debug.LogWarning("Object spawner with name " + gameObject.name + " is disabled");
            enabled = false;
            return;
        }

        Assert.IsTrue(m_minY < m_maxY, "Min Y must be less than Max Y");

        m_minX = m_camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        m_maxX = m_camera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        Assert.IsTrue(m_minX < m_maxX, "Min X must be less than Max X");

        m_lastSpawnedY = m_minY - m_objectSpawnDistance;

        for (int i = 0; i < m_initialObjectCount; ++i)
        {
            if (CanSpawnObject())
            {
                SpawnRandomisedObject();
            }
        }

        Assert.IsTrue(m_prefabs.Count > 0, "No prefabs to spawn");
        foreach (var pair in m_prefabs)
        {
            m_totalWeight += pair.Value;
            Assert.IsTrue(pair.Value >= 0, "Weight must be >= than 0");
        }
        Assert.IsTrue(m_totalWeight > 0, "Total weight must be > 0");
    }

    void Update()
    {
        if (CanSpawnObject())
        {
            SpawnRandomisedObject();
        }

        RemoveDistantObjects();
    }

    private void SpawnRandomisedObject()
    {
        float randomWeight = Random.Range(0, m_totalWeight);

        foreach (var pair in m_prefabs)
        {
            randomWeight -= pair.Value;
            if (randomWeight <= 0)
            {
                InstantiateObject(pair.Key, GetRandomObjectPosition());
                break;
            }
        }
    }

    private bool CanSpawnObject()
    {
        Assert.IsTrue(m_lastSpawnedY + m_objectSpawnDistance + 0.01 >= m_minY, "Last spawned Y is too low");
        bool bunnyHighEnough = m_bunnyTransform.position.y + SPAWN_DISTANCE_FROM_BUNNY > m_lastSpawnedY + m_objectSpawnDistance;
        bool bunnyInRange = m_bunnyTransform.position.y < m_maxY;
        return bunnyHighEnough && bunnyInRange;
    }

    private Vector3 GetRandomObjectPosition()
    {
        float x = Random.Range(m_minX, m_maxX);
        return new Vector3(x, m_lastSpawnedY + m_objectSpawnDistance, transform.position.z);
    }

    private void InstantiateObject(GameObject prefab, Vector3 pos)
    {
        m_lastSpawnedY = pos.y;
        GameObject newObject = Instantiate(prefab, pos, Quaternion.identity);
        instantiatedObjects.Add(newObject);
    }

    void RemoveDistantObjects()
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
