using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PAC_OptionToolSize : PAC_OptionTool
{
    public PAC_OptionToolSize ()
    {
        _size = 1;
        _rectOptionTool = new Rect(0, 0, 50, 50);
    }
    public override void OnShowGui()
    {
        int sizeTemp = EditorGUI.IntField(_rectOptionTool, _size);
        if (sizeTemp > 0)
        {
            _size = sizeTemp;
        }
    }
}
