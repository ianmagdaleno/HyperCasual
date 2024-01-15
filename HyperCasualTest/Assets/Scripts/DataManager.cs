using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private int money = 0;
    private int level = 1;

    private static DataManager instance;

    public int Money
    {
        get { return money; }
        set { money = value; }
    }

    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    public static DataManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject singletonObject = new GameObject("DataManager");
                instance = singletonObject.AddComponent<DataManager>();
                DontDestroyOnLoad(singletonObject);
            }
            return instance;
        }
    }
    public void EarnMoney(int amount)
    {
        Money += amount;
    }

    public void IncreaseLevel()
    {
        Level++;
    }
}