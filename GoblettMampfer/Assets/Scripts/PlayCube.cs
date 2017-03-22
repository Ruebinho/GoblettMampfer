using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCube : MonoBehaviour {

    public int thisCubesSize = 0;
    public bool isActivated = false;

    private Material cubeMaterial = null;
    private Color cubeColorStart;
    private Color cubeColorWhileActivated = Color.red;

    private void Awake()
    {
        cubeMaterial = GetComponent<MeshRenderer>().material;
        cubeColorStart = GetComponent<MeshRenderer>().material.color;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CycleColor()
    {
        Debug.Log("CycleColor() : " + name);
    }

    public void ActivateCube()
    {
        cubeMaterial.color = cubeColorWhileActivated;
        isActivated = true;
    }

    public void DeactivateCube()
    {
        cubeMaterial.color = cubeColorStart;
        isActivated = false;
    }
}
