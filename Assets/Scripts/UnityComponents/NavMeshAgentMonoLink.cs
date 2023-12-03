using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshAgentMonoLink : MonoLink<NavMeshAgentLink>
{
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (Value.Value == null)
        {
            Value = new NavMeshAgentLink
            {
                Value = GetComponent<NavMeshAgent>()
            };
        }
    }
#endif
}