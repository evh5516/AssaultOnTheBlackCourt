using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    private bool inAttackRange;
    public GameObject dresden;
    public float attackTimer;

    // Start is called before the first frame update
    void Start()
    {     
    }

    // Update is called once per frame
    void Update()
    {

        if (inAttackRange == true)
        {
            if (attackTimer < 1)
            {
                attackTimer += Time.deltaTime;
            }
            else if (attackTimer >= 1)
            {
                dresden.GetComponent<Dresden>().Health -= 25;
                attackTimer = 0;
            }
        }
    }

    public void OnTriggerEnter(Collider obj)
    {
        if (obj.tag == "Player")
        {
            inAttackRange = true;
        }
    }
}
