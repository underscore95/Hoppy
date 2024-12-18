using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct AudioData
{
    public AudioClip AudioClip;
    private AudioSource m_audioSource;

    public void Play(float pitch)
    {
        if (m_audioSource == null)
        {
            m_audioSource = new GameObject("AudioSource").AddComponent<AudioSource>();
            m_audioSource.clip = AudioClip;
        }
        if (!m_audioSource.isPlaying)
        {
            m_audioSource.pitch = pitch;
            m_audioSource.Play();
        }
    }
}

[CreateAssetMenu(fileName = "SoundEffectScript", menuName = "Scriptable Objects/SoundEffectScript")]
public class SoundEffectScript : ScriptableObject
{
    [SerializeField] private List<AudioData> m_sounds;
    [SerializeField] private float m_minPitch = 0.9f;
    [SerializeField] private float m_maxPitch = 1.1f;

    public void PlaySound()
    {
        int index = Random.Range(0, m_sounds.Count);
        m_sounds[index].Play(Random.Range(m_minPitch, m_maxPitch));
    }
}
