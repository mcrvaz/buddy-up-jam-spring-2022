using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponAnimationMapping : ScriptableObject
{
    [field: SerializeField] public WeaponAnimationMappingEntry[] Entries { get; private set; }

    readonly Dictionary<WeaponAnimationId, string> animationMapping = new Dictionary<WeaponAnimationId, string>();

    public string GetClipName (WeaponAnimationId animationId)
    {
        if (!animationMapping.TryGetValue(animationId, out var clipName))
        {
            clipName = Entries.First(x => x.AnimationId == animationId).AnimationName;
            animationMapping.Add(animationId, clipName);
        }
        return clipName;
    }
}