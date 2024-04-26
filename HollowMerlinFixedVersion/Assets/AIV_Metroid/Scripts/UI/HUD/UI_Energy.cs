using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UI_Energy : MonoBehaviour
{

    private ProgressBar energyBar;

    private void Awake() {
        energyBar = GetComponent<UIDocument>().rootVisualElement.Q<ProgressBar>("EnergyBar");
    }

    private void OnEnable() {
        GlobalEventManager.AddListener(GlobalEventIndex.PlayerEnergyUpdated, OnPlayerEnergyUpdated);

    }

    private void OnDisable() {
        GlobalEventManager.RemoveListener(GlobalEventIndex.PlayerEnergyUpdated, OnPlayerEnergyUpdated);
    }

    private void OnPlayerEnergyUpdated(GlobalEventArgs message) { 
        GlobalEventArgsFactory.PlayerEnergyUpdatedParser(message, out float maxEnergy, out float currentEnergy);
        energyBar.value = currentEnergy / maxEnergy;
    }

}
