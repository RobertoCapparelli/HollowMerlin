using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class UI_SoundMenu : MonoBehaviour
{

    [SerializeField] private UIDocument UIDocument;

    private Slider MasterSlider;
    private Slider SoundFXSlider;
    private Slider MusicSlider;

    private VisualElement root;

    void Start()
    {
        root = UIDocument.rootVisualElement.Q<VisualElement>("Container");

        MasterSlider = root.Q<Slider>("MasterSlider");
        SoundFXSlider = root.Q<Slider>("SoundFXSlider");
        MusicSlider = root.Q<Slider>("MusicSlider");

        // Trova il dragger degli slider anziché gli slider stessi
        VisualElement masterDragger = MasterSlider.Q("unity-dragger");
        VisualElement soundFXDragger = SoundFXSlider.Q("unity-dragger");
        VisualElement musicDragger = MusicSlider.Q("unity-dragger");

        // Registra callback per l'evento DragUpdatedEvent sui dragger
        masterDragger.RegisterCallback<DragUpdatedEvent>(evt => OnSliderDragUpdated(MasterSlider));
        soundFXDragger.RegisterCallback<DragUpdatedEvent>(evt => OnSliderDragUpdated(SoundFXSlider));
        musicDragger.RegisterCallback<DragUpdatedEvent>(evt => OnSliderDragUpdated(MusicSlider));
    }

    private void Update()
    {
        if (MasterSlider.HasMouseCapture())
        {
            Debug.Log("Interact with slider");
        }

    }


    private void OnSliderDragUpdated(Slider slider)
    {
        // Aggiorna il volume dell'audio in base al valore dello slider
        float volume = slider.value;
        Debug.Log(slider.name + " slider value changed to: " + volume);
        // Qui potresti utilizzare il valore del volume per regolare l'audio
    }
}
