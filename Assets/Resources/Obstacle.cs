using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    static readonly System.Random random = new System.Random();
    public List<Transform> nodes = new List<Transform>();
    public GameObject square1;
    public GameObject square2;
    void Awake()
    {
        var width = Random.Range(0.4f, 0.6f);
        var height = Random.Range(0.4f, 0.6f);
        
        square1.transform.localPosition = new Vector3( -width / 2, -height / 2, Global.obstacleZ);
        square1.transform.localScale = new Vector3( width, height, 1);
        square2.transform.localPosition = new Vector3( -width / 2, -height / 2, Global.obstacleZ);
        square2.transform.localScale = new Vector3( width, height, 1);

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
