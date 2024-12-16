using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform m_target;
    [Tooltip("The speed at which the camera follows the target, only follows in Y")]
    [SerializeField] private float m_inverseSmoothSpeed = 1.0f / 10.0f;
    [SerializeField] private float m_offset = 0.0f;

    private float currentVelocity;

    void FixedUpdate()
    {
        float targetY = m_target.position.y + m_offset;

        float y = Mathf.SmoothDamp(transform.position.y, targetY, ref currentVelocity, m_inverseSmoothSpeed);

        transform.position = new Vector3(
            transform.position.x,
            y,
            transform.position.z
        );
    }

}
