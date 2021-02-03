using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CanvasController : MonoBehaviour
{
    public static CanvasController instance;

    [SerializeField] private GameObject fishingPanel;
    [SerializeField] private GameObject wantToFishPanel;
    GameObject fishToDestroy;

    [SerializeField] private GameObject barrelPanel;
    [SerializeField] private GameObject wantBarrelPanel;
    GameObject barrelToDestroy;

    [SerializeField] private GameObject collectTotemPanel;
    [SerializeField] private GameObject wantToCollectPanel;
    GameObject totemToDestroy;

    [SerializeField] private GameObject chestPanel;
    private TextMeshProUGUI chestText;

    [SerializeField] private GameObject chefNote;

    public Slider shield1, shield2, shield3, shield4, healthBar,fishing,collecting, barrelSlider;
    [SerializeField] private int damageCounter = 0;
    public float healthAmount = 4;

    public Slider witchHealthBar;
    [SerializeField] private GameObject witchStats;

    [SerializeField]private float immortalityAmount = 2f;
    private bool canTakeDamage = true;

    [SerializeField] private GameObject LevelCompletePanel, GameOverPanel;
    public bool isGameRunning = true;

    [SerializeField] private GameObject DiscoverPanel;
    [SerializeField] private Sprite fishDiscover, buoyDiscover, chestDiscover, barrelDiscover;
    [SerializeField] private Image dialogDiscover;
    private bool fishDisc = false, barrelDisc = false, buoyDisc = false, chestDisc = false;

    bool isFishing = false;
    bool isBarrel = false;
    bool isCollectingTotem = false;
    bool isCollected = false;
    bool startActivity = true;

    public float FillTime = 5.0f;
    void Awake()
    {
        instance = this;
        chestText = chestPanel.GetComponent<TextMeshProUGUI>();
        
       
    }

    

    private void Update()
    {
        if (isFishing)
        {
            if (startActivity)
            {
                ResetSlider(fishing);
            }
            if (Input.GetKey(KeyCode.F))
            {
                startActivity = false;
                wantToFishPanel.SetActive(false);
                fishingPanel.SetActive(true);
                fishing.value = Time.time;
                if (fishing.value == fishing.maxValue)
                {
                    isFishing = false;
                    UpdateHealthBar("inc");
                    wantToFishPanel.SetActive(false);
                    fishingPanel.SetActive(false);
                    Destroy(fishToDestroy);
                    startActivity = true;

                }
            }
            if (Input.GetKeyUp(KeyCode.F))
            {
                ResetSlider(fishing);
                startActivity = true;
                fishingPanel.SetActive(false);
                wantToFishPanel.SetActive(true);
            }

        }

        if (isCollectingTotem)
        {
            if (startActivity)
            {
                ResetSlider(fishing);
            }
            if (Input.GetKey(KeyCode.G))
            {
                startActivity = false;
                wantToCollectPanel.SetActive(false);
                collectTotemPanel.SetActive(true);
                collecting.value = Time.time;
                if (collecting.value == collecting.maxValue)
                {
                    isCollectingTotem = false;
                    wantToCollectPanel.SetActive(false);
                    collectTotemPanel.SetActive(false);
                    OpenChest.instance.AddTotem();
                    Destroy(totemToDestroy.GetComponent<Collider>());
                    startActivity = true;
                }
            }
            if (Input.GetKeyUp(KeyCode.G))
            {
                startActivity = true;
                ResetSlider(collecting);
                collectTotemPanel.SetActive(false);
                wantToCollectPanel.SetActive(true);
            }

        }

        if (isBarrel)
        {
            if (startActivity)
            {
                ResetSlider(barrelSlider);
            }
            if (Input.GetKey(KeyCode.F))
            {
                startActivity = false;
                wantBarrelPanel.SetActive(false);
                barrelPanel.SetActive(true);
                barrelSlider.value = Time.time;
                if (barrelSlider.value == barrelSlider.maxValue)
                {
                    isBarrel = false;
                    wantBarrelPanel.SetActive(false);
                    barrelPanel.SetActive(false);
                    Destroy(barrelToDestroy.GetComponent<Collider>());
                    startActivity = true;
                }
            }
            if (Input.GetKeyUp(KeyCode.F))
            {
                startActivity = true;
                ResetSlider(barrelSlider);
                barrelPanel.SetActive(false);
                wantBarrelPanel.SetActive(true);
            }

        }

        if (isCollected)
        {
            if (Input.GetKey(KeyCode.G))
            {
                OpenChest.instance.StopParticles();
                OpenChest.instance.OpenChestCap();
                DOVirtual.DelayedCall(1.50f, () => { chefNote.SetActive(true); });
                
            }
           

        }

    }
    public void UpdateHealthBar(string action)
    {
        if (!isGameRunning) return;
        if (!canTakeDamage) return;

        if (action == "dec")
        {
            if (damageCounter < 4)
            {
                switch (damageCounter)
                {
                    case 0:
                        shield4.value = 0;
                        break;
                    case 1:
                        shield3.value = 0;
                        break;
                    case 2:
                        shield2.value = 0;
                        break;
                    case 3:
                        shield1.value = 0;
                        break;
                }
                canTakeDamage = false;
                DOVirtual.DelayedCall(immortalityAmount, () => { canTakeDamage = true; });
                damageCounter++;
            }
            else
            {
                Debug.Log(healthAmount);
                healthAmount--;
                healthBar.value = healthAmount/4;
                if (healthAmount == 0)
                {
                    ShowGameOver();
                }
                else
                {
                    canTakeDamage = false;
                    DOVirtual.DelayedCall(immortalityAmount, () => { canTakeDamage = true; });
                }
            }
        }
        else if (action == "inc")
        {
            if (healthBar.value != 1)
            {
                healthAmount++;
                healthBar.value = healthAmount / 4f;
            }
        }
        else if (action == "start")
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
                PlayerPrefs.SetFloat("Life", 1);

            if (!PlayerPrefs.HasKey("Life"))
                PlayerPrefs.SetFloat("Life", healthBar.value);

            healthBar.value = PlayerPrefs.GetFloat("Life");
        }
        else if (action == "end")
        {  
            PlayerPrefs.SetFloat("Life", healthBar.value);
        }
        
    }

    public void UpdateWitchHealth(float health)
    {
        if (!isGameRunning) return;

        if (health == 0)
        {
            witchStats.SetActive(false);
        }

        witchHealthBar.value = health;
        
        
            
        
    }

    public void RetryClicked()
    {
        isGameRunning = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel()
    {
        Debug.Log("loadnext");
        isGameRunning = true;
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        nextIndex %= SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextIndex);
    }

    private void ShowGameOver()
    {
        isGameRunning = false;
        GameOverPanel.SetActive(true);
    }

    public void ShowLevelComplete()
    {
        isGameRunning = false;
        LevelCompletePanel.SetActive(true);
    }

    public void UpdateFishSlider(string action, GameObject obj)
    {
        if (action == "press")
        {
            wantToFishPanel.SetActive(true);
            ResetSlider(fishing);
            isFishing = true;
            fishToDestroy = obj;
        }
        if (action == "stop")
        {
            fishingPanel.SetActive(false);
            wantToFishPanel.SetActive(false);
            isFishing = false;
            ResetSlider(fishing);
        }
    
    }

    public void UpdateTotemSlider(string action, GameObject obj)
    {
        if (action == "press")
        {
            wantToCollectPanel.SetActive(true);
            ResetSlider(collecting);
            isCollectingTotem = true;
            totemToDestroy = obj;
        }
        if (action == "stop")
        {
            wantToCollectPanel.SetActive(false);
            collectTotemPanel.SetActive(false);
            ResetSlider(collecting);
            isCollectingTotem = false;
        }

    }

    public void UpdateBarrelSlider(string action, GameObject obj)
    {
        if (action == "press")
        {
            wantBarrelPanel.SetActive(true);
            ResetSlider(barrelSlider);
            isBarrel = true;
            barrelToDestroy = obj;
        }
        if (action == "stop")
        {
            wantBarrelPanel.SetActive(false);
            barrelPanel.SetActive(false);
            ResetSlider(collecting);
            isBarrel = false;
        }

    }

    private void ResetSlider(Slider slider)
    {
        slider.minValue = Time.time;
        slider.maxValue = Time.time + FillTime;
    }
    

    public void ChestIntereaction(string action, GameObject obj, int count)
    {
        if (action == "locked")
        {
            if (count == 1)
            { chestText.text = "you have to find " + count.ToString() + " totem to open the chest"; }
            else
            chestText.text = "you have to find " + count.ToString() + " totems to open the chest";
            
            chestPanel.SetActive(true);
            //chestToDestroy = obj;
        }
        if (action == "out")
        {
            chestPanel.SetActive(false);
        }
        if (action == "open")
        {
            isCollected = true;
            chestText.text = "Press G to open the Chest";
            chestPanel.SetActive(true);


        }
        

    }

    public void OpenDiscoverDialogs(string discover)
    {
      
       if (discover == "fish" && !fishDisc)
        {
            fishDisc = true;
            dialogDiscover.sprite = fishDiscover;
            DiscoverPanel.SetActive(true);
        }
        else if (discover == "barrel" && !barrelDisc)
        {
            barrelDisc = true;
            dialogDiscover.sprite = barrelDiscover;
            DiscoverPanel.SetActive(true);
        }
        else if (discover == "buoy" && !buoyDisc)
        {
            buoyDisc = true;
            dialogDiscover.sprite = buoyDiscover;
            DiscoverPanel.SetActive(true);
        }
        else if (discover == "chest" && !chestDisc)
        {
            chestDisc = true;
            dialogDiscover.sprite = chestDiscover;
            DiscoverPanel.SetActive(true);
        }
        

    }

    public void CalcHealth()
    {
        healthAmount = PlayerPrefs.GetFloat("Life")*4;
    }



}