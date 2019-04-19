using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtGenerator : MonoBehaviour
{
    public GameObject DirtPrefab;
    public int SpawnEveryNFrames = 10;

    private void Update()
    {
        if (Time.frameCount % SpawnEveryNFrames == 0)
        {
            Instantiate(DirtPrefab);
        }
    }
}
