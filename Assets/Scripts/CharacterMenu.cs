using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterMenu : MonoBehaviour
{
    // Text fields
    public TextMeshProUGUI levelText, hitpointText, moneyText, upgradeCostText, xpText;

    //Logic
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    // Character Selection
    public void OnArrowClick(bool right)
    {
        if (right)
        {
            currentCharacterSelection++;
            //if ran out of characters
            if(currentCharacterSelection == GameManager.instance.playerSprites.Count)
                currentCharacterSelection = 0; //reset to 0

            OnSelectionChange();
        }
        else
        {
            currentCharacterSelection--;
            //if ran out of characters
            if(currentCharacterSelection < 0)
                currentCharacterSelection = GameManager.instance.playerSprites.Count -1; //reset to last

            OnSelectionChange();
        }
    }

    private void OnSelectionChange()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }

    //Weapon Upgrade
    public void OnUpgradeClick()
    {
        if(GameManager.instance.TryUpgradeWeapon())
            UpdateMenu();
    }

    //Update Character Information, run it when showing the menu
    public void UpdateMenu()
    {
        // Meta
        //hitpointText.text = GameManager.instance.player.hitPoint.ToString() + " / " + GameManager.instance.player.maxHitPoint.ToString();
        moneyText.text = GameManager.instance.money.ToString();
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();

        // Weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        if(GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count)
            upgradeCostText.text = "MAX";
            
        else
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString(); 

        //xp Bar
        int currLevel = GameManager.instance.GetCurrentLevel();
        //if max level just show amount of xp earned.
        if(currLevel == GameManager.instance.xpTable.Count)
        {
            xpText.text = GameManager.instance.xp.ToString() + " total experience points";
            xpBar.localScale = Vector3.one;
        }
        else
        {
            //grab previous level requirement and next levelup amount needed.
            int prevLevelXp = GameManager.instance.GetXpToLevel(currLevel - 1); 
            int currLevelXp = GameManager.instance.GetXpToLevel(currLevel);

            int diff = currLevelXp - prevLevelXp;   //difference
            //current xp in the gamemanager - amount to hit current level.
            int currXPIntoLevel = GameManager.instance.xp - prevLevelXp;
            //find ratio of the amount of progress made
            float completionRatio = (float)currXPIntoLevel / (float)diff;
            xpBar.localScale = new Vector3(completionRatio, 1, 1);
            xpText.text = currXPIntoLevel.ToString() + " / " + diff.ToString();
        }
    }
}
