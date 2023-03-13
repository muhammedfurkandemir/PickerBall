using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorState : MonoBehaviour
{
    [SerializeField] private GameManager _GameManger;
    [SerializeField] private Animator _Animator;
    public void GateIsUp()
    {
        _Animator.Play("GateIsUp");
    }
    public void Finish()
    {
        _GameManger.pickerIsMove = true;
    }
}
