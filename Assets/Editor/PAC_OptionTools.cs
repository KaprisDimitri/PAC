using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAC_OptionTools
{
    private PAC_OptionTool[] _optionTools;

    private Rect _rectOptionTools;

    public PAC_OptionTool[] OptionTools
    {
        get { return _optionTools; }
        set { _optionTools = value; }
    }

    public PAC_OptionTools (Rect rectOptionTools)
    {
        _rectOptionTools = rectOptionTools;
    }

    public void OnShowGUI ()
    {
        GUI.BeginGroup(_rectOptionTools);

        for(int i = 0; i< _optionTools.Length;i++)
        {
            _optionTools[i].OnShowGui();
        }

        GUI.EndGroup();
    }
}
