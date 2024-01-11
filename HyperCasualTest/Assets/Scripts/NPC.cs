using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    
    public void TakeHit()
    {
        GetComponent<Animator>().enabled = false;
        GetComponent<Ragdoll>().ActivateRagdoll();
    }
}
