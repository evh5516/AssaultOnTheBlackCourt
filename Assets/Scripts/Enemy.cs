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
    [SerializeField]
    private bool ranged; 
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //health = 100; 
    }

    // Update is called once per frame
    void Update()
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

        if (ranged)
        {
            CastSpell(); 
        }
    }

    public override void CalcSteeringForces()
    {
        Pursuit(pathNodes[currentPathNode]);

        if ((transform.position - pathNodes[currentPathNode].transform.position).sqrMagnitude < 1) currentPathNode++;

        if (currentPathNode == pathNodes.Count) currentPathNode = 0; 
    }

    public override Vector3 Separation()
    {
        return Vector3.zero;
    }

    public void CastSpell()
    {

    }
}
