
using UnityEngine;

public class PlatformJump : MonoBehaviour
{
    public float v0 = 10f;    // vertical speed
    public float g = 10f;     // gravity
    public Animator animator;
    float _maxAnim = .667f;
    private Vector3 startPos;
    private Vector3 targetPos;
    private float t = 0f;
    private float tTotal;
    private float vx;
    private bool jumping = false;

    void Update()
    {
        if (!jumping && Input.GetKeyDown(KeyCode.Space))
        {
            targetPos = new Vector3(transform.position.x + 5f, transform.position.y + 2f, transform.position.z);
            StartJump(targetPos);
        }

        if (jumping)
        {
            t += Time.deltaTime;

            float y = -0.5f * g * t * t + v0 * t + startPos.y;
            float x = startPos.x + vx * t;
            transform.position = new Vector3(x, y, startPos.z);

            if (t >= tTotal)
            {
                transform.position = targetPos;
                jumping = false;
                if (animator != null) animator.speed = 1f;
            }
        }
        
    }

    void StartJump(Vector3 target)
    {
        startPos = transform.position;

        float a = -0.5f * g;
        float b = v0;
        float c = startPos.y - target.y;

        float discriminant = b * b - 4 * a * c;
        if (discriminant < 0)
        {
            Debug.LogError("Target platform unreachable!");
            return;
        }

        float sqrtD = Mathf.Sqrt(discriminant);
        float t1 = (-b + sqrtD) / (2 * a);
        float t2 = (-b - sqrtD) / (2 * a);

        tTotal = Mathf.Max(t1, t2);
        Debug.Log("Total jump time: " + tTotal);
        vx = (target.x - startPos.x) / tTotal; 

        t = 0f;
        jumping = true;

        if (animator != null)
        {
            animator.Play("Jump");
            animator.speed = _maxAnim / tTotal; 
        }
    }
}
