using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : MonoBehaviour
{
    private BackPack playerBackPack;

    private void OnTriggerStay(Collider other)
    {
        playerBackPack = other.GetComponent<BackPack>();

        if (playerBackPack != null && playerBackPack.npcs.Count > 0)
        {
            BuyNPC();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        playerBackPack = null;
    }
    void BuyNPC()
    {
        playerBackPack.RemoveNPC(this.gameObject);
        DataManager.Instance.EarnMoney(1);
    }
}
