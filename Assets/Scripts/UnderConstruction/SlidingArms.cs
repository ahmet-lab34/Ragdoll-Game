using System;
using UnityEngine;

public class SlidingArms : MonoBehaviour
{
    private FixedJoint2D fixedJoint;

    void OnTriggerEnter2D(Collider2D collider) {
        if (!collider.CompareTag("Climable")) return;
        if (fixedJoint != null) return;
        if (collider.attachedRigidbody == null) return;
        
        // Create a fixed joint to the object
        fixedJoint = gameObject.AddComponent<FixedJoint2D>();
        fixedJoint.connectedBody = collider.attachedRigidbody;
    }

    void OnTriggerExit2D(Collider2D collider){
        if (!collider.CompareTag("Climable")) return;
        if (fixedJoint == null) return;
        if (fixedJoint.connectedBody != collider.attachedRigidbody) return;

        Destroy(fixedJoint);
        fixedJoint = null;
    }
}
