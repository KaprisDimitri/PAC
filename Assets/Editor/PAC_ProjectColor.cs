using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAC_ProjectColor
{
    private PAC_MainWindow _window;

    private Rect _rectGroupeColor;
    private Rect _rectButtonColor;
    private Rect _rectButtonPage;
    private ColorPage[] colorPage;

    private int maxPerPage;
    private int actualPage;
    public PAC_ProjectColor (Rect rectGroupeColor, PAC_MainWindow window)
    {
        _window = window;
        _rectGroupeColor = rectGroupeColor;
        _rectButtonColor = new Rect(0, 0, 50, 50);
        _rectButtonPage = new Rect(0, _rectGroupeColor.height - _rectButtonColor.height, 33, 50);
        colorPage = new ColorPage[1];
        colorPage[0] = new ColorPage();
        actualPage = 0;
        maxPerPage = 16;
    }

    public void OnShowGUI ()
    {
        GUI.BeginGroup(_rectGroupeColor);
        Rect _rectButtonColortemp = _rectButtonColor;
        for (int i = 0; i < colorPage[actualPage].colorList.Length; i++)
        {
            GUI.backgroundColor = colorPage[actualPage].colorList[i];
            int moduloX = (i % 2);
            int moduloY = 0;
            if (moduloX == 1) { moduloY = 0; } else if(moduloX == 0) { moduloY = 1; }
            _rectButtonColortemp.x = _rectButtonColortemp.width * moduloX;
            _rectButtonColortemp.y += _rectButtonColortemp.height * moduloY;
            if (GUI.Button(_rectButtonColortemp,  ""))
            {
                if(_window.ToolSelected != null)
                {
                    if (_window.ToolSelected.GetType() == typeof(PAC_ToolPaint))
                    {
                        _window.ToolSelected.GetOptionTools[0].GetColor = colorPage[actualPage].colorList[i];
                    }
                }
                
            }
        }
        GUI.backgroundColor = Color.white;

        _rectButtonColortemp = _rectButtonPage;
        if (GUI.Button(_rectButtonColortemp, "<"))
        {
            ChangePage(-1);
        }

        _rectButtonColortemp.x = _rectButtonPage.width ;
        GUI.Label(_rectButtonColortemp, actualPage+1 +"/"+colorPage.Length);

        _rectButtonColortemp.x = _rectButtonPage.width*2;
        if (GUI.Button(_rectButtonColortemp, ">"))
        {
            ChangePage(1);
        }


        
        GUI.EndGroup();
    }

    public void AddColor (Color colorToAdd)
    {
        if(colorPage.Length == 0 || colorPage[colorPage.Length-1].colorList.Length == maxPerPage)
        {
            AddColorPage();

        }
        colorPage[colorPage.Length - 1].AddColor(colorToAdd);
        _window.Repaint();
    }

    private void AddColorPage ()
    {
        ColorPage[] colorTemp = colorPage;
        colorPage = new ColorPage[colorTemp.Length + 1];

        for (int i = 0; i < colorTemp.Length; i++)
        {
            colorPage[i] = colorTemp[i];
        }

        colorPage[colorTemp.Length] = new ColorPage();
    }

    private void ChangePage (int index)
    {
        if(index == -1)
        {
            int actualPageTemp = actualPage - 1;
            if(actualPageTemp < 0)
            {
                actualPageTemp = colorPage.Length - 1;
            }
            actualPage = actualPageTemp;
        }
        else if (index == 1)
        {
            int actualPageTemp = actualPage + 1;
            if (actualPageTemp >= colorPage.Length)
            {
                actualPageTemp = 0;
            }
            actualPage = actualPageTemp;
        }

       
    }
}

public class ColorPage
{
    public Color[] colorList;

    public ColorPage()
    {
        colorList = new Color[0];
    }

    public void AddColor(Color colorToAdd)
    {
        Color[] colorTemp = colorList;
        colorList = new Color[colorTemp.Length + 1];

        for (int i = 0; i < colorTemp.Length; i++)
        {
            colorList[i] = colorTemp[i];
        }

        colorList[colorTemp.Length] = colorToAdd;
    }

}
