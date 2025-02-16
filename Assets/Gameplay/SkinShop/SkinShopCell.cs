using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class SkinShopCell : MonoBehaviour
{
    public List<SkinSaveInfo> SkinSaveInfos { get => YandexGame.savesData.SkinSaveInfos; set => YandexGame.savesData.SkinSaveInfos = value; }


    public int SkinSafeInfoIndex;

    public CharacterSkin CharacterSkin;

    public TextMeshProUGUI CountViewAdsText;

    public bool IsNeedRotate;

    public Vector3 Rotation;

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

        _skinShopController.SkinShopCells.Add(transform.GetComponent<SkinShopCell>());

        for (int i = 0; i < SkinSaveInfos.Count; i++)
        {
            if (SkinSaveInfos[i].SkinCellName == transform.name)
            {
                SkinSafeInfoIndex = i;
                SkinShopCell tempSkinShopCell = transform.GetComponent<SkinShopCell>();
                for (int j = 0; j < _skinShopController.SkinShopCells.Count; j++)
                {
                    if (tempSkinShopCell == _skinShopController.SkinShopCells[j])
                    {
                        _cellIndex = j;
                        break;
                    }

                }
            }
        }


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

        if (SkinSaveInfos[_cellIndex].IsPurchased == true)
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
            skinSaveTemp.IsPurchased = true;
            SkinSaveInfos[_cellIndex] = skinSaveTemp;
            YandexGame.SaveProgress();
        }
        else
        {
            _priceInAD.SetActive(false);
            SkinSaveInfo skinSaveTemp = SkinSaveInfos[_cellIndex];
            skinSaveTemp.IsPurchased = true;
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
