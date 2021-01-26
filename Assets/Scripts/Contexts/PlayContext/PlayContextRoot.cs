using strange.extensions.context.impl;

public class PlayContextRoot : ContextView
{
    private void Awake()
    {
        context = new PlayContext(this, true);
        context.Start();
    }
}
