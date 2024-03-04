using UnityEngine;

public class Walls : MonoBehaviour
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
