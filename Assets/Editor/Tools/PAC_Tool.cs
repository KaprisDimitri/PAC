using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PAC_Tool
{
    protected string _name;

    protected PAC_Project _projectSelected;

    protected PAC_OptionTool[] _optionTools;

    protected PAC_MainWindow _window;

    public PAC_MainWindow Window
    {
        set { _window = value; }
    }

    public string Name
    {
        get { return _name; }
    }

    public PAC_Project ProjectSelected
    {
        set { _projectSelected = value; }
        get { return _projectSelected; }
    }

    public PAC_OptionTool[] GetOptionTools
    {
        get { return _optionTools; }
    }

    public void InitOptionTools(PAC_Tool tool)
    {
       
        for (int i = 0; i < _optionTools.Length; i++)
        {
            if (i != 0)
            {
                _optionTools[i].InitInListOptionTool(_optionTools[i - 1], tool);

            }
            else
            {
                _optionTools[i].InitInListOptionTool(null, tool);
            }
        }
    }

    public abstract void OnShowGui();

    protected abstract void SetOptionTool();
}
