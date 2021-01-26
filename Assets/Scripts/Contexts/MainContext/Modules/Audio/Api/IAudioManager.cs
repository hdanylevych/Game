using UnityEngine;

public interface IAudioManager
{
    void Initialize(AudioSource sfxSource, AudioSource backgroundAudioSource);
    void SetNewAudioSettings(NewAudioSettings settings);
}
