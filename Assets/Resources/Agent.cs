using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Agent : MonoBehaviour
{
    public static ReducedVisibility graph;
    public static int spawned = 0;

    public static readonly Vector3 halfExtents = new Vector3(radius, radius, 1);
    public static readonly float radius = 0.1f;
    public static readonly float velocity = 2.5f;

    // Agent status: 0 - Moving, 1 - Reached, 2 - Waiting
    private int status = 0; 
    public SpriteRenderer Renderer { get { return GetComponent<SpriteRenderer>();}}
   
    public GameObject destination;
    public GameObject destinationPrefab;

    private List<Vector3> path;

    private int failures = 0;
    private int collisions = 0;
    bool Retry{
        get{
            if (failures >=3) {
                failures = 0;
                return false;
            }
            failures++;
            return true;
        }
    }
    bool Collided {
        get{
            return collisions > 0;
        }
    }
    void Start()
    {
        if (graph == null) 
        {
            graph = FindObjectOfType<LevelManager>().Graph;
        }
        
        // Set Color
        var color = Random.ColorHSV();
        color.a = 1;
        Renderer.color = color;
        spawned++;
        var pos = Global.RandomPosition(halfExtents);
        pos.z = Global.agentZ;

        destination = Instantiate(destinationPrefab, pos, Quaternion.identity);
        destination.GetComponent<SpriteRenderer>().color = Renderer.color;

        GoToDestination();
    }

    void GoToDestination()
    {
        var dir = destination.transform.position - this.transform.position;
        var pos = new Vector3(transform.position.x, transform.position.y, Global.obstacleZ);
        if (!Physics.BoxCast(pos, halfExtents, dir, Quaternion.identity))
        {
            status = 0;
            path = new List<Vector3>(){ destination.transform.position};
            Move();
            return;
        }
        var gr = new Graph(graph, transform.position, destination.transform.position);
        path = gr.AStarSearch();
        if (path.Count == 0)
        {
            Debug.Log("Failed to find a path");
        }
        status = 0;
        Move();
    }

    void GoToRandomDestination()
    {
        var pos = Global.RandomPosition(halfExtents);
        pos.z = Global.agentZ;
        this.destination.transform.position = pos;
        GoToDestination();
    }

    void Move()
    {
        if(path.Count == 0)
        {
            status = 1;
            return;
        }

        float maxdistanceDelta = velocity * Time.deltaTime;
        Vector3 target = path[path.Count - 1];
        this.transform.position = Vector3.MoveTowards(transform.position, target, maxdistanceDelta);
        if (Vector3.Distance(this.transform.position, target) < 0.001f)
        {
            path.RemoveAt(path.Count - 1);
        }
    }
    
    void BackTrack()
    {
        if (path.Count != 0)
        {
            float maxdistanceDelta = Random.Range(1, 2) * velocity * Time.deltaTime;
            var target = path[path.Count - 1];
            this.transform.position = Vector3.MoveTowards(transform.position, -target, maxdistanceDelta);
        }
    }

    void RetryDestination()
    {
        if (Retry)
        {
            BackTrack();
            GoToDestination();
        } 
        else
        {
            // trigger a new destination
            status = 1;
        }
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        switch(status){
            case 0:
                if(!Collided)
                {
                    Move();
                }
                break;
            case 1:
                Invoke("GoToRandomDestination", Random.Range(0.2f, 1));
                status = 2;
                break;
            default:
                break;
        }
        
    }
}
