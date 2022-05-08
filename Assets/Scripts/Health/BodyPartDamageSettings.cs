using System;
using UnityEngine;

[Serializable]
public class BodyPartDamageSettings
{
    [field: SerializeField] public BodyPartDamageSettingsEntry[] Entries { get; private set; }

    public float GetMultiplier (BodyPart part)
    {
        if (Entries == null)
            return 1;

        foreach (var item in Entries)
        {
            if (item.BodyPart == part)
                return item.DamageMultiplier;
        }
        return 1f;
    }
}