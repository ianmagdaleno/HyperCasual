using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMarket : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (DataManager.Instance.Money > 0)
        {
            BuyLevel();
        }
        else
        {
            return;
        }
    }
    void BuyLevel()
    {
        DataManager.Instance.IncreaseLevel();
    }
}
