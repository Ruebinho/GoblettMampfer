using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCube : MonoBehaviour
{

    public int thisCubesSize = 0;
    public Vector3 initialCubePosition;

    public bool isActivated = false;
    public bool isClickable = false;
    public GameObject CubePlacedOnField;

    private Material cubeMaterial = null;
    private Color cubeColorStart;
    private Color cubeColorWhileActivated = Color.red;
    private Color cubeColorWhenWon = Color.green;

    private void Awake()
    {
        cubeMaterial = GetComponent<MeshRenderer>().material;
        if (tag.Equals("Player1"))
        {
            cubeMaterial.color = Color.yellow;
        }
        else if (tag.Equals("Player2"))
        {
            cubeMaterial.color = Color.blue;
        }
        cubeColorStart = GetComponent<MeshRenderer>().material.color;
    }

    // Use this for initialization
    void Start()
    {
        initialCubePosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

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

    public void setPlacedField(GameObject feld)
    {
        CubePlacedOnField = feld;
    }

    public void ChangeCubeColorWhenWon()
    {
        cubeMaterial.color = cubeColorWhenWon;
    }

    public void ReturnCubeToInitialPosition()
    {
        this.transform.position = initialCubePosition;
    }
}
