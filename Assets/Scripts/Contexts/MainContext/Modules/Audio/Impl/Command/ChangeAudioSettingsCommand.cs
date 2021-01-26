
using strange.extensions.command.impl;

public class ChangeAudioSettingsCommand : Command
{
    [Inject] public NewAudioSettings NewAudioSettings { get; set; }
    [Inject] public IAudioManager AudioManager { get; set; }

    public override void Execute()
    {
        AudioManager.SetNewAudioSettings(NewAudioSettings);
    }
}
