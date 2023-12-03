using Leopotam.EcsLite;

public class MonoEntity : MonoLinkBase
{
    private int _entity;

    private MonoLinkBase[] _monoLinks;
    
    public MonoLink<T> Get<T>() where T : struct
    {
        foreach (MonoLinkBase link in _monoLinks)
        {
            if (link is MonoLink<T> monoLink)
            {
                return monoLink;
            }
        }

        return null;
    }

    public override void Make(int entity, EcsWorld world)
    {
        _entity = entity;

        _monoLinks = GetComponents<MonoLinkBase>();
        foreach (var monoLink in _monoLinks)
        {
            if (monoLink is MonoEntity)
            {
                continue;
            }

            monoLink.Make(entity, world);
        }
        
        ref var gameObjectLink = ref world.GetPool<GameObjectLink>().Add(entity);
        gameObjectLink.Value = gameObject;
    }
}