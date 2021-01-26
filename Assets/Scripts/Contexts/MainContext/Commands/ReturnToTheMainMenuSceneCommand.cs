using strange.extensions.command.impl;
using UnityEngine.SceneManagement;

public class ReturnToTheMainMenuSceneCommand : Command
{
    [Inject] public EnableContextViewSignal EnableContextViewSignal { get; set; }

    public override void Execute()
    {
        SceneManager.UnloadSceneAsync(1);

        SceneManager.sceneUnloaded += (scene) => EnableContextViewSignal.Dispatch();
    }
}
