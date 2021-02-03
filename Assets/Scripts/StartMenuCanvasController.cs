using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuCanvasController : MonoBehaviour
{
    [SerializeField] private GameObject storyPanel;

    public void PlayStory()
    {
        storyPanel.SetActive(true);
    }
}
