using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class BackgroundParallaxEffect : MonoBehaviour
{
    private ParallaxCamera parallaxCamera;
    private CinemachineVirtualCamera cm;
    [Tooltip("The velocity in which the background will move. The objects further away should move slower")]
    [SerializeField] private Vector2 scrollSpeed;
    [Tooltip("If the component will repeat infinitely then use this. Else uncheck and the object will move")]
    [SerializeField] private bool hasRepeatingBackground;
    private Rigidbody2D targetRB;

    private Vector2 offset;

    private Material material;


    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        cm = transform.root.GetComponentInChildren<CinemachineVirtualCamera>();
        
        if (cm == null)
        {
            Debug.LogError(this.name + " has not found Cinemachine Virtual Camera. Make sure its " +
                "in the correct order");
            return;
        }

        if (cm.Follow == null)
        {
            Debug.LogError(this.name + " has not found a Follow Target in CM Virtual Camera. " +
                "Make sure to assign one first");
            return;
        }

        if (cm.Follow.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb2d))
        {
            targetRB = rb2d;
        }
        else
        {
            Debug.LogError(this.name + " has not found a Rigidbody2D in the Follow Target");
            return;
        }
    }

    void Start()
    {
        if (parallaxCamera == null)
            if (Camera.main.TryGetComponent(out ParallaxCamera parallaxCam))
            {
                parallaxCamera = parallaxCam;
            }
            else
            {
                Debug.LogError("Main camera has no Parallax Camera assigned");
            }
        if (parallaxCamera != null)
            parallaxCamera.OnParallaxCamera += Move;
    }

    /// <summary>
    /// Move will be called when the camera detects that its position has changed.
    /// To use Repeating Background your first need to go to the Image Import Settings
    /// And change the "Wrap Mode" To Repeat. This will ensure that the image loops properly.
    /// </summary> 
    /// <param name="delta">Delta is the difference from its previous position compared to the current one</param>
    private void Move(float delta)
    {
        
        if (hasRepeatingBackground)
        {
            offset = (targetRB.velocity.x * 0.1f) * (scrollSpeed / 100) * Time.deltaTime;
            material.mainTextureOffset += offset;
        }
        else
        {
            Vector3 newPos = transform.localPosition;
            newPos.x += delta * (scrollSpeed.x / 10);
            newPos.y += delta * (scrollSpeed.y / 10);
            transform.localPosition = newPos;
        }
        
    }
}
