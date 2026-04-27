using UnityEngine;

public class Bouncy : MonoBehaviour
{
    [Tooltip("Base impulse strength applied upward.")]
    [SerializeField] private float bounceStrength = 15f;

    [Tooltip("How much bounce strength is reduced after each collision.")]
    [Range(0f, 1f)]
    [SerializeField] private float bounceDecay = 0.8f;

    [Tooltip("How quickly the bounce strength restores back to full over time.")]
    [SerializeField] private float bounceRestorePerSecond = 0.5f;

    private float currentBounceFactor = 1f;

    void FixedUpdate()
    {
        currentBounceFactor = Mathf.MoveTowards(currentBounceFactor, 1f, bounceRestorePerSecond * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Transform root = collision.transform.root;

        if (!root.CompareTag("Player"))
            return;

        Rigidbody2D rb = collision.rigidbody;

        if (rb == null)
            return;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);

        rb.AddForce(Vector2.up * bounceStrength * currentBounceFactor, ForceMode2D.Impulse);

        currentBounceFactor = Mathf.Clamp01(currentBounceFactor * bounceDecay);
    }
}