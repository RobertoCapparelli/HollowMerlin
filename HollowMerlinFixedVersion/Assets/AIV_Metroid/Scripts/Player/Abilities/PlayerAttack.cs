using AIV_Metroid_Player;
using Codice.Client.Common.GameUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerAttack : PlayerAbilityBase
{
    /*C'è un problema nella parte grafica, viene chiamato prima l'input e poi l'OnTriggerEnter, quidi la variabile per settare se il colpo è andato a segno o meno
     *viene settata dopo la chiamata all'animator! 
     */
    private const string attackNoHitAnimatorString = "AttackNoHit";
    private const string attackHitAnimatorString = "AttackHit";

    #region Field

    [SerializeField]
    private bool attackOnStay;
    [SerializeField]
    private DamageContainer damageContainer;
    [SerializeField]
    private float attackCooldown;
    [SerializeField]
    private BoxCollider2D attackCollider;
    [SerializeField]
    private Animator animator;
   [SerializeField]
    private Vector3 offset = new Vector3(2.5f, 0, 0);

    private bool canAttack;
    private bool hit;

    #endregion

    #region Mono

    protected void OnEnable()
    {
        InputManager.Player.Attack.performed += OnInputPerform;

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

        GetDirectionAttack();

        attackCollider.enabled = true;
        
        StartCoroutine(WaitForAttackCooldown());

        SetAnimationParameter(attackHitAnimatorString, attackNoHitAnimatorString);

    }

    private void GetDirectionAttack()
    {
        Vector3 resultOffset = playerController.transform.forward.x >= 0 ? offset : -offset;
        gameObject.transform.SetPositionAndRotation(playerController.transform.position + resultOffset,
                                                    playerController.transform.rotation);

        float verticalDirection = InputManager.Player_Vertical;
        if (verticalDirection == 0) return;

        /*
         * Risetto il pivot della rotazione nello stesso punto per il colpo in verticale per semplificare i calcoli
         * infatti mettiamo caso che il player sia girato verso destra e debba colpire in alto, allora la rotazione da fare sarebbe di 90 gradi, ma se fosse girato
         * a sinistra allora la rotazione dovrebbe essere di -90, questo sia per l'alto che il basso

        */
        gameObject.transform.SetPositionAndRotation(playerController.transform.position + offset,
                                                    Quaternion.identity);

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

        hit = true;

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

    #region Coroutine

    IEnumerator WaitForAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        attackCollider.enabled = false;
        canAttack = true;
    }
    #endregion

    #region Visual
    private void SetAnimationParameter(string attackHit, string attackNoHit)
    {
        if (hit)
        {
            animator.SetTrigger(Animator.StringToHash(attackHit));

        } else animator.SetTrigger(Animator.StringToHash(attackNoHit));

        hit = false;
    }
    #endregion

}
