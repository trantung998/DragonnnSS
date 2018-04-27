using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        print(player.ToString());
    }

    private void LateUpdate()
    {
        transform.position = player.transform.position;
    }
}
