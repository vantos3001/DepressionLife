public class InputFeature : Feature
{
    public InputFeature(ISystemFactory systemFactory)
    {
        this
            .Add(systemFactory.Create<PlayerInputSystem>());
    }
}
