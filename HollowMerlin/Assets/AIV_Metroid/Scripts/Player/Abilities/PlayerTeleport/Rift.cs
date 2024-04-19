using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

namespace AIV_Metroid_Player
{
    public class Rift : MonoBehaviour
    {

        
        public SpriteRenderer spriteRenderer;

        public bool IsValid;

        private void Awake()
        {
           spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetSpriteRender(bool enable)
        {
            spriteRenderer.enabled = enable;

        }

        public void PlaceRift(Vector3 position, float seconds)
        {
            //Place the rift
            IsValid = true;
            transform.position = position;
            spriteRenderer.enabled = true;
            
        }
        public void RemoveRift()
        {
            IsValid = false;
            spriteRenderer.enabled = false;
        }


        #region PrivateMethods
        private IEnumerator StartRiftCoroutine(float maxTimer)
        {
            yield return new WaitForSeconds(maxTimer);
            RemoveRift();
            Debug.Log("Rift eliminated");
        }
        #endregion

        public void StopRiftCoroutine()
        {
                // Stop the coroutine using the instance reference
                StopCoroutine("StartRiftCoroutine");
                RemoveRift();
        }




    }
}
