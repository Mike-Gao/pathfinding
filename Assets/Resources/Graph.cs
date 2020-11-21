using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{


    private ReducedVisibility graph;
    private GraphNode orig;
    private GraphNode dest;

    public Graph(ReducedVisibility singleton, Vector2 start, Vector2 end)
    {
        List<GraphNode> nodes = new List<GraphNode>(singleton.vertices.Count + 2);
        orig = new GraphNode(new Vertex {value = start}, 0);
        dest = new GraphNode(new Vertex {value = end}, 0);

        foreach(var v in singleton.vertices)
        {
            var node = new GraphNode(v, v.neighbors.Count + 1);
            v.curdownstream = node;
            nodes.Add(node);
        }

        Vector3 startPos = new Vector3(start.x, start.y, Global.obstacleZ);
        Vector3 endPos = new Vector3(end.x, end.y, Global.obstacleZ);
        Vector3 tmp = new Vector3();

        foreach(var node in nodes)
        {
            tmp.x = node.v.value.x;
            tmp.y = node.v.value.y;
            tmp.z = Global.obstacleZ;

            if (!Physics.Linecast(startPos, tmp))
            {
                var cost = (start - node.v.value).magnitude;
                orig.neighbors.Add((node, cost));
                node.neighbors.Add((orig, cost));
            }

            if (!Physics.Linecast(endPos, tmp))
            {
                var cost = (end - node.v.value).magnitude;
                dest.neighbors.Add((node, cost));
                node.neighbors.Add((dest, cost));
            }

            foreach(var (n, cost) in node.v.neighbors)
            {
                node.neighbors.Add((n.curdownstream, cost));
            }
        }

    }

    public float Heuristic(GraphNode n)
    {
        return (n.v.value - dest.v.value).magnitude;
    }

    public GraphNode ComputeLowestFScore(HashSet<GraphNode> set, Dictionary<GraphNode,float> fscore)
    {
        GraphNode lowest = null;
        float current = float.PositiveInfinity;

        foreach(var n in set)
        {
            
        }
    }
    public List<Vector3> AStarSearch()
    {
        // idea borrowed directly from wikipedia
        var openSet = new HashSet<GraphNode>();
        openset.Add(orig);
        var cameFrom = new Dictionary<GraphNode, GraphNode>();
        var gScore = new Dictionary<GraphNode, float>();
        gScore.Add(orig, 0);
        var fScore = new Dictionary<GraphNode, float>();
        fScore.Add(orig, Heuristic(orig))

        while(openSet.Count != 0)
        {
            var cur = ComputeLowestFScore(openSet, fScore);

            if (cur == dest)
            {
                return ReconstructPath(cameFrom, cur);
            }
        }
        return new List<Vector3>();

    }


    
}
