using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    [SerializeField]
    private int _hunger;
    [SerializeField]
    private int _happiness;

    private bool _serverTime;

    void Start()
    {
        UpdateStatus();
    }

    void UpdateStatus()
    {
        if (!PlayerPrefs.HasKey("_hunger"))
        {
            _hunger = 100;
            PlayerPrefs.SetInt("_hunger", _hunger);
        }
        else
        {
            _hunger = PlayerPrefs.GetInt("_hunger");
        }

        if (!PlayerPrefs.HasKey("_happiness"))
        {
            _happiness = 100;
            PlayerPrefs.SetInt("_happiness", _happiness);
        }
        else
        {
            _happiness = PlayerPrefs.GetInt("_happiness");
        }

        if (_serverTime)
        {
            UpdateServer();
        }
        else
        {
            UpdateDevice();
        }
    }

    void UpdateServer()
    {

    }

    void UpdateDevice()
    {

    }

    public int hunger
    {
        get { return _hunger; }
        set { _hunger = value; }
    }

    public int happiness
    {
        get { return _happiness; }
        set { _happiness = value; }
    }
    
}
