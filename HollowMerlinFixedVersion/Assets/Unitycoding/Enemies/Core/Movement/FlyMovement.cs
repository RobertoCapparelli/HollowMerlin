
using ICode.Actions.UnityRigidbody2D;
using UnityEngine;

public enum FlyEnemyControllerStatus
{
    moving,
    hitted,
    dying,
    dead
}
public class FlyMovement : MonoBehaviour, IEnemyMovement
{
    #region Private_Attributes


    protected Rigidbody2D myRigidbody;
    private BoxCollider2D myCollider;

    private float movementSpeed;
    private Vector2 inputDirection;

    private Vector2 preHitVelocity;
    private FlyEnemyControllerStatus status;


    #endregion

    #region Properties

    public float MovementSpeed { get { return movementSpeed; } set { if (value < 0) { movementSpeed = 0; return; } movementSpeed = value; } }
    public Vector2 InputDirection { get { return inputDirection; } }
    public bool FaceDirection { get; private set; } 
    public bool IsGrounded { get { return false; } }

    public float Speed { get; set; }
    public float Amplitude { get; set; }

    #endregion

    #region UnityGameLoop
    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponentInChildren<BoxCollider2D>();
        ResetMe();
    }

    private void FixedUpdate()
    {     
        if (status == FlyEnemyControllerStatus.dead) return;
        SetYMovementByWave();
    }

    private void SetYMovementByWave()
    {
        float yMovement = Amplitude * Mathf.Sin(Time.time * Speed);
        myRigidbody.velocity += new Vector2(0, yMovement);
    }


    #endregion


    #region IEnemyMovement

    public void SetMovementSpeed(float movementSpeed)
    {
        MovementSpeed = movementSpeed;
    }

    public void SetFaceDirection(bool value)
    {
        FaceDirection = value;
    }

    public void SetInputDirection(Vector2 inputDirection)
    {
        this.inputDirection = inputDirection.normalized;
        Vector2 performedVelocity = PerformedVelocity(this.inputDirection);
        switch (status)
        {
            case FlyEnemyControllerStatus.hitted:
                preHitVelocity = performedVelocity;
                break;
            case FlyEnemyControllerStatus.moving:
                myRigidbody.velocity = performedVelocity;
                if (FaceDirection)
                {
                    ChangeYRotation();
                }
                break;
            case FlyEnemyControllerStatus.dead:
                if (inputDirection == Vector2.zero)
                {
                    myRigidbody.velocity = performedVelocity;
                }
                break;
        }
    }

    protected virtual Vector2 PerformedVelocity(Vector2 direction)
    {
        return new Vector2(inputDirection.x * movementSpeed, 0);
    }

    public void SetVerticalMovement(float speed, float amplitude)
    {
        Speed = speed;
        Amplitude = amplitude;


    }
    private void ChangeYRotation()
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.y = inputDirection.x < 0 ? 180 : 0;
        transform.eulerAngles = eulerRotation;
    }
    public void ReverseInputDirection()
    {
        throw new System.NotImplementedException();
    }

    public void Jump()
    {
        throw new System.NotImplementedException();
    }

    public void StopMovement()
    {
        throw new System.NotImplementedException();
    }

    public void SetJumpForce(float jumpForce)
    {
        throw new System.NotImplementedException();
    }

    public void Hitted(Vector2 hitForce, Vector3 sourcePosition)
    {
        throw new System.NotImplementedException();
    }

    public void Die(Vector2 dieForce, Vector3 sourcePosition)
    {

    }

    public void Die()
    {
        SetMovementSpeed(0);
        myRigidbody.gravityScale = 3;
        status = FlyEnemyControllerStatus.dead;

        
        
    }

    public void Teleport(Vector3 position)
    {
        throw new System.NotImplementedException();
    }

    #endregion

    public void ResetMe()
    {
        FaceDirection = true;
        status = FlyEnemyControllerStatus.moving;
    }
}
