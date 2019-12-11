using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soulfire : Pickup
{
    private GameObject dresden; 

    // Start is called before the first frame update
    void Start()
    {
        dresden = GameObject.Find("Dresden");
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Effect()
    {
        List<float> spellDamages = dresden.GetComponent<SpellCast>().damages; 

        for (int i = 0; i < spellDamages.Count; i++)
        {
            spellDamages[i] += 50; 
        }

        dresden.GetComponent<SpellCast>().powerDraining += 1;
    }

    public override void ReverseEffect()
    {
        List<float> spellDamages = dresden.GetComponent<SpellCast>().damages;

        for (int i = 0; i < spellDamages.Count; i++)
        {
            spellDamages[i] -= 50;
        }

        dresden.GetComponent<SpellCast>().powerDraining -= 1;
    }
}
