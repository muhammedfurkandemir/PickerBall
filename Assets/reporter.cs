using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reporter : MonoBehaviour
{
    [SerializeField] private GameManager _GameManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("picker_trigger"))
        {

        }
    }
}
