using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularArray : IEnumerable<Vertex>
{
    private readonly Vertex[] circular;
    public int Length {
        get {
            return circular.Length;
        }
    }
    
    public CircularArray(List<Transform> t, List<bool> isInfectionPt)
    {
        circular = new Vertex[t.Count];
        for(int i = 0; i < circular.Length; i++)
        {
            circular[i] = new Vertex();
        }
        for(int i = 0; i < circular.Length; i++)
        {
            int prev = i - 1;
            int next = i + 1;
            if (prev < 0) prev = circular.Length - 1;
            if (next == circular.Length) next = 0;
            circular[i].next = circular[next];
            circular[i].prev = circular[prev];
            circular[i].value = t[i].position;
            circular[i].isInflection = isInfectionPt[i];
        }

    }

    public Vertex this[int i]
    {
        get {
            return circular[i];
        }
    }


    public IEnumerator<Vertex> GetEnumerator()
    {
        return ((IEnumerable<Vertex>)circular).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return circular.GetEnumerator();
    }


}
