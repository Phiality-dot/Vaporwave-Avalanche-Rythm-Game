using UnityEngine;
using UnityEngine.UI;

public class FirstPersonController : MonoBehaviour
{
    public float speed = 0.0f;
    private float mouseSensitivity = PlayerInfo.sensetivity;
    public TMPro.TextMeshProUGUI countdownText;
    public Transform cameraTransform;
    private float countdownTime = 4.3f;
    private CharacterController characterController;
    private float verticalRotation = 0;
    private float verticalRotationMin = -90;
    private float verticalRotationMax = 90;
    private float HorRotation = 0f;
    public float teleportDistance = 4f;
    public float tapTime = 0.3f;
    public float cooldownTime = 3f;
    public ParticleSystem ps;
    private float lastTapTime = -100f;
    private bool shiftPressed = false;
    private bool onCooldown = false;

    void Start()
    {
        ps = cameraTransform.gameObject.GetComponentInChildren<ParticleSystem>();
        characterController = GetComponent<CharacterController>();
        Invoke("Unfreeze", countdownTime);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        countdownTime -= Time.deltaTime;
        countdownText.text = "Time until unfreeze: " + Mathf.Round(countdownTime).ToString();

        if (countdownTime <= 0)
        {
            // dashing
            if (!onCooldown)
            {
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    shiftPressed = true;
                    if (Input.GetKey(KeyCode.D))
                    {
                        transform.position += Vector3.right * teleportDistance;
                        onCooldown = true;
                        Invoke("ResetCooldown", cooldownTime);
                        ps.Play();
                        shiftPressed = false;
                    }
                    else if (Input.GetKey(KeyCode.A))
                    {
                        transform.position += Vector3.left * teleportDistance;
                        onCooldown = true;
                        Invoke("ResetCooldown", cooldownTime);
                        ps.Play();
                        shiftPressed = false;
                    }

                }
                else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
                {

                    shiftPressed = false;
                    lastTapTime = Time.time;

                }
                    if (shiftPressed)
                    {
                        if (Input.GetKey(KeyCode.D))
                        {
                            transform.position += Vector3.right * teleportDistance;
                            onCooldown = true;
                            Invoke("ResetCooldown", cooldownTime);
                        shiftPressed = false;
                        }
                        else if (Input.GetKey(KeyCode.A))
                        {
                            transform.position += Vector3.left * teleportDistance;
                            onCooldown = true;
                            Invoke("ResetCooldown", cooldownTime);
                            shiftPressed = false;
                        }
                    }
                
                
            }
            // end dashing
            countdownText.text = "";
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 moveDirection = new Vector3(horizontal, 0, vertical);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            characterController.SimpleMove(new Vector3(horizontal * speed, 0 , 1 * speed));

            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            
            HorRotation += mouseX * mouseSensitivity * Time.deltaTime;
            HorRotation = Mathf.Clamp(HorRotation, verticalRotationMin, verticalRotationMax);
            verticalRotation -= mouseY * mouseSensitivity * Time.deltaTime;
            verticalRotation = Mathf.Clamp(verticalRotation, verticalRotationMin, verticalRotationMax);
            cameraTransform.localRotation = Quaternion.Euler(verticalRotation,  HorRotation , 0);
        }
    }
    void ResetCooldown()
    {
        onCooldown = false;
    }
    void Unfreeze()
    {
        speed = 10.0f;
    }
}
