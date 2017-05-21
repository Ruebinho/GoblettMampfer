using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    #region labelVariables

    Text labelP1;
    Text labelP2;

#endregion

    // Use this for initialization
    void Start () {
        labelP1 = GameObject.Find("Label_Player1").GetComponent<Text>();
        labelP2 = GameObject.Find("Label_Player2").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeTextColorForPlayerTurn(int thisPlayersTurn)
    {
        if (thisPlayersTurn == 1)
        {
            labelP2.color = Color.black;
            labelP1.color = Color.red;
        } else
        {
            labelP1.color = Color.black;
            labelP2.color = Color.red;
        }
    }
}
