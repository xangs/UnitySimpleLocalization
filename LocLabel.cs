using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using Game.Utils;

[RequireComponent(typeof(Text))]
public class LocLabel : MonoBehaviour, INeedLocalization
{
    public static Dictionary<string, Word> words = new Dictionary<string, Word>();
    public static bool initialized = false;   


    public bool localized = false;
    public string key;
    public string text;
    public bool update = false;
    public float delay = 0.2f;
    float lastTime = 0;
    public List<Mask> masks = new List<Mask>();

    public static string result;
    private Text label;

    void Init()
    {
        Debug.Log("Init!");
    }

    void Awake()
    {        
        if (!initialized)
            Initialize();
        label = GetComponent<Text>();
        ItemCounter.refresh += UpdateLabel;
    }

    void OnEnable()
    {
        Awake();
        UpdateLabel();
    }

    public static void Initialize()
    {
        initialized = true;
    }
    
    void OnDestroy()
    {
              
    }

    void Update()
    {
        
        if (!update) return;
        if (lastTime + delay > Time.unscaledTime) return;
        lastTime = Time.unscaledTime;
        UpdateLabel();
        Debug.Log(label);
    }

    void UpdateLabel()
    {
        result = GetText();
        foreach (Mask mask in masks)
        result = result.Replace("{" + mask.key + "}", words[mask.value].Invoke());
        label.text = result;        
    }

    public string GetText()
    {
        return localized ? Localization.main[key] : text;
    }

    public List<string> RequriedLocalizationKeys()
    {
        List<string> result = new List<string>();        
        return result;
    }

    public delegate string Word();

    [System.Serializable]
    public class Mask
    {
        public string key = "";
        public string value = "";

        public Mask(string _key)
        {
            key = _key;
        }
    }
}
