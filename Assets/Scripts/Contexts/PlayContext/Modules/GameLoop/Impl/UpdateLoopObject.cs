using strange.extensions.mediation.impl;

using UnityEngine;

public class UpdateLoopObject : View
{
    [Inject] public IUpdateLoopController UpdateLoopController { get; set; }

    private void Update()
    {
        UpdateLoopController.Update(Time.deltaTime);
    }
}
