using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float _speed = 20;

    private Rigidbody _rb;
    private Vector3 _vecVelocity;

    public void Init()
    {
        gameObject.SetActive(true);

        _rb = GetComponent<Rigidbody>();
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(-60, 60));
        
        GameController.OnGame.AddListener(() => _rb.velocity = -transform.up * _speed);
    }

    void FixedUpdate()
    {
        _rb.velocity = _rb.velocity.normalized * _speed;
        _vecVelocity = _rb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        _rb.velocity = Vector3.Reflect(_vecVelocity, collision.contacts[0].normal);

        if (collision.gameObject.CompareTag(Constants.TAG_BOTTOM))
        {
            GameController.Instance.Defeat();
            gameObject.SetActive(false);
        }

        if (!collision.gameObject.CompareTag(Constants.TAG_BRICK) ||
            !collision.gameObject.CompareTag(Constants.TAG_BOTTOM))
        {
            GameController.Instance.ControllerSound.PlaySound(SoundName.COLLISION);
        }
    }
}
