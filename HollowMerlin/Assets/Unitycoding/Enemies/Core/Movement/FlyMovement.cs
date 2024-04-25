
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

    private float maxFlyingSpeed = 10;

    protected Rigidbody2D myRigidbody;
    private BoxCollider2D myCollider;

    private float movementSpeed;
    private Vector2 inputDirection;

    private Vector2 preHitVelocity;
    private FlyEnemyControllerStatus status;

    private float verticalMovementRange = 2.0f; // Range for random vertical movement
    private float maxVerticalSpeed = 5.0f; // Maximum vertical speed
    #endregion

    private float HitForce = 0;

    #region Properties

    public float MovementSpeed { get { return movementSpeed; } set { if (value < 0) { movementSpeed = 0; return; } movementSpeed = value; } }
    public Vector2 InputDirection { get { return inputDirection; } }
    public float MaxFlyingSpeed { get { return maxFlyingSpeed; } set { maxFlyingSpeed = value; } }
    public bool FaceDirection { get; private set; } // If applicable for flying enemies
    public bool IsGrounded { get { return false; } }

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
        switch (status)
        {
            case FlyEnemyControllerStatus.dying:
                Die(Vector3.zero, Vector3.zero);
                break;
        }
        //IncreaseFallSpeed();
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
        Vector2 desiredVelocity = new Vector2(1, 1);
        float movementSpeedModifier = 1;
        float airBuoyancyForce = 1;

        this.inputDirection = inputDirection.normalized;
        Vector2 performedVelocity = PerformedVelocity(this.inputDirection);
        switch (status)
        {
            case FlyEnemyControllerStatus.hitted:
                preHitVelocity = performedVelocity;
                break;
            case FlyEnemyControllerStatus.moving:
                // Apply movement speed with a drag factor to simulate air resistance
                myRigidbody.velocity = performedVelocity;

                // Add a slight upward force to simulate air buoyancy (optional)
                myRigidbody.AddForce(Vector2.up * airBuoyancyForce);
                if (FaceDirection)
                {
                    ChangeYRotation();
                }
                break;
            case FlyEnemyControllerStatus.dead:
                // Stop movement entirely (optional: handle falling physics)
                myRigidbody.velocity = Vector2.zero;
                break;
        }
    }
    protected virtual Vector2 PerformedVelocity(Vector2 direction)
    {
        // Calculate horizontal velocity based on input direction and movement speed
        float horizontalVelocity = inputDirection.x * movementSpeed;

        // Introduce random variation for vertical velocity
        float randomVerticalVelocity = Random.Range(-verticalMovementRange, verticalMovementRange);
        float verticalVelocity = myRigidbody.velocity.y + randomVerticalVelocity;

        // Clamp vertical velocity to avoid excessive bouncing
        verticalVelocity = Mathf.Clamp(verticalVelocity, -maxVerticalSpeed, maxVerticalSpeed);

        // Return the modified velocity
        return new Vector2(horizontalVelocity, verticalVelocity);
    }

    private void ChangeYRotation() {
        if (inputDirection.x == 0 || movementSpeed == 0) return;
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
        throw new System.NotImplementedException();
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
