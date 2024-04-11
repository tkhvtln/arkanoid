using UnityEngine;

public class LevelController : MonoBehaviour
{
    public Walls Wall => _walls;

    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private BrickCreator _brickCreator;
    [SerializeField] private Walls _walls;
    
    void Start() 
    {
        if (GameController.instance != null)
            GameController.instance.levelController = this;     
    }

    public void Init(int level)
    {
        _walls.Init();
        _brickCreator.Init(_levelConfig.TextureBricks[level % _levelConfig.TextureBricks.Length]);
    }
}
