using System;

public class ComboManager
{
    public event Action OnValueChanged { add { } remove { } }

    public float ComboValue { get; private set; }

    readonly ComboSettings settings;
}