using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTurnHandler : MonoBehaviour
{
    #region variables

    #region gameTurnVariables
    public int thisPlayersTurn = 1;
    public int totalPlayers = 2;
    public int totalTurns = 1;
    public GameObject gameArea;
    public bool gameIsWon;
    #endregion

    #region spielfeldVariables
    private FeldBehaviour feldObenLinks;
    private FeldBehaviour feldObenMitte;
    private FeldBehaviour feldObenRechts;
    private FeldBehaviour feldMitteLinks;
    private FeldBehaviour feldMitteMitte;
    private FeldBehaviour feldMitteRechts;
    private FeldBehaviour feldUntenLinks;
    private FeldBehaviour feldUntenMitte;
    private FeldBehaviour feldUntenRechts;
    #endregion

    #region spielfeldTagVariables
    private string tagOL;
    private string tagOM;
    private string tagOR;
    private string tagML;
    private string tagMM;
    private string tagMR;
    private string tagUL;
    private string tagUM;
    private string tagUR;
    #endregion

    #region winCanvasVariables
    private Canvas winCanvas;
    private Text winText;
    #endregion

    private FeldBehaviour[] winningFields = new FeldBehaviour[3];

    #endregion

    // Use this for initialization
    void Start()
    {
        winCanvas = GameObject.Find("WinCanvas").GetComponent<Canvas>();
        RestartGame();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MakeCubesForPlayersTurnClickable()
    {
        foreach (GameObject playcube in GameObject.FindGameObjectsWithTag("Player" + thisPlayersTurn))
        {
            playcube.GetComponent<PlayCube>().isClickable = true;
        }
    }

    public void MakeCubesUnclickableAfterTurn()
    {
        foreach (GameObject playcube in GameObject.FindGameObjectsWithTag("Player" + thisPlayersTurn))
        {
            playcube.GetComponent<PlayCube>().isClickable = false;
        }
    }

    public void MakeAllCubesUnclickableAfterWin()
    {
        foreach (PlayCube playcube in FindObjectsOfType<PlayCube>())
        {
            playcube.GetComponent<PlayCube>().isClickable = false;
        }
    }

    public void NextTurn()
    {
        if (!CheckWinningConditions())
        {
            if (thisPlayersTurn < totalPlayers)
            {
                MakeCubesUnclickableAfterTurn();
                totalTurns++;
                thisPlayersTurn++;
                MakeCubesForPlayersTurnClickable();
            }
            else
            {
                MakeCubesUnclickableAfterTurn();
                totalTurns++;
                thisPlayersTurn = 1;
                MakeCubesForPlayersTurnClickable();
            }
        }
        else
        {
            Debug.Log("Player " + thisPlayersTurn + " Won!");
        }

    }

    public bool CheckWinningConditions()
    {
        if (CheckWinningPatterns())
        {
            gameIsWon = true;
            MakeAllCubesUnclickableAfterWin();

            ShowWinCanvas();

            return gameIsWon;
        }
        else
        {
            return gameIsWon;
        }

    }

    public bool CheckWinningPatterns()
    {
        getCurrentFields();
        getPlayerTagsFromFields();

        //If a certain pattern is filled with cubes from one player only he wins
        if (tagOL == tagOM && tagOL == tagOR)
        {
            setWinningPattern(feldObenLinks, feldObenMitte, feldObenRechts);
            return true;
        }
        else if (tagML == tagMM && tagML == tagMR)
        {
            setWinningPattern(feldMitteLinks, feldMitteMitte, feldMitteRechts);
            return true;
        }
        else if (tagUL == tagUM && tagUL == tagUR)
        {
            setWinningPattern(feldUntenLinks, feldUntenMitte, feldUntenRechts);
            return true;
        }
        else if (tagOL == tagML && tagOL == tagUL)
        {
            setWinningPattern(feldObenLinks, feldMitteLinks, feldUntenLinks);
            return true;
        }
        else if (tagOM == tagMM && tagOM == tagUM)
        {
            setWinningPattern(feldObenMitte, feldMitteMitte, feldUntenMitte);
            return true;
        }
        else if (tagOR == tagMR && tagOR == tagUR)
        {
            setWinningPattern(feldObenRechts, feldMitteRechts, feldUntenRechts);
            return true;
        }
        else if (tagOL == tagMM && tagOL == tagUR)
        {
            setWinningPattern(feldObenLinks, feldMitteMitte, feldUntenRechts);
            return true;
        }
        else if (tagOR == tagMM && tagOR == tagUL)
        {
            setWinningPattern(feldObenRechts, feldMitteMitte, feldUntenLinks);
            return true;
        }

        else
        {
            return false;
        }
    }

    public string CheckTopPlaycubePlayerTagOnField(FeldBehaviour feld)
    {
        Debug.Log(feld);
        PlayCube topCubeOnField = feld.playcubesOnField[feld.playcubesOnField.Count - 1];
        Debug.Log(topCubeOnField);

        string playerTag = topCubeOnField.GetComponent<Transform>().tag;

        return playerTag;


    }

    public void getCurrentFields()
    {
        feldObenLinks = GameObject.Find("Feld_ObenLinks").GetComponent<FeldBehaviour>();
        feldObenMitte = GameObject.Find("Feld_ObenMitte").GetComponent<FeldBehaviour>();
        feldObenRechts = GameObject.Find("Feld_ObenRechts").GetComponent<FeldBehaviour>();
        feldMitteLinks = GameObject.Find("Feld_MitteLinks").GetComponent<FeldBehaviour>();
        feldMitteMitte = GameObject.Find("Feld_MitteMitte").GetComponent<FeldBehaviour>();
        feldMitteRechts = GameObject.Find("Feld_MitteRechts").GetComponent<FeldBehaviour>();
        feldUntenLinks = GameObject.Find("Feld_UntenLinks").GetComponent<FeldBehaviour>();
        feldUntenMitte = GameObject.Find("Feld_UntenMitte").GetComponent<FeldBehaviour>();
        feldUntenRechts = GameObject.Find("Feld_UntenRechts").GetComponent<FeldBehaviour>();
    }

    public void getPlayerTagsFromFields()
    {
        if (feldObenLinks.GetComponent<FeldBehaviour>().Listlength > 0)
        {
            tagOL = CheckTopPlaycubePlayerTagOnField(feldObenLinks);
        }
        else
        {
            tagOL = "0";
        }

        if (feldObenMitte.GetComponent<FeldBehaviour>().Listlength > 0)
        {
            tagOM = CheckTopPlaycubePlayerTagOnField(feldObenMitte);
        }
        else
        {
            tagOM = "1";
        }

        if (feldObenRechts.GetComponent<FeldBehaviour>().Listlength > 0)
        {
            tagOR = CheckTopPlaycubePlayerTagOnField(feldObenRechts);
        }
        else
        {
            tagOR = "2";
        }

        if (feldMitteLinks.GetComponent<FeldBehaviour>().Listlength > 0)
        {
            tagML = CheckTopPlaycubePlayerTagOnField(feldMitteLinks);
        }
        else
        {
            tagML = "3";
        }

        if (feldMitteMitte.GetComponent<FeldBehaviour>().Listlength > 0)
        {
            tagMM = CheckTopPlaycubePlayerTagOnField(feldMitteMitte);
        }
        else
        {
            tagMM = "4";
        }

        if (feldMitteRechts.GetComponent<FeldBehaviour>().Listlength > 0)
        {
            tagMR = CheckTopPlaycubePlayerTagOnField(feldMitteRechts);
        }
        else
        {
            tagMR = "5";
        }

        if (feldUntenLinks.GetComponent<FeldBehaviour>().Listlength > 0)
        {
            tagUL = CheckTopPlaycubePlayerTagOnField(feldUntenLinks);
        }
        else
        {
            tagUL = "6";
        }

        if (feldUntenMitte.GetComponent<FeldBehaviour>().Listlength > 0)
        {
            tagUM = CheckTopPlaycubePlayerTagOnField(feldUntenMitte);
        }
        else
        {
            tagUM = "7";
        }

        if (feldUntenRechts.GetComponent<FeldBehaviour>().Listlength > 0)
        {
            tagUR = CheckTopPlaycubePlayerTagOnField(feldUntenRechts);
        }
        else
        {
            tagUR = "8";
        }
    }

    public void setWinningPattern(FeldBehaviour field1, FeldBehaviour field2, FeldBehaviour field3)
    {
        winningFields[0] = field1;
        winningFields[1] = field2;
        winningFields[2] = field3;

        foreach (FeldBehaviour feld in winningFields)
        {
            feld.ChangeFieldColor(Color.green);
            feld.playcubesOnField[feld.playcubesOnField.Count - 1].GetComponent<PlayCube>().ChangeCubeColorWhenWon();
        }

    }

    public void ShowWinCanvas()
    {
        winCanvas.GetComponentInChildren<Text>().text = "Player " + thisPlayersTurn + " gewinnt!!!";
        winCanvas.enabled = true;
    }

    public void RestartGame()
    {
        winCanvas.enabled = false;

        thisPlayersTurn = 1;
        totalTurns = 1;

        foreach (PlayCube playcube in FindObjectsOfType<PlayCube>())
        {
            playcube.ReturnCubeToInitialPosition();
            playcube.DeactivateCube();
        }

        foreach (FeldBehaviour spielfeld in FindObjectsOfType<FeldBehaviour>())
        {
            spielfeld.ResetField();
        }

        MakeCubesForPlayersTurnClickable();
    }
}
