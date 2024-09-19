using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Splines;
using UnityEngine.UI;
using TMPro;
public class MapController : MonoBehaviour
{
    [SerializeField] private List<GameObject> imagePrefab; // Prefab for the images
    [SerializeField] private RectTransform content; // Content object of the Scroll View
    [SerializeField] private Button levelBtnPerfab;

    [SerializeField] private int  levelCount = 33;
    [SerializeField] private int levelPerTile = 4; // number of level per map tiles
    private int poolSize; // Number of map tile to keep in the pool
    int lvl = 1;
    private Queue<GameObject> imagePool;
    private float imageHeight = 1920;

    void Start()
    {
        imagePool = new Queue<GameObject>();
        poolSize = levelCount / levelPerTile;
        poolSize += (levelCount % levelPerTile) > 0 ? 1 : 0;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject img = Instantiate(imagePrefab[i % 2], content);
            for(int j = 0; j < levelPerTile; j++)
            {
                if(lvl<=levelCount){
                    Button leveBtn = Instantiate(levelBtnPerfab, img.GetComponent<RectTransform>());
                    leveBtn.GetComponentInChildren<TextMeshProUGUI>().text = (lvl++).ToString();
                }
            }
            img.SetActive(false);
            imagePool.Enqueue(img);
        }

        for (int i = 0; i < poolSize; i++)
        {
            AddImage();
        }
        PositionViewPort();
    }

    private void PositionViewPort()
    {
        float contentHeight = poolSize * imageHeight;
        Vector2 newPosition = content.anchoredPosition;
        newPosition.y = -contentHeight/2;
        content.anchoredPosition = newPosition;
    }

    void Update()
    {
        if (content.childCount > 0)
        {
            RectTransform firstChild = content.GetChild(0).GetComponent<RectTransform>();
            if (firstChild.anchoredPosition.y > imageHeight)
            {
               RemoveImage();
               AddImage();
            }
        }
    }

    void AddImage()
    {
        if (imagePool.Count > 0)
        {
            GameObject img = imagePool.Dequeue();
            img.SetActive(true);
            img.transform.SetAsLastSibling();
            img.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -content.childCount * imageHeight);
            imagePool.Enqueue(img);
        }
    }

    void RemoveImage()
    {
        if (content.childCount > 0)
        {
            Transform firstChild = content.GetChild(0);
            firstChild.gameObject.SetActive(false);
            firstChild.SetAsLastSibling();
        }
    }
}
