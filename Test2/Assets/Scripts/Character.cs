using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class Character : MonoBehaviour
{
    public static Character main;
    public bool isEnemy;
    public float health=5, maxHealth=5;
    public float speed=2;
    private Animator anim;
    private CharacterController control;
    private void Awake()
    {
        if (!isEnemy)
        {
            main = this;
        }
        health = maxHealth;
        anim = GetComponent<Animator>();
        control = GetComponent<CharacterController>();
    }
    public void Move(Vector3 move)
    {
        if (move != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 20);
        }
        move = move.normalized;
        control.Move(move*Time.deltaTime*speed);
        anim.SetFloat("Speed_f", move.magnitude);
    }
}
