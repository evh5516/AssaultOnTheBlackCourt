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
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void CalcSteeringForces()
    {
        Pursuit(pathNodes[currentPathNode]);

        if ((transform.position - pathNodes[currentPathNode].transform.position).sqrMagnitude < 9) currentPathNode++;

        if (currentPathNode == pathNodes.Count) currentPathNode = 0; 
    }

    public override Vector3 Separation()
    {
        return Vector3.zero;
    }
}
