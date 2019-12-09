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
    #endregion

    #region Properties
    public float DresdenHealth
    {
        get { return dresdenHealth; }
        set { dresdenHealth = value; }
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
