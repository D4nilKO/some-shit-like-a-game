using System.Collections;
using UnityEngine;

public abstract class ActiveSkill: Skill
{
    public virtual void ActivateSkill()
    {
        print("default activate skill");
    }

    public override void InitializeSkill(bool withStart)
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
        Attribute.timeBtwSpawns = Attribute.startTimeBtwSpawns;
        Attribute.timeBtwActions = Attribute.startTimeBtwActions;

        if (withStart)
        {
            StopAllCoroutines();
            StartCoroutine(MainTimer());
        }
    }

    public virtual IEnumerator MainTimer() //корутина таймера
    {
        Attribute.timeBtwSpawns = Attribute.startTimeBtwSpawns; //обнуляем таймер
        while (Attribute.timeBtwSpawns > 0) //если время больше 0, то уменьшаем его по чуть-чуть
        {
            Attribute.timeBtwSpawns -= Time.deltaTime; //само уменьшение таймера
            yield return null; //продолжить выполнение после этого кадра
        }

        //ActivateSkill(); //активация оружия
        yield return StartCoroutine(DamageObjects()); // запускает корутину и ждет ее выполнения(пидорас)
        StartCoroutine(MainTimer()); //перезапуск таймера спавна
    }

    public virtual IEnumerator DamageObjects()
    {
        yield return null;
        print("base DamageObjects");
    }

}