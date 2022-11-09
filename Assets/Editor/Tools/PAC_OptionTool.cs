using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PAC_OptionTool 
{
    protected PAC_Tool _tool;

    protected Color _color;
    protected int _size;
    protected float _valueOpacity;


    protected Rect _rectOptionTool;

    public float ValueOpacity
    {
        get { return _valueOpacity; }
    }

    public Rect RectOptionTool
    {
        get { return _rectOptionTool; }
    }

    public Color GetColor
    {
        get { return _color; }
        set {  _color = value; }
    }
    public int GetSize
    {
        get { return _size; }
    }

    public void InitInListOptionTool(PAC_OptionTool previessOptionTool, PAC_Tool tool)
    {
        
        _tool = tool;
        if (previessOptionTool != null)
        {
            _rectOptionTool.x += previessOptionTool.RectOptionTool.width;
        }
    }
    public abstract void OnShowGui();
}
