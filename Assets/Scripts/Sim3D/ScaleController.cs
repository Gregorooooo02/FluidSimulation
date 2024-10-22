using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScaleController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text xScaleText;
    [SerializeField] private TMP_Text yScaleText;

    private void Awake() {
        xScaleText.text = "x = " + transform.localScale.x.ToString();
        yScaleText.text = "z = " + transform.localScale.y.ToString();
    }

    public void ScaleX(float scale) {
        transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);

        string scaleString = scale.ToString();
        if (scaleString.Length > 4) {
            scaleString = scaleString.Substring(0, 4);
        }

        xScaleText.text = "x = " + scaleString;
    }

    public void ScaleZ(float scale) {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, scale);
        
        string scaleString = scale.ToString();
        if (scaleString.Length > 4) {
            scaleString = scaleString.Substring(0, 4);
        }

        yScaleText.text = "z = " + scaleString;
    }
}
