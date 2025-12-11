using System;
using UnityEngine;

public static class AudioEvents
{
    public static event Action<AudioClip> OnSfxRequested;
    public static event Action<AudioClip> OnMusicRequested;

    public static void PlaySfx(AudioClip clip) => OnSfxRequested?.Invoke(clip);
    public static void PlayMusic(AudioClip clip) => OnMusicRequested?.Invoke(clip);
}
