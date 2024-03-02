using UnityEngine;

public static class RigidbodyExtension 
{
    public static void ResetVelocity(this Rigidbody rb)
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
