using UnityEngine;
using UnityEngine.UI;

public class ButtonWithTransparentBackground : MonoBehaviour
{
    private void Start()
    {
        var image = GetComponent<Image>();
        if (image == null)
        {
            Debug.Log("NULL");
        }
        image.alphaHitTestMinimumThreshold = 1f;
    }
}
