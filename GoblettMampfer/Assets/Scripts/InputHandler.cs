using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {

    PlayCube activatedCube;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitRaycast = new RaycastHit();

            

            if (Physics.Raycast(ray, out hitRaycast, Camera.main.farClipPlane))
            {
                PlayCube playCube = hitRaycast.transform.GetComponent<PlayCube>();

                if (playCube != null)
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
                activatedCube.DeactivateCube();
            }
            // Set selected cube to active cube and activate
            activatedCube = playCube;
            activatedCube.ActivateCube();
        }
        else
        {   // if selected cube is activated then deactivate
            activatedCube.DeactivateCube();
        }
    }
}
