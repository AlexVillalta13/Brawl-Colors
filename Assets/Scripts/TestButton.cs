using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestButton : MonoBehaviour
{
    [SerializeField] Image image;

    public void ChangeColor()
    {
        image.color = Color.red;
    }
}
