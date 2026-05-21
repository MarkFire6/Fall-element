using UnityEngine;
using TMPro;

public class ShopSystem : MonoBehaviour
{
    [Header("Money")]
    public int playerMoney = 100;

    [Header("Shop UI")]
    public GameObject shopPanel;

    [Header("Text")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI messageText;

    void Start()
    {
        shopPanel.SetActive(true);

        UpdateMoneyUI();

        messageText.text = "Shop: ";
    }

    // OPEN SHOP
    public void OpenShop()
    {
        shopPanel.SetActive(true);
    }

    // CLOSE SHOP
    public void CloseShop()
    {
        shopPanel.SetActive(false);
    }

    // BUY SWORD
    public void BuySword()
    {
        int cost = 50;

        if (playerMoney >= cost)
        {
            playerMoney -= cost;

            UpdateMoneyUI();

            messageText.text = "Bought Sword!";
        }
        else
        {
            messageText.text = "Not enough money!";
        }
    }

    // BUY POTION
    public void BuyPotion()
    {
        int cost = 25;

        if (playerMoney >= cost)
        {
            playerMoney -= cost;

            UpdateMoneyUI();

            messageText.text = "Bought Potion!";
        }
        else
        {
            messageText.text = "Not enough money!";
        }
    }

    void UpdateMoneyUI()
    {
        moneyText.text = "Money: $" + playerMoney;
    }
}
