using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeldBehaviour : MonoBehaviour {

    private Material feldMaterial = null;
    private Color feldMaterialColorStart;

    private void Awake()
    {
        feldMaterial = GetComponent<MeshRenderer>().material;
        feldMaterialColorStart = GetComponent<MeshRenderer>().material.color;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseOver()
    {
        feldMaterial.color = Color.green;
    }

    private void OnMouseExit()
    {
        feldMaterial.color = feldMaterialColorStart;
    }


}
