using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : MonoBehaviour
{
    private BackPack playerBackPack;

    private void LateUpdate()
    {
        if(playerBackPack != null) 
        {
            BuyNPC();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        playerBackPack = other.GetComponent<BackPack>();
        if (playerBackPack != null)
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
    }
}
