using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class MotionController : MonoBehaviour
{

    int new_direction = 0;
    int current_direktion = 0;
    private Vector3 newRotation;
    bool isMoving = false;
    public float speed = 12f;
    Vector3 destPos; //позиция куда двигаемся
    AudioSource AudioSource1;
    [HideInInspector] private Text Text__info003;
    [HideInInspector] private Text Text__info001;
    [HideInInspector] private Text Text__info002;

    public Transform Pointer;  // collide with Ray

    private Vector3[] DirectionM = new Vector3[4];


    float cellSize = _global.Global_Scale;


    private void Start()
    {
        //AudioSource1 = GetComponent<AudioSource>();

        Text__info001 = GameObject.Find("Text__info001").GetComponent<Text>();
        Text__info002 = GameObject.Find("Text__info002").GetComponent<Text>();
        Text__info003 = GameObject.Find("Text__info003").GetComponent<Text>();
        Text__info003.text = "Text__info003";

        DirectionM[0] = Vector3.forward;
        DirectionM[1] = Vector3.right;
        DirectionM[2] = -Vector3.forward;
        DirectionM[3] = -Vector3.right;





    }

    void Update()
    {

        Debug.DrawRay(transform.position, DirectionM[new_direction] * cellSize,Color.red);


        if (isMoving)
        {

            if (current_direktion != new_direction)
            {
                switch (new_direction)
                {
                    case 1:
                        newRotation = new Vector3(0, 90, 0);
                        break;
                    case 2:
                        newRotation = new Vector3(0, 180, 0);
                        break;
                    case 3:
                        newRotation = new Vector3(0, -90, 0);
                        break;
                    case 0:
                        newRotation = new Vector3(0, 0, 0);
                        break;
                    default:
                        newRotation = new Vector3(0, 0, 0);
                        break;
                }

                transform.eulerAngles = newRotation;

                //  transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, newRotation, Time.deltaTime/ speed_time_rotation);

            }


            destPos = transform.position + DirectionM[new_direction] * cellSize;




            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, destPos, step);//двигаем персонажа
            transform.position = destPos;
            if (transform.position == destPos) isMoving = false;



        }
        else
        {
            current_direktion = new_direction;

            if (Input.GetKeyDown(KeyCode.W)) { new_direction = 0; isMoving = true; }
            else if (Input.GetKeyDown(KeyCode.D)) { new_direction = 1; isMoving = true; }
            else if (Input.GetKeyDown(KeyCode.S)) { new_direction = 2; isMoving = true; }
            else if (Input.GetKeyDown(KeyCode.A)) { new_direction = 3; isMoving = true; }
            if (Input.GetKeyDown(KeyCode.F12)) { _global.Cheat77 = true; }
            
        }


        if (isMoving)

        {

            Ray ray = new Ray(transform.position, DirectionM[new_direction] * cellSize);


            if (Physics.Raycast(transform.position, DirectionM[new_direction], out RaycastHit hit, cellSize))

            {
                Text__info003.text = hit.collider.gameObject.tag;
                if (hit.collider.gameObject.tag == "Box")


                {
                    Text__info003.text = hit.transform.position.x.ToString() + " " + hit.collider.gameObject.tag;
                    Pointer.position = hit.point;
                    

                    var ObjToMove = hit.collider.gameObject;
                    if (TryMoveBox(ObjToMove))
                        MoveBox(ObjToMove);
                    else
                        isMoving = false;
                }

                else 
                {
                    // wall !!!! 
                    isMoving = false;
                }

            }




        }


       


    }

    bool TryMoveBox(GameObject ObjToMove)
    {
    
        if (Physics.Raycast(ObjToMove.transform.position, DirectionM[new_direction], out RaycastHit hit, cellSize))
            return false;
        else
            return true;
    }



    void MoveBox(GameObject  ObjToMove)
    {
        var destPos2 = ObjToMove.transform.position + DirectionM[new_direction] * cellSize;
        ObjToMove.transform.position = destPos2;
    }



}