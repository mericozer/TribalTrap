using UnityEngine;
using UnityEngine.UI;

public class Retry : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            CanvasController.instance.RetryClicked();
        });
    }
}
