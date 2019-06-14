using UnityEngine;
using System;
using System.Collections.Generic;

[Flags]
public enum Stats
{
    HP,  // Hit Points
    MHP, // Max Hit Points
    ATK, // Physical Attack
    DEF, // Physical Defense
    MP,  // Magic Points
    MMP, // Max Magic Points
    MAT, // Magic Attack
    MDF, // Magic Defense
    EVD, // Evade
    RES, // Status Resistance
    SPD, // Speed
    MOV, // Move Range
    JMP, // Jump Height
}

public class StatsEventArgs : EventArgs
{
    public Stats stat { get; protected set; }
    public float value;
    public float oldValue { get; protected set; }

    public StatsEventArgs(Stats s, float val, float old)
    {
        stat = s;
        value = val;
        oldValue = old;
    }
}

[Serializable]
public class Statistics
{
    [SerializeField]
    protected float[] _data = new float[Enum.GetValues(typeof(Stats)).Length];

    public event EventHandler<StatsEventArgs> statWillChange;
    public event EventHandler<StatsEventArgs> statDidChange;

    public float this[Stats s]
    {
        get { return _data[Convert.ToInt32(s)]; }
        set { SetValue(s, value); }
    }

    protected void SetValue(Stats s, float value)
    {
        var oldValue = _data[Convert.ToInt32(s)];
        var statChange = new StatsEventArgs(s, value, oldValue);

        if (statWillChange != null)
            statWillChange(this, statChange);

        _data[Convert.ToInt32(s)] = statChange.value;

        if (statDidChange != null)
            statDidChange(this, statChange);
    }
}
