using UnityEngine;
using UnityWeld.Binding;

[Binding]
public class LoadingScreenMV : CanvasMV
{
    private string _text;

    [Binding]
    public string Text
    {
        get => _text;

        set
        {
            _text = value + "%";

            OnPropertyChanged();
        }
    }

    [Inject] public UpdateLoadingProgressSignal UpdateLoadingProgressSignal { get; set; }

    [PostConstruct]
    private void Initialize()
    {
        UpdateLoadingProgressSignal.AddListener(UpdateLoadingProgress);
    }

    public void SetLoadingScreenEnabled(bool enabled)
    {
        gameObject.SetActive(enabled);
        Text = "0";
    }

    private void UpdateLoadingProgress(int progress)
    {
        Text = Mathf.Round(progress).ToString();
    }
}
