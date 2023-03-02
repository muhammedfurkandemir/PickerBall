using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pickerObject;
    [SerializeField] private bool pickerIsMove;
    void Start()
    {
        pickerIsMove = true; 
    }

    // Update is called once per frame
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
}
