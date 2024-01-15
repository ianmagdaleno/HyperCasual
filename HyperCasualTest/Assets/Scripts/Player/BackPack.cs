using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPack : MonoBehaviour
{
    [SerializeField] private GameObject backPackTransform;
    [SerializeField] private float distanceBetweenNPCs = 1f;
    [SerializeField] private float maxBag = 2f;

    public List<Ragdoll> npcs = new List<Ragdoll>();

    private bool isAddingNPC = false; 

    private void Update()
    {
        UpdatePositionNPCs();
        UpdateRotationNPCs();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 6 && npcs.Count < maxBag && !isAddingNPC)
        {
            StartCoroutine(IndexNPC(other.gameObject));
        }
    }
    void UpdatePositionNPCs()
    {
        for (int i = 0; i < npcs.Count; i++)
        {
            Transform currentTransform = npcs[i].gameObject.transform;

            if (i == 0)
            {
                currentTransform.position = Vector3.Lerp(currentTransform.position, backPackTransform.transform.position, 1);
            }
            else
            {
                currentTransform.position = Vector3.Lerp(currentTransform.position, npcs[i - 1].gameObject.transform.position, 0.1f);
            }
        }
    }
    void UpdateRotationNPCs()
    {
        for (int i = 0; i < npcs.Count; i++)
        {
            Transform currentTransform = npcs[i].gameObject.transform;

            if (i == 0)
            {
                currentTransform.rotation = Quaternion.Lerp(currentTransform.rotation, Quaternion.Euler(new Vector3(currentTransform.rotation.x, transform.rotation.y * 100, currentTransform.rotation.z)), 1);
            }
            else
            {
                currentTransform.rotation = Quaternion.Lerp(currentTransform.rotation, npcs[i - 1].gameObject.transform.rotation, 0.1f);
            }
        }
    }
    IEnumerator IndexNPC(GameObject npc)
    {
        isAddingNPC = true; 

        yield return new WaitForSeconds(2f);

        Ragdoll npcRagdoll = FindRagdollParent(npc.transform);

        if (npcRagdoll != null && !npcs.Contains(npcRagdoll))
        {
            npcRagdoll.UnactivateRagdoll();

            Transform bodyTransform = npcRagdoll.transform.GetChild(0).GetChild(0);

            bodyTransform.localPosition = new Vector3(0f, distanceBetweenNPCs * npcs.Count, 0f);
            bodyTransform.rotation = Quaternion.identity;
            bodyTransform.localRotation = Quaternion.Euler(new Vector3(90f, 0f, -90f));
            npcs.Add(npcRagdoll);
        }

        isAddingNPC = false;
    }
    public void RemoveNPC(GameObject destiny)
    {
        if (npcs.Count > 0)
        {
            npcs[0].transform.position = Vector3.Lerp(npcs[0].transform.position, destiny.transform.position, 0.9f);
            Destroy(npcs[0].gameObject, 1.2f);
            npcs.Remove(npcs[0]);
        }
    }
    public void UpgradeMax(float valueToIncrease)
    {
        maxBag += valueToIncrease;
    }
    Ragdoll FindRagdollParent(Transform objeto)
    {
        Ragdoll ragdoll = objeto.GetComponent<Ragdoll>();

        if (ragdoll != null)
        {
            return ragdoll;
        }
        else if (objeto.parent != null)
        {
            return FindRagdollParent(objeto.parent);
        }
        return null;
    }
}