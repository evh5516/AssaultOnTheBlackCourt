using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    // Start is called before the first frame update
    #region Fields
    [SerializeField]
    private float dresdenHealth;
    private int score;
    private Queue<(Pickup, GameObject)> activePickups = new Queue<(Pickup, GameObject)>(); 
    #endregion

    #region Properties
    public float DresdenHealth
    {
        get { return dresdenHealth; }
        set { dresdenHealth = value; }
    }
    public Queue<(Pickup, GameObject)> ActivePickups
    {
        get { return activePickups; }
        set { activePickups = value; }
    }
    public int Score
    {
        get { return score; }
        set { score = value; }
    }
    #endregion
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
