using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public bool HoldIt = true;
    public bool DoUseStamina = true;
    public float UsedStamina;
    public KeyCode key;

    private bool doIt = false;

    enum AbilityState
    {
        ready,
        active,
    }

    AbilityState state = AbilityState.ready;

    public virtual void Activate(GameObject parent) { }
    public virtual void Disactivate(GameObject parent) { }

    public void DoAbility(GameObject parent)
    {
        Enable(parent);

        switch (state)
        {
            // ready
            case AbilityState.ready:
                {
                    if (doIt == true)
                    {
                        Activate(parent);
                        state = AbilityState.active;
                    }
                    break;
                }

            // active
            case AbilityState.active:
                {
                    if(doIt == false)
                    {
                        Disactivate(parent);
                        state = AbilityState.ready; //Again
                    }

                    break;
                }
        }
    }

    private void Enable(GameObject parent)
    {
        if (DoUseStamina == true)
        {
            if (GameManager.gameManager._PlayerStamina.Stamina >= 0)
            {
                if (HoldIt)
                {
                    if (Input.GetKey(key))
                    {
                        doIt = true;
                        GameManager.gameManager._PlayerStamina.UseStaminaByTime(UsedStamina);
                    }
                    else
                    {
                        doIt = false;
                    }
                }
                else
                {
                    if (Input.GetKey(key))
                    {
                        doIt = true;
                        GameManager.gameManager._PlayerStamina.UseStamina(UsedStamina);
                    }
                    else
                    {
                        doIt = false;
                    }
                }
            }
        }
        else
        {
            if (HoldIt)
            {
                if (Input.GetKey(key))
                {
                    doIt = true;
                }
                else
                {
                    doIt = false;
                }
            }
            else
            {
                if (Input.GetKeyDown(key))
                {
                    doIt = true;
                }
                else
                {
                    doIt = false;
                }
            }
        }
    }

}