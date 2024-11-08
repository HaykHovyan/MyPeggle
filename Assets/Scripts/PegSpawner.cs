using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject peg;
    [SerializeField]
    private float MinX, MinY, MaxX, MaxY;
    [SerializeField]
    private int amountX, amountY;
    [SerializeField]
    private float randomStrength = 1;
    private float intervalX, intervalY;
    
    void Start()
    {
        intervalX = (MaxX - MinX) / amountX;
        intervalY = (MaxY - MinY) / amountY;
        Spawn();
    }

    void Spawn()
    {
        int pegCount = 0;
        for (float i = MinY; i < MaxY; i += intervalY)
        {
            for (float j = MinX; j < MaxX; j += intervalX)
            {
                Instantiate(peg, new Vector3(j + (Random.value - 0.5f) * 2 * randomStrength, i + (Random.value - 0.5f) * 2 * randomStrength, 0), new Quaternion());
                pegCount++;
            }
        }
        GameController.pegsLeft = pegCount;
    }
}