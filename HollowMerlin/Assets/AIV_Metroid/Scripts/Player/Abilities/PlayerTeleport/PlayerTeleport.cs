using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AIV_Metroid_Player
{

    public class TeleportAbility : PlayerAbilityBase
    {
        #region SerializedField

        [SerializeField]
        protected int NumberRifts;
        [SerializeField]
        protected float MaxTimer;
        [SerializeField]
        protected GameObject RiftPrefab;
        #endregion

        #region PrivateField

        private RiftPool riftPool;
        #endregion

        #region Mono
        protected void OnEnable()
        {
            InputManager.Player.Teleport.performed += OnInputTeleportPerform;
            InputManager.Player.PlaceRift.performed += OnInputPlaceRiftPerform;
        }

        protected void OnDisable()
        {
            InputManager.Player.Teleport.performed -= OnInputTeleportPerform;
            InputManager.Player.PlaceRift.performed -= OnInputPlaceRiftPerform;
        }
        #endregion

        #region Override

        public override void Init(PlayerController playerController, PlayerVisual playerVisual)
        {
            base.Init(playerController, playerVisual);

            riftPool = new RiftPool(NumberRifts, RiftPrefab); //Create the pool

        }
        public override void OnInputEnabled()
        {
            isPrevented = false;
        }
        public override void OnInputDisabled()
        {
            isPrevented = true;
            StopAbility();
        }
        public override void StopAbility()
        {
            riftPool.RemoveAllPlacedRifts();
        }

        #endregion

        #region Callbacks
        protected void OnInputTeleportPerform(InputAction.CallbackContext input)
        {
            //TODO: if (CanTeleport())
            Teleport();
        }
        protected void OnInputPlaceRiftPerform(InputAction.CallbackContext input)
        {
            if (!input.performed) return;

            //TODO:if (CanPlaceRift())
            PlaceRift();
        }

        #endregion

        #region Public Methods  
        public void Teleport()
        {
            //Get the first rift placed
            Rift rift = riftPool.GetPlacedRift();

            if (rift != null)
            {
                //Teleport!
                playerController.transform.position = rift.transform.position;
                rift.IsValid = false;
                rift.SetSpriteRender(false);
            }
            else Debug.Log("No rift placed");
        }
        public void PlaceRift()
        {
            // Get an available rift from the pool
            Rift rift = riftPool.GetAvailableRift();

            if (rift == null)
            {
                Debug.Log("No rift valid");
                return;
            }
            //Place the rift
            rift.PlaceRift(playerController.transform.position, MaxTimer);
           // StartCoroutine(StartRiftCoroutine(rift));

            Debug.Log("RiftPlaced");
        }

        #endregion

       
    }
}