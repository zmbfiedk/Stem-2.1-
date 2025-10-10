using UnityEngine;

public class JumpingBlock : MonoBehaviour
{
    [SerializeField] Transform Block;
    [SerializeField] Vector3 gravityBegin = new Vector3(0, -1f, 0);
    [SerializeField] Vector3 velocityBegin = new Vector3(0, 3f, 0);
    float ybegin;
    Vector3 velocity;
    Vector3 gravity;
    enum State { ground, airborne };
    State myState = State.ground;

    [SerializeField] float Timer = 0f;

    void Start()
    {
        ybegin = Block.position.y;
        velocity = Vector3.zero;
        gravity = Vector3.zero;
    }

    void Update()
    {
        if (myState == State.ground)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("jumped");
                myState = State.airborne;
                velocity = velocityBegin;
                gravity = gravityBegin;
                Timer = 0f;
            }
        }

        velocity += gravity * Time.deltaTime;
        Block.position += velocity * Time.deltaTime;

        if (myState == State.airborne)
        {
            Timer += Time.deltaTime;
            if (Block.position.y < ybegin)
            {
                Debug.Log(Timer.ToString("F2") + " seconds");
                Debug.Log("airborne");
                velocity = Vector3.zero;
                gravity = Vector3.zero;
                Block.position = new Vector3(Block.position.x, ybegin, 0);
                myState = State.ground;
            }
        }
    }
}