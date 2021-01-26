using System.Collections.Generic;

using DG.Tweening;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityWeld.Binding;

[Binding]
public class MenuMediator : View
{
    private Dictionary<MenuType, IMenu> _menus = new Dictionary<MenuType, IMenu>(3);
    private IMenu _activeMenu;

    private Tween _moveOutTween;
    private Tween _moveInTween;

    [SerializeField] private float MenuTransitionDuration = 1f;

    private void Start()
    {
        base.Start();

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            var childGO = gameObject.transform.GetChild(i);
            var menu = childGO.GetComponent<IMenu>();

            menu.Initialize();

            menu.OnChangeMenuClick += ProcessMenuChanging;

            _menus.Add(menu.Type, menu);
        }

        StartWithMainMenu();
    }

    public void StartWithMainMenu()
    {
        _activeMenu = _menus[MenuType.Main];
        _activeMenu.IsEnabled = true;
        _activeMenu.IsActive = true;
    }

    public void DisableActiveMenu()
    {
        if (_activeMenu == null)
            return;

        _activeMenu.IsActive = false;
        _activeMenu.IsEnabled = false;
        _activeMenu = null;
    }

    public void ProcessMenuChanging(MenuType type)
    {
        if (_menus.ContainsKey(type) == false)
        {
            Debug.LogError($"MenuMediator: couldn't find menu with type: {type}.");
            return;
        }

        var oldActiveMenu = _activeMenu;

        IMenu menuToChange = _menus[type];
        menuToChange.RectTransform.anchoredPosition = new Vector2(Screen.width, 0);
        menuToChange.IsEnabled = true;

        _moveInTween = menuToChange.RectTransform.DOMoveX(Screen.width / 2, MenuTransitionDuration).OnComplete(() => SetNewActiveMenu(oldActiveMenu, menuToChange));
        _moveOutTween = _activeMenu.RectTransform.DOMoveX(-Screen.width / 2, MenuTransitionDuration);

        _activeMenu.IsActive = false;
        _activeMenu = null;
        
        _moveOutTween.Play();
        _moveInTween.Play();
    }

    private void SetNewActiveMenu(IMenu oldActiveMenu, IMenu newActiveMenu)
    {
        oldActiveMenu.IsActive = false;
        oldActiveMenu.IsEnabled = false;

        _activeMenu = newActiveMenu;
        _activeMenu.IsActive = true;
    }
}
