using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public float clampLeft { get; private set; }
    public float clampRight { get; private set; }

    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private BrickCreator _brickCreator;
    [SerializeField] private Walls _walls;
    

    void Start() 
    {
        GameController.Instance.ControllerLevel = this;     
    }

    public void Init(int level)
    {
        SetBorders();
        _brickCreator.Init(_levelConfig.TextureBricks[level % _levelConfig.TextureBricks.Length]);
    }

    private void SetBorders()
    {
        float rayDistance = Mathf.Infinity;     
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main); //Ordering: [0] = Left, [1] = Right, [2] = Down, [3] = Up, [4] = Near, [5] = Far

        for (int i = 0; i < 4; i++)
        {
            Vector3 vecToWall = transform.position - _walls[i].position;
            Ray ray = new Ray(_walls[i].position, vecToWall);
           
            if (planes[i].Raycast(ray, out float distance))
                rayDistance = distance;

            rayDistance = Mathf.Clamp(rayDistance, 0, vecToWall.magnitude);
            _walls[i].position = ray.GetPoint(rayDistance);
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
