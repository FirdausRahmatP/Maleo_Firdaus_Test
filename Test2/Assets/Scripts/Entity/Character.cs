using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class Character : Life
{
    public static Character main;
    private void Awake()
    {
        main = this;
    }
    private void Start()
    {
        base.Init();
        onDeath.AddListener(UIController.Instance.OnGameEnd);

    }
    public void Shoot()
    {
        base.Shoot();
    }
}
