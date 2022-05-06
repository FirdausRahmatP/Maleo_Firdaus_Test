using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public float health = 5, maxHealth = 5;
    public float speed = 2;
    public float damage = 40;
    public int clip = 24;
    public float fireDelay = 0.3f;
    public float reloadTime = 2.5f;
    public string attackAnim;
    private Animator anim;
    private CharacterController control;
    private void Start()
    {
        Init();
    }
    public void Init()
    {
        health = maxHealth;
        anim = GetComponent<Animator>();
        control = GetComponent<CharacterController>();
    }
    private float shootTimer;
    private void Update()
    {
        shootTimer += Time.deltaTime;
        if(shoot && shootTimer > fireDelay)
        {
            shootTimer = 0;
            Shoot();
        }
    }
    public void Move(Vector3 move)
    {
        if (!shoot)
            Look(move);
        move = move.normalized;
        control.Move(((move*speed)+(Vector3.down*10)) * Time.deltaTime);
        anim.SetFloat("Speed_f", move.magnitude);
    }
    public bool shoot;
    public void Look(Vector3 look)
    {
        if (look != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(look);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 20);
        }
    }
    public void Shoot(Vector3 look, bool shooting)
    {
        Look(look);
        shoot = shooting;
    }
    public Damage bullet;
    public Transform shootPoint;
    public void Shoot()
    {
        Damage d = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        d.damage = damage;
        anim.Play(attackAnim, 5, 0);
    }
    public void Damage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
