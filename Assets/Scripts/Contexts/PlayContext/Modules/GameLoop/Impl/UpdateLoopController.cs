public class UpdateLoopController : IUpdateLoopController
{
    [Inject] public ICoinsController CoinsController { get; set; }

    public void Update(float deltaTime)
    {
        CoinsController.Update(deltaTime);
    }
}
