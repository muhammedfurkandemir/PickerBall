using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

[Serializable]
public class BallAreaTechnicalOperation
{
    public Animator BallAreaElevator;
    public TextMeshProUGUI CountText;
    public int GoalBall;
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pickerObject;
    [SerializeField] private GameObject BallControlObject;
    [SerializeField] private bool pickerIsMove;

    int CountOfBallThrown;

    [SerializeField] private List<BallAreaTechnicalOperation> _BallAreaTechnicalOperation = new List<BallAreaTechnicalOperation>();
    void Start()
    {
        pickerIsMove = true;

        _BallAreaTechnicalOperation[0].CountText.text = CountOfBallThrown + "/" + _BallAreaTechnicalOperation[0].GoalBall;
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

    public void CountBall()
    {
        CountOfBallThrown++;
        _BallAreaTechnicalOperation[0].CountText.text = CountOfBallThrown + "/" + _BallAreaTechnicalOperation[0].GoalBall;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(BallControlObject.transform.position, BallControlObject.transform.localScale);
       // https://docs.unity3d.com/ScriptReference/Gizmos.html
    }
}
