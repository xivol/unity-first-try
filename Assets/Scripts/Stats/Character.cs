using UnityEngine;
using System.Collections;

public interface ILevelingDelegate {
    uint currentLevel(uint exp);
    uint minimalExperience(uint level);
}

[RequireComponent(typeof(Actor))]
public class Character : Core<Character>
{
    protected Actor _actor;

    public new string name;

    public CharacterRace race;
    public CharacterClass charcterClass;
    public Statistics stats;

    protected ILevelingDelegate _leveling;
    protected uint _exp;
    public uint experience { 
        get { return _exp; }
        set { SetExperience(value); }
    }

    protected virtual void SetExperience(uint newExp) {
        if (newExp < _exp) {
            Debug.LogError("Experience cannot be taken from character");
            return;
        }

        for (var lvl = level + 1; lvl < _leveling.currentLevel(newExp); lvl++) 
        {
            //didLevelUp(this, lvl);
        }
        _exp = newExp;
    }

    public uint level { 
        get { 
            return _leveling.currentLevel(_exp); 
        }
    }


}
