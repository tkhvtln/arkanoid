using UnityEngine;

public class Walls : MonoBehaviour
{
    public float ÑlampLeft { get; private set; }
    public float ÑlampRight { get; private set; }

    [SerializeField] private Transform _trWallLeft;
    [SerializeField] private Transform _trWallRight;
    [SerializeField] private Transform _trWallBottom;
    [SerializeField] private Transform _trWallTop;

    private Transform this[int i]
    {
        get
        {
            return i switch
            {
                0 => _trWallLeft,
                1 => _trWallRight,
                2 => _trWallBottom,
                3 => _trWallTop,
                _ => _trWallTop,
            };
        }

        set
        {
            switch (i)
            {
                case 0:
                    _trWallLeft = value;
                    break;
                case 1:
                    _trWallRight = value;
                    break;
                case 2:
                    _trWallBottom = value;
                    break;
                case 3:
                    _trWallTop = value;
                    break;
                default:
                    _trWallTop = value;
                    break;
            }
        }
    }

    public void Init()
    {
        SetBorders();
    }

    private void SetBorders()
    {
        float rayDistance = Mathf.Infinity;
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main); //Ordering: [0] = Left, [1] = Right, [2] = Down, [3] = Up, [4] = Near, [5] = Far

        for (int i = 0; i < 4; i++)
        {
            Vector3 vecToWall = transform.position - this[i].position;
            Ray ray = new Ray(this[i].position, vecToWall);

            if (planes[i].Raycast(ray, out float distance))
                rayDistance = distance;

            rayDistance = Mathf.Clamp(rayDistance, 0, vecToWall.magnitude);
            this[i].position = ray.GetPoint(rayDistance);
        }

        ÑlampLeft = _trWallLeft.position.x + _trWallLeft.localScale.x / 2;
        ÑlampRight = _trWallRight.position.x - _trWallRight.localScale.x / 2;
    }
}
