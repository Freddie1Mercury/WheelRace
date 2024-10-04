using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffAndDebuffBarsPool : MonoBehaviour
{
    [SerializeField] private GameObject _buffBarPrefab;
    [SerializeField] private GameObject _debuffBarPrefab;
    [SerializeField] private Transform _canvas;

    [SerializeField] private RectTransform _buffStartSpawnPosition;
    [SerializeField] private RectTransform _debuffStartSpawnPosition;

    public List<GameObject> BuffBars = new List<GameObject>();
    public List<GameObject> DebuffBars = new List<GameObject>();

    public void Start()
    {

        if (BuffBars.Count == 0)
        {
            CreatePool(true);
        }
        if (DebuffBars.Count == 0)
        {
            CreatePool(false);
        }
    }



    public int GetPool(bool isBuff)
    {

        int buffOrDebuffBarIndex = 0;
        if (isBuff)
        {

            for (int i = 0; i < BuffBars.Count; i++)
            {
                if (!BuffBars[i].activeSelf)
                {
                    BuffBars[i].SetActive(true);
                    return i;
                }
                if (BuffBars[i].activeSelf)
                {
                    buffOrDebuffBarIndex++;
                }
                if (buffOrDebuffBarIndex == BuffBars.Count - 1)
                {
                    CreatePool(isBuff);
                    BuffBars[BuffBars.Count - 1].SetActive(true);
                    return BuffBars.Count - 1;
                }

            }

        }
        else
        {

            for (int i = 0; i < DebuffBars.Count; i++)
            {

                if (!DebuffBars[i].activeSelf)
                {
                    DebuffBars[i].SetActive(true);
                    return i;
                }
                if (DebuffBars[i].activeSelf)
                {
                    buffOrDebuffBarIndex++;
                }
                if (buffOrDebuffBarIndex == DebuffBars.Count - 1)
                {
                    CreatePool(isBuff);
                    DebuffBars[DebuffBars.Count - 1].SetActive(true);
                    return DebuffBars.Count - 1;
                }
            }

        }
        return -1;
    }

    public void ReleasePool(bool isBuff, int buffOrDebuffBarIndex)
    {
        if (isBuff)
        {
            BuffBars[buffOrDebuffBarIndex].GetComponent<Image>().fillAmount = 1;
            BuffBars[buffOrDebuffBarIndex].SetActive(false);
        }
        if (!isBuff)
        {
            DebuffBars[buffOrDebuffBarIndex].GetComponent<Image>().fillAmount = 1;
            DebuffBars[buffOrDebuffBarIndex].SetActive(false);
        }
    }

    private void CreatePool(bool isBuff)
    {
        if (isBuff)
        {
            if (BuffBars.Count == 0)
            {
                BuffBars.Add(Instantiate(_buffBarPrefab));
                BuffBars[BuffBars.Count - 1].transform.SetParent(_canvas);
                BuffBars[BuffBars.Count - 1].GetComponent<RectTransform>().anchoredPosition = _buffStartSpawnPosition.anchoredPosition;
            }
            else
            {
                RectTransform buffBarnewPosition = new RectTransform();
                buffBarnewPosition.anchoredPosition = new Vector2(BuffBars[BuffBars.Count - 1].GetComponent<RectTransform>().anchoredPosition.x + 150, BuffBars[BuffBars.Count - 1].GetComponent<RectTransform>().anchoredPosition.y);
                BuffBars.Add(Instantiate(_buffBarPrefab));
                BuffBars[BuffBars.Count-1].GetComponent<RectTransform>().anchoredPosition = buffBarnewPosition.anchoredPosition;
            }
        }
        else
        {

            if (DebuffBars.Count == 0)
            {
                DebuffBars.Add(Instantiate(_debuffBarPrefab));
                DebuffBars[DebuffBars.Count - 1].transform.SetParent(_canvas);
                DebuffBars[DebuffBars.Count - 1].GetComponent<RectTransform>().anchoredPosition = _debuffStartSpawnPosition.anchoredPosition;
            }
            else
            {
                RectTransform debuffBarnewPosition = new RectTransform();
                debuffBarnewPosition.anchoredPosition = new Vector2(DebuffBars[DebuffBars.Count - 1].GetComponent<RectTransform>().anchoredPosition.x + 150, DebuffBars[DebuffBars.Count - 1].GetComponent<RectTransform>().anchoredPosition.y);
                DebuffBars.Add(Instantiate(_debuffBarPrefab));
                DebuffBars[BuffBars.Count - 1].transform.SetParent(_canvas);
                DebuffBars[DebuffBars.Count-1].GetComponent<RectTransform>().anchoredPosition = debuffBarnewPosition.anchoredPosition;
            }

        }
    }
}
