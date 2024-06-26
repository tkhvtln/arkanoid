using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private TrailRenderer _trail;
    [SerializeField] private ParticleSystem _effectDestroy;

    private float _speed;

    private Rigidbody _rb;
    private Vector3 _vecVelocity;

    public void Init(float speed, Vector3 vecPosition)
    {
        _speed = speed;
        _effectDestroy.transform.parent = transform;

        transform.rotation = Quaternion.Euler(0, 0, Random.Range(-60, 60));
        transform.position = vecPosition + new Vector3(0, transform.localScale.y / 2 + 0.1f, 0);

        _rb = GetComponent<Rigidbody>();
        _rb.ResetVelocity();
  
        GameController.OnGame.AddListener(() => _rb.velocity = transform.up * _speed);
        GameController.OnWin.AddListener(() => gameObject.SetActive(false));

        gameObject.SetActive(true);
    }

    void FixedUpdate()
    {
        if (!GameController.instance.IsGame) return;

        _rb.velocity = _rb.velocity.normalized * _speed;
        _vecVelocity = _rb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 vecReflect = Vector3.Reflect(_vecVelocity, collision.contacts[0].normal);
        if (Vector3.Angle(vecReflect, collision.contacts[0].normal) < 1)
            vecReflect = Quaternion.Euler(0, 0, 10) * vecReflect;

        _rb.velocity = vecReflect;

        if (!collision.gameObject.CompareTag(Constants.TAG_BRICK) || !collision.gameObject.CompareTag(Constants.TAG_BOTTOM))
            GameController.instance.soundController.PlaySound(SoundName.COLLISION);

        if (collision.gameObject.CompareTag(Constants.TAG_BOTTOM))
        {
            _trail.Clear();
            _effectDestroy.transform.parent = null;
            _effectDestroy.Play();

            GameController.instance.Defeat();
            gameObject.SetActive(false);
        }        
    }
}
