using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private Transform _trPlatform;
    [SerializeField] private Ball _ball;

    private Vector3 _vecMovement;

    private Rigidbody _rb;
    private Camera _camera;
    
    private Transform _trCamera;
    private Transform _transform;

    private float _clampLeft;
    private float _clampRight;

    #region MOBILE_DEVICE_VARIABLES
    private float _deltaX = 0;
    private float _currentX = 0;
    #endregion

    private LevelController _levelController => GameController.Instance.ControllerLevel;

    public void Init()
    {
        _camera = Camera.main;
        _trCamera = _camera.transform;
        _transform = transform;
        _vecMovement = Vector3.zero;
        _rb = GetComponent<Rigidbody>();

        _clampLeft = _levelController.ÑlampLeft + _trPlatform.localScale.x / 2;
        _clampRight = _levelController.ÑlampRight - _trPlatform.localScale.x / 2;

        _transform.position = new Vector3(0, _transform.position.y, 0);
        Vector3 vecPostionSpawnBall = transform.position + new Vector3(0, _trPlatform.localScale.y / 2, 0);

        _ball.Init(_playerConfig.SpeedBall, vecPostionSpawnBall);     
    }

    void Update()
    {
        if (!GameController.Instance.IsGame) return;

#if UNITY_EDITOR
        GetInputDesktop();
#endif

#if UNITY_ANDROID || UNITY_IOS
        GetInputMobile();
#endif

        _vecMovement = new Vector3(Mathf.Clamp(_vecMovement.x, _clampLeft, _clampRight), _transform.position.y, 0);
    }

    void FixedUpdate()
    {
        if (!GameController.Instance.IsGame) return;

        _rb.MovePosition(_vecMovement);
    }

    private void GetInputDesktop()
    {
        float x = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, -_trCamera.position.z)).x;
        _vecMovement =  new Vector3(x, _transform.position.y, 0);
    }

    private void GetInputMobile()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            _currentX = _camera.ScreenToWorldPoint(new Vector3(touch.position.x, 0, -_trCamera.position.z)).x;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _deltaX = _currentX - transform.position.x;
                    break;

                case TouchPhase.Moved:
                    _vecMovement = new Vector3(_currentX - _deltaX, _transform.position.y, 0);
                    break;

                case TouchPhase.Ended:
                    break;
            }
        }
    }
}
