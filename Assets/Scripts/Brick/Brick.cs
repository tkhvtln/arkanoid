using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;

    public void SetColor(Color color)
    {
        _meshRenderer.material.color = color;
    }
}
