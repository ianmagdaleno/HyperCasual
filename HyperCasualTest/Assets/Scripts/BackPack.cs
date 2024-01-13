using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPack : MonoBehaviour
{
    public Transform backPackTransform;
    public float inercia = 1f;

    private List<Ragdoll> npcs = new List<Ragdoll>();

    private void Update()
    {
        //AnexarNPC();   
        UpdatePositionNPCs();
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Enemy"))
    //    {
    //        IndexNPC(other.gameObject);
    //    }
    //}
    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log(other.gameObject.name);
        //if (other.gameObject.CompareTag("Enemy"))
        //{
        //    IndexNPC(other.gameObject);
        //}
    }
    void IndexNPC(GameObject npc)
    {
        Ragdoll npcRigidbody = npc.GetComponent<Ragdoll>();

        if (npcRigidbody != null && !npcs.Contains(npcRigidbody))
        {
            npcRigidbody.UnactivateRagdoll();

            npcRigidbody.transform.parent = backPackTransform;
            npcs.Add(npcRigidbody);
        }
    }
    void UpdatePositionNPCs()
    {
        for (int i = 0; i < npcs.Count; i++)
        {
            Vector3 targetPosition = backPackTransform.position + Vector3.up * i * 0.5f;
            npcs[i].transform.position = Vector3.Lerp(npcs[i].transform.position, targetPosition, Time.deltaTime * inercia);
        }
    }
}