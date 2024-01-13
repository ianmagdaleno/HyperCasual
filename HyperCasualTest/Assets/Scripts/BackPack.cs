using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPack : MonoBehaviour
{
    public float inercia = 1f;
    public Transform backPackTransform;
    private List<Ragdoll> npcs = new List<Ragdoll>();

    //private void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.layer == 6)
    //    {
    //        IndexNPC(other.gameObject);
    //    }
    //}
    void IndexNPC(GameObject npc)
    {
        Ragdoll npcRagdoll = FindRagdollParent(npc.transform);

        if (npcRagdoll != null && !npcs.Contains(npcRagdoll))
        {
            npcRagdoll.UnactivateRagdoll();

            npcRagdoll.transform.parent = backPackTransform;
            npcs.Add(npcRagdoll);
        }
    }

    Ragdoll FindRagdollParent(Transform objeto)
    {
        Ragdoll ragdoll = objeto.GetComponent<Ragdoll>();

        if (ragdoll != null)
        {
            Debug.Log("encontrouuu !!!!");
            return ragdoll;
        }
        else if (objeto.parent != null)
        {
            Debug.Log(objeto.name + "Foi pro pai" + objeto.parent.name );
            return FindRagdollParent(objeto.parent);
        }

        Debug.Log("nao encontrou nada");
        return null;
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