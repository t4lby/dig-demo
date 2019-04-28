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
            if (Random.Range(0f, 1f) < collision.GetComponentInParent<Shovel>().ParticleRetention)
            {
                var spawn = Instantiate(DirtParticlePrefab, transform.position, Quaternion.identity);
                spawn.GetComponent<Rigidbody2D>().velocity = collision.GetComponentInParent<Shovel>().ParticleFlyVelocity;
            }
        }
    }
}
