using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    static readonly System.Random random = new System.Random();
    public List<Vector2> nodes = new List<Vector2>();

    void Awake()
    {
        
        int[] rotation = new int[] { 0, 90, 180, 270 };
        var deg = rotation[random.Next(rotation.Length)];
        this.gameObject.transform.Rotate(0, 0, deg);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
