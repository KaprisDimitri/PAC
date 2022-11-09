using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PAC_OptionToolColor : PAC_OptionTool
{
    Rect _rectColorField;
    Rect _rectButtonSave;
    public PAC_OptionToolColor ()
    {
        _rectOptionTool = new Rect(0, 0, 100, 50);
        _rectColorField = new Rect(0, 0, 50, 50);
        _rectButtonSave = new Rect(50, 0, 50, 50);
        _color = Color.blue;
    }

    public override void OnShowGui()
    {
        GUI.BeginGroup(_rectOptionTool);
        _color = EditorGUI.ColorField(_rectColorField, _color);
        if(GUI.Button(_rectButtonSave, "Save C"))
        {
            _tool.ProjectSelected.ProjectColor.AddColor(_color);
        }
        GUI.EndGroup();
    }


}
