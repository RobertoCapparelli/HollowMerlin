using AIV_Metroid_Player;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AIV_Metroid_Player
{
    public class RiftPool
    {
        private List<Rift> availableRifts;
        private List<Rift> placedRifts;

        public RiftPool(int initialPoolSize, GameObject riftPrefab)
        {
            availableRifts = new List<Rift>(initialPoolSize);  //Pooling
            placedRifts = new List<Rift>(initialPoolSize);
            for (int i = 0; i < initialPoolSize; i++)
            {
                GameObject riftObject = GameObject.Instantiate(riftPrefab);    
                Rift rift = riftObject.AddComponent<Rift>(); 
                availableRifts.Add(rift);
            }
        }

        public Rift GetAvailableRift() //Method for pick up a valid rift  (a valid rift is an unplaced rift)
        {
            // Find a valid Rift
            Rift availableRift = availableRifts.Find(rift => !rift.IsValid);

            // Active Rift
            if (availableRift != null)
            {
                availableRift.IsValid = true;
                placedRifts.Add(availableRift);
                return availableRift;

            }
            return null;
        }
        public Rift GetPlacedRift()  //Method for get the first placed rift
        {
            if (placedRifts.Count > 0)
            {
                Rift lastPlacedRift = placedRifts[placedRifts.Count - 1];
                placedRifts.RemoveAt(placedRifts.Count - 1);

                return lastPlacedRift;
            }
            else
            {
                return null;
            }
        }

        public void RemovePlacedRift() //Method for remove the first placed rift
        {
            if (placedRifts.Count > 0)
            {

                placedRifts.RemoveAt(0);
            }

        }
        public void RemovePlacedRiftWithCoroutine(Rift riftToRemove) //Need this method for pass the value of the rift to remove
        {
            int index = availableRifts.IndexOf(riftToRemove);
            RemovePlacedRift();

            if (index >= 0)
            {
                availableRifts[index].IsValid = false;
            }
        }
        public void RemoveAllPlacedRifts() //Stop Teleport (Maybe with damage? OnDamage action)
        {
            foreach (Rift rift in placedRifts) { placedRifts.Remove(rift); }
            foreach (Rift rift in availableRifts) { rift.IsValid = false; }
        }
    }
}
