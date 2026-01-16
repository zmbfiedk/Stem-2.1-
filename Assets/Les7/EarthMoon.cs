using UnityEngine;

public class EarthMoon : MonoBehaviour
{
    [SerializeField] GameObject Earth;
    [SerializeField] GameObject Moon;

    Vector3 velocityMoon;
    Vector3 accelerationMoon;
    Vector3 difference;
    Vector3 direction;

    float distance;

    void Start()
    {
        velocityMoon = new Vector3(1, 2, 3);

    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        Earth.transform.Rotate(0, -1f, 0);

        difference = Earth.transform.position - Moon.transform.position;
        distance = difference.magnitude; ;
        direction = difference.normalized;

        accelerationMoon = 100 * direction / (distance * distance);

        velocityMoon += accelerationMoon * Time.deltaTime;
        Moon.transform.position += velocityMoon * Time.deltaTime;
    }
}