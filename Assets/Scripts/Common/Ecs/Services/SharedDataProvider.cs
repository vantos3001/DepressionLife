using Leopotam.EcsLite;

public class SharedDataProvider
{
    public SharedData SharedData { get; private set; }
    public EcsWorld World => SharedData?.World;

    public void SetSharedData(SharedData sharedData)
    {
        SharedData = sharedData;
    }
}
