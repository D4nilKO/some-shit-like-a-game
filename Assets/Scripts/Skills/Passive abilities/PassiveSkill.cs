using UnityEngine;

public abstract class PassiveSkill: Skill
{
    public virtual void ActivateSkill()
    {
        Debug.LogWarning("Activate skill is empty");
    }    
}