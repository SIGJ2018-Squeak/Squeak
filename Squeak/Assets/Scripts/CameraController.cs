using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject player;
    private PlayerMovement playerMovement;

    public float followAhead;
    public float followAbove;
   
    public float smoothing;

    private Vector3 _targetPosition;
    private float zPosition;

    // Use this for initialization
    void Start()
    {
        zPosition = transform.position.z;
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        float verticalFollowAhead;
        if (playerMovement.gravityReversed)
        {
            verticalFollowAhead = -followAbove;
        }
        else
        {
            verticalFollowAhead = followAbove;
        }

        if (player.transform.localScale.x > 0f)
        {
            _targetPosition = new Vector3(player.transform.position.x + followAhead, player.transform.position.y + verticalFollowAhead, zPosition);
        }
        else
        {
            _targetPosition = new Vector3(player.transform.position.x - followAhead, player.transform.position.y + verticalFollowAhead, zPosition);
        }

        transform.position = Vector3.Lerp(transform.position, _targetPosition, smoothing * Time.deltaTime);
    }
}
