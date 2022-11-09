using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Drop down qui s affiche quand on click sur un button du menu
/// </summary>
public class PAC_MenuDropDown : EditorWindow
{
    private PAC_Button[] _buttons;
    private Rect _rectButtons;
    public void InitMenuDropDown (PAC_Button[] buttons, Rect rectButtons)
    {
        _buttons = buttons;
        _rectButtons = rectButtons;
    }

    private void OnGUI()
    {
        for(int i = 0; i< _buttons.Length;i++)
        {
            Rect rectButtons = _rectButtons;
            rectButtons.y = _rectButtons.height * i;
            if (GUI.Button(rectButtons, _buttons[i].Name))
            {
                _buttons[i].action();
                this.Close();
            }
        }
    }


}
