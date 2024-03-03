using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickCreator : MonoBehaviour
{
    [SerializeField] private Brick _brickPrefab;

    private Texture2D _texBrick;

    private int _countBricks = 0;
    private int _destroyBricks = 0;

    public void Init(Texture2D texBrick)
    {
        _texBrick = texBrick;

        Create();
        Brick.OnDestroy.AddListener(() =>
        {
            if (++_destroyBricks >= _countBricks)
                GameController.Instance.Win();
        });
    }

    private void Create()
    {
        for (int x = 0; x < _texBrick.width; x++)
        {
            for (int y = 0;  y < _texBrick.height; y++)
            {
                Color color = _texBrick.GetPixel(x, y);
                if (color.a < 0.5f) continue;

                Brick brick = Instantiate(_brickPrefab, new Vector3(x, y, 0), Quaternion.identity, transform);
                brick.SetColor(color);

                _countBricks++;
            }
        }

        transform.position = new Vector3(-_texBrick.width / 2, 0, 0);
    }
}
