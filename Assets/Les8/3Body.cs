using UnityEngine;

public class ThreeBodySameMass : MonoBehaviour
{
    public Vector3 velocity;

    public static float mass = 1f;          // SAME mass for all bodies
    public static float gravityStrength = 1f;

    private static ThreeBodySameMass[] bodies;

    void Start()
    {
        bodies = FindObjectsOfType<ThreeBodySameMass>();
    }

    void FixedUpdate()
    {
        Vector3 acceleration = Vector3.zero;

        foreach (ThreeBodySameMass other in bodies)
        {
            if (other == this) continue;

            Vector3 direction = other.transform.position - transform.position;
            float distance = direction.magnitude;

            distance = Mathf.Max(distance, 0.2f);

            Vector3 force =
                gravityStrength *
                mass /
                (distance * distance) *
                direction.normalized;

            acceleration += force;
        }

        velocity += acceleration * Time.fixedDeltaTime;
        transform.position += velocity * Time.fixedDeltaTime;
    }
}
