using strange.extensions.mediation.impl;

public class ContextViewDisabler : View
{
    [Inject] public EnableContextViewSignal EnableContextViewSignal { get; set; }
    [Inject] public DisableContextViewSignal DisableContextViewSignal { get; set; }

    [PostConstruct]
    private void Initialize()
    {
        DisableContextViewSignal.AddListener(() => gameObject.SetActive(false));
        EnableContextViewSignal.AddListener(() => gameObject.SetActive(true));
    }
}
