﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealtBar : MonoBehaviour
{
    public static HealtBar instance { get; private set; }
    public Image mask;
    float originalSize;

    float maxHealth = 100;
    float currentHealth = 100;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        originalSize = mask.rectTransform.rect.width;
        SetValue(0.5f);

    }
    public void SetMaxHealth(float health)
    {
        maxHealth = health;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0f)
        {
            currentHealth = 0f;
        }
        SetHealth(currentHealth);

    }
    public void AddHealth(float health)
    {
        currentHealth += health;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        SetHealth(currentHealth);

    }
    public void SetHealth(float health)
    {
        currentHealth = health;
        SetValue((1 / maxHealth) * currentHealth);
    }

    private void SetValue(float value)
    {
        if (value > 1f)
        {
            value = 1f;
        }
        if (value<0f)
        {
            value = 0f;
        }
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}