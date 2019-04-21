using UnityEngine;
using System;

public abstract class CharacterRace : ScriptableObject
{
    public const Stats Attributes = Stats.MHP | Stats.ATK | Stats.DEF | 
                                    Stats.MMP | Stats.MAT | Stats.MDF | 
                                    Stats.SPD | Stats.MOV | Stats.JMP;

    public Statistics baseStats;
}
