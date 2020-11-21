using System.Collections;
using System.Collections.Generic;
public class GraphNode
{
    public readonly Vertex v;
    public readonly List<(GraphNode node, float cost)> neighbors;
    
    public GraphNode(Vertex v, int count)
    {
        this.v = v;
        if (count > 0)
        {
            neighbors = new List<(GraphNode node, float cost)> (count);
        } else {
            neighbors = new List<(GraphNode node, float cost)> ();
        }
    }

    public override int GetHashCode()
    {
        return v.value.GetHashCode();
    }
}
