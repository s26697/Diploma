using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioConfigSO config;

    private AudioSource musicSource;
    private AudioSource sfxSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Init();
    }

    private void Init()
    {
        musicSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        musicSource.loop = true;
        musicSource.volume = config.musicVolume;

        sfxSource.loop = false;
        sfxSource.volume = config.sfxVolume;

        AudioEvents.OnSfxRequested += PlaySfx;
        AudioEvents.OnMusicRequested += PlayMusic;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            AudioEvents.OnSfxRequested -= PlaySfx;
            AudioEvents.OnMusicRequested -= PlayMusic;
        }
    }

    private void PlaySfx(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip, config.sfxVolume);
    }

    private void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;
        musicSource.clip = clip;
        musicSource.volume = config.musicVolume;
        musicSource.Play();
    }
}
