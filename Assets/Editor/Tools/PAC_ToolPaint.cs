using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAC_ToolPaint : PAC_Tool
{
    public PAC_ToolPaint()
    {
        
        _name = "Paint";
        SetOptionTool();
    }

    protected override void SetOptionTool()
    {
        _optionTools = new PAC_OptionTool[2];
        _optionTools[0] = new PAC_OptionToolColor();
        _optionTools[1] = new PAC_OptionToolSize();
        InitOptionTools(this);
    }

    

    public override void OnShowGui()
    {    
        if(_projectSelected!=null)
        {
            if (Event.current.isMouse)
            {
                if (Event.current.button == 0)
                {
                    ActionOnClick();
                }
                
            }

            //FollowTexture();
        } 
    }

    private void FollowTexture()
    {
        Vector2 mousePos = _projectSelected.GridPixelArt.ScreenOriginToGridOrigin(Event.current.mousePosition);
        if (_projectSelected.GridPixelArt.IsMouseInGrid(mousePos))
        {
            mousePos = _projectSelected.GridPixelArt.ScreenPosToGridPos(mousePos);
            Vector2[] pixelToColor = _projectSelected.GridPixelArt.GetPixelWithSize(mousePos, _optionTools[1].GetSize);
            if (pixelToColor.Length != 0)
            {
                Vector2 newPos = _projectSelected.GridPixelArt.ScreenOriginToGridOrigin(new Vector2(pixelToColor[0].x, pixelToColor[0].y));
                Vector2 newPos2 = GUIUtility.GUIToScreenPoint(new Vector2(pixelToColor[0].x, pixelToColor[0].y));
                newPos2 = _projectSelected.GridPixelArt.ScreenOriginToGridOrigin(newPos2);
                Debug.Log(pixelToColor[0]);
                Debug.Log(newPos);
                Debug.Log(newPos2);
                Texture2D textureToFollow = _projectSelected.GridPixelArt.GetTextureFromVectors(pixelToColor, _optionTools[0].GetColor);
                if (textureToFollow == null) { Debug.Log("Bas alors"); }
                
               
               
                GUI.DrawTexture(new Rect(newPos2.x, newPos2.y, textureToFollow.width, textureToFollow.height), textureToFollow);
                _window.Repaint();
            }
        }
    }
    private void ActionOnClick ()
    {
        Vector2 mousePos = _projectSelected.GridPixelArt.ScreenOriginToGridOrigin(Event.current.mousePosition);
        if (_projectSelected.GridPixelArt.IsMouseInGrid(mousePos))
        {
            mousePos = _projectSelected.GridPixelArt.ScreenPosToGridPos(mousePos);
            Vector2[] pixelToColor = _projectSelected.GridPixelArt.GetPixelWithSize(mousePos, _optionTools[1].GetSize) ;


            _projectSelected.GridPixelArt.ColorPixels(pixelToColor, _optionTools[0].GetColor);
            _window.Repaint();
        }
    }

    
}
