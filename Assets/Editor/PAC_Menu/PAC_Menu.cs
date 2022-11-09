using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAC_Menu
{
    /// <summary>
    /// Variable qui Contient la window Main
    /// </summary>
    private PAC_MainWindow _window;

    #region Variable Rect
    private Rect _rectGroupeMenu;
    private Rect _rectButton;
    
    #endregion

    private ContainerButton[] buttonsOnMenu;

    private PAC_MenuDropDown _dropDownWindow;

    #region Fonction Initiation Menu
    public PAC_Menu(Rect rectGroupeMenu, Rect rectButton, PAC_MainWindow window)
    {
        _rectGroupeMenu = rectGroupeMenu;
        _rectButton = rectButton;
       
        _window = window;
        InitButtonsOnMenu();
    }

    
    private void InitButtonsOnMenu ()
    {
        buttonsOnMenu = new ContainerButton[2];

        #region Set Premier Button
        PAC_Button[] buttonsInContainer = new PAC_Button[3];
        buttonsInContainer[0] = new PAC_Button("New");
        buttonsInContainer[0].action = _window.OpenWindowCreatNewProject;

        buttonsInContainer[1] = new PAC_Button("Save As");
        buttonsInContainer[1].action = _window.OpenWindowSaveAs;

        buttonsInContainer[2] = new PAC_Button("Save");
        buttonsInContainer[2].action = _window.Save;


        buttonsOnMenu[0] = new ContainerButton("File", buttonsInContainer);
        #endregion

        #region Set Deuxieme Button
        buttonsInContainer = new PAC_Button[0];

        buttonsOnMenu[1] = new ContainerButton("Edite", buttonsInContainer);
        #endregion
    }
    #endregion
    public void ShowOnGUI()
    {
        GUI.BeginGroup(_rectGroupeMenu);

        for(int i = 0; i< buttonsOnMenu.Length;i++)
        {
            Rect rectForButton = _rectButton;
            rectForButton.x  = rectForButton.width * i;
           if (GUI.Button(rectForButton, buttonsOnMenu[i].NameButton))
           {
                ActionOnClickButtonMenu(buttonsOnMenu[i].ButtonsInContainer, rectForButton);
           }
        }

        GUI.EndGroup();
    }


    /// <summary>
    /// Fonction qui affiche le dropDown du button dans le menu
    /// </summary>
    /// <param name="_buttonsInContainer">buttons dans le drop down contenu dans le Container</param>
    /// <param name="rectButtonClick">rect du button clicker</param>
    private void ActionOnClickButtonMenu (PAC_Button[] _buttonsInContainer, Rect rectButtonClick)
    {
        //Creation de la fenetre
        _dropDownWindow = ScriptableObject.CreateInstance<PAC_MenuDropDown>();
        _dropDownWindow.InitMenuDropDown(_buttonsInContainer, _rectButton);

        //Rect remis en worldPosition
        Vector2 vectorInScreenSpanwn = GUIUtility.GUIToScreenPoint(new Vector2(rectButtonClick.x, rectButtonClick.y));
        Rect rectDropDown = rectButtonClick;
        rectDropDown.x = vectorInScreenSpanwn.x;
        rectDropDown.y = vectorInScreenSpanwn.y;
     

        //Affichage du dropDown
        _dropDownWindow.ShowAsDropDown(rectDropDown, new Vector2(rectButtonClick.width, rectButtonClick.height * _buttonsInContainer.Length));
    }

    /// <summary>
    /// Class qui contient les information a afficher dans les drop down de chaque button du menu
    /// </summary>
    private class ContainerButton
    {
        /// <summary>
        /// Nom du button afficher
        /// </summary>
        private string _nameButton;

        /// <summary>
        /// Button contenue dans le dropDown
        /// </summary>
        private PAC_Button[] _buttonsInContainer;

        public string NameButton
        {
            get { return _nameButton; }
        }

        public PAC_Button[] ButtonsInContainer
        {
            get { return _buttonsInContainer; }
        }

        public ContainerButton (string nameButton, PAC_Button[] buttonsInContainer)
        {
            _nameButton = nameButton;
            _buttonsInContainer = buttonsInContainer;
        }
    }
}
