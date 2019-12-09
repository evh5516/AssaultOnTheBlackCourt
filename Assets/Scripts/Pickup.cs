using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public delegate void PickupEffectDelegate();
        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Dresden")
        {
            GameObject dresden = collision.gameObject;
            dresden.GetComponent<Dresden>().activePickups.Add(this); 
            Destroy(gameObject); 
        }
    }
}
