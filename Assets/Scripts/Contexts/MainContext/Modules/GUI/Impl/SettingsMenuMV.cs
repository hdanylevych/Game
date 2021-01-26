using UnityEngine;
using UnityWeld.Binding;

[Binding]
public class SettingsMenuMV : MenuMV
{
    private bool _isSFXEnabled = true;
    private bool _isBackgroundMusicEnabled = true;

    [Binding]
    public bool IsSFXEnabled
    {
        get => _isSFXEnabled;

        set
        {
            if (_isSFXEnabled == value)
                return;

            _isSFXEnabled = value;

            OnPropertyChanged();
        }
    }

    [Binding]
    public bool IsBackgroundMusicEnabled
    {
        get => _isBackgroundMusicEnabled;

        set
        {
            if (_isBackgroundMusicEnabled == value)
                return;

            _isBackgroundMusicEnabled = value;

            OnPropertyChanged();
        }
    }

    [Binding]
    public void OnBackgroundMusicToggleValueChanged()
    {
        IsBackgroundMusicEnabled = !IsBackgroundMusicEnabled;
        ChangeAudioSettingsSignal.Dispatch(new NewAudioSettings()
                                               {
                                                   IsSFXEnabled = _isSFXEnabled,
                                                   IsBackgroundMusicEnabled = _isBackgroundMusicEnabled
                                               });
    }

    [Binding]
    public void OnSFXToggleValueChanged()
    {
        IsSFXEnabled = !IsSFXEnabled;
        ChangeAudioSettingsSignal.Dispatch(new NewAudioSettings()
                                               {
                                                   IsSFXEnabled = _isSFXEnabled,
                                                   IsBackgroundMusicEnabled = _isBackgroundMusicEnabled
                                               });
    }

    [Binding]
    public void OnBackClicked()
    {
        if (!CanProcessInput)
            return;

        ButtonClickedSignal.Dispatch();
        InvokeOnChangeMenuClick(MenuType.Main);
    }

    [Inject] public ButtonClickedSignal ButtonClickedSignal { get; set; }
    [Inject] public ChangeAudioSettingsSignal ChangeAudioSettingsSignal { get; set; }
    [Inject] public IAudioSettingsProvider AudioSettingsProvider { get; set; }

    [PostConstruct]
    private void Construct()
    {
        IsSFXEnabled = AudioSettingsProvider.IsSFXEnabled;
        IsBackgroundMusicEnabled = AudioSettingsProvider.IsBackgroundMusicEnabled;
    }

    public override void Initialize()
    {
        _type = MenuType.Settings;
        _rectTransform = GetComponent<RectTransform>();

        IsActive = false;
        IsEnabled = false;
    }
}
