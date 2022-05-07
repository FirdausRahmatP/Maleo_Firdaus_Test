using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Text healthText;
    public Image healthImage;
    private void Awake()
    {
        
    }
    private void OnEnable()
    {
        Character.main.onDamaged += OnDamaged;
    }
    private void OnDisable()
    {
        Character.main.onDamaged -= OnDamaged;
    }
    public void OnDamaged(float damage,float health)
    {
        healthText.text = health.ToString();
        healthImage.fillAmount = health / Character.main.maxHealth;
    }
}
