using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;

public class InitializeViewCommand : Command
{
    private const string MainCanvasLocation = "GUI/MainCanvas";

    [Inject(ContextKeys.CONTEXT_VIEW)] public GameObject Root { get; set; }

    public override void Execute()
    {
        var mainCanvasPrefab = Resources.Load<GameObject>(MainCanvasLocation);

        if (mainCanvasPrefab == null)
        {
            Debug.LogError($"InitializeViewCommand: could not load MainCanvasPrefab by location: {MainCanvasLocation}.");
        }

        GameObject.Instantiate(mainCanvasPrefab, Root.transform);
    }
}
