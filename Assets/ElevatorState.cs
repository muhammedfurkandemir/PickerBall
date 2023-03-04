using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorState : MonoBehaviour
{
    [SerializeField] private GameManager _GameManger;
    [SerializeField] private Animator _Animator;
    void GateIsUp()
    {
        _Animator.Play("GateIsUp");
    }
    void finish()
    {
        _GameManger.pickerIsMove = true;
    }
}
