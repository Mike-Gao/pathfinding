using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Obstacle : MonoBehaviour
{

    static readonly System.Random random = new System.Random();


    public GameObject square1;
    public GameObject square2;

    public List<Transform> nodes;
    public List<bool> inflectionPt;
    public CircularArray vertices;
    void Awake()
    {
        var width = Random.Range(0.4f, 0.6f);
        var height = Random.Range(0.4f, 0.6f);
        var a = Random.Range(0.4f, 0.6f);
        var b = Random.Range(0.8f, 1.2f);
        square1.transform.localScale = new Vector3( b, a, 1);
        square1.transform.localPosition = new Vector3( -width + (b / 2), a / 2, 0);
        
        square2.transform.localScale = new Vector3( width, height, 1);
        square2.transform.localPosition = new Vector3( -width / 2, -height / 2, 0);
        

        int[] rotation = new int[] { 0, 90, 180, 270 };
        var deg = rotation[random.Next(rotation.Length)];
        this.gameObject.transform.Rotate(0, 0, deg);
        Assert.IsTrue(nodes.Count == inflectionPt.Count);
        vertices = new CircularArray(nodes, inflectionPt);
    }

}
