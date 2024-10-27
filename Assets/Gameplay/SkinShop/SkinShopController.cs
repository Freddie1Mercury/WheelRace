using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinShopController : MonoBehaviour
{
    public List<GameObject> SkinShopCells;

    [SerializeField] private GameObject _player;

    [SerializeField] private MoneyManager _moneyManager;

    private SkinShopCell _skinShopCell;

    private MeshFilter _playerMeshFilter;

    private MeshRenderer _playerRenderer;

    private MeshCollider _playerMeshColider;


    private void Awake()
    {
        _playerMeshFilter = _player.GetComponent<MeshFilter>();
        _playerRenderer = _player.GetComponent<MeshRenderer>();
        _playerMeshColider = _player.GetComponent<MeshCollider>();
    }
    private void BuySkin(int cellIndex)
    {
        _skinShopCell = SkinShopCells[cellIndex].GetComponent<SkinShopCell>();

        if (_moneyManager.AllMoney < _skinShopCell.CharacterSkin.SkinPrice)
        {
            return;
        }

        _moneyManager.DeductMoney(_skinShopCell.CharacterSkin.SkinPrice);
        _skinShopCell.UnlockCell();

    }

    public void SelectSkin(int cellIndex)
    {
        _skinShopCell = SkinShopCells[cellIndex].GetComponent<SkinShopCell>();

        if (!_skinShopCell.IsPurchasing)
        {
            BuySkin(cellIndex);
            return;
        }
        if (!_skinShopCell.IsPurchasing)
        {
            return;
        }

        for (int i = 0; i < SkinShopCells.Count; i++)
        {
            if (i != cellIndex)
            {
                SkinShopCells[i].GetComponent<SkinShopCell>().HideORShowSkinSelectionIndicator(false);
            }
        }

        ReplaceMeshAndScale(_skinShopCell.CharacterSkin.SkinMesh);
        _skinShopCell.HideORShowSkinSelectionIndicator(true);

    }


    private void ReplaceMeshAndScale(Mesh mesh)
    {
        float currentGlobalSize = GetMaxSize(_playerRenderer.bounds.size);

        _playerMeshFilter.mesh = _skinShopCell.CharacterSkin.SkinMesh;
        _playerMeshColider.sharedMesh = _skinShopCell.CharacterSkin.SkinMesh;
        _playerRenderer.material = _skinShopCell.CharacterSkin.SkinMaterial;

        float newGlobalSize = GetMaxSize(_playerRenderer.bounds.size);

        if (newGlobalSize > 0)
        {
            float scaleFactor = currentGlobalSize / newGlobalSize;
            _player.transform.localScale *= scaleFactor;
        }
            
    }

    private float GetMaxSize(Vector3 size)
    {
        return Mathf.Max(size.x, size.y, size.z);
    }
}
