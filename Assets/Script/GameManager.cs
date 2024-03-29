using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

[Serializable]
public class BallAreaTechnicalOperation
{
    public List<GameObject> Balls;
    public Animator BallAreaElevator;
    public TextMeshProUGUI CountText;
    public int GoalBall;
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject PickerObject;
    [SerializeField] private GameObject[] PickerPalletObject;
    bool PickerPalletActive;
    [SerializeField] private GameObject[] BonusBalls;
    [SerializeField] private GameObject BallControlObject;
    public bool pickerIsMove;
    [SerializeField] private GameObject[] FinalEfect;

    int CountOfBallThrown;

    int TotalCheckPoint;
    int CurentCheckPointIndex;

    float fingerPositionX;

    [SerializeField] private List<BallAreaTechnicalOperation> _BallAreaTechnicalOperation = new List<BallAreaTechnicalOperation>();
    void Start()
    {
        pickerIsMove = true;
        TotalCheckPoint = _BallAreaTechnicalOperation.Count-1;
        for (int i = 0; i < _BallAreaTechnicalOperation.Count; i++)
        {
            _BallAreaTechnicalOperation[i].CountText.text = CountOfBallThrown + "/" + _BallAreaTechnicalOperation[i].GoalBall;
        }
       
    }

    void Update()
    {
        if (pickerIsMove)
        {
            PickerObject.transform.position += 5f * Time.deltaTime * PickerObject.transform.forward;

            if (Time.timeScale!=0)
            {

                if (Input.touchCount>0)
                {
                    Touch touch = Input.GetTouch(0);
                    Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));
                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            fingerPositionX = touchPosition.x - PickerObject.transform.position.x;
                            //finger position picker ile parmak arasındaki x konumundaki mesafeyi alır.
                            break;
                        case TouchPhase.Moved:
                            if ( touchPosition.x - fingerPositionX > -1.15f && touchPosition.x - fingerPositionX < 1.15f )
                            {
                                PickerObject.transform.position = Vector3.Lerp(PickerObject.transform.position,
                                    new Vector3(touchPosition.x - fingerPositionX, PickerObject.transform.position.y,
                                    PickerObject.transform.position.y), 3f);
                            }
                            break;
                       
                    }
                }

                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    PickerObject.transform.position = Vector3.Lerp(PickerObject.transform.position, new Vector3
                        (PickerObject.transform.position.x - .2f, PickerObject.transform.position.y, PickerObject.transform.position.z),.05f);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    PickerObject.transform.position = Vector3.Lerp(PickerObject.transform.position, new Vector3
                        (PickerObject.transform.position.x + .2f, PickerObject.transform.position.y, PickerObject.transform.position.z), .05f); 
                }
            }

        }
    }

    public void LimitReached()
    {
        if (PickerPalletActive)
        {
            PickerPalletObject[0].SetActive(false);
            PickerPalletObject[1].SetActive(false);
        }
        pickerIsMove = false;
        Invoke("CheckPointFinish", 2f);
        Collider[] Collhit = Physics.OverlapBox(BallControlObject.transform.position, BallControlObject.transform.localScale / 2,
            Quaternion.identity); //https://docs.unity3d.com/ScriptReference/Physics.OverlapBox.html

        int i = 0;
        while (i<=Collhit.Length-1)
        {
            Collhit[i].GetComponent<Rigidbody>().AddForce(new Vector3(0, 0,.6f),ForceMode.Impulse);
            // https://docs.unity3d.com/ScriptReference/Rigidbody.AddForce.html
            // https://docs.unity3d.com/ScriptReference/ForceMode.html
            i++;
        }
        Debug.Log(i);

    }

    void CheckPointFinish()
    {
        if (CountOfBallThrown>= _BallAreaTechnicalOperation[CurentCheckPointIndex].GoalBall)
        {
            _BallAreaTechnicalOperation[CurentCheckPointIndex].BallAreaElevator.Play("Elevator");
            print("kazandın");
           
            foreach (var item in _BallAreaTechnicalOperation[CurentCheckPointIndex].Balls)
            {
                item.SetActive(false);
            }           
            if (CurentCheckPointIndex==TotalCheckPoint)
            {
                print("oyun bitti");
                foreach (var item in FinalEfect)
                {
                    if (!item.activeInHierarchy)
                    {
                        item.SetActive(true);
                    }
                }
                Invoke("TimeIsStop", 1.5f);
            }
            else
            {
                CurentCheckPointIndex++;
                CountOfBallThrown = 0;
                if (PickerPalletActive)
                {
                    PickerPalletObject[0].SetActive(true);
                    PickerPalletObject[1].SetActive(true);
                }
            }
            
        }
        else
        {
            print("kaybettin");
        }
    }
    void TimeIsStop()
    {
        Time.timeScale = 0;
    }
   
    public void CountBall()
    {
        CountOfBallThrown++;
        _BallAreaTechnicalOperation[CurentCheckPointIndex].CountText.text = CountOfBallThrown + "/" +
            _BallAreaTechnicalOperation[CurentCheckPointIndex].GoalBall;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(BallControlObject.transform.position, BallControlObject.transform.localScale);
       // https://docs.unity3d.com/ScriptReference/Gizmos.html
    }
    public void PickerPalletOpen()
    {
        PickerPalletActive = true;
        PickerPalletObject[0].SetActive(true);
        PickerPalletObject[1].SetActive(true);
    }
    public void BonusBallsAdd(int bonusBallIndex)
    {
        BonusBalls[bonusBallIndex].SetActive(true);
    }
}
