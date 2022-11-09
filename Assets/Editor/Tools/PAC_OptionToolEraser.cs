using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PAC_OptionToolEraser : PAC_OptionTool
{
    public PAC_OptionToolEraser ()
    {
        _rectOptionTool = new Rect(0, 0, 150, 50);
        _valueOpacity = 0;
    }

    public override void OnShowGui()
    {
        _valueOpacity = EditorGUI.Slider(_rectOptionTool, _valueOpacity, 0, 1);
    }

}
