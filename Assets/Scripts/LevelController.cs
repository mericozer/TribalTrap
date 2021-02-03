using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GoalIsland"))
        {
            CanvasController.instance.UpdateHealthBar("end");
            CanvasController.instance.ShowLevelComplete();
        }
    }
}
