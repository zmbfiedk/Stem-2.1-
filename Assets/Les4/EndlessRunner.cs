using UnityEngine;

[RequireComponent(typeof(Animator))]
public class endlessRunner : MonoBehaviour
{
    [SerializeField] float vbegin = 4f;      
    [SerializeField] float g = -5f;         

    [SerializeField] float forwardSpeed = 2f; 
    [SerializeField] string runAnimName = "Run";
    [SerializeField] string jumpAnimName = "Jump";

    Animator animator;

    enum State { running, jumping };
    State myState = State.running;

    Vector3 velocity = Vector3.zero;
    Vector3 acceleration = Vector3.zero;

    float tmax = 1.667f;
    float t = 0f;

    float startY;

    void Start()
    {
        animator = GetComponent<Animator>();
        startY = transform.position.y;

        if (animator.runtimeAnimatorController != null)
        {
            var clips = animator.runtimeAnimatorController.animationClips;
            foreach (var c in clips)
            {
                if (c.name == jumpAnimName)
                {
                    tmax = c.length;
                    if (Mathf.Abs(tmax) > 0.0001f)
                    {
                        g = -2f * vbegin / tmax;
                    }
                    break;
                }
            }
        }

        if (animator != null)
        {
            animator.Play(runAnimName);
        }
    }

    void Update()
    {
        float dt = Time.deltaTime;

        Vector3 pos = transform.position;
        pos.x += forwardSpeed * dt;

        switch (myState)
        {
            case State.running:
                velocity.y = 0f;
                acceleration = Vector3.zero;
                t = 0f;

                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    StartJump();
                }

                if (animator != null)
                {
                    animator.Play(runAnimName);
                }

                break;

            case State.jumping:
                acceleration = new Vector3(0f, g, 0f);
                velocity += acceleration * dt;
                pos.y += velocity.y * dt;

                t += dt;

                if (t >= tmax || pos.y <= startY)
                {
                    EndJump(ref pos);
                }
                break;
        }

        transform.position = pos;
    }

    void StartJump()
    {
        if (myState == State.jumping) return; 

        myState = State.jumping;
        t = 0f;
        velocity.y = vbegin;

        if (tmax > 0.0001f)
        {
            g = -2f * vbegin / tmax;
        }

        if (animator != null)
        {
            animator.Play(jumpAnimName, 0, 0f);
        }
    }

    void EndJump(ref Vector3 pos)
    {
        pos.y = startY;
        velocity.y = 0f;
        acceleration = Vector3.zero;
        t = 0f;
        myState = State.running;

        if (animator != null)
        {
            animator.Play(runAnimName, 0, 0f);
        }
    }

    public void TriggerJump()
    {
        StartJump();
    }
}
