using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public void TakeHit(Vector3 direction)
    {
        GetComponent<Animator>().enabled = false;
        GetComponent<Ragdoll>().TakeHit(direction);
    }
}
