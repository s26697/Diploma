using UnityEngine;

[CreateAssetMenu(fileName = "AudioConfig", menuName = "Game/Audio/Audio Config")]
public class AudioConfigSO : ScriptableObject
{
    [Header("Volume Settings")]
    [Range(0f, 1f)] public float musicVolume = 0.6f;
    [Range(0f, 1f)] public float sfxVolume = 1f;
}
