using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour {

    #region Fields
    GameObject dresden;
    public List<GameObject> enemies = new List<GameObject>();

    public CollisionDetection collisionDetection;
    public GameObject agentManager;

    public bool showDebugLines;
    #endregion

    // Use this for initialization
    void Start ()
    {
        dresden = agentManager.GetComponent<AgentManager>().dresden;
        enemies = agentManager.GetComponent<AgentManager>().enemies;

        collisionDetection = gameObject.GetComponent<CollisionDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        //runs through the list of humans and zombies
        //and applies the AABB collision method to all of them 
        //to check if they're colliding
        for (int j = 0; j < enemies.Count; j++)
        {
            bool colliding = collisionDetection.AABBCollision(enemies[j], dresden);
            dresden.GetComponent<MeshInfo>().collidingBools.Add(colliding);
        }

        //Collision resolution

        ////saves the number of zombies before the change
        ////to eliminate calls to indices in arrays that don't exist
        //int enemyCountBeforeChange = enemies.Count; 

        ////Run through the list of humans and the number of zombies before anything has been changed
        //for (int j = 0; j < enemyCountBeforeChange; j++)
        //{
        //    //if the human was colliding with a zombie
        //    //turn it into a zombie and then break to the next iteration of the loop
        //    if (dresden.GetComponent<MeshInfo>().collidingBools[j])
        //    {
        //        //GameObject newZombie = Instantiate(zombiePrefab, humans[i].transform.position, humans[i].transform.rotation);
        //        //newZombie.transform.position = new Vector3(newZombie.transform.position.x, newZombie.transform.localScale.y / 2, newZombie.transform.position.z);
        //        //newZombie.GetComponent<Zombie>().humans = humans;
        //        //zombies.Add(newZombie);
        //        //newZombie.GetComponent<Zombie>().zombies = zombies;
        //        //newZombie.GetComponent<Zombie>().obstacles = humans[i].GetComponent<Human>().obstacles;
        //        //newZombie.GetComponent<Zombie>().showDebugLines = agentManager.GetComponent<AgentManager>().showDebugLines;
        //        //GameObject infectedHuman = humans[i];
        //        //humans.RemoveAt(i);
        //        //Destroy(infectedHuman);
        //        //i--;
        //        //break;
        //    }
        //}
    }
}
