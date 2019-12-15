using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hellfire : Pickup
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
            spellDamages[i] *= 1.5f;
        }

        dresden.GetComponent<Dresden>().MAX_HEALTH /= 2;

        //changing particles
        ParticleSystem.ShapeModule shape = gameObject.GetComponentInChildren<ParticleSystem>().shape;
        shape.angle = 30.99963f;
        ParticleSystem.MainModule main = gameObject.GetComponentInChildren<ParticleSystem>().main;
        main.startSize = 0.1f;

        //setting parent
        gameObject.GetComponentInChildren<ParticleSystem>().gameObject.transform.parent = dresden.transform;

        //setting local position
        ParticleSystem[] pss = dresden.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem ps in pss)
        {
            ps.gameObject.transform.localPosition = new Vector3(-0.066f, -0.485f, 0);
        }
    }

    public override void ReverseEffect()
    {
        List<float> spellDamages = dresden.GetComponent<SpellCast>().damages;

        for (int i = 0; i < spellDamages.Count; i++)
        {
            spellDamages[i] /= 1.5f;
        }

        dresden.GetComponent<Dresden>().MAX_HEALTH *= 2;

        //setting local position
        ParticleSystem[] pss = dresden.GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < pss.Length; i++)
        {
            if (pss[i].gameObject.name == "HellfireParticles")
            {
                Destroy(pss[i].gameObject);
                return;
            }
        }
    }
}
