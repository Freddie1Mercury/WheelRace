using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class SkinShopController : MonoBehaviour
{
    public List<SkinShopCell> SkinShopCells = new List<SkinShopCell>();

    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _skinStoreViewObject;

    [SerializeField] private MoneyManager _moneyManager;

    [SerializeField] private Button _buySkinButton;
    [SerializeField] private Transform _camera;

    private SkinShopCell _skinShopCell;

    private MeshFilter _playerMeshFilter;

    private MeshRenderer _playerRenderer;

    private MeshCollider _playerMeshColider;

    private float _originalGlobalSize;

    private Bounds _skinStoreViewObjectOriginalScale;
    private Bounds _playerOriginalScale;


    private void Awake()
    {
        _playerMeshFilter = _player.GetComponent<MeshFilter>();
        _playerRenderer = _player.GetComponent<MeshRenderer>();
        _playerMeshColider = _player.GetComponent<MeshCollider>();

        _playerOriginalScale = _player.GetComponent<MeshCollider>().bounds;
        _skinStoreViewObjectOriginalScale = _skinStoreViewObject.GetComponent<MeshCollider>().bounds;

    }

    public void BuySkin(int cellIndex)
    {
        _skinShopCell = SkinShopCells[cellIndex].GetComponent<SkinShopCell>();
        Debug.Log("BuySkin");
        if (_skinShopCell.SkinSaveInfos[_skinShopCell.SkinSafeInfoIndex].IsPurchased)
        {
            return;
        }

        if (!_skinShopCell.CharacterSkin.IsPriceInAD)
        {
            if (_moneyManager.AllMoney < _skinShopCell.CharacterSkin.SkinPrice)
            {
                return;
            }
            _moneyManager.DeductMoney(_skinShopCell.CharacterSkin.SkinPrice);
            _skinShopCell.UnlockCell();
            SelectSkin(cellIndex);
            _buySkinButton.gameObject.SetActive(false);
        }
        else
        {
            if (_skinShopCell.SkinSaveInfos[_skinShopCell.SkinSafeInfoIndex].CountViewAds == _skinShopCell.CharacterSkin.SkinPriceInAD)
            {
                return;
            }
            YandexGame.RewVideoShow(1);
            SkinSaveInfo tempSkinSafeInfo = new SkinSaveInfo();
            tempSkinSafeInfo = _skinShopCell.SkinSaveInfos[_skinShopCell.SkinSafeInfoIndex];
            tempSkinSafeInfo.CountViewAds += 1;
            _skinShopCell.SkinSaveInfos[_skinShopCell.SkinSafeInfoIndex] = tempSkinSafeInfo;
            _skinShopCell.CountViewAdsText.text = _skinShopCell.SkinSaveInfos[_skinShopCell.SkinSafeInfoIndex].CountViewAds.ToString();
            YandexGame.SaveProgress();
            if (_skinShopCell.SkinSaveInfos[_skinShopCell.SkinSafeInfoIndex].CountViewAds == _skinShopCell.CharacterSkin.SkinPriceInAD)
            {
                _skinShopCell.UnlockCell();
                SkinSaveInfo skinSaveTemp = _skinShopCell.SkinSaveInfos[_skinShopCell.SkinSafeInfoIndex];
                skinSaveTemp.IsPurchased = true;
                _skinShopCell.SkinSaveInfos[_skinShopCell.SkinSafeInfoIndex] = skinSaveTemp;
                SelectSkin(cellIndex);
                _buySkinButton.gameObject.SetActive(false);
                YandexGame.SaveProgress();
                return;
            }
        }

    }

    public void SelectSkin(int cellIndex)
    {
        _skinShopCell = SkinShopCells[cellIndex].GetComponent<SkinShopCell>();
        Debug.Log(_skinShopCell.SkinSaveInfos.Count + "SkinSaveInfos Count");

        for (int i = 0; i < SkinShopCells.Count; i++)
        {
            if (_skinShopCell.SkinSaveInfos[_skinShopCell.SkinSafeInfoIndex].IsPurchased)
            {
                if (i != cellIndex)
                {
                    SkinShopCells[i].GetComponent<SkinShopCell>().HideORShowSkinSelectionIndicator(false);
                }
            }
        }

        if (!_skinShopCell.SkinSaveInfos[_skinShopCell.SkinSafeInfoIndex].IsPurchased)
        {
            _buySkinButton.gameObject.SetActive(true);
        _buySkinButton.onClick.RemoveAllListeners();
        _buySkinButton.onClick.AddListener(() => BuySkin(cellIndex));
        }
        if (_skinShopCell.SkinSaveInfos[_skinShopCell.SkinSafeInfoIndex].IsPurchased)
        {
            _buySkinButton.gameObject.SetActive(false);
        }
        ReplaceMeshAndScale(_skinShopCell.CharacterSkin.SkinMesh, _skinStoreViewObject.GetComponent<MeshFilter>(),
           _skinStoreViewObject.GetComponent<MeshCollider>(), _skinStoreViewObject.GetComponent<MeshRenderer>(), _skinStoreViewObject, _skinStoreViewObjectOriginalScale);
        Debug.Log(SkinShopCells.Count + "SkinShopCell Count");
        if (!_skinShopCell.SkinSaveInfos[_skinShopCell.SkinSafeInfoIndex].IsPurchased)
        {
            return;
        }

        ReplaceMeshAndScale(_skinShopCell.CharacterSkin.SkinMesh, _playerMeshFilter, _playerMeshColider, _playerRenderer, _player, _playerOriginalScale);
        _skinShopCell.HideORShowSkinSelectionIndicator(true);

    }


    private void ReplaceMeshAndScale(Mesh mesh, MeshFilter meshFilter, MeshCollider meshCollider, MeshRenderer meshRenderer, GameObject scaleObject, Bounds originalScale)
    {

        if (_skinShopCell.IsNeedRotate)
        {
            scaleObject.transform.rotation = Quaternion.Euler(_skinShopCell.Rotation);
        }
        else
        {
            scaleObject.transform.rotation = Quaternion.identity;
        }

        meshFilter.mesh = _skinShopCell.CharacterSkin.SkinMesh;
        meshCollider.sharedMesh = _skinShopCell.CharacterSkin.SkinMesh;
        meshRenderer.material = _skinShopCell.CharacterSkin.SkinMaterial;

        _originalGlobalSize = GetMaxSize(originalScale.size);
        float newGlobalSize = GetMaxSize(meshRenderer.bounds.size);

        if (newGlobalSize > 0)
        {
            float scaleFactor = _originalGlobalSize / newGlobalSize;
            scaleObject.transform.localScale *= scaleFactor;
        }

    }

    private float GetMaxSize(Vector3 size)
    {
        return Mathf.Max(size.x, size.y, size.z);
    }
}
