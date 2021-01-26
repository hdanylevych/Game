using DG.Tweening;

using UnityEngine;
using UnityWeld.Binding;

[Binding]
public class MainMenuMV : MenuMV
{
    private const float NoInternetMessageDisplayDuration = 5f;

    [SerializeField] private GameObject _textGO;

    [Binding]
    public void OnPlayClicked()
    {
        if (!CanProcessInput)
            return;

        ButtonClickedSignal.Dispatch();
        ChangeScreenToLoadingSignal.Dispatch();
    }

    [Binding]
    public void OnSettingsClicked()
    {
        if (!CanProcessInput)
            return;

        ButtonClickedSignal.Dispatch();
        InvokeOnChangeMenuClick(MenuType.Settings);
    }

    [Inject] public ButtonClickedSignal ButtonClickedSignal { get; set; }
    [Inject] public ChangeScreenToLoadingSignal ChangeScreenToLoadingSignal { get; set; }
    [Inject] public NoInternetConnectionSignal NoInternetConnectionSignal { get; set; }

    [PostConstruct]
    public void Contruct()
    {
        NoInternetConnectionSignal.AddListener(DisplayNoInternetMessage);
    }

    public override void Initialize()
    {
        _type = MenuType.Main;
        _rectTransform = GetComponent<RectTransform>();
        
        IsActive = false;
        IsEnabled = false;
    }

    private void DisplayNoInternetMessage()
    {
        _textGO.SetActive(true);

        _textGO.transform.DOShakeScale(5f, .2f, 3, 10f).OnComplete(() => _textGO.SetActive(false)).Play();
    }
}