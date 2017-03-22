using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FeldBehaviour : MonoBehaviour {

    private Material feldMaterial = null;
    private Color feldMaterialColorStart;
    private List<PlayCube> playcubesOnField;
    private int currentArrayPosition = 0;
    PlayCube lastCube;

    private void Awake()
    {
        feldMaterial = GetComponent<MeshRenderer>().material;
        feldMaterialColorStart = GetComponent<MeshRenderer>().material.color;
        playcubesOnField = new List<PlayCube>();
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

    public void TakeCubeIntoFieldArray(PlayCube playcube)
    {
        if(playcubesOnField.Count <= 2)
        {
            if(CompareToPlacedCubeSizeBigger(playcube))
            {
                playcubesOnField.Add(playcube);
            }
        }
    }

    private bool CompareToPlacedCubeSizeBigger(PlayCube playcube)
    {
        int Listlenght = playcubesOnField.Count();
        
        if (Listlenght >= 1)
        {
        lastCube = playcubesOnField.ElementAt(Listlenght).GetComponent<PlayCube>();
        }

        if (lastCube != null)
        {
            if (lastCube.thisCubesSize < playcube.thisCubesSize)
            {
                return true;
            }
            else
            {
                return false;
            }
        } else
        {
            return true;
        }
        
    }


}
