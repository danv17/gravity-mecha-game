using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void LateUpdate()
    {
        if (player == null || player.Equals(null))
            player = GameObject.FindGameObjectWithTag("Player");

        //transform.position = new Vector3(player.transform.position.x, 2.5f, -10f);
        transform.position = new Vector3(Mathf.Clamp(player.transform.position.x, 5.6f, 17.4f), 2.5f, -10f);
    }
}
