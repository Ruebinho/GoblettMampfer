using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    #region inputHandlerVariables

    PlayCube activatedCube;
    bool cubeActivated = false;

    private FeldBehaviour selectedFeld;

    private GameTurnHandler gameTurnHandler;

    #endregion

    // Use this for initialization
    void Start()
    {
        gameTurnHandler = GameObject.Find("GameManager").GetComponent<GameTurnHandler>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!cubeActivated)
        {
            MausklickCubeSelection();
        }
        else
        {
            MausclickCubePlacement();
        }

    }

    private void MausclickCubePlacement()
    {
        if (Input.GetMouseButtonDown(0) && cubeActivated)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitRaycast = new RaycastHit();



            if (Physics.Raycast(ray, out hitRaycast, Camera.main.farClipPlane))
            {

                selectedFeld = hitRaycast.transform.GetComponent<FeldBehaviour>();
                //Debug.Log(selectedFeld.transform.position);
                if (selectedFeld != null)
                {
                    if (PlaceCubeOnField(activatedCube))
                    {
                        DeactivateCube();
                        gameTurnHandler.NextTurn();
                    } else
                    {
                        
                    }

                }
                else
                {
                    MausklickCubeSelection();
                }

            }
        }
    }

    private bool PlaceCubeOnField(PlayCube activatedCube)
    {
        //Debug.Log(activatedCube);
        //Debug.Log(activatedCube.transform.position);
        //Debug.Log(selectedFeld.transform.position);
        if (selectedFeld.TakeCubeIntoFieldArray(activatedCube))
        {
            activatedCube.transform.position = selectedFeld.transform.position;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void MausklickCubeSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitRaycast = new RaycastHit();



            if (Physics.Raycast(ray, out hitRaycast, Camera.main.farClipPlane))
            {
                PlayCube playCube = hitRaycast.transform.GetComponent<PlayCube>();
                //Debug.Log(playCube);
                if (playCube != null && playCube.isClickable)
                {
                    CubeSelection(playCube);
                }

            }
        }
    }

    private void CubeSelection(PlayCube playCube)
    {   // When playcube isn't activated yet
        if (!playCube.isActivated)
        {   // Check if there is any activated cube
            if (activatedCube != null)
            {   // If so deactivate active cube
                DeactivateCube();
            }
            // Set selected cube to active cube and activate
            ActivateCube(playCube);
        }
        else
        {   // if selected cube is activated then deactivate
            DeactivateCube();
        }
    }

    private void ActivateCube(PlayCube playCube)
    {
        activatedCube = playCube;
        activatedCube.ActivateCube();
        cubeActivated = true;
    }

    private void DeactivateCube()
    {
        activatedCube.DeactivateCube();
        cubeActivated = false;
    }
}
