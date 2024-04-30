using AIV_Metroid_Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : PlayerAbilityBase
{
    private const string attackAnimatorString = "Attack";

    #region Field

    [SerializeField]
    private bool attackOnStay;
    [SerializeField]
    private DamageContainer damageContainer;
    [SerializeField]
    private float attackCooldown;

    private bool canAttack;

    protected BoxCollider2D attackCollider;

    #endregion

    #region Mono

    protected void OnEnable()
    {
        InputManager.Player.Attack.performed += OnInputPerform;
        attackCollider = gameObject.GetComponent<BoxCollider2D>();
        attackCollider.enabled = false;
        canAttack = true;
    } 

    protected void OnTriggerEnter2D(Collider2D other)
    {
        InternalTrigger(other);
    }

    protected void OnTriggerStay2D(Collider2D other)
    {
        if (!attackOnStay) return;
        InternalTrigger(other);
    }
    #endregion

    #region privateMethods
    private void OnInputPerform(InputAction.CallbackContext input)
    {
        if (!input.performed) return;
        if (!canAttack) return;

        attackCollider.enabled = true;
        StartCoroutine(WaitForAttackCooldown());
        SetAnimationParameter();

    }
    #endregion

    #region protectedMethods
    protected void InternalTrigger(Collider2D other)
    {
        if (!canAttack) return;
        if (other.gameObject == gameObject) return;

        IDamageble damageable = other.GetComponent<IDamageble>();
        if (damageable == null) return;


        Vector2 hitPosition = other.ClosestPoint(transform.position);
        damageContainer.SetContactPoint(hitPosition);
        damageable.TakeDamage(damageContainer);

        canAttack = false;


    }
    #endregion

    #region Coroutine

    IEnumerator WaitForAttackCooldown()
    {       
        yield return new WaitForSeconds(attackCooldown); // Wait for attack cooldown duration
        attackCollider.enabled = false;
        canAttack = true;
    }
    #endregion

    #region AbilityMethods

    public override void OnInputDisabled()
    {
        canAttack = false;
    }

    public override void OnInputEnabled()
    {
       canAttack = true;
    }

    public override void StopAbility()
    {
        StartCoroutine(WaitForAttackCooldown());
    }

    #endregion

    #region Visual
    private void SetAnimationParameter()
    {
            playerVisual.SetAnimatorParameter(attackAnimatorString);
    }
    #endregion

}
