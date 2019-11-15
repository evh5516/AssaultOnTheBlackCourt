using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCast : MonoBehaviour
{
    [SerializeField]
    private float holdStrength;

    public List<int> powerLimits;

    public GameObject[] spellPrefabs;
    public int currentSpellIndex; 

    // Start is called before the first frame update
    void Start()
    {
        //powerLimits = new List<int>(3);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            holdStrength += Time.deltaTime;
        }

        if(Input.GetMouseButtonUp(0))
        {
            GameObject spell = Instantiate(spellPrefabs[0], Camera.main.GetComponent<CollisionManager>().dresden.transform.position, Camera.main.GetComponent<CollisionManager>().dresden.transform.rotation);
            Camera.main.GetComponent<CollisionManager>().projectiles.Add(spell);

            spell.GetComponent<Projectile>().Velocity = Camera.main.GetComponent<CollisionManager>().dresden.GetComponent<Dresden>().velocity.normalized * 2; 

            if (holdStrength > 0 && holdStrength < powerLimits[1])
            {
                spell.transform.localScale = new Vector3(1, 1, 1);

                spell.GetComponent<Projectile>().Damage = 25; 
            }
            else if (holdStrength > powerLimits[1] && holdStrength < powerLimits[2])
            {
                spell.transform.localScale = new Vector3(2, 2, 1);
                //Level 2

                spell.GetComponent<Projectile>().Damage = 75;
            }
            else if (holdStrength > powerLimits[3])
            {
                spell.transform.localScale = new Vector3(3, 3, 1);

                //Level 3

                spell.GetComponent<Projectile>().Damage = 150;
            }
        }
    }
}
