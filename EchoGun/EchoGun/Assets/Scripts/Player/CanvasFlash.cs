using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Text))]
public class CanvasFlash : MonoBehaviour {

    Text text;

    [SerializeField]
    protected Gradient colorGradient;

    [SerializeField]
    protected float flashDuration;

    void Start () {
        text = GetComponent<Text>();
    }

    public void Flash() {
        Callback.DoLerp((float l) => {
            text.color = colorGradient.Evaluate(l);
        }, flashDuration, this);
    }
}
