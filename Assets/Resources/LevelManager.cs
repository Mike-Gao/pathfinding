using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] edges;
    public readonly List<Vector2> pts = new List<Vector2>();
    public readonly List<Obstacle> obstacles = new List<Obstacle>();

    public Obstacle Obstacle;

    void Start()
    {
        ObstacleSpawner();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool Overlap(Vector2 new_p, List<Vector2> pos, float l)
    {
        foreach (var p in pos)
        {
            if (Mathf.Abs(p.x - new_p.x) < l && Mathf.Abs(p.y - new_p.y) < l) return true;
        }
        return false;
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
            Instantiate(Obstacle, p, Quaternion.identity);
        }

    }

}
