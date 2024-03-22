using TMPro;
using UnityEngine;

public class PanelMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textLevel;

    public void Init() 
    {

    }

    public void Show(int level) 
    {
        gameObject.SetActive(true);
        _textLevel.text = $"LEVEL {level + 1}";
    }

    public void Hide() 
    {
        gameObject.SetActive(false);
    }
}
