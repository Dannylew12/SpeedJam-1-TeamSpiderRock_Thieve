using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rigidBody;
    private CapsuleCollider capsuleCollider;
    private Camera camera;
    public float mouseSensitivity = 1f;
    private FootSteps footSteps;
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        camera = transform.GetChild(0).GetComponent<Camera>();

        /// lock mouse for nice fps view
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        footSteps = transform.GetComponentInChildren<FootSteps>();
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
            {
                curSpeed = runSpeed;
                if (floor == SurfaceType.FLOORTYPE.STONE)
                    footSteps.SetAudio(FootSteps.SFXTYPE.SR);
                else
                    footSteps.SetAudio(FootSteps.SFXTYPE.WR);
            }
            else
            {
                if (floor == SurfaceType.FLOORTYPE.STONE)
                    footSteps.SetAudio(FootSteps.SFXTYPE.SW);
                else
                    footSteps.SetAudio(FootSteps.SFXTYPE.WW);
            }

            /// check for crouching state
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                capsuleCollider.height = 1f;
                Vector3 nPos = camera.transform.position;
                nPos.y -= .5f;
                camera.transform.position = nPos;
                nPos = transform.position;
                nPos.y -= 1f;
                transform.position = nPos;
            }
            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                capsuleCollider.height = 2f;
                Vector3 nPos = camera.transform.position;
                nPos.y += .5f;
                camera.transform.position = nPos;
                nPos = transform.position;
                nPos.y += 1f;
                transform.position = nPos;
            }
            if (Input.GetKey(KeyCode.LeftControl))
                curSpeed = crouchSpeed;

            float forwDir = Input.GetAxis("ver");
            float strfDir = Input.GetAxis("hor");
            forwDir = forwDir > 0 ? 1 : (forwDir < 0 ? -1 : 0);
            strfDir = strfDir > 0 ? 1 : (strfDir < 0 ? -1 : 0);
            Vector3 movementDirection = (forwDir * transform.forward + strfDir * transform.right).normalized;
            rigidBody.velocity = curSpeed * movementDirection;

            if (rigidBody.velocity.sqrMagnitude != 0)
                footSteps.PlayAudio();
            else
                footSteps.StopAudio();
        }
    }

    private SurfaceType.FLOORTYPE floor = SurfaceType.FLOORTYPE.DEFAULT;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Floor")
        {
            floor = collision.collider.GetComponent<SurfaceType>().floor;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Floor")
        {
            floor = collision.collider.GetComponent<SurfaceType>().floor;
            if (floor == SurfaceType.FLOORTYPE.WOOD)
                floor = SurfaceType.FLOORTYPE.STONE;
            else if (floor == SurfaceType.FLOORTYPE.STONE)
                floor = SurfaceType.FLOORTYPE.WOOD;
        }
    }
	[SerializeField] private TMP_Text loseText;
    public void Lose()
    {
        /// PAUSE THE GAME
        Time.timeScale = 0f;
        loseText.gameObject.SetActive(true);
    }
}

