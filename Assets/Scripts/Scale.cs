using UnityEngine;

public class Scale : MonoBehaviour
{
    public Transform boxA;
    public Transform boxB;
    
    private Vector3 lastBoxAPos;
    private Vector3 lastBoxBPos;
    
    void Start()
    {
        if (boxA == null || boxB == null)
        {
            Debug.LogError("Scale: BoxA and BoxB transforms must be assigned in the Inspector!");
            enabled = false;
            return;
        }
        
        Debug.Log("Scale constraint active: BoxA and BoxB synced");
        lastBoxAPos.y = boxA.position.y;
        lastBoxBPos.y = boxB.position.y;
    }

    void LateUpdate()
    {
        // Calculate how much each box moved this frame
        Vector3 moveA = boxA.position - lastBoxAPos;
        Vector3 moveB = boxB.position - lastBoxBPos;
        
        // Pulley constraint: if one box moves, the other should move in the opposite direction
        // Net Y movement should be zero: moveA.y + moveB.y = 0
        float totalYMove = moveA.y + moveB.y;
        
        if (Mathf.Abs(totalYMove) > 0.0001f)
        {
            // Split the correction: each box moves half the difference
            // But in opposite directions to create the mirror effect
            Vector3 correctionA = boxA.position;
            Vector3 correctionB = boxB.position;
            
            correctionA.y -= totalYMove * 0.5f;
            correctionB.y += totalYMove * 0.5f;  // Opposite direction!
            
            boxA.position = correctionA;
            boxB.position = correctionB;
        }
        
        // Store positions for next frame
        lastBoxAPos.y = boxA.position.y;
        lastBoxBPos.y = boxB.position.y;
    }
}
