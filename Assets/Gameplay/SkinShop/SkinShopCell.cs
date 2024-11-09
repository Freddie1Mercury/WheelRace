using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinShopCell : MonoBehaviour
{
    public bool IsPurchased;

    public int CountViewAds;

    public CharacterSkin CharacterSkin;

    public TextMeshProUGUI CountViewAdsText;

    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _priceInADText;

    [SerializeField] private GameObject _price;
    [SerializeField] private GameObject _priceInAD;

    private Button _cellButton;

    private int _cellIndex;

    private SkinShopController _skinShopController;




    private void Awake()
    {
        _cellButton = transform.GetComponent<Button>();

        Transform temp = transform.Find("ContentImage");
        Image contentImage = temp.GetComponent<Image>();
        contentImage.sprite = CharacterSkin.SkinCellSprite;

        _skinShopController = GameObject.Find("SkinShopController").GetComponent<SkinShopController>();

        _skinShopController.SkinShopCells.Add(transform.gameObject);
        _cellIndex = _skinShopController.SkinShopCells.Count - 1;

        temp = transform.Find("Price/PriceText");
        Debug.Log("temp" + temp.name);
        _priceText.text = CharacterSkin.SkinPrice.ToString();

        if (!CharacterSkin.IsPriceInAD)
        {
            _price.gameObject.SetActive(true);
        }
        else
        {
            _priceInAD.gameObject.SetActive(true);
            _priceInADText.text = CharacterSkin.SkinPriceInAD.ToString();
        }

        _cellButton.onClick.AddListener(() => _skinShopController.SelectSkin(_cellIndex));
    }

    public void UnlockCell()
    {
        Transform temp = transform.Find("LockImage");
        GameObject lockImage = temp.gameObject;
        lockImage.SetActive(false);

        if (!CharacterSkin.IsPriceInAD)
        {
        _price.SetActive(false);
        IsPurchased = true;
        }
        else
        {
            _priceInAD.SetActive(false);
            IsPurchased = true;
        }
    }

    public void HideORShowSkinSelectionIndicator(bool show)
    {
        Transform temp = transform.Find("Selected");
        GameObject selectedImage = temp.gameObject;
        selectedImage.SetActive(show);
    }


}
