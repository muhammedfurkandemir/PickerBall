using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBallPallet : MonoBehaviour
{
    [SerializeField] private GameManager _GameManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("picker_trigger"))
        {
            _GameManager.PickerPalletOpen();
            gameObject.SetActive(false);
        }
    }
}
