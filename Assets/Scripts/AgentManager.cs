﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour {

    #region Fields
    public GameObject dresden;
    public List<GameObject> enemies = new List<GameObject>();

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
        //if (Input.GetKeyUp("d"))
        //{
        //    showDebugLines = !showDebugLines;
            
        //    dresden.GetComponent<Vehicle>().showDebugLines = showDebugLines;

        //    for(int i = 0; i < enemies.Count; i++)
        //    {
        //        enemies[i].GetComponent<Vehicle>().showDebugLines = showDebugLines;
        //    }
        //}
    }

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
