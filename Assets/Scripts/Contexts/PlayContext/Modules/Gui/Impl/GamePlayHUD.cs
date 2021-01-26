using UnityWeld.Binding;

[Binding]
public class GamePlayHUD : CanvasMV
{
    private string _coinsText;

    [Binding]
    public void OnMenuClicked()
    {
        ReturnToTheMainMenuSceneSignal.Dispatch();
    }

    [Binding]
    public string CoinsText
    {
        get => _coinsText;

        set
        {
            _coinsText = "Coins: " + value;

            OnPropertyChanged();
        }
    }

    [Inject] public ReturnToTheMainMenuSceneSignal ReturnToTheMainMenuSceneSignal { get; set; }
    [Inject] public CollectedCoinsUpdateSignal CollectedCoinsUpdateSignal { get; set; }


    [PostConstruct]
    private void Initialize()
    {
        CollectedCoinsUpdateSignal.AddListener(UpdateCoinsField);
        CoinsText = "0";
    }

    private void UpdateCoinsField(int coinsNumber)
    {
        CoinsText = coinsNumber.ToString();
    }
}
