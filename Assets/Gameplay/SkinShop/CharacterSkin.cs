using UnityEngine;


[CreateAssetMenu(menuName = "Shop/CharacterSkin", fileName = "CharacterSkin")]
public class CharacterSkin : ScriptableObject
{
    public Mesh SkinMesh => _skinMesh;
    public Material SkinMaterial => _skinMaterial;
    public Sprite SkinCellSprite => _skinCellSprite;
    public int SkinPrice => _skinPrice;

    [SerializeField] private Mesh _skinMesh;

    [SerializeField] private Material _skinMaterial;

    [SerializeField] private Sprite _skinCellSprite;

    [SerializeField] private int _skinPrice;

}
