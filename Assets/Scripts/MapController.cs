using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    [SerializeField] private List<GameObject> imagePrefab; // Prefab for the images
    [SerializeField] private RectTransform content; // Content object of the Scroll View
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform topPoint;
    [SerializeField] private RectTransform bottomPoint;

    private List<RectTransform> mapList;
    [SerializeField] private int mapCnt = 0;
    private float imageHeight = 1920;
    private int initTiles = 6;

    void Start()
    {
        mapList = new List<RectTransform>();
        for (int i = 0; i < initTiles; i++)
        {
            mapCnt++;
            AddTile();
        }

        //content.sizeDelta = new Vector2(content.sizeDelta.x, 5 * imageHeight);
        positionViewport();
    }

    private void positionViewport()
    {
        float contentHeight = initTiles * imageHeight;
        Vector2 newPosition = content.anchoredPosition;
        newPosition.y = -contentHeight / 2;
        content.anchoredPosition = newPosition;
    }

    private void Update()
    {
       // RectTransform ele5 = mapList[5];
        RectTransform ele1 = mapList[0];

        if(mapCnt>=6){
            if (ele1.position.y > topPoint.position.y)
            {
                Destroy(ele1.gameObject);
                mapList.Remove(ele1);
                AddTile();
                mapCnt++;
            }
            //else if (ele5.position.y < bottomPoint.position.y)
            //{
            //    Destroy(ele5.gameObject);
            //    mapList.Remove(ele5);
            //    AddTileAtTop();
            //    mapCnt--;
            //}
        }

    }

    void AddTile()
    {
        GameObject map = Instantiate(imagePrefab[mapCnt % 2], content);
        RectTransform mapRect = map.GetComponent<RectTransform>();

        //if (mapList.Count > 0)
        //{
        //    RectTransform lastTile = mapList[mapList.Count - 1];
        //    mapRect.anchoredPosition = new Vector2(lastTile.anchoredPosition.x, lastTile.anchoredPosition.y - imageHeight);
        //}
        //else
        //{
        //    mapRect.anchoredPosition = new Vector2(0, 0);
        //}

        mapList.Add(mapRect);
    }

    void AddTileAtTop()
    {
        GameObject map = Instantiate(imagePrefab[mapCnt % 2], content);
        RectTransform mapRect = map.GetComponent<RectTransform>();

        if (mapList.Count > 0)
        {
            RectTransform firstTile = mapList[0];
            mapRect.anchoredPosition = new Vector2(firstTile.anchoredPosition.x, firstTile.anchoredPosition.y + imageHeight);
        }
        else
        {
            mapRect.anchoredPosition = new Vector2(0, 0);
        }

        mapList.Insert(0, mapRect);
        mapRect.SetAsFirstSibling();
    }

}
