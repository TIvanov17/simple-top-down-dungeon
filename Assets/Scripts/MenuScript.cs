using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    //  Text fields
    public Text levelText;
    public Text healthText;
    public Text goldText;
    public Text upgradeCostText;
    public Text xpText;

    // Logic fields
    private int indexOfCurrentCharacter = 0;
    public Image currentCharacterSprite;
    public Image currentWeaponSprite;
    public RectTransform xpBar;

    public void OnArrowClick(bool isRight)
    {
        indexOfCurrentCharacter = isRight? 
            indexOfCurrentCharacter + 1:
            indexOfCurrentCharacter - 1;

        CheckForCorrectIndexSprite();
        ChangeCharacterSprite();
    }


    private void ChangeCharacterSprite()
    {
        currentCharacterSprite.sprite = GameManager.instance.playerSprites[indexOfCurrentCharacter];
        GameManager.instance.player.ChangeSprite(indexOfCurrentCharacter);
    }

    private void CheckForCorrectIndexSprite()
    {
        Debug.Log(indexOfCurrentCharacter == GameManager.instance.playerSprites.Count);
       
        if (indexOfCurrentCharacter == GameManager.instance.playerSprites.Count)
        {
            indexOfCurrentCharacter = 0;
            return;
        }
        if (indexOfCurrentCharacter < 0)
        {
            indexOfCurrentCharacter = GameManager.instance.playerSprites.Count - 1;
        }

    }

    // Weapon Upgrade
    public void OnUpgradeClick()
    {
        if (GameManager.instance.TryUpgradeWeapon())
        {
            UpdateMenu();
        }
    }

    // Update character info
    public void UpdateMenu()
    {
        int weaponIndex = GameManager.instance.currentWeapon.weaponLevel;
        int currentLevel = GameManager.instance.GetCurrentLevel();

        SetWeapon(weaponIndex);
        SetMetaInfo(currentLevel);
        SetXp(currentLevel);
    }
    
    private void SetWeapon(int weaponIndex)
    {
        // Weapon
        currentWeaponSprite.sprite = GameManager.instance.weaponSprites[weaponIndex];
        upgradeCostText.text = GameManager.instance.weaponPrices[weaponIndex].ToString();
    }

    private void SetMetaInfo(int currentLevel)
    { 
        // Meta
        levelText.text = currentLevel.ToString();
        healthText.text = GameManager.instance.player.health.ToString();
        goldText.text = GameManager.instance.gold.ToString();
    }

    private void SetXp(int currentLevel)
    {
        // XP
        if (currentLevel == GameManager.instance.xpTable.Count)
        {
            xpText.text = GameManager.instance.experience.ToString() + " total xp.";
            return;
        }

        SetXpOnCurrentLevel(currentLevel);
    }

    private void SetXpOnCurrentLevel(int currentLevel)
    {
        int prevLevelXp = GameManager.instance.GetXpToLevel(currentLevel - 1);
        int currentLevelXp = GameManager.instance.GetXpToLevel(currentLevel);

        int diff = currentLevelXp - prevLevelXp;
        int currentXpIntoLevel = GameManager.instance.experience - prevLevelXp;

        float completionRatio = (float)currentXpIntoLevel / (float)diff;

        xpBar.localScale = new Vector3(completionRatio, 1, 1);
        xpText.text = currentXpIntoLevel.ToString() + " / " + diff;
    }

}
