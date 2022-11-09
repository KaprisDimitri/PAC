using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAC_Tools
{
    private PAC_MainWindow _window;

    private PAC_Tool[] _tools;

    private Rect _rectGroupTools;

    private Rect _rectButtonTools;

    

    public PAC_Tools (Rect rectGroupTools, Rect rectButtonTools, PAC_MainWindow window)
    {
        _rectGroupTools = rectGroupTools;
        _rectButtonTools = rectButtonTools;

        _window = window;

        InitTools();
    }

    private void InitTools()
    {
        _tools = new PAC_Tool[2];

        _tools[0] = new PAC_ToolPaint();
        _tools[0].Window = _window;

        _tools[1] = new PAC_ToolEraser();
        _tools[1].Window = _window;
    }

    public void OnShowGUI ()
    {
        GUI.BeginGroup(_rectGroupTools);
        for(int i = 0;i<_tools.Length;i++)
        {
            Rect temp = _rectButtonTools;
            temp.y = _rectButtonTools.height * i;
            if (GUI.Button(temp, _tools[i].Name))
            {
                _window.ToolSelected = _tools[i];
            }
        }
        GUI.EndGroup();
    }
}
