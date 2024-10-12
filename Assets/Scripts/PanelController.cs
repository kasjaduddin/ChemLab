using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public RectTransform panelTransform;

    void Start()
    {
        AdjustHeight();
    }

    void AdjustHeight()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null && panelTransform != null)
        {
            Vector3 newPosition = panelTransform.position;
            newPosition.y = mainCamera.transform.position.y + 0.6f;
            panelTransform.position = newPosition;
        }
    }
}
