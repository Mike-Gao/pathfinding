using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global
{
    // Start is called before the first frame update
    public static readonly float obstacleZ = -2;
    public static readonly float graphZ = -3;

    public static Vector3 RandomPosition()
    {
        float x_min = -9;
        float x_max = 9;
        float y_min = -5;
        float y_max = 5;

        while (true)
        {
            var pos = new Vector3(Random.Range(x_min, x_max), Random.Range(y_min, y_max), 1);
            bool inArena = Physics.Raycast(pos, Vector3.back, 10);
            bool isOverlap = Physics.Raycast(pos, Vector3.back, 10);
            if (!inArena || isOverlap) continue;
            pos.z = 0;
            return pos;
        }
    }
}
