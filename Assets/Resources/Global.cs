using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global
{

    public static readonly float obstacleZ = -2;
    public static readonly float graphZ = -3;
    public static readonly float agentZ = -4;

    // generate a random position on the floor that is not overlapping with agents and obstacle

    private static bool CubeCast(Vector3 orig, float l)
    {
        Vector3 pos = new Vector3(orig.x + l / 2, orig.y + l / 2, orig.z);
        if (!Physics.Raycast(pos, Vector3.back, 2)) return false;
        pos.x -= l;
        if (!Physics.Raycast(pos, Vector3.back, 2)) return false;
        pos.x += l;
        pos.y -= l;
        if (!Physics.Raycast(pos, Vector3.back, 2)) return false;
        return true;
    }

    public static Vector3 RandomPosition(Vector3 halfExtents)
    {
        float x_min = -9;
        float x_max = 9;
        float y_min = -5;
        float y_max = 5;

        while (true)
        {
            var pos = new Vector3(Random.Range(x_min, x_max), Random.Range(y_min, y_max), 1);
            bool inArena = CubeCast(pos, halfExtents.x * 2);
            pos.z = 0;
            bool isOverlap = Physics.BoxCast(pos, halfExtents, Vector3.back);
            if (!inArena || isOverlap) continue;
            return pos;
        }
    }

    public static TValue GetValueOrReturnDefault<TKey, TValue> (this Dictionary<TKey, TValue> dict, TKey key, TValue defaultval = default)
    {
        if (dict.TryGetValue(key, out TValue value)) {
            return value;
        }
        return defaultval;
    }

}
