using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animEnd : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ShootingEnd()
    {
        animator.SetBool("IsShooting", false);
    }
    public void WaveAttackEnd()
    {
        animator.SetBool("Do waveAttack", false);
    }
}
