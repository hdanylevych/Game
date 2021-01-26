
using strange.extensions.command.impl;
using strange.extensions.context.api;

using UnityEngine;

public class InitializeAudioCommand : Command
{
    [Inject(ContextKeys.CONTEXT_VIEW)] public GameObject Root { get; set; }
    [Inject] public IAudioManager AudioManager { get; set; }

    public override void Execute()
    {
        var audioSourceInstance = new GameObject("GameAudioSource");
        
        var sfxSourceComponent = audioSourceInstance.AddComponent<AudioSource>();
        var backgroundSourceComponent = audioSourceInstance.AddComponent<AudioSource>();

        sfxSourceComponent.playOnAwake = false;
        backgroundSourceComponent.playOnAwake = false;

        sfxSourceComponent.priority = 30;
        backgroundSourceComponent.loop = true;

        AudioManager.Initialize(sfxSourceComponent, backgroundSourceComponent);
    }
}
