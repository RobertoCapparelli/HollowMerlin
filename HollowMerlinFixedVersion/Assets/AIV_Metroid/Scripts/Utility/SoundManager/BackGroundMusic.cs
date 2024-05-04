using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    [SerializeField]
    private AudioClip backGroundMusicFX;

    // Start is called before the first frame update
    void Start()
    {
        SoundMenager.instance.PlaySoundFXClip(backGroundMusicFX, transform, 1f);
    }

}
