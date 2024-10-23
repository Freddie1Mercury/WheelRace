using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinShopController : MonoBehaviour
{
    public List<GameObject> SkinShopCells;

    [SerializeField] private GameObject _player;

    [SerializeField] private MoneyManager _moneyManager;

    private SkinShopCell _skinShopCell;

   
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

        _skinShopCell.ShowSkinSelectionIndicator();
        _player.GetComponent<MeshFilter>().mesh = _skinShopCell.CharacterSkin.SkinMesh;
        _player.GetComponent<MeshCollider>().sharedMesh = _skinShopCell.CharacterSkin.SkinMesh;
        _player.GetComponent<Renderer>().material = _skinShopCell.CharacterSkin.SkinMaterial;
    }

    
}
