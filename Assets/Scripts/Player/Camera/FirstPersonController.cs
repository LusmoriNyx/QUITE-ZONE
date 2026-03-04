using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [SerializeField] private Transform point1;

    [SerializeField] private Transform cameraPlayer;
    [SerializeField] private Transform player;
    [SerializeField] private float sensitivityMouse = 250f;
    private float _rotationX = 0f; //Giới hạn nhìn lên/xuống

    private bool _isActive = true;
    private void Start()
    {
        EventBus.AddListener("SwitchCameraMode", FirstPersonCameraMode);
        EventBus.AddListener("ThirdPersonCameraMode", DisableFirstPersonCameraMode);
    }
    private void Update()
    {
        if (_isActive)
        {
            HandleLookFirstPerson();
        }
    }
    private void OnDestroy()
    {
        EventBus.RemoveListener("SwitchCameraMode", FirstPersonCameraMode);
        EventBus.RemoveListener("ThirdPersonCameraMode", DisableFirstPersonCameraMode);
    }
    private void FirstPersonCameraMode(object data)
    {
        if(!_isActive)
        {
            cameraPlayer.position = point1.position;
            _isActive = true;
        }
    }
    private void DisableFirstPersonCameraMode(object data)
    {
        _isActive = false;
    }
    private void HandleLookFirstPerson()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivityMouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityMouse * Time.deltaTime;

        _rotationX -= mouseY;
        _rotationX = Mathf.Clamp(_rotationX, -70f, 90f);

        cameraPlayer.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
    }
}
