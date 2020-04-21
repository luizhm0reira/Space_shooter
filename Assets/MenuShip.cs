using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuShip : MonoBehaviour
{
    public float x_Start, y_Start;
    public int columnLenght, rowLenght;
    public float x_Space, y_Space;
    public GameObject[] prefabs;

    void Start()
    {
        //for (int i = 0; i < columnLenght * rowLenght; i++)
        for (int i = 0; i < prefabs.Length; i++)
        {
            Instantiate(prefabs[i], new Vector3(x_Start + (x_Space * (i % columnLenght)), y_Start + (-y_Space * (i / columnLenght))), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
