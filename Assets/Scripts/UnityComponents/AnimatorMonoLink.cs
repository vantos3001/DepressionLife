using UnityEngine;

public class AnimatorMonoLink : MonoLink<AnimatorLink>
{
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (Value.Value == null)
        {
            Value = new AnimatorLink
            {
                Value = GetComponentInChildren<Animator>()
            };
        }
    }
#endif
}