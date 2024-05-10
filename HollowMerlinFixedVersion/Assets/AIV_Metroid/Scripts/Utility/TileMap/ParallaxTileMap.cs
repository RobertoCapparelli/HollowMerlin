using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxTileMap : MonoBehaviour
{
    [SerializeField]
    private Transform MainCamera;
    [SerializeField]
    private float RelativeMove;
    [SerializeField]
    private bool FollowYAxesCamera;

    private float YAxesStartPosition;


    //Si potrebbe prendere la distanza dell'oggetto dalla camera e se la Y si distanzia troppo smette di seguire

    //Ho fatto la parallasse in questo modo perchè non avevo immagini da mettere con diverse profondità

    void Start()
    {
        YAxesStartPosition = transform.position.y;
    }
    void Update()
    {
        if (FollowYAxesCamera)
        {
        transform.position = new Vector2(MainCamera.position.x * RelativeMove, MainCamera.position.y);            
        }
        else { transform.position = new Vector2(MainCamera.position.x * RelativeMove, YAxesStartPosition); }
    }
}
