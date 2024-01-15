using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private int money = 0;
    private int level = 1;
    private int baseLevel = 0;

    private static DataManager instance;
    private HudManager hud;
    private PlayerController player;

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
    public int BaseLevel
    {
        get { return baseLevel; }
        set { baseLevel = value; }
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
    private void Start()
    {
        hud = GameObject.FindFirstObjectByType<HudManager>();
        player = GameObject.FindFirstObjectByType<PlayerController>();
    }
    public void EarnMoney(int amount)
    {
        Money += amount;
        if(hud != null)
        {
            hud.UpdateMoney(money);
        }
    }
    public void IncreaseLevel()
    {
        Money--;
        Level++;
        hud.UpdateMoney(money);
        hud.UpdateXP(1);
    }
    public void LevelUp()
    {
        Level = 0;
        baseLevel++;

        if(player != null)
        {
            player.LevelUp();
        }
    }
}