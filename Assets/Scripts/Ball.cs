using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private float _speed;

    private Rigidbody _rb;
    private Vector3 _vecVelocity;

    public void Init(float speed)
    {
        gameObject.SetActive(true);

        _speed = speed;

        _rb = GetComponent<Rigidbody>();
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        
        transform.position = new Vector3(0, -5, 0);
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(-60, 60));
        
        GameController.OnGame.AddListener(() => _rb.velocity = -transform.up * _speed);
    }

    void FixedUpdate()
    {
        if (!GameController.Instance.IsGame) return;

        _rb.velocity = _rb.velocity.normalized * _speed;
        _vecVelocity = _rb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 vecReflect = Vector3.Reflect(_vecVelocity, collision.contacts[0].normal);
        if (Vector3.Angle(vecReflect, collision.contacts[0].normal) < 10)
            vecReflect = Quaternion.Euler(0, 0, 10) * vecReflect;

        _rb.velocity = vecReflect;

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
