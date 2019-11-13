using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCast : MonoBehaviour
{
    [SerializeField]
    private float holdStrength;

    public List<int> powerLimits;
    // Start is called before the first frame update
    void Start()
    {
        powerLimits = new List<int>(3);
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
            if (holdStrength > 0 && holdStrength < powerLimits[1])
            {
                //Level 1
            }
            else if (holdStrength > powerLimits[1] && holdStrength < powerLimits[2])
            {
                //Level 2
            }
            else if (holdStrength > powerLimits[3])
            {
                //Level 
            }
        }
    }
}
