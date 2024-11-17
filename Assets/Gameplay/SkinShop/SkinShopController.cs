using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class SkinShopController : MonoBehaviour
{
    public List<SkinShopCell> SkinShopCells => YandexGame.savesData.SkinShopCells;

    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _skinStoreViewObject;

    [SerializeField] private MoneyManager _moneyManager;

    [SerializeField] private Transform _buyButtonPosition;

    [SerializeField] private Button _buySkinButton;
    [SerializeField] private Transform _camera;

    private SkinShopCell _skinShopCell;

    private MeshFilter _playerMeshFilter;

    private MeshRenderer _playerRenderer;

    private MeshCollider _playerMeshColider;


    private void Awake()
    {
        Debug.Log(YandexGame.savesData.SkinShopCells);
        _playerMeshFilter = _player.GetComponent<MeshFilter>();
        _playerRenderer = _player.GetComponent<MeshRenderer>();
        _playerMeshColider = _player.GetComponent<MeshCollider>();
    }

    public void BuySkin(int cellIndex)
    {
        _skinShopCell = SkinShopCells[cellIndex].GetComponent<SkinShopCell>();
        Debug.Log("BuySkin");
        if (_skinShopCell.IsPurchased)
        {
            return;
        }
        if (_moneyManager.AllMoney < _skinShopCell.CharacterSkin.SkinPrice)
        {
            return;
        }

        if (!_skinShopCell.CharacterSkin.IsPriceInAD)
        {
        _moneyManager.DeductMoney(_skinShopCell.CharacterSkin.SkinPrice);
        _skinShopCell.UnlockCell();
            SelectSkin(cellIndex);
        }
        else
        {
            if (_skinShopCell.CountViewAds == _skinShopCell.CharacterSkin.SkinPriceInAD)
            {
                return;
            }
                YandexGame.RewVideoShow(1);
            _skinShopCell.CountViewAds +=1;
            _skinShopCell.CountViewAdsText.text = _skinShopCell.CountViewAds.ToString();
            if (_skinShopCell.CountViewAds == _skinShopCell.CharacterSkin.SkinPriceInAD)
            {
                _skinShopCell.UnlockCell();
                _skinShopCell.IsPurchased = true;
                SelectSkin(cellIndex);
                return;
            }
        }
    }

    public void SelectSkin(int cellIndex)
    {
        _skinShopCell = SkinShopCells[cellIndex].GetComponent<SkinShopCell>();

        for (int i = 0; i < SkinShopCells.Count; i++)
        {
            if (i != cellIndex)
            {
                SkinShopCells[i].GetComponent<SkinShopCell>().HideORShowSkinSelectionIndicator(false);
            }
        }

        _buySkinButton.onClick.RemoveAllListeners();
        _buySkinButton.onClick.AddListener(() => BuySkin(cellIndex));
        ReplaceMeshAndScale(_skinShopCell.CharacterSkin.SkinMesh, _skinStoreViewObject.GetComponent<MeshFilter>(), 
           _skinStoreViewObject.GetComponent<MeshCollider>(), _skinStoreViewObject.GetComponent<MeshRenderer>(), _skinStoreViewObject);

        if (!_skinShopCell.IsPurchased)
        {
            return;
        }

        ReplaceMeshAndScale(_skinShopCell.CharacterSkin.SkinMesh, _playerMeshFilter, _playerMeshColider, _playerRenderer, _player);
        _skinShopCell.HideORShowSkinSelectionIndicator(true);

    }


    private void ReplaceMeshAndScale(Mesh mesh, MeshFilter meshFilter, MeshCollider meshCollider, MeshRenderer meshRenderer, GameObject scaleObject)
    {
      
        float currentGlobalSize = GetMaxSize(meshRenderer.bounds.size);

        meshFilter.mesh = _skinShopCell.CharacterSkin.SkinMesh;
        meshCollider.sharedMesh = _skinShopCell.CharacterSkin.SkinMesh;
        meshRenderer.material = _skinShopCell.CharacterSkin.SkinMaterial;

        float newGlobalSize = GetMaxSize(meshRenderer.bounds.size);

        if (newGlobalSize > 0)
        {
            float scaleFactor = currentGlobalSize / newGlobalSize;
            scaleObject.transform.localScale *= scaleFactor;
        }


    }

    private float GetMaxSize(Vector3 size)
    {
        return Mathf.Max(size.x, size.y, size.z);
    }
}
