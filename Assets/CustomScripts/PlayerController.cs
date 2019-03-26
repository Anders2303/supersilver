using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    
    public float speed = 50;

    private Camera pCamera;
    private Vector2 pInput;
    private CharacterController pCharacterController;
    private Vector3 pMoveDir = Vector3.zero;

    // Start is called before the first frame update
    void Start() {
        if(!isLocalPlayer) {
            GetComponentInChildren<Camera>().enabled = false;
            return;
        }

        pCharacterController = GetComponent<CharacterController>();
    }

    public override void OnStartLocalPlayer() {
        base.OnStartLocalPlayer();

        GetComponent<MeshRenderer>().material.color = Color.red;
        pCamera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update() {

        // Check if is local player.
        if (!isLocalPlayer) {
            return;
        }
    }

    private void FixedUpdate() {
        
        // Check if is local player.
        if(!isLocalPlayer) {
            return;
        }

        GetInput();

        // always move along the camera forward as it is the direction that it being aimed at
        Vector3 desiredMove = transform.forward * pInput.y + transform.right * pInput.x;

        // get a normal for the surface that is being touched to move along it
        RaycastHit hitInfo;
        Physics.SphereCast(transform.position, pCharacterController.radius, Vector3.down, out hitInfo,
                           pCharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
        desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

        pMoveDir.x = desiredMove.x * speed;
        pMoveDir.z = desiredMove.z * speed;

    }


    private void GetInput() {

        // Read input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");


        pInput = new Vector2(horizontal, vertical);

        // normalize input if it exceeds 1 in combined length:
        if (pInput.sqrMagnitude > 1) {
            pInput.Normalize();
        }

    }
}
