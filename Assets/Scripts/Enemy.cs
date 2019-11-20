using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Vehicle
{
    #region Fields
    [SerializeField]
    private List<GameObject> pathNodes; 
    [SerializeField]
    private int currentPathNode;
    [SerializeField]
    private float colorTicker; 
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        obstacles = new List<GameObject>();

        GameObject[] desks = GameObject.FindGameObjectsWithTag("Obstacle");
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall"); 

        foreach (GameObject desk in desks)
            obstacles.Add(desk); 
        foreach (GameObject wall in walls)
            obstacles.Add(wall); 
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            if (colorTicker >= 0.5f)
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                colorTicker = 0.0f;
            }

            if (gameObject.GetComponent<SpriteRenderer>().color == Color.red)
            {
                colorTicker += Time.deltaTime;
            }

            if (health <= 0)
            {
                Destroy(gameObject);
            }

            ultimateForce += ObstacleAvoidance();
            GenerateFriction(frictCoeff);
            base.Update();
        }
    }

    public override void CalcSteeringForces()
    {
        if (pathNodes != null && pathNodes.Count != 0)
        {
            ultimateForce += Seek(pathNodes[currentPathNode]);

            if ((transform.position - pathNodes[currentPathNode].transform.position).sqrMagnitude < 9) currentPathNode++;

            if (currentPathNode == pathNodes.Count) currentPathNode = 0;
        }
    }

    public override Vector3 Separation()
    {
        return Vector3.zero;
    }
}
