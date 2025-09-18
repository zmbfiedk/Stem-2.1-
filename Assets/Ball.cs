using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField] private Vector3 velocity = new Vector3(1, 1, 0);
    [SerializeField] private Vector2 Minscreen, Maxscreen;
    [SerializeField] Vector3 acceleration = new Vector3(0, -9.8f, 0);
    void Start()
    {
        Minscreen = Camera.main.ScreenToWorldPoint(Vector2.zero);
        Maxscreen = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }


    void Update()
    {

        Vector3 pos = transform.position;
        velocity += acceleration * Time.deltaTime;
        if (pos.x < Minscreen.x || pos.x > Maxscreen.x)
        {
            velocity.x = -velocity.x;
        }
        if (pos.y < Minscreen.y || pos.y > Maxscreen.y)
        {
            velocity.y = -velocity.y;
        }
        transform.position += velocity * Time.deltaTime;
    }
}
