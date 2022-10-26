using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataUI : MonoBehaviour
{
    Image healthSlider;
    Image bulletsNumberSlider;
	Image expSlider;

	public Text levelText;
	public Text healthText;
	public Text bulletsNumberText;
	public Text expText;

	public Text damageText;
	public Text defenceText;
	public Text criticalMultiplierText;
	public Text criticalChanceText;

	public Text keyText;

	private void Awake()
    {
        healthSlider = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        bulletsNumberSlider = transform.GetChild(1).GetChild(0).GetComponent<Image>();
		expSlider = transform.GetChild(2).GetChild(0).GetComponent<Image>();
	}

    private void Update()
    {
        UpdateHealth();
        UpdateBulletsNumber();
		UpdateExp();
		UpdateText();
	}

	private void UpdateText()
	{
		levelText.text = "Level " + GameManager.Instance.playerStats.characterData.currentLevel;
		healthText.text = GameManager.Instance.playerStats.CurrentHealth + " / " + GameManager.Instance.playerStats.MaxHealth;
		bulletsNumberText.text = GameManager.Instance.playerStats.CurrentBulletsNumber + " / " + GameManager.Instance.playerStats.MaxBulletsNumber;
		expText.text = GameManager.Instance.playerStats.characterData.currentExp + " / " + GameManager.Instance.playerStats.characterData.baseExp;
		damageText.text = GameManager.Instance.playerStats.attackData.minDamage + " - " + GameManager.Instance.playerStats.attackData.maxDamage;
	    defenceText.text = "" + GameManager.Instance.playerStats.characterData.currentDefence;
	    criticalMultiplierText.text = (int)(GameManager.Instance.playerStats.attackData.criticalMultiplier * 100f) + " %";
	    criticalChanceText.text = (int)(GameManager.Instance.playerStats.attackData.criticalChance * 100f) + " %";
		keyText.text = GameManager.Instance.playerStats.characterData.key;
}

	private void UpdateHealth()
    {
        float sliderPercent = (float)GameManager.Instance.playerStats.CurrentHealth
            / (float)GameManager.Instance.playerStats.MaxHealth;
        healthSlider.fillAmount = sliderPercent;
    }

    private void UpdateBulletsNumber()
    {
        float sliderPercent = (float)GameManager.Instance.playerStats.CurrentBulletsNumber
            / (float)GameManager.Instance.playerStats.MaxBulletsNumber;
        bulletsNumberSlider.fillAmount = sliderPercent;
    }

	private void UpdateExp()
	{
		float sliderPercent = (float)GameManager.Instance.playerStats.characterData.currentExp
			/ (float)GameManager.Instance.playerStats.characterData.baseExp;
		expSlider.fillAmount = sliderPercent;
	}
}
