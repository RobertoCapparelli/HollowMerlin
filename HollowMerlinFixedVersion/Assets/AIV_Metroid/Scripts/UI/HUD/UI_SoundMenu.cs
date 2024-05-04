using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class UI_SoundMenu : MonoBehaviour
{
    /*Non funziona perchè non c'è l'input del mouse nell'input system
     * 
     * TODO: Aggiungere il mouse nel nuovo sistema di input.
     */


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

        
        VisualElement masterDragger = MasterSlider.Q("unity-dragger");
        VisualElement soundFXDragger = SoundFXSlider.Q("unity-dragger");
        VisualElement musicDragger = MusicSlider.Q("unity-dragger");

        
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
        
    }
}
