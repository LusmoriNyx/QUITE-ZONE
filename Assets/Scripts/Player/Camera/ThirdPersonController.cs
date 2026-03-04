using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    [SerializeField] private Transform point2;
    [SerializeField] private Transform player;
    [SerializeField] private Transform cameraPlayer;

    [SerializeField] private float sensitivityMouse = 250f;
    private float _rotationX = 0f;
    private float _rotationY = 0f;
    private float radius;

    private bool _isActive = false;
    //private Vector2 degrees;
    private void Start()
    {
        radius = Vector3.Distance(player.position, point2.position);
        EventBus.AddListener("ThirdPersonCameraMode", ThirdPersonCameraMode);
        EventBus.AddListener("SwitchCameraMode", DisableThirdPersonCameraMode);
    }
    private void Update()
    {
        if (_isActive)
        {
            RotationCamera();
        }
    }
    private void OnDestroy()
    {
        EventBus.RemoveListener("ThirdPersonCameraMode", ThirdPersonCameraMode);
        EventBus.RemoveListener("SwitchCameraMode", DisableThirdPersonCameraMode);
    }
    private void ThirdPersonCameraMode(object data)
    {
        if(!_isActive)
        {
            cameraPlayer.position = point2.position;
            _isActive = true;
        }
    }
    private void DisableThirdPersonCameraMode(object data)
    {
        _isActive = false;
    }
    private void RotationCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivityMouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityMouse * Time.deltaTime;

        //Bieu dien truc quay X trong khong gian 3D (len /xuong)
        _rotationX -= mouseY;
        //Gioi han goc quay X tu -90 do den 90 do
        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);

        //Bieu dien truc quay Y trong khong gian 3D (trai /phai)
        _rotationY += mouseX;

        //Tao rotation tu 2 truc quay X va Y
        Quaternion rotation = Quaternion.Euler(_rotationX, _rotationY, 0f);
        Vector3 distance = player.position + (rotation * new Vector3(0f, 0f, -radius));
        cameraPlayer.position = distance;
        cameraPlayer.LookAt(player);
    }
}
