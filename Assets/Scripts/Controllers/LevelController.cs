using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public float clampLeft { get; private set; }
    public float clampRight { get; private set; }

    [SerializeField] private BrickCreator _brickCreator;
    [SerializeField] private Walls _walls;
    

    void Start() 
    {
        GameController.Instance.ControllerLevel = this;

        SetBorders();
        _brickCreator.Init();
    }

    private void SetBorders()
    {
        float rayMinDistance = Mathf.Infinity;
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        for (int i = 0; i < 4; i++)
        {
            Vector3 vecToWall = transform.position - _walls[i].position;
            Ray ray = new Ray(_walls[i].position, vecToWall);
           
            for (int p = 0; p < 4; p++)
                if (planes[p].Raycast(ray, out float distance))
                    if (distance < rayMinDistance)
                        rayMinDistance = distance;

            rayMinDistance = Mathf.Clamp(rayMinDistance, 0, vecToWall.magnitude);
            _walls[i].position = ray.GetPoint(rayMinDistance);
        }

        clampLeft = _walls.trWallLeft.position.x + _walls.trWallLeft.localScale.x / 2;
        clampRight = _walls.trWallRight.position.x - _walls.trWallRight.localScale.x / 2;
    }
}

[Serializable]
struct Walls
{   
    public Transform trWallLeft;
    public Transform trWallRight;
    public Transform trWallBottom;
    public Transform trWallTop;

    public Transform this[int i]
    {
        get
        {
            return i switch 
            {              
                0 => trWallLeft,
                1 => trWallRight,
                2 => trWallBottom,
                3 => trWallTop,
                _ => trWallTop,
            };
        }

        set
        {
            switch (i)
            {
                case 0:
                    trWallLeft = value;
                    break;
                case 1:
                    trWallRight = value;
                    break;
                case 2:
                    trWallBottom = value;
                    break;
                case 3:
                    trWallTop = value;
                    break;
                default:
                    trWallTop = value;
                    break;
            }
        }
    }
}
