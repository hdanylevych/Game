using System;

using UnityEngine;

public interface IMenu
{
    bool IsActive { get; set; }
    bool IsEnabled { get; set; }

    MenuType Type { get; }
    RectTransform RectTransform { get; }

    event Action<MenuType> OnChangeMenuClick;

    void Initialize();
}
