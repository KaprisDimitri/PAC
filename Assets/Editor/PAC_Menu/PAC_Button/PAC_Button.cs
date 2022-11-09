using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAC_Button
{
    private string _name;
    public string Name { get { return _name; } }

    public PAC_Button(string name) { _name = name; }

    public delegate void ActionButton();

    public ActionButton action;

}
