using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerHUD : MonoBehaviour
{
    public static PlayerHUD instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    [SerializeField] private ProgressBar healthBar;
    [SerializeField] private Score scoreUI;

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        healthBar.SetValues(currentHealth, maxHealth);
    }

    public void UpdateScoreAmount()
    {
        scoreUI.AddToScore();
    }



}
