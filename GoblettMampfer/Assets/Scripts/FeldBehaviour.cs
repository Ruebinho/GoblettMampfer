using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class FeldBehaviour : MonoBehaviour
{

    private Material feldMaterial = null;
    private Color feldMaterialColorStart;
    public List<PlayCube> playcubesOnField;
    public int Listlength = 0;

    private PlayCube lastCube;
    private GameTurnHandler gameTurnHandler;

    private void Awake()
    {
        feldMaterial = GetComponent<MeshRenderer>().material;
        feldMaterialColorStart = GetComponent<MeshRenderer>().material.color;
        playcubesOnField = new List<PlayCube>();
    }

    // Use this for initialization
    void Start()
    {
        gameTurnHandler = GameObject.Find("GameManager").GetComponent<GameTurnHandler>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseOver()
    {
        if (!gameTurnHandler.gameIsWon)
        {
        ChangeFieldColor(Color.white);
        }
    }

    private void OnMouseExit()
    {
        if (!gameTurnHandler.gameIsWon)
        {
            ChangeFieldColor(feldMaterialColorStart);
        }
    }

    public bool TakeCubeIntoFieldArray(PlayCube playcube)
    {
        if (playcubesOnField.Count <= 2)
        {
            Debug.Log(playcubesOnField);

            if (playcube.CubePlacedOnField.GetInstanceID() != this.gameObject.GetInstanceID())
            {
                if (CompareToPlacedCubeSizeBigger(playcube))
                {
                    //remove cube from old field list
                    LastCubeRemoved(playcube);

                    //put cube on new field list
                    Listlength++;
                    playcubesOnField.Add(playcube);
                    playcube.setPlacedField(this.gameObject);
                    Debug.Log("Placemnt worked");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                playcubesOnField.Add(playcube);
                return true;
            }
        }
        else
        {
            return false;
        }
    }

    private bool CompareToPlacedCubeSizeBigger(PlayCube playcube)
    {
        Debug.Log(Listlength);

        if (Listlength >= 1)
        {
            lastCube = playcubesOnField.ElementAt(Listlength - 1).GetComponent<PlayCube>();
            if (lastCube != null)
            {
                if (lastCube.thisCubesSize < playcube.thisCubesSize)
                {
                    return true;
                }
                else
                {
                    FlashFieldForWrongMove();
                    return false;
                }
            }
            else
            {
                return true;
            }

        }
        else
        {
            return true;
        }
    }

    public void ChangeFieldColor(Color color)
    {
        feldMaterial.color = color;
    }

    public void ChangeFieldColorToStartColor()
    {
        feldMaterial.color = feldMaterialColorStart;
    }

    private void FlashFieldForWrongMove()
    {
        ChangeFieldColor(Color.red);
        Invoke("ChangeFieldColorToStartColor", 1f);
    }

    public void LastCubeRemoved(PlayCube playcube)
    {
        if (playcube.CubePlacedOnField != null)
        {
            GameObject oldField = playcube.CubePlacedOnField;
            FeldBehaviour oldfeldscript = oldField.GetComponent<FeldBehaviour>();
            oldfeldscript.playcubesOnField.Remove(playcube);
            oldfeldscript.Listlength--;
        }
        else
        {
            playcubesOnField.Remove(playcube);
        }
    }

    public void ResetField()
    {
        ChangeFieldColorToStartColor();
        playcubesOnField = new List<PlayCube>();
        Listlength = 0;
    }
}
