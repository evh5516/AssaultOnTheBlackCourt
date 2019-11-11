using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour {

    #region Fields
    GameObject dresden;
    public List<GameObject> enemies = new List<GameObject>();
    public GameObject psg;

    public Terrain terrain;

    public bool showDebugLines;

    public GameObject[] obstacles;
    #endregion

    // Use this for initialization
    void Start ()
    {
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

        //CreateObjects();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //MovePSG();

        if (Input.GetKeyUp("d"))
        {
            showDebugLines = !showDebugLines;
            
            dresden.GetComponent<Vehicle>().showDebugLines = showDebugLines;

            for(int i = 0; i < enemies.Count; i++)
            {
                enemies[i].GetComponent<Vehicle>().showDebugLines = showDebugLines;
            }
        }
    }

    /// <summary>
    /// Initially instantiates all of the objects in the scene
    /// and sets their values that need to be set
    /// </summary>
    //public void CreateObjects()
    //{
    //    psg = Instantiate(psgPrefab, CreateRandomPosition(psgPrefab), Quaternion.identity);
    //    enemies.Add(Instantiate(zombiePrefab, CreateRandomPosition(zombiePrefab), Quaternion.identity));

    //    zombies[0].GetComponent<Zombie>().humans = humans;
    //    zombies[0].GetComponent<Zombie>().zombies = zombies;
    //    zombies[0].GetComponent<Zombie>().showDebugLines = showDebugLines;
    //    zombies[0].GetComponent<Zombie>().obstacles = obstacles;

    //    for (int i = 0; i < numberOfHumans; i++)
    //    {
    //        humans[i].GetComponent<Human>().humans = humans;
    //        humans[i].GetComponent<Human>().showDebugLines = showDebugLines;
    //    }
    //}

    /// <summary>
    /// Creates a random position 
    /// </summary>
    /// <param name="prefab">The prefab to be used to retrieve local scale to set the y position</param>
    /// <returns>A vector with a random x and z position</returns>
    public Vector3 CreateRandomPosition(GameObject prefab)
    {
        Vector3 random = new Vector3(Random.Range(5f, terrain.terrainData.size.x - 5), prefab.transform.localScale.y/2, Random.Range(5f, terrain.terrainData.size.z - 5));
        return random;
    }

    /// <summary>
    /// Moves the PSG to a random position when the humans get close to it
    /// </summary>
    //public void MovePSG()
    //{
    //    for (int i = 0; i < humans.Count; i++)
    //    {
    //        if ((psg.transform.position - humans[i].transform.position).sqrMagnitude < 1)
    //        {
    //            psg.transform.position = CreateRandomPosition(psgPrefab);
    //        }
    //    }
    //}

    /// <summary>
    /// Displays whether Debug lines are currently shown
    /// and how to turn them on and off
    /// </summary>
    public void OnGUI()
    {
        GUI.Box(new Rect(0, 0, 100, 40), "Show Debug \nLines:" + showDebugLines.ToString());
        GUI.Box(new Rect(0, 50, 130, 55), "Press the 'D' key\n to turn Debug Lines\n On and Off");
    }
}
