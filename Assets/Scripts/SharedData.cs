using Cinemachine;
using Leopotam.EcsLite;
using SevenBoldPencil.EasyEvents;

public class SharedData
{
    public EcsWorld World;
    public EventsBus EventsBus;
    public PlayerInputData PlayerInputData = new PlayerInputData();
    public CinemachineVirtualCamera Camera;
}