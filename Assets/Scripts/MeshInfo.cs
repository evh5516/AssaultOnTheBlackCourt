using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshInfo : MonoBehaviour {

    #region Fields
    public GameObject obj;
    public MeshRenderer meshRenderer;
    public Vector3 center;
    public Vector3 min;
    public Vector3 max;
    public Vector3 size;
    public Vector3 extents;

    public float radius;

    public List<bool> collidingBools = new List<bool>();
    #endregion

    // Use this for initialization
    void Start ()
    {
        if (obj == null)
        {
            obj = gameObject;
        }
        meshRenderer = obj.GetComponent<MeshRenderer>();
        center = meshRenderer.bounds.center;
        min = meshRenderer.bounds.min;
        max = meshRenderer.bounds.max;
        size = meshRenderer.bounds.size;
        extents = meshRenderer.bounds.extents;
    }
	
	// Update is called once per frame
	void Update ()
    {
        center = meshRenderer.bounds.center;
        min = meshRenderer.bounds.min;
        max = meshRenderer.bounds.max;
        collidingBools.Clear();
    }
}
