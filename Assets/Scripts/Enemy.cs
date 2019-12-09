using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private float attackTime;
#endregion

// Start is called before the first frame update
void Start()
    {
        gameObject.GetComponent<Animator>().SetBool("Moving", true); 
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
                if (attackTime < 1.0f)
                {
                    attackTime += Time.deltaTime;
                }
                else if (attackTime >= 1.0f)
                {

                    dresden.GetComponent<Dresden>().Health -= 25;
                    if (dresden.GetComponent<Dresden>().Health <= 0)
                    {
                        dresden.GetComponent<Dresden>().Health = 0;
                        SceneManager.LoadScene("Credits");
                        Destroy(dresden);
                        return;
                    }
                    attackTime = 0.0f;
                    Debug.Log("Damage Dealt");
                }
            }

            //triggeredObstacles.Clear(); 
            ultimateForce = Vector3.zero;

            ultimateForce += ObstacleAvoidance() * 2;

            //Debug.Log(ultimateForce); 
            //GenerateFriction(frictCoeff);
            base.Update();

            OrientSprite(); 
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

    private void OrientSprite()
    {
        bool flip = gameObject.GetComponentInChildren<SpriteRenderer>().flipX;
        float dot = Vector3.Dot(direction, transform.right);

        if (dot < 0) flip = true;
        else if (dot > 0) flip = false;

        gameObject.GetComponentInChildren<SpriteRenderer>().flipX = flip;
    }
}
