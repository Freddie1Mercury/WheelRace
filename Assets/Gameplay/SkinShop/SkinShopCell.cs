using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinShopCell : MonoBehaviour
{
    public CharacterSkin CharacterSkin;

    [SerializeField] private TextMeshProUGUI _priceText;

    private Button _cellButton;

    private int _cellIndex;

    private SkinShopController _skinShopController;

    public bool IsPurchasing;


    private void Awake()
    {
        _cellButton = transform.GetComponent<Button>();

        Transform temp = transform.Find("ContentImage");
        Image contentImage = temp.GetComponent<Image>();
        contentImage.sprite = CharacterSkin.SkinCellSprite;

        _skinShopController = GameObject.Find("SkinShopController").GetComponent<SkinShopController>();

        _skinShopController.SkinShopCells.Add(transform.gameObject);
        _cellIndex = _skinShopController.SkinShopCells.Count - 1;

        _priceText.text = CharacterSkin.SkinPrice.ToString();

        _cellButton.onClick.AddListener(() => _skinShopController.SelectSkin(_cellIndex));
    }

    public void UnlockCell()
    {
        Transform temp = transform.Find("LockImage");
        GameObject lockImage = temp.gameObject;
        lockImage.SetActive(false);

        temp = transform.Find("Price");
        GameObject price = temp.gameObject;
        price.SetActive(false);
        IsPurchasing = true;
    }

    public void ShowSkinSelectionIndicator()
    {
        Transform temp = transform.Find("Selected");
        GameObject selectedImage = temp.gameObject;
        selectedImage.SetActive(true);
    }

    public void HideSkinSelectionIndicator()
    {
        Transform temp = transform.Find("Selected");
        GameObject selectedImage = temp.gameObject;
        selectedImage.SetActive(false);
    }
}
