using System.Collections;
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
    [SerializeField] private GameObject pickerObject;
    [SerializeField] private GameObject BallControlObject;
    public bool pickerIsMove;

    int CountOfBallThrown;

    int TotalCheckPoint;
    int CurentCheckPointIndex;

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
            pickerObject.transform.position += 5f * Time.deltaTime * pickerObject.transform.forward;

            if (Time.timeScale!=0)
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    pickerObject.transform.position = Vector3.Lerp(pickerObject.transform.position, new Vector3
                        (pickerObject.transform.position.x - .2f, pickerObject.transform.position.y, pickerObject.transform.position.z),.05f);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    pickerObject.transform.position = Vector3.Lerp(pickerObject.transform.position, new Vector3
                        (pickerObject.transform.position.x + .2f, pickerObject.transform.position.y, pickerObject.transform.position.z), .05f); 
                }
            }

        }
    }

    public void LimitReached()
    {
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
                Time.timeScale = 0;
            }
            else
            {
                CurentCheckPointIndex++;
                CountOfBallThrown = 0;
            }
            
        }
        else
        {
            print("kaybettin");
        }
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
}
