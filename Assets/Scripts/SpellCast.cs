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

    public GameObject dresden; 

    // Start is called before the first frame update
    void Start()
    {
        dresden = Camera.main.GetComponent<CollisionManager>().dresden; 
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
            GameObject spell = Instantiate(spellPrefabs[0], dresden.transform.position + dresden.GetComponent<Dresden>().velocity.normalized, dresden.transform.rotation);
            Camera.main.GetComponent<CollisionManager>().projectiles.Add(spell);

            Vector3 velocity = (new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0) - dresden.transform.position).normalized;
            velocity *= spell.GetComponent<Projectile>().Speed;

            if (Vector3.Dot(velocity, dresden.GetComponent<Dresden>().velocity) < 0)
            {
                dresden.GetComponent<Dresden>().velocity = -dresden.GetComponent<Dresden>().velocity;
            }

            spell.GetComponent<Projectile>().Velocity = velocity; 

            if (holdStrength > 0 && holdStrength < powerLimits[0])
            {
                spell.transform.localScale = new Vector3(1, 1, 1);

                spell.GetComponent<Projectile>().Damage = 25;
                holdStrength = 0;
            }
            else if (holdStrength > powerLimits[0] && holdStrength < powerLimits[1])
            {
                spell.transform.localScale = new Vector3(0.75f, 0.75f, 1);
                //Level 2

                spell.GetComponent<Projectile>().Damage = 75;
                holdStrength = 0;
            }
            else if (holdStrength > powerLimits[1])
            {
                spell.transform.localScale = new Vector3(0.5f, 0.5f, 1);

                //Level 3

                spell.GetComponent<Projectile>().Damage = 150;
                holdStrength = 0;
            }
        }
    }
}
