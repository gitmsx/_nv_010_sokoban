using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameStart : MonoBehaviour
{

     private Text Text__info003;
    [HideInInspector] private Text Text__info001;
    [HideInInspector] private Text Text__info002;
    public int Criterii_win2 = 4;

    private float TimeToCheckCircle = 2;
    private float TimeToCheck;
    private int CurrentLevel = 1;



    private List<GameObject> listPoints = new List<GameObject>();
    public GameObject PF_chess1;
    public GameObject PF_chess2;

    public GameObject PlaceToBox;


    float cellSize = _global.Global_Scale;





    void Start()
    {
        float scale_pf = _global.Global_Scale;
        GameObject[] ChessTmp = new GameObject[2];

        Text__info001 = GameObject.Find("Text__info001").GetComponent<Text>();
        Text__info002 = GameObject.Find("Text__info002").GetComponent<Text>();
        Text__info003 = GameObject.Find("Text__info003").GetComponent<Text>();
        Text__info003.text = "Text__info003";
        CurrentLevel = 1;
        LevelStart(CurrentLevel);

    }



    void LevelStart(int Lvl)
    {
        GameObject _handl = GameObject.Find("_GameStart");
        _handl.AddComponent<ReadMaps>();
        _handl.GetComponent<ReadMaps>().Start1(Lvl);
    }


    void Update()
    {
        TimeToCheck += Time.deltaTime;
        if (TimeToCheckCircle < TimeToCheck)
        {
            TimeToCheck = 0;
            if (CheckWin()) NewLevel();
        }
    }



    void DestroyByTag(string Tagg)
    {
        GameObject[] object3 = GameObject.FindGameObjectsWithTag(Tagg);
        foreach (GameObject objectTM in object3)
            Destroy(objectTM);




    }

    void NewLevel()
    {
        DestroyByTag("Wall");
        DestroyByTag("Box");
        DestroyByTag("Player");
        DestroyByTag("Target");

        LevelStart(++CurrentLevel);
    }




    bool CheckWin()
    {
        int all_Targets = 0;
        int all_Cheked = 0;
        int layerMask = 1 << 8;

        Text__info001.text = "";
        GameObject[] object2 = GameObject.FindGameObjectsWithTag("Target");


        if (_global.Cheat77 == true) { _global.Cheat77 = false; return true; }



        foreach (GameObject gameObject in object2)
        {
            all_Targets++;
            Vector3 TP = gameObject.transform.position;
            TP = new Vector3(TP.x, TP.y + 1, TP.z);


            if (Physics.Raycast(TP, -Vector3.up, out RaycastHit hit, cellSize * 2.4f, layerMask))
                all_Cheked++;
        }

        Text__info002.text = "  Remains boxes " + (all_Targets - all_Cheked).ToString();
        if (all_Targets - all_Cheked <= Criterii_win2) return true;
        return false;
    }





}
