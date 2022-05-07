using UnityEngine;

public class ComboSettings : ScriptableObject
{
    [field: SerializeField] public float ChainTime { get; private set; }
    [field: SerializeField] public BodyPartDamageSettingsEntry[] BodyPartEntries { get; private set; }
    [field: SerializeField] public float ComboToCurrencyRatio { get; private set; }

    public float GetMultiplier (BodyPart part)
    {
        foreach (var item in BodyPartEntries)
        {
            if (item.BodyPart == part)
                return item.DamageMultiplier;
        }
        return 1f;
    }
}
