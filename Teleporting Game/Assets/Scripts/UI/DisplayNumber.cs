using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayNumber : MonoBehaviour
{
    public FloatReference NumberToDisplay;

    private void Awake()
    {
        m_Text = GetComponent<Text>();
    }

    void Update()
    {
        m_Text.text = NumberToDisplay.GetValue().ToString();
    }


    private Text m_Text;
}
