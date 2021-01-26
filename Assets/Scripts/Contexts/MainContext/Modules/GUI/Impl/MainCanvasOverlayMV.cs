using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityWeld.Binding;

[Binding]
public class MainCanvasOverlayMV : CanvasMV
{
    private Tween _fadeTween;

    [SerializeField] private Image _fadeImage;
    [SerializeField] private MenuMediator _menuMediator;
    [SerializeField] private LoadingScreenMV _loadingScreenMv;
    [SerializeField] private GameObject _canvasRoot;

    [Inject] public StartLoadPlaySceneSignal StartLoadPlaySceneSignal { get; set; }
    [Inject] public ChangeScreenToLoadingSignal ChangeScreenToLoadingSignal { get; set; }
    [Inject] public DisableContextViewSignal DisableContextViewSignal { get; set; }
    [Inject] public EnableContextViewSignal EnableContextViewSignal { get; set; }
    [Inject] public ReturnToTheMainMenuSignal ReturnToTheMainMenuSignal { get; set; }


    [PostConstruct]
    private void Initialize()
    {
        ChangeScreenToLoadingSignal.AddListener(StartLoadingSequence);
        DisableContextViewSignal.AddListener(DisableMainCanvas);
        EnableContextViewSignal.AddListener(EnableMainCanvas);
        ReturnToTheMainMenuSignal.AddListener(ReturnToTheMainMenu);
    }

    private void StartLoadingSequence()
    {
        _menuMediator.DisableActiveMenu();
        _loadingScreenMv.SetLoadingScreenEnabled(true);
        _loadingScreenMv.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
        var loadingScreenTween = _loadingScreenMv.transform.DOScale(1, .1f).OnComplete(InvokeLoadingScreen);

        _fadeImage.gameObject.SetActive(true);
        _fadeImage.color = new Color32(0, 0, 0, 0);
        _fadeImage.DOFade(1f, 1f).OnComplete(() => loadingScreenTween.Play()).Play();
    }

    private void InvokeLoadingScreen()
    {
        _fadeImage.color = new Color32(0, 0, 0, 0);
        _fadeImage.gameObject.SetActive(false);
        
        StartLoadPlaySceneSignal.Dispatch();
    }

    private void DisableMainCanvas()
    {
        _loadingScreenMv.SetLoadingScreenEnabled(false);
        _canvasRoot.gameObject.SetActive(false);
    }

    private void EnableMainCanvas()
    {
        _menuMediator.StartWithMainMenu();
        _canvasRoot.gameObject.SetActive(true);
    }

    private void ReturnToTheMainMenu()
    {
        _loadingScreenMv.SetLoadingScreenEnabled(false);
        _menuMediator.StartWithMainMenu();
    }
}
