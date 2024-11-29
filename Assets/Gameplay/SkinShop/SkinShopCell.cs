using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class SkinShopCell : MonoBehaviour
{
    public List<SkinSaveInfo> SkinSaveInfos { get => YandexGame.savesData.SkinSaveInfos; set => YandexGame.savesData.SkinSaveInfos = value; }

    public int CountViewAds  { get => YandexGame.savesData.CountViewAds; set => YandexGame.savesData.CountViewAds = value; }

    public CharacterSkin CharacterSkin;

    public TextMeshProUGUI CountViewAdsText;

    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _priceInADText;

    [SerializeField] private GameObject _price;
    [SerializeField] private GameObject _priceInAD;
    [SerializeField] private GameObject _lockImage;

    private Button _cellButton;

    private int _cellIndex;

    private SkinShopController _skinShopController;




    private void Awake()
    {
        _cellButton = transform.GetComponent<Button>();

        Transform tempTransform = transform.Find("ContentImage");
        Image contentImage = tempTransform.GetComponent<Image>();
        contentImage.sprite = CharacterSkin.SkinCellSprite;

        _skinShopController = GameObject.Find("SkinShopController").GetComponent<SkinShopController>();

        SkinSaveInfo tempSkinSaveInfo = new SkinSaveInfo();
        tempSkinSaveInfo.SkinCellName = transform.name;

        bool isNewCell = true;

        foreach (var item in SkinSaveInfos)
        {
            if (item.SkinCellName == transform.name)
            {
                isNewCell = false;
            }
        }
        if (isNewCell)
        {
            SkinSaveInfos.Add(tempSkinSaveInfo);
            YandexGame.SaveProgress();
        }

        for (int i = 0; i < SkinSaveInfos.Count; i++)
        {
            if (SkinSaveInfos[i].SkinCellName == transform.name)
            {
                _cellIndex = i;
                break;
            }
        }

        _skinShopController.SkinShopCells.Add(transform.GetComponent<SkinShopCell>());

        tempTransform = transform.Find("Price/PriceText");
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

        if (SkinSaveInfos[_cellIndex].IsPuchased == true)
        {
            UnlockCell();
        }

        YandexGame.SaveProgress();
    }

    public void UnlockCell()
    {
        _lockImage.SetActive(false);

        if (!CharacterSkin.IsPriceInAD)
        {
            _price.SetActive(false);
            SkinSaveInfo skinSaveTemp = SkinSaveInfos[_cellIndex];
            skinSaveTemp.IsPuchased = true;
            SkinSaveInfos[_cellIndex] = skinSaveTemp;
            YandexGame.SaveProgress();
        }
        else
        {
            _priceInAD.SetActive(false);
            SkinSaveInfo skinSaveTemp = SkinSaveInfos[_cellIndex];
            skinSaveTemp.IsPuchased = true;
            SkinSaveInfos[_cellIndex] = skinSaveTemp;
            YandexGame.SaveProgress();
        }

    }

    public void HideORShowSkinSelectionIndicator(bool show)
    {
        Transform temp = transform.Find("Selected");
        GameObject selectedImage = temp.gameObject;
        selectedImage.SetActive(show);
    }


}
