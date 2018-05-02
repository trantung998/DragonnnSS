using System;
using UnityEngine;
using UnitySampleAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class ClickToMovePlayer : MonoBehaviour
{
    [SerializeField]
    private float stopMoveRadius = 0.2f;

    [SerializeField] private float atkRange = 1f;

    [SerializeField]
    CameraRaycaster cameraRaycaster;

    ThirdPersonCharacter m_Character;   // A reference to the ThirdPersonCharacter on the object
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

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, atkRange);

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
                    break;
                default:
                    print("Unreachable");
                    return;
            }
            
        }

        var distance = currentClickTarget - transform.position;
        if (distance.magnitude <= stopMoveRadius)
        {
            distance = Vector3.zero;
        }

        m_Character.Move(distance, false, false, transform.position + transform.forward * 100);
    }
}

