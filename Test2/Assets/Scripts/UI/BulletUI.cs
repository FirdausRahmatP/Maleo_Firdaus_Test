using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletUI : MonoBehaviour
{
    public Text bulletText;
    private void OnEnable()
    {
        Character.main.onBullet += SetBullet;
    }
    private void OnDisable()
    {
        Character.main.onBullet -= SetBullet;
    }
    public void SetBullet(int bullet)
    {
        bulletText.text = bullet.ToString();
    }
}
