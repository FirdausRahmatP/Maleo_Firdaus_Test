using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Life : MonoBehaviour
{
    public bool canAttack=true;
    public bool immortal;
    public bool death;
    public float health = 5, maxHealth = 5;
    public float speed = 2;
    public float damage = 40;
    public bool infiniteClip;
    public int clip = 24;
    public int clipMax = 24;
    public float fireDelay = 0.3f;
    public float bulletDelay = 0;
    public float reloadTime = 2.5f;
    public string attackAnim="Attack";
    public int attackLayer=0;
    public Action<float, float> onDamaged;
    public Action<int> onBullet;
    public UnityEvent onDeath;
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
    private void Update()
    {
        LifeUpdate();   
    }
    private float shootTimer;
    public void LifeUpdate()
    {
        shootTimer += Time.deltaTime;
        if (canAttack && shoot && shootTimer > fireDelay && clip > 0)
        {
            shootTimer = 0;
            anim.Play(attackAnim, attackLayer, 0);
            Invoke("Shoot", bulletDelay);
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
        if (death)
        {
            return;
        }
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
        if(CameraController.Instance.currentMode == CameraController.CameraMode.TopDown)
        {
            shootPoint.localRotation = Quaternion.identity;
        }
        Damage d = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        d.owner = this;
        d.damageCharacter = damage;
        d.damageZombie = damage;
        if(!infiniteClip)
            clip--;
        onBullet?.Invoke(clip);
        if(clip<=0)
            StartCoroutine(Reload());
    }
    public float reloadTimer;
    public IEnumerator Reload()
    {
        anim.SetBool("Reload_b", true);
        yield return new WaitForSeconds(reloadTime);
        clip = clipMax;
        anim.SetBool("Reload_b", false);
        onBullet?.Invoke(clip);
    }
    public void Damage(float damage)
    {
        if (death)
        {
            return;
        }
        health -= damage;
        health = health <= 0 ? 0 : health;
        onDamaged?.Invoke(damage, health);
        if(health <= 0)
        {
            OnDeath();
        }
    }
    public virtual void OnDeath()
    {
        if (death)
        {
            return;
        }
        if (!immortal)
        {
            PlayDeath();
        }
        death = true;
        onDeath?.Invoke();
    }
    public void PlayDeath()
    {
        this.enabled = false;
        anim.SetBool("Death_b", true);
        control.enabled = false;
        if(gameObject != Character.main.gameObject)
        {
            StartCoroutine(Decay());
        }
    }
    IEnumerator Decay()
    {
        yield return new WaitForSeconds(3);
        for (int i = 0; i < 100; i++)
        {
            transform.position += Vector3.down * Time.deltaTime * 10;
            yield return null;
        }
        Destroy(gameObject);
    }
}
