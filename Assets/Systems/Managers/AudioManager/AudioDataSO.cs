using UnityEngine;

[CreateAssetMenu(fileName = "AudioData", menuName = "Game/Audio/Data")]
public class AudioDataSO : ScriptableObject
{
    [Header("Clips")]
    public AudioClip[] clips;

    public AudioClip GetRandom()
    {
        if (clips == null || clips.Length == 0)
            return null;

        return clips[Random.Range(0, clips.Length)];
    }
}
