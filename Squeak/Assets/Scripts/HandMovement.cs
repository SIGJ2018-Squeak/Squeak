using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMovement : MonoBehaviour {

    public float handMoveSpeed;
    public float moveDuration;
    public int dir;

    private float _handMovementTimer;

	// Use this for initialization
	void Start () {
        dir = 1;
        _handMovementTimer = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        _handMovementTimer += Time.deltaTime;

        if(dir == 1)
        {
            if(_handMovementTimer > moveDuration)
            {
                dir = -1;
                _handMovementTimer = 0f;
            }
            else
            {
                transform.Translate(1f * Time.deltaTime * handMoveSpeed, 0f, 0f);
            }
        }
        else
        {
            if (_handMovementTimer > moveDuration)
            {
                dir = 1;
                _handMovementTimer = 0f;
            }
            else
            {
                //transform.Translate(-1f * Time.deltaTime * handMoveSpeed, 0f, 0f);
                transform.Translate(-1f * Time.deltaTime * handMoveSpeed, 0f, 0f);
            }
        }
	}
}
