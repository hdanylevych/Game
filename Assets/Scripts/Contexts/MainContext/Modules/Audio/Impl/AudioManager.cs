using System.Collections.Generic;
using UnityEngine;

public class AudioManager : IAudioManager, IAudioSettingsProvider
{
    private bool _initialized = false;
    private AudioSource _sfxSource;
    private AudioSource _backgroundAudioSource;
    private Dictionary<AudioClipName, AudioClip> _audioClips = new Dictionary<AudioClipName, AudioClip>();

    private bool _isSFXEnabled = true;
    private bool _isBackgroundMusicEnabled = true;

    public bool Initialized
    {
        get => _initialized;
    }


    public bool IsSFXEnabled
    {
        get => _isSFXEnabled;
        
        set
        {
            if (_isSFXEnabled == value)
                return;

            _isSFXEnabled = value;
        }
}

    public bool IsBackgroundMusicEnabled
    {
        get => _isBackgroundMusicEnabled;
        
        set
        {
            if (_isBackgroundMusicEnabled == value)
                return;

            _isBackgroundMusicEnabled = value;

            if (_isBackgroundMusicEnabled)
            {
                PlayBackgroundMusic(AudioClipName.Background);
            }
            else
            {
                _backgroundAudioSource.Stop();
            }
        }
    }

    [Inject] public ButtonClickedSignal ButtonClickedSignal { get; set; }
    [Inject] public ChangeScreenToLoadingSignal ChangeScreenToLoadingSignal { get; set; }
    [Inject] public PlaySceneLoadedSignal PlaySceneLoadedSignal { get; set; }

    [PostConstruct]
    private void PostConstruct()
    {
        ButtonClickedSignal.AddListener(() => PlaySFX(AudioClipName.Click));
        ChangeScreenToLoadingSignal.AddListener(() => PlayBackgroundMusic(AudioClipName.LoadingBackground));

        PlaySceneLoadedSignal.AddListener(() => PlayBackgroundMusic(AudioClipName.Background));
    }

    public void Initialize(AudioSource sfxSource, AudioSource backgroundAudioSource)
    {
        if (Initialized)
            return;

        _sfxSource = sfxSource;
        _backgroundAudioSource = backgroundAudioSource;

        _audioClips.Add(
            AudioClipName.Click,
            Resources.Load<AudioClip>("Sounds/Clips/ButtonClick"));

        _audioClips.Add(
            AudioClipName.Background,
            Resources.Load<AudioClip>("Sounds/Clips/BackgroundMusic"));

        _audioClips.Add(
            AudioClipName.LoadingBackground,
            Resources.Load<AudioClip>("Sounds/Clips/LoadingBackgroundMusic"));

        PlayBackgroundMusic(AudioClipName.Background);
        _initialized = true;
    }
    public void SetNewAudioSettings(NewAudioSettings settings)
    {
        IsSFXEnabled = settings.IsSFXEnabled;
        IsBackgroundMusicEnabled = settings.IsBackgroundMusicEnabled;
    }

    private void PlaySFX(AudioClipName name)
    {
        if (!_isSFXEnabled)
            return;

        _sfxSource.PlayOneShot(_audioClips[name]);
    }

    private void PlayBackgroundMusic(AudioClipName name)
    {
        if (!_isBackgroundMusicEnabled)
            return;

        _backgroundAudioSource.clip = _audioClips[name];
        _backgroundAudioSource.Play();
    }

    private void StopAllClips()
    {
        _backgroundAudioSource.Stop();
        _sfxSource.Stop();
    }
}
