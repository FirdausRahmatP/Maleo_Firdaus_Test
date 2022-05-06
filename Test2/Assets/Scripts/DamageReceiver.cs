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
        Quaternion rot = Quaternion.LookRotation(source.transform.position - transform.position);
        GameObject g = Instantiate(effect, source.transform.position, rot);
        g.AddComponent<DestroyDelay>().delay = 2;
    }
}
