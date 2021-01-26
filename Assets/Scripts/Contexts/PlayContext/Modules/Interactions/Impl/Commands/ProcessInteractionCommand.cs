using strange.extensions.command.impl;

public class ProcessInteractionCommand : Command
{
    [Inject] public IInteractable Interactable { get; set; }
    [Inject] public ICoinsController CoinsController { get; set; }

    public override void Execute()
    {
        if (Interactable is ICoin coin)
        {
            CoinsController.CollectCoin(coin.Model);
        }
    }
}
