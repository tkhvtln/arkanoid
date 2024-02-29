using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickCreator : MonoBehaviour
{
    [SerializeField] private Brick _brickPrefab;
    [SerializeField] private Texture2D _texBrick;

    void Start()
    {
        Create();
    }

    private void Create()
    {
        for (int x = 0; x < _texBrick.width; x++)
        {
            for (int y = 0;  y < _texBrick.height; y++)
            {
                Color color = _texBrick.GetPixel(x, y);
                if (color.a == 0) continue;

                Brick brick = Instantiate(_brickPrefab, new Vector3(x, y, 0), Quaternion.identity, transform);
                brick.SetColor(color);
            }
        }

        transform.position = new Vector3(-_texBrick.width / 2, _texBrick.width / 8, 0);
    }
}
