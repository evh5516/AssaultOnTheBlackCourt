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
    private bool inAttackRange;
    private GameObject dresden;
    private float attackRange;
    private float distance;
#endregion

// Start is called before the first frame update
void Start()
    {
        attackRange = 1.5f;
        health = 100;
        obstacles = new List<GameObject>();

        GameObject[] desks = GameObject.FindGameObjectsWithTag("Obstacle");
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall"); 

        foreach (GameObject desk in desks)
            obstacles.Add(desk); 
        foreach (GameObject wall in walls)
            obstacles.Add(wall);

        dresden = GameObject.FindWithTag("Player");

        triggeredObstacles = new List<int>(); 
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
                Camera.main.GetComponent<UIManager>().enemies.Remove(gameObject);
                Destroy(gameObject);
            }

            distance = Vector3.Distance(transform.position, dresden.transform.position);

            if (distance < attackRange)
            {
                dresden.GetComponent<Dresden>().Health -= 25;
                Debug.Log("Damage Dealt");
            }

            //triggeredObstacles.Clear(); 
            ultimateForce = Vector3.zero;

            ultimateForce += ObstacleAvoidance() * 2;

            //Debug.Log(ultimateForce); 
            //GenerateFriction(frictCoeff);
            base.Update();
        }
    }

    public override void CalcSteeringForces()
    {

        if (pathNodes != null && pathNodes.Count != 0)
        {
            if ((transform.position - dresden.transform.position).sqrMagnitude < 4)
            {
                ultimateForce += Pursuit(dresden) * 4; 
            }
            else
            {
                ultimateForce += Seek(pathNodes[currentPathNode]) * 2;

                if ((transform.position - pathNodes[currentPathNode].transform.position).sqrMagnitude < 1) currentPathNode++;

                if (currentPathNode == pathNodes.Count) currentPathNode = 0;
            }
        }
    }

    public override Vector3 Separation()
    {
        return Vector3.zero;
    }

    
}
