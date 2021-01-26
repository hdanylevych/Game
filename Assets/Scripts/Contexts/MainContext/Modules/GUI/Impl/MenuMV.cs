using System;
using UnityEngine;
using UnityWeld.Binding;

public abstract class MenuMV : CanvasMV, IMenu
{
    protected bool _isEnabled;
    protected RectTransform _rectTransform;
    protected MenuType _type = MenuType.Settings;

    public MenuType Type => _type;

    public RectTransform RectTransform => _rectTransform;
    public bool IsActive { get; set; } = false;

    [Binding]
    public bool IsEnabled
    {
        get => _isEnabled;

        set
        {
            _isEnabled = value;
            gameObject.SetActive(value);
            OnPropertyChanged();
        }
    }

    protected bool CanProcessInput => IsEnabled && IsActive;

    public event Action<MenuType> OnChangeMenuClick;

    public abstract void Initialize();

    protected void InvokeOnChangeMenuClick(MenuType menuType)
    {
        OnChangeMenuClick?.Invoke(menuType);
    }
}
