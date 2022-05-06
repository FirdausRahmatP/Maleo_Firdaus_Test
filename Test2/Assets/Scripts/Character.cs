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
    public void Shoot()
    {
        base.Shoot();
    }
}
