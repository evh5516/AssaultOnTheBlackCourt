using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private Vector3 velocity;
    [SerializeField]
    private float timer;

    [SerializeField]
    private int damage;
    #endregion

    #region Properties
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    public Vector3 Velocity
    {
        get { return velocity; }
        set { velocity = value; }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        timer = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        transform.position += velocity; 

        if (timer == 5)
        {
            DestroySpell(); 
        }
    }

    public void DestroySpell()
    {
        Camera.main.GetComponent<CollisionManager>().projectiles.Remove(gameObject);
        Destroy(this);
    }
}
