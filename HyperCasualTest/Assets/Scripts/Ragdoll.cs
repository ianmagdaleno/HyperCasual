using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    public Transform ragdollObject;
    private List<Rigidbody> bones = new List<Rigidbody>();

    private void Start()
    {
        StartActiveRagdoll();
    }
    public void ActivateRagdoll()
    {
        foreach (Rigidbody r in bones)
        {
            r.useGravity = true;
            r.isKinematic = false;
        }
    }
    public void StartActiveRagdoll()
    {
        foreach (Rigidbody r in ragdollObject.GetComponentsInChildren<Rigidbody>())
        {
            bones.Add(r);
            r.useGravity = false;
            r.isKinematic = true;
        }
    }
}
