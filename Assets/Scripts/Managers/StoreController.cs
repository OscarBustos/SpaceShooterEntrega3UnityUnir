using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreController : MonoBehaviour
{
    [SerializeField] GameManagerSO gameManager;
    [SerializeField] StoreItemsListSO storeList;
    [SerializeField] Image selectedItemImage;
    [SerializeField] TextMeshProUGUI selectedItemText;
    [SerializeField] GameObject[] itemsButtons;
    [SerializeField] Button buyButton;
    [SerializeField] TextMeshProUGUI totalCoinsText;

    private Image[] buttonImage;
    private TextMeshProUGUI[] buttonText;

    private int selectedItemIndex;
    private bool itemsAvailable;

    private void Awake()
    {
        UpdateStore();
    }

    private void UpdateStore()
    {
        totalCoinsText.text = gameManager.TotalCoins.ToString();
        buttonImage = new Image[itemsButtons.Length];
        buttonText = new TextMeshProUGUI[itemsButtons.Length];
        for (int i = 0; i < itemsButtons.Length; i++)
        {
            Image[] images = itemsButtons[i].GetComponentsInChildren<Image>();
            TextMeshProUGUI valueText = itemsButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText[i] = valueText;
            StoreItemSO item = storeList.GetItem(i);
            if (item == null)
            {
                itemsButtons[i].SetActive(false);
            }
            else
            {
                valueText.text = item.Value.ToString();
                for (int j = 0; j < images.Length; j++)
                {
                    if (images[j].gameObject.CompareTag("ItemImage"))
                    {
                        images[j].sprite = item.Sprite;
                        buttonImage[i] = images[j];
                        break;
                    }
                }

                if (i == 0)
                {
                    UpdateSelectedItem(i);
                }
                itemsAvailable = true;
            }
        }
        if (!itemsAvailable)
        {
            selectedItemImage.sprite = null;
            selectedItemText.text = null;
            buyButton.gameObject.SetActive(false);
        }
    }
    public void UpdateSelectedItem(int index)
    {
        selectedItemIndex = index;
        selectedItemImage.sprite = buttonImage[selectedItemIndex].sprite;
        selectedItemText.text = buttonText[selectedItemIndex].text;
        itemsButtons[selectedItemIndex].GetComponent<Button>().Select();
        int itemValue = int.Parse(selectedItemText.text);
        if (gameManager.TotalCoins < itemValue)
        {
            buyButton.enabled = false;
        } 
        else
        {
            buyButton.enabled = true;
        }
    }

    public void Skip()
    {
        gameManager.PauseResumeGame();
        gameObject.SetActive(false);
    }

    public void Buy()
    {
        StoreItemSO item = storeList.GetItem(selectedItemIndex);
        switch (item.ItemType)
        {
            case StoreItemType.POWER_UP: 
                {
                    PlayerPrefs.SetInt("CannonNumber", item.CannonNumber);
                    break;
                }
            case StoreItemType.SKIN:
                {
                    PlayerPrefs.SetString("Skin", item.Sprite.name);
                    break;
                }
        }
        gameManager.TotalCoins -= item.Value;
        storeList.RemoveItem(selectedItemIndex);
        UpdateStore();
        gameManager.ReloadPlayerPrefs();
    }
}
