using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hud);
            Destroy(menu);

            return;
        }

        instance = this; 
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    public Player player;
    public Weapon currentWeapon;
    public FloatingTextManager floatingTextManager;
    public RectTransform healthBar;
    public Animator deathMenuAnimator;
    public GameObject hud;
    public GameObject menu;

    public int gold;
    public int experience;

    public void SaveState()
    {
        string save = "";
        
        save += "0" + "|";
        save += gold.ToString() + "|";
        save += experience.ToString() + "|";
        save += currentWeapon.weaponLevel.ToString();
        
        PlayerPrefs.SetString("SaveState", save);

        Debug.Log("Save State");
    }

    public void LoadState(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState;
      
        if (!PlayerPrefs.HasKey("SaveState"))
        {    
            return;
        }
        
        string[] playerSaveData = PlayerPrefs.GetString("SaveState").Split('|');
        
        gold = int.Parse(playerSaveData[1]); 
        experience = int.Parse(playerSaveData[2]);
        currentWeapon.weaponLevel = int.Parse(playerSaveData[3]);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player.transform.position = GameObject.Find("TeleportSpot").transform.position;
    }

    public void ShowText(string message, int fontSize,
                         Color color, Vector3 position,
                         Vector3 motion, float duration)
    {
        floatingTextManager.Show(message, fontSize, 
                                 color, position, 
                                 motion, duration
                                 );

    }

    public bool TryUpgradeWeapon()
    {
        if(weaponPrices.Count <= currentWeapon.weaponLevel)
        {
            return false;
        }

        if(gold >= weaponPrices[currentWeapon.weaponLevel])
        {
            gold -= weaponPrices[currentWeapon.weaponLevel];
            currentWeapon.UpgradeWeapon();
            return true;
        }

        return false;
    }

    public void GrandXp(int xp)
    {
        int currentLevel = GetCurrentLevel();
        experience += xp;

        if(currentLevel < GetCurrentLevel())
        {
            OnLevelUp();
        }
    }

    private void OnLevelUp()
    {
        Debug.Log("Level up!");
        player.OnLevelUp();
        ChangeHealthBar();
    }

    public int GetXpToLevel(int level)
    {
        int currentLevel = 0;
        int xpAmount = 0;
        
        while(currentLevel < level)
        {
            xpAmount += xpTable[currentLevel++];
        }

        return xpAmount;
    }

    public int GetCurrentLevel()
    {
        int result = 0;
        int totalXp = 0;

        while(experience >= totalXp)
        {
            int currentXp = xpTable[result];
            
            totalXp += currentXp;
            result++;

            if (IsMaxLevel(result)) { return result; }   
        }

        return result;
    }

    public void ChangeHealthBar()
    {
        float ratio = player.health / (float) player.maxHealth;
        healthBar.localScale = new Vector3(ratio, 1, 1); 
    }

    public void Respawn()
    {
        deathMenuAnimator.SetTrigger("Hide");
        SceneManager.LoadScene("Main");
        player.Respawn();
    }

    private bool IsMaxLevel(int result)
    {
        return result == xpTable.Count;
    }
}