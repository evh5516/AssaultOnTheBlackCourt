using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(12, 16); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        try
        {
            GameObject.Find("DataManager").GetComponent<DataManager>().DresdenHealth = collision.gameObject.GetComponent<Dresden>().Health;
        }
        catch
        {
            GameObject.Find("DataManager(Clone)").GetComponent<DataManager>().DresdenHealth = collision.gameObject.GetComponent<Dresden>().Health;
        }
        Camera.main.GetComponent<UIManager>().LoadNextLevel();
    }
}
