using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCast : MonoBehaviour
{
    [SerializeField]
    private float holdStrength;

    public List<float> powerLimits;

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

            Vector3 velocity = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.GetComponent<CollisionManager>().dresden.transform.position;
            velocity.Normalize();
            velocity *= spell.GetComponent<Projectile>().Speed;

            spell.GetComponent<Projectile>().Velocity = velocity; 

            if (holdStrength > 0 && holdStrength < powerLimits[0])
            {
                spell.transform.localScale = new Vector3(1, 1, 1);

                spell.GetComponent<Projectile>().Damage = 25;
                holdStrength = 0;
            }
            else if (holdStrength > powerLimits[0] && holdStrength < powerLimits[1])
            {
                spell.transform.localScale = new Vector3(2, 2, 1);
                //Level 2

                spell.GetComponent<Projectile>().Damage = 75;
                holdStrength = 0;
            }
            else if (holdStrength > powerLimits[1])
            {
                spell.transform.localScale = new Vector3(3, 3, 1);

                //Level 3

                spell.GetComponent<Projectile>().Damage = 150;
                holdStrength = 0;
            }
        }
    }
}
