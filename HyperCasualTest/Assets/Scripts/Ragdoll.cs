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
    public void TakeHit(Vector3 direction)
    {
        foreach (Rigidbody r in bones)
        {
            r.useGravity = true;
            r.isKinematic = false;

            r.AddForce(direction * 5000);
        }
    }
    public void UnactivateRagdoll()
    {
        foreach (Rigidbody r in bones)
        {
            r.useGravity = false;
            r.isKinematic = true;
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
