using System;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public virtual AttributeSkill Attribute { get; set; }

    public virtual void Upgrade()
    {
        print("default Upgrade skill");
    }

    public virtual void InitializeSkill(bool withStart)
    {
        if (enabled)
        {
            Attribute.startLvl = 1;
        }
        else
        {
            Attribute.startLvl = 0;
        }
        Attribute.lvl = Attribute.startLvl;
    }
    
}