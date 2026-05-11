public class PlayerFeature : Feature
{
    public PlayerFeature(ISystemFactory systemFactory)
    {
        this
            .Add(systemFactory.Create<CameraInitSystem>())
            .Add(systemFactory.Create<PlayerInitSystem>())
            .Add(systemFactory.Create<PlayerMoveSystem>())
            .Add(systemFactory.Create<PlayerAnimationSystem>())
            .Add(systemFactory.Create<MoveCameraByInputSystem>());
    }
}
