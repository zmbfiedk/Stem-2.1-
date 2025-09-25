using UnityEngine;

public class Point : MonoBehaviour
{
    public float x;
    public float y;

    void Update()
    {
        transform.position = new Vector3(x, y, 0);
    }
}
