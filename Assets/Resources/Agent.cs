using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Agent : MonoBehaviour
{
    public static ReducedVisibility graph;

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

    private bool Retry {
        get {
            if (failures >= 3){
                failures = 0;
                return false;
            }
            return true;
        }
    }
    private bool Collided {
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
        var pos = Global.RandomPosition(halfExtents);
        pos.z = Global.agentZ;

        destination = Instantiate(destinationPrefab, pos, Quaternion.identity);
        destination.GetComponent<SpriteRenderer>().color = Renderer.color;

        GoToDestination();
    }

    void GoToDestination()
    {
        path = Graph.ComputePath(graph, this.transform.position, this.destination.transform.position);
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
            Global.success++;
            status = 2;
            // 200ms - 1000ms, choose new destination
            Invoke("GoToRandomDestination", Random.Range(0.2f, 1f));
            return;
        }

        float maxdistanceDelta = velocity * Time.deltaTime;
        Vector3 target = path[path.Count - 1];
        transform.position = Vector3.MoveTowards(transform.position, target, maxdistanceDelta);
        if (Vector3.Distance(transform.position, target) < 0.001f)
        {
            path.RemoveAt(path.Count - 1);
        }
    }
    
    void BackTrack()
    {
        if (path.Count != 0)
        {
            float maxdistanceDelta = -0.5f * velocity * Time.deltaTime;
            var target = path[path.Count - 1];
            this.transform.position = Vector3.MoveTowards(transform.position, target, maxdistanceDelta);
        }
    }

    // Retry after collision
    void RetryDestination()
    {
        
        if (Retry)
        {
            Global.pathReplanned += 1;
            GoToDestination();
        } 
        else
        {
            GoToRandomDestination();
        }
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (status == 0 && !Collided) {
            Move();
        } 
    }

    // If they collided, wait between 100ms and 500ms and replan
    private void OnTriggerStay2D(Collider2D other)
    {
        if (status == 2)
        {
            return;
        }
        status = 2;
        //Debug.Log("OnTriggerStay");
        // Push back both collider
        var thisToThat = other.transform.position - transform.position;
        var offset = radius * 2 - thisToThat.magnitude;
        thisToThat.Normalize();
        transform.position -= 2.0f * offset * thisToThat;
        other.transform.position += 2.0f * offset * thisToThat;
        Invoke("RetryDestination", Random.Range(0.1f, 0.5f));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        collisions++;
        BackTrack();
        //Debug.Log("OnTriggerEnter");
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        collisions--;
        //Debug.Log("OnTriggerExit");
    }

}
