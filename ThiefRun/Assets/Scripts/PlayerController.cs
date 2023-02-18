using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rigidBody;
    private CapsuleCollider capsuleCollider;
    private Camera camera;
    public float mouseSensitivity = 1f;
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        camera = transform.GetChild(0).GetComponent<Camera>();

        /// lock mouse for nice fps view
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float crouchSpeed;
    private void Update()
    {
        /// camera and body rotation evaluated
        {
            float turn = Input.GetAxis("msx") * mouseSensitivity;
            transform.Rotate(0, turn, 0);

            Vector3 nRotation = camera.transform.localEulerAngles;
            float tilt = Input.GetAxis("msy") * mouseSensitivity;
            if (nRotation.x > 180f)
                nRotation.x -= 360f;
            float tiltEval = Mathf.Clamp(nRotation.x - tilt, -50, 50);
            nRotation.x = tiltEval;
            camera.transform.localEulerAngles = nRotation;
        }

        /// movement evaluated
        {
            float curSpeed = walkSpeed;
            if (Input.GetKey(KeyCode.LeftShift))
                curSpeed = runSpeed;

            /// check for crouching state
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                capsuleCollider.height = 1f;
                Vector3 nPos = camera.transform.position;
                nPos.y -= 1;
                camera.transform.position = nPos;
            }
            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                capsuleCollider.height = 2f;
                Vector3 nPos = camera.transform.position;
                nPos.y += 1;
                camera.transform.position = nPos;
            }
            if (Input.GetKey(KeyCode.LeftControl))
                curSpeed = crouchSpeed;

            float forwDir = Input.GetAxis("ver");
            float strfDir = Input.GetAxis("hor");
            forwDir = forwDir > 0 ? 1 : (forwDir < 0 ? -1 : 0);
            strfDir = strfDir > 0 ? 1 : (strfDir < 0 ? -1 : 0);
            Vector3 movementDirection = (forwDir * transform.forward + strfDir * transform.right).normalized;
            rigidBody.velocity = curSpeed * movementDirection;
        }
    }

}
