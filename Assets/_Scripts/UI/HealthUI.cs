using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    private List<LifeElementUI> healthImages;

    [SerializeField] private Sprite fullHealth, emptyHealth;

    [SerializeField] private LifeElementUI healthPrefab;

    public void Initialize(int maxHealth)
    {
        healthImages = new List<LifeElementUI>();

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for(int i = 0; i < maxHealth; i++)
        {
            LifeElementUI life = Instantiate(healthPrefab);
            life.transform.SetParent(transform, false); // Mantenemos el �ltimo par�metro como falso para que no afecte la escala de la imagen
            healthImages.Add(life);
        }
    }

    public void SetHealth(int currentHealth)
    {
        for (int i = 0; i < healthImages.Count; i++)
        {
            if (i < currentHealth)
            {
                healthImages[i].SetSprite(fullHealth);
            }
            else
            {
                healthImages[i].SetSprite(emptyHealth);
            }
        }
    }
}
