using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject player;
    public float followAhead;
    public float followAbove;
   
    public float smoothing;

    private Vector3 _targetPosition;
    private float zPosition;

    // Use this for initialization
    void Start()
    {
        zPosition = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.localScale.x > 0f)
        {
            _targetPosition = new Vector3(player.transform.position.x + followAhead, player.transform.position.y + followAbove, zPosition);
        }
        else
        {
            _targetPosition = new Vector3(player.transform.position.x - followAhead, player.transform.position.y + followAbove, zPosition);
        }

        transform.position = Vector3.Lerp(transform.position, _targetPosition, smoothing * Time.deltaTime);
    }
}
