using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : MonoBehaviour
{
    public Map Map;
    public int MapIndexA, MapIndexB;
    public GameObject DirtParticlePrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "shovel")
        {
            //Also adjust bit map in map controller.
            Map.RemoveDirt(MapIndexA, MapIndexB);
            GetComponent<Collider2D>().enabled = false;
            var spawn = Instantiate(DirtParticlePrefab, transform.position, Quaternion.identity);
            spawn.GetComponent<Rigidbody2D>().velocity = (collision.transform.position - transform.position) * 50;

        }
    }
}
