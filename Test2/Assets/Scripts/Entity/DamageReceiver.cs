using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    public float multiplier = 1;
    public Life life;
    public GameObject effect;
    public void Damage(float damage,Transform source)
    {
        life.Damage(damage * multiplier);
        GameObject g = Instantiate(effect);
        g.transform.position = source.position;
        g.transform.right = -source.forward;
        g.AddComponent<DestroyDelay>().delay = 2;
    }
}
