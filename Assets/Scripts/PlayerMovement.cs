using System;
using UnityEngine;
using UnitySampleAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    CameraRaycaster cameraRaycaster;

    ThirdPersonCharacter m_Character;   // A reference to the ThirdPersonCharacter on the object
    [SerializeField]
    Vector3 currentClickTarget;
        
    private void Start()
    {
        //cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(cameraRaycaster.gameObject.transform.position, currentClickTarget);
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        
        if (Input.GetMouseButton(0))
        {
            var layerhit = cameraRaycaster.layerHit;
            switch (layerhit)
            {
                case Layer.RaycastEndStop:
                    print("Unreachable");
                    break;
                case Layer.Enemy:
                    print("enemy");
                    break;
                case Layer.Walkable:
                    currentClickTarget = cameraRaycaster.hit.point;
                    m_Character.Move(currentClickTarget - transform.position, false, false, transform.position + transform.forward * 100);
                    break;
                default:
                    print("Unreachable");
                    return;
            }
            
        }
        
    }
}

