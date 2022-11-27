using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CSVParseSystem : MonoBehaviour
{
    public TextAsset textAssetData;

    [System.Serializable]
    public class SkillUpgrade
    {
        public int lvl;
        public float timeBtwSpawns;
        public int countOfStrike;
        public float damage;
        public string upgradeText;

        // public int lvl;
        // public int timeBtwSpawns;
        // public int damage;
        // public string upgradeText;
    }

    [System.Serializable]
    public class SkillUpgradeList

    {
        public SkillUpgrade[] skillUpgrade;
    }

    public SkillUpgradeList mySkillUpgradeList = new SkillUpgradeList();


    // Start is called before the first frame update
    private void Start()
    {
        ReadCSV();
    }

    void ReadCSV()
    {
        string[] data = textAssetData.text.Split(new string[] { ";", "\n" }, StringSplitOptions.None);

        int tableSize = data.Length / 5 - 1;
        mySkillUpgradeList.skillUpgrade = new SkillUpgrade[tableSize];

        for (int i = 0; i < tableSize; i++)

        {
            mySkillUpgradeList.skillUpgrade[i] = new SkillUpgrade();
            mySkillUpgradeList.skillUpgrade[i].lvl = int.Parse(data[5 * (i + 1)]);
            mySkillUpgradeList.skillUpgrade[i].timeBtwSpawns = float.Parse(data[5 * (i + 1) + 1]);
            mySkillUpgradeList.skillUpgrade[i].countOfStrike = int.Parse(data[5 * (i + 1) + 2]);
            mySkillUpgradeList.skillUpgrade[i].damage = float.Parse(data[5 * (i + 1) + 3]);
            mySkillUpgradeList.skillUpgrade[i].upgradeText = data[5 * (i + 1) + 4];
        }
    }

    // public TextAsset textAssetData;
    //
    // [Serializable]
    // public class SkillUpgrade
    // {
    //     public int lvl;
    //     public int timeBtwSpawns;
    //     public int damage;
    //     public string upgradeText;
    // }
    //
    // [System.Serializable]
    //
    // public class SkillUpgradeList
    //
    // {
    //     public SkillUpgradeList[] skillUpgradeList;
    // }
    //
    // public SkillUpgradeList whipUpgradeList = new SkillUpgradeList();
    //
    // private void Start()
    // {
    //     ReadCSV();
    // }
    //
    // private void ReadCSV()
    // {
    //     string[] data = textAssetData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
    //     int tableSize = data.Length / 5 - 1;
    //     whipUpgradeList.skillUpgradeList = new skillUpgradeList[]
    // }
}