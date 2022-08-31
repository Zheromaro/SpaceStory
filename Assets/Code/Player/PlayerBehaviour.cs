using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private HealthUIvisual _healthUIvisual;
    private StaminaUIvisual _staminaUIvisual;
    private float stamina;
    private float health;

    private void Start()
    {
        _staminaUIvisual = transform.Find("StaminaCanvas").GetComponent<StaminaUIvisual>();
        health = GameManager.gameManager._PlayerHealth.Health;
        stamina = GameManager.gameManager._PlayerStamina.Stamina;
        GameManager.gameManager._PlayerHealth.Target = transform;
    }

    private void Update()
    {
        DamageAndHeal();
        UseAndBackStamina();

        Dead();
    }
    private void DamageAndHeal()
    {
        if (health > GameManager.gameManager._PlayerHealth.Health)
        {
            _healthUIvisual.Damaged(GetHealthNormalized());
            GameManager.gameManager.DoStopMotion(0.1f);
            StartCoroutine(WaitForTimeToBack());
            health = GameManager.gameManager._PlayerHealth.Health;
        }
        else if (health < GameManager.gameManager._PlayerHealth.Health)
        {
            _healthUIvisual.Healed(GetHealthNormalized());
            health = GameManager.gameManager._PlayerHealth.Health;
        }
    }
    private void UseAndBackStamina()
    {
        if (stamina > GameManager.gameManager._PlayerStamina.Stamina)
        {
            _staminaUIvisual.UseStamina(GetStaminaNormalized());
            stamina = GameManager.gameManager._PlayerHealth.Health;
        }
        else if (stamina < GameManager.gameManager._PlayerStamina.Stamina)
        {
            _staminaUIvisual.StaminaBack(GetStaminaNormalized());
            stamina = GameManager.gameManager._PlayerHealth.Health;
        }
    }

    private void Dead()
    {
        if (GameManager.gameManager._PlayerHealth.Health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private float GetHealthNormalized()
    {
        return (float)GameManager.gameManager._PlayerHealth.Health / GameManager.gameManager._PlayerHealth.MaxHealth;
    }
    private float GetStaminaNormalized()
    {
        return (float)GameManager.gameManager._PlayerStamina.Stamina / GameManager.gameManager._PlayerStamina.MaxStamina;
    }

    private IEnumerator WaitForTimeToBack()
    {
        while (GameManager.gameManager.waiting == true)
            yield return null;

        GameManager.gameManager.DoSlowMotion();
    }

}
