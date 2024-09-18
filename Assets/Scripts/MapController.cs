using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Splines;
public class MapController : MonoBehaviour
{
    public List<GameObject> imagePrefab; // Prefab for the images
    public RectTransform content; // Content object of the Scroll View
    public int poolSize = 20; // Number of images to keep in the pool
    private Queue<GameObject> imagePool;
    private float imageHeight = 1920;
    Spline beziers;

    void Start()
    {
        imagePool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject img = Instantiate(imagePrefab[i%2], content);
            img.SetActive(false);
            imagePool.Enqueue(img);
        }

        for (int i = 0; i < poolSize; i++)
        {
            AddImage();
        }
        float contentHeight = poolSize * imageHeight;
        Debug.Log(contentHeight);
        Vector2 newPosition = content.anchoredPosition;
        newPosition.y = contentHeight/2;
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
