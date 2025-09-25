using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parabola : MonoBehaviour
{
    [SerializeField] private Vector2 minScreen, maxScreen;

    [SerializeField] private int numberOfPoints = 10;
    [SerializeField] private float a = -1f, b = 1f, c = 1f;

    [SerializeField] private Point pointPrefab;

    void Start()
    {
        minScreen = Camera.main.ScreenToWorldPoint(Vector2.zero);
        maxScreen = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));


    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){ 
        
        QuadraticFunction quadratic = new QuadraticFunction(a, b, c);
        float dx = (maxScreen.x - minScreen.x) / (numberOfPoints - 1);
        for (int i = 0; i < numberOfPoints; i++)
        {
            float x = minScreen.x + i * dx;
            float y = quadratic.CalculateY(x);

            Point pointCopy = Instantiate(pointPrefab);
            pointCopy.x = x;
            pointCopy.y = y;
        }
        }

    }
}
