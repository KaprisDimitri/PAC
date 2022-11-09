using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAC_ToolEraser : PAC_Tool
{
    public PAC_ToolEraser ()
    {
        _name = "Erase";
        SetOptionTool();
    }

    public override void OnShowGui()
    {
        if (_projectSelected != null)
        {
            if (Event.current.isMouse)
            {
                if (Event.current.button == 0)
                {
                    Vector2 mousePos = _projectSelected.GridPixelArt.ScreenOriginToGridOrigin(Event.current.mousePosition);
                    if (_projectSelected.GridPixelArt.IsMouseInGrid(mousePos))
                    {
                        mousePos = _projectSelected.GridPixelArt.ScreenPosToGridPos(mousePos);
                        Vector2[] pixelToColor = _projectSelected.GridPixelArt.GetPixelWithSize(mousePos, _optionTools[1].GetSize);
                        _projectSelected.GridPixelArt.SubOpacityPixel(pixelToColor, _optionTools[0].ValueOpacity);
                        _window.Repaint();
                    }
                }
            }
        }
    }

    protected override void SetOptionTool()
    {
        _optionTools = new PAC_OptionTool[2];
        _optionTools[0] = new PAC_OptionToolEraser();
        _optionTools[1] = new PAC_OptionToolSize();
        InitOptionTools(this);
    }
}
