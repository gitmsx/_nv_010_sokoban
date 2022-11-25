using System;
using UnityEngine;
using UnityEngine.UI;

public class MotionController : MonoBehaviour
{

    public float speed_time_rotation = 3;
    public float speed = 5.0f;
    public float cellSize = 2.0f;//размер ячейки, а также расстояни на которое нужно сдвинуться если была нажата кнопка
    bool isMoving = false;//находимся ли в движении
    GameObject Scr1;
    Vector3 direction;//направление движения
    Vector3 destPos;//позиция куда двигаемся


    [SerializeField] AudioClip MoveSound;
    [SerializeField] AudioClip WinSound;
    [SerializeField] AudioClip LoseSound;

    private AudioSource AudioSource1;


    public bool AudioIsEnabled = true;




    //    private bool isMoveEnd = false; // премещение закончено
    private bool KeyPressed = false; // новое движение
    private bool SoundStepPlayed = false; // новое движение

    private Rigidbody rb;
    int current_direktion = 0;
    int new_direktion = 0;
    private Vector3 newRotation;
  

    [HideInInspector] private Text Text__info003;
    [HideInInspector] private Text Text__info002;


    private void Start()
    {
        AudioSource1 = GetComponent<AudioSource>();

        Text__info002 = GameObject.Find("TextInfo2").GetComponent<Text>();
        Text__info003 = GameObject.Find("TextInfo3").GetComponent<Text>();

    }

    void Update()
    {


        if (isMoving && !SoundStepPlayed)
        {
            SoundStepPlayed = true;
            AudioSource1.PlayOneShot(MoveSound);

        }
        if (!isMoving && KeyPressed)
            {

            SoundStepPlayed = false;
            var trans2 = transform.rotation.y;

            var angle = Vector3.Angle(Vector3.forward, transform.forward);
            var SignedAngle = Vector3.SignedAngle(Vector3.forward, transform.forward, Vector3.up);


            var angleToEulerAngles = transform.rotation.ToEulerAngles();
            //  var angleToEulerAngles2 = transform.rotation.EulerAngles(transform.forward);


            // isMoveEnd = false;
            // rayCast1.raycast();
            KeyPressed = false;
            




            Text__info002.text = " rotation.y " + trans2.ToString() + " SignedAngle  " + SignedAngle.ToString() + " angle " + angle.ToString() + " Euler Angle " + angleToEulerAngles.ToString() + " Euler Angle y" + angleToEulerAngles.y.ToString();


            if (Mathf.Approximately(SignedAngle, 90))
            {
                Text__info003.text = " SignedAngle right " + SignedAngle.ToString();
            }
            else
                if (Mathf.Approximately(SignedAngle, -90))
            {
                Text__info003.text = " SignedAngle left " + SignedAngle.ToString();
            }
            else
                if (Mathf.Approximately(SignedAngle, 0))
            {
                Text__info003.text = " SignedAngle forvard " + SignedAngle.ToString();
            }
            if (Mathf.Approximately(Math.Abs( SignedAngle),  180))
            {
                Text__info003.text = " SignedAngle back  180.000 " + SignedAngle.ToString();
            }
            
        }




        if (isMoving)
        {
            KeyPressed = false;
            if (current_direktion != new_direktion)
            {
                switch (new_direktion)
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



            float step = speed * Time.deltaTime;//расстояние, которое нужно пройти в текущем кадре
            transform.position = Vector3.MoveTowards(transform.position, destPos, step);//двигаем персонажа
                                                                                        //достигли нужной позиции - отключаем движение, включаем ловлю нажатых клавиш
            if (transform.position == destPos) isMoving = false;

            KeyPressed = true;

        }
        else
        {


            current_direktion = new_direktion;
            if (Input.GetKeyDown(KeyCode.W))
            {
                //move up
                direction = Vector3.forward;
                destPos = transform.position + direction * cellSize;
                isMoving = true;
                new_direktion = 0;

            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                //move left
                direction = Vector3.left;
                destPos = transform.position + direction * cellSize;
                isMoving = true;
                new_direktion = 3;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                //move down
                direction = Vector3.back;
                destPos = transform.position + direction * cellSize;
                isMoving = true;
                new_direktion = 2;

            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                //move right
                direction = Vector3.right;
                destPos = transform.position + direction * cellSize;
                isMoving = true;
                new_direktion = 1;

            }
        }
    }
}