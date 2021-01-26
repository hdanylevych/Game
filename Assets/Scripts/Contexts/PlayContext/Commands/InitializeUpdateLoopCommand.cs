using strange.extensions.command.impl;

using strange.extensions.context.api;

using UnityEngine;

public class InitializeUpdateLoopCommand : Command
{
    [Inject(ContextKeys.CONTEXT_VIEW)] public GameObject ContextRoot { get; set; }

    public override void Execute()
    {
        var updateLoopGO = new GameObject("UpdateObject");
        updateLoopGO.transform.parent = ContextRoot.transform;
        updateLoopGO.AddComponent<UpdateLoopObject>();
    }
}
