using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : MonoBehaviour
{
    public Map Map;
    public int MapIndexA, MapIndexB;

    private void OnTriggerEnter2D(Collider2D collision)
    {


        //Also adjust bit map in map controller.
        Map.RemoveDirt(MapIndexA, MapIndexB);
    }
}
