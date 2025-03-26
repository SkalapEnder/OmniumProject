using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class WindowService : MonoBehaviour
{
    [SerializeField] private Window[] windows;
    private Dictionary<Type, Window> windowsDictionary;

    public void Initialize()
    {
        windowsDictionary = new Dictionary<Type, Window>();
        foreach (Window window in windows)
        {
            windowsDictionary.Add(window.GetType(), window);
            window.Hide(true);
            window.Initialize();
        }
        ShowWindow<MainMenuWindow>(true);
    }

    public T GetWindow<T>() where T : Window
    {
        return windowsDictionary[typeof(T)] as T;
    }

    public void ShowWindow<T>(bool isImmediately) where T : Window
    {
        var window = windowsDictionary[typeof(T)] as T;
        if (window == null)
        {
            Debug.LogError("Not found window");
            return;
        }
        window.Show(isImmediately);
    }

    public void HideWindow<T>(bool isImmediately) where T : Window
    {
        var window = windowsDictionary[typeof(T)] as T;
        if (window == null)
        {
            Debug.LogError("Not found window");
            return;
        }
        window.Hide(isImmediately);
    }

    public void HideAllWindow(bool isImmediately)
    {
        foreach(Window window in windows)
        {
            if (window == null)
            {
                Debug.LogError("Not found window");
                return;
            }
            window.Hide(isImmediately);
        }
    }
}
