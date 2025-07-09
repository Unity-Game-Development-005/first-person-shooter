
using UnityEngine;


public class RagdollToggle : MonoBehaviour
{
    void Start()
    {
        // set all the character's ragdoll rigidbody components to kinematic
        // disable the ragdoll physics
        foreach (Rigidbody ragdollRigidbody in GetComponentsInChildren<Rigidbody>())
        {
            ragdollRigidbody.isKinematic = true;
        }
    }


    public void TriggerRagdoll()
    {
        // reset all the character's ragdoll rigidbody components to be non-kinematic
        // enable the ragdoll physics
        foreach (Rigidbody ragdollRigidbody in GetComponentsInChildren<Rigidbody>())
        {
            ragdollRigidbody.isKinematic = false;
        }


        // disable the character collider component
        GetComponent<Collider>().enabled = false;
    }




} // end of class
