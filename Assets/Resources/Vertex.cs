using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertex
{
    public Vector2 value;
    public Vertex prev;
    public Vertex next;

    public List<Vertex> neighbors = new List<Vertex>();

    public Vector3 getValue(float z)
    {
        return new Vector3(value.x, value.y, z);
    }

    public override int GetHashCode()
    {
        return value.GetHashCode();
    }

    public override string ToString()
    {
        return value.ToString();
    }
}
