using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Assertions;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Transform> stage_vertices;
    public List<bool> stage_inflectionPts;

    public ReducedVisibility Graph {get; private set;}

    public GameObject Point;
    public GameObject Line;
    
    UnityEvent graph_event = new UnityEvent();

    public readonly List<CircularArray> shapes = new List<CircularArray>();
    public readonly List<Obstacle> obstacles = new List<Obstacle>();

    public Obstacle Obstacle;

    public Agent agent;
    public List<Agent> agents = new List<Agent>();
    public int agentLimit;

    void Start()
    {
        StagePrep();
        ObstacleSpawner();
        // Adding a bit of delay to allow object to spawn properly
        Invoke("ShowReducedVisabilityGraph", 0.5f);
        // Invoke AgentSpawner after building Reduced Visability Graph
        Invoke("AgentSpawner", 0.5f);
        // Debug
        // foreach( var list in shapes ){
        //     for(int i = 0; i < list.Length; i++){
        //         var ret = Instantiate(Line);
        //         LineRenderer lr = ret.GetComponent<LineRenderer>();
        //         var pos = new Vector3[] {list[i].getValue(Global.graphZ), list[i].prev.getValue(Global.graphZ)};
        //         lr.positionCount = pos.Length;
        //         lr.SetPositions(pos);
        //         lr.startColor = Color.red;
        //         lr.endColor = Color.green;
        //         lr.startWidth = 0.05f;
        //         lr.endWidth = 0.05f;
        //     }
        // }
    }


    void AgentSpawner()
    {
        while(agents.Count < agentLimit)
        {
            var pos = Global.RandomPosition(Agent.halfExtents);
            pos.z = Global.agentZ;
            agents.Add(Instantiate(agent, pos, Quaternion.identity));
        }
    }

    bool Overlap(Vector2 new_p, List<Vector2> pos, float l)
    {
        foreach (var p in pos)
        {
            if (Mathf.Abs(p.x - new_p.x) < l && Mathf.Abs(p.y - new_p.y) < l) return true;
        }
        return false;
    }

    void StagePrep()
    {
        Assert.IsTrue(stage_vertices.Count == stage_inflectionPts.Count);
        shapes.Add(new CircularArray(stage_vertices, stage_inflectionPts));
    }

    void ObstacleSpawner()
    {
        List<Vector2> positions = new List<Vector2>();
        float x_min = -4.00F;
        float x_max = 4.00F;
        float y_max = 2.00F;
        float y_min = -2.00F;
        int numOfObstacles = Random.Range(3, 5);
        int iter = 0;
        while (positions.Count < numOfObstacles && iter < 1000)
        {
            Vector2 new_pos = new Vector2(Random.Range(x_min, x_max), Random.Range(y_min, y_max));
            if(!Overlap(new_pos, positions, 1.6f))
            {
                positions.Add(new_pos);
                print(new_pos);
            }
            iter ++;
        }
        foreach (var p in positions)
        {
            var obstacle = Instantiate(Obstacle, new Vector3(p.x, p.y, Global.obstacleZ), Quaternion.identity);
            obstacles.Add(obstacle);
            shapes.Add(obstacle.vertices);
        }

    }


    
    void ShowReducedVisabilityGraph()
    {
        Graph = new ReducedVisibility(shapes);
        foreach ( var vertex in Graph.vertices)
        {
            Instantiate(Point, vertex.getValue(Global.graphZ), Quaternion.identity);
        }

        foreach (var (v1, v2) in Graph.edges)
        {
            var ret = Instantiate(Line);
            LineRenderer lr = ret.GetComponent<LineRenderer>();
            var pos = new Vector3[] {v1.getValue(Global.graphZ), v2.getValue(Global.graphZ)};
            lr.positionCount = pos.Length;
            lr.SetPositions(pos);
            lr.startColor = Color.blue;
            lr.endColor = Color.yellow;
            lr.startWidth = 0.05f;
            lr.endWidth = 0.05f;
        }
    }

}
