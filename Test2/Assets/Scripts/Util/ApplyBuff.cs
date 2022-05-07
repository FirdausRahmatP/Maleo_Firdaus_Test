using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyBuff : MonoBehaviour
{
    private Life target;
    public bool canAttack;
    public float speedMultiplier;
    public void Apply(Life target) {
        this.target = target;
        target.speed *= speedMultiplier;
        target.canAttack = canAttack;
    }
}
