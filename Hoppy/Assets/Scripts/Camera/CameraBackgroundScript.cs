using UnityEngine;

public class CameraBackgroundScript : MonoBehaviour
{
    [SerializeField] private Camera m_camera;
    [SerializeField] private Transform m_playerTransform;
    [SerializeField] private float m_frequency = 1.0f;
    private float offset;

    private void Start()
    {
       offset = Random.Range(-100.0f, 100.0f);
    }

    void Update()
    {
        m_camera.backgroundColor = GetBackgroundColor(m_playerTransform.position.y);
    }

    private Color GetBackgroundColor(float y)
    {
        float r = Mathf.Clamp01(Mathf.PerlinNoise1D(m_frequency * y + offset + 238.34f));
        float g = Mathf.Clamp01(Mathf.PerlinNoise1D(m_frequency * y + offset - 192.09f));
        float b = Mathf.Clamp01(Mathf.PerlinNoise1D(m_frequency * y + offset + 581.72f));
        return new Color(r, g, b);
    }
}
