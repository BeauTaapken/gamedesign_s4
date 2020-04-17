using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public float minHealthToSlice;
    public Image mask;

    private float maxHealth = 100;
    private float currentHealth;
    private float originalSize;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        originalSize = mask.rectTransform.rect.width;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * (currentHealth / 100.0f));
    }

    public float getCurrentHealth()
    {
        return currentHealth;
    }

    public float getMinHealthToSlice()
    {
        return minHealthToSlice;
    }
}
