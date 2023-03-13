using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBallPallet : MonoBehaviour
{
    [SerializeField] private GameManager _GameManager;
    [SerializeField] private string BallType;
    [SerializeField] private int BonusBallIndex;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("picker_trigger"))
        {
            if (BallType== "BonusBallPallet")
            {
                _GameManager.PickerPalletOpen();
                gameObject.SetActive(false);
            }
            else if (BallType=="BonusBall")
            {
                _GameManager.BonusBallsAdd(BonusBallIndex);
                gameObject.SetActive(false);
            }
            
        }
    }
}
