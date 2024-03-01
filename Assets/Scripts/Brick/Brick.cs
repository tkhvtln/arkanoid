using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Brick : MonoBehaviour
{
    public static UnityEvent OnDestroy = new UnityEvent();

    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private ParticleSystem _destroyEffect;

    public void SetColor(Color color)
    {
        _meshRenderer.material.color = color;
        _destroyEffect.GetComponent<ParticleSystemRenderer>().material.color = color;  
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Constants.TAG_BALL))
        {
            OnDestroy?.Invoke();

            _destroyEffect.Play();
            _destroyEffect.transform.parent = transform.parent;

            GameController.Instance.ControllerSound.PlaySound(SoundName.DESTROY);

            gameObject.SetActive(false);
        }
    }   
}
