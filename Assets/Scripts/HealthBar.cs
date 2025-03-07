using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Color low;
    public Color high;
    public Vector3 offset;
    
    void Update()
    {
       // Debug.Log($"camera {camera.WorldToScreenPoint(transform.position + offset)}");
        // Camera.main.WorldToScreenPoint()
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);
    }

    public void SetHealth(float health, float maxHealth)
    {
        slider.gameObject.SetActive(health < maxHealth);
        slider.value = health;
        slider.maxValue = maxHealth;
        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slider.normalizedValue);
    }
}