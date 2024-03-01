using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Ball _ball;
    [SerializeField] private Transform _trPlatform;

    private Vector3 _vecMove;

    private Rigidbody _rb;
    private Camera _camera;
    
    private Transform _trCamera;
    private Transform _transform;

    private LevelController _levelController => GameController.Instance.ControllerLevel;

    public void Init()
    {
        _camera = Camera.main;
        _trCamera = _camera.transform;
        _transform = transform; 

        _rb = GetComponent<Rigidbody>();        
        _transform.position = new Vector3(0, _transform.position.y, 0);

        _ball.Init();
    }

    void Update()
    {
        if (!GameController.Instance.IsGame) return;

        float x = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, -_trCamera.position.z)).x;
        x = Mathf.Clamp(x, _levelController.clampLeft + _trPlatform.localScale.x / 2, _levelController.clampRight - _trPlatform.localScale.x / 2);
        _vecMove = new Vector3(x, _transform.position.y, 0);
    }

    void FixedUpdate()
    {
        if (!GameController.Instance.IsGame) return;

        _rb.MovePosition(_vecMove);
    }
}
