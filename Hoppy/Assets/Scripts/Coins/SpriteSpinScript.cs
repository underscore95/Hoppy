using UnityEngine;

public class CoinSpinScript : MonoBehaviour
{
    [SerializeField] private float m_spinSpeed = 100.0f;
    [SerializeField] private Vector3 m_spinAxis = Vector3.up;

    void Start()
    {
        m_spinAxis.Normalize();
    }

    void Update()
    {
        gameObject.transform.Rotate(m_spinAxis, m_spinSpeed * Time.deltaTime);
    }
}
