using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReducedVisibility
{

    public readonly List<Vertex> vertices = new List<Vertex>();
    public readonly List<(Vertex, Vertex)> edges = new List<(Vertex, Vertex)>();
    public ReducedVisibility(List<CircularArray> list)
    {
        foreach (CircularArray circ in list)
        {
            foreach (var vertex in circ)
            {
                if(vertex.isInflection == true)
                {
                    vertices.Add(vertex);
                }
            }
        }

        for(int i = 0; i < vertices.Count; i++)
        {
            for(int j = i + 1; j < vertices.Count; j++)
            {
                var node_1 = vertices[i];
                var node_2 = vertices[j];
                var pt_1 = new Vector3(node_1.value.x, node_1.value.y, Global.obstacleZ);
                var pt_2 = new Vector3(node_2.value.x, node_2.value.y, Global.obstacleZ);

                Vector3 dir = pt_2 - pt_1;
                if (!Physics.Raycast(pt_1, dir, dir.magnitude) && BitTangent(node_1, node_2))
                {
                    edges.Add((node_1, node_2));
                }
            }
        }
    }

    public bool BitTangent(Vertex v1, Vertex v2)
    {
        var f = SignedArea(v1.next.value, v1.value, v2.value);
        var g = SignedArea(v1.prev.value, v1.value, v2.value);
        if (f != g && f != 0 && g != 0)
        {
            return false;
        }
        var s = SignedArea(v2.next.value, v2.value, v1.value);
        var t = SignedArea(v2.prev.value, v2.value, v1.value);
        if (s != t && s != 0 && t != 0)
        {
            return false;
        }
        return true;
    }

    public int SignedArea(Vector2 v1, Vector2 v2, Vector2 v3)
    {
        float val = (v2.x - v1.x) * (v3.y - v1.y) - (v3.x - v1.x) * (v2.y - v1.y);
        if (val == 0)
        {
            return 0;
        } else if (val > 0) {
            return 1;
        }
        return -1;
    }
}
