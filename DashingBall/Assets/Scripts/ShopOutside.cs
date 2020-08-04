using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopOutside : MonoBehaviour
{

    BoxCollider2D collider;
    RectTransform rectTransform;
    Image panelImage;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        panelImage = GetComponent<Image>();
        collider = GetComponent<BoxCollider2D>();

        SetBoxCollider();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetBoxCollider()
    {

        if (collider != null)
        {
            Image image = rectTransform.gameObject.GetComponent<Image>();

            if (image.preserveAspect)
            {

                var originalW = (int)(image.sprite.rect.width);
                var originalH = (int)(image.sprite.rect.height);

                var currentW = image.rectTransform.rect.width;
                var currentH = image.rectTransform.rect.height;

                var ratio = Mathf.Min(currentW / originalW, currentH / originalH);

                var newW = image.sprite.rect.width * ratio;
                var newH = image.sprite.rect.height * ratio;

                collider.size = new Vector2(newW, newH);
            }
            else
            {

                collider.size = image.rectTransform.rect.size;

            }
        }
    }

}
