using AIV_Metroid_Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

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
    [SerializeField]
    private float bounceForce;

    private bool canAttack;

    protected BoxCollider2D attackCollider;
    protected Animator animator;

    [SerializeField]
    Vector3 offset = new Vector3(2.5f, 0, 0);


    #endregion

    #region Mono

    protected void OnEnable()
    {
        InputManager.Player.Attack.performed += OnInputPerform;
        attackCollider = gameObject.GetComponent<BoxCollider2D>();
        animator = gameObject.GetComponent<Animator>();


        attackCollider.enabled = false;
        canAttack = true;
    }
    protected void FixedUpdate()
    {

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

        GetDirectionAttack();

        attackCollider.enabled = true;
        StartCoroutine(WaitForAttackCooldown());

        //Visual
        //animator.transform.SetPositionAndRotation(attackCollider.transform.localPosition, attackCollider.transform.rotation);

        SetAnimationParameter("FireAttack");

    }

    private void GetDirectionAttack(float a)
    {
        gameObject.transform.SetPositionAndRotation(playerController.transform.position +
                                        (gameObject.transform.forward.x > 0 ? offset : -offset), Quaternion.identity);



        float verticalDirection = InputManager.Player_Vertical;

        if (verticalDirection == 0) return;

        float horizontalDirection = InputManager.Player_Horizontal;

        if (playerController.ComputedDirection.y < 0)
        {
            gameObject.transform.RotateAround(gameObject.transform.position - offset, Vector3.forward, 90);

        }
        else
        {
            gameObject.transform.RotateAround(gameObject.transform.position - offset, Vector3.forward, -90);
        }


    }
    private void GetDirectionAttack()
    {
        Vector3 resultOffset = playerController.transform.forward.x >= 0 ? offset : -offset;
        gameObject.transform.position = playerController.transform.position + resultOffset;
        gameObject.transform.rotation = Quaternion.identity;

        float verticalDirection = InputManager.Player_Vertical;
        if (verticalDirection == 0) return;

        gameObject.transform.position = playerController.transform.position + offset;
        if (verticalDirection < 0.0)
        {
            gameObject.transform.RotateAround(playerController.transform.position, Vector3.forward, -90);
        }
        else
        {
            gameObject.transform.RotateAround(playerController.transform.position, Vector3.forward, 90);
        }

    }
    #endregion

    #region protectedMethods
    protected void InternalTrigger(Collider2D other)
    {
        if (!canAttack) return;
        if (other.gameObject == gameObject) return;

        IDamageble damageable = other.GetComponent<IDamageble>();
        if (damageable == null) return;


        Vector3 hitPosition = other.ClosestPoint(transform.position);
        damageContainer.SetContactPoint(hitPosition);
        damageable.TakeDamage(damageContainer);



        /*  
         * Ho provato ad aggiungere un impulso al player che quando colpisce un environment ha una forza nella direzione opposta al colpo,
         * però il player si sposta solo sull'asse y, ho testato sia con la velocity sia con l'impulso, anche inserendo il vector.up (come abbiamo
         * fatto in classe) il player continua a spostarsi solo sull'asse delle y. presumo che il problema sia che nel frame in cui viene calcolato 
         * il tutto anche il collider dell'attacco fa parte del player e quindi potrebbe bloccarlo, però non capisco allora perchè sull'asse delle y riesce
         * a spostarsi. 
         
        Vector3 hitDirection = playerController.transform.position  - hitPosition;
        
        playerController.SetImpulse(hitDirection.normalized * bounceForce); */


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
    private void SetAnimationParameter(string name)
    {
        animator.SetTrigger(Animator.StringToHash(name));

    }
    #endregion

}
