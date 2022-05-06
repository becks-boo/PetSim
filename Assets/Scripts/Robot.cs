using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    [SerializeField]
    private int _hunger;
    [SerializeField]
    private int _happiness;
    [SerializeField]
    private string _name;

    private int _clickCount;
    private bool _serverTime;

    void Start()
    {
        PlayerPrefs.SetString("then", "05/05/2022 11:20:12");
        UpdateStatus();

        if (!PlayerPrefs.HasKey("name"))
        {
            PlayerPrefs.SetString("name", "Robot");
        }

        _name = PlayerPrefs.GetString("name");
    }

    void Update()
    {
        GetComponent<Animator>().SetBool("jump", gameObject.transform.position.y > -2.9f);

        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Clicked");
            // To grab the mouse location, where clicked
            Vector2 v = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            // To check what was clicked
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(v), Vector2.zero);

            if (hit)
            {
                if (hit.transform.gameObject.tag == "Robot")
                {
                    _clickCount++;
                    if (_clickCount >= 3)
                    {
                        _clickCount = 0;
                        UpdateHappiness(1);
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1000000));
                    }
                }
            }
        }
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

        if (!PlayerPrefs.HasKey("then"))
        {
            PlayerPrefs.SetString("then", GetStringTime());
        }

        TimeSpan ts = GetTimeSpan();

        _hunger -= (int)(ts.TotalHours * 2);
        if (_hunger < 0)
        {
            _hunger = 0;
        }

        _happiness -= (int)((100 - _hunger) * (ts.TotalHours / 5));
        if (_happiness < 0)
        {
            _happiness = 0;
        }

        if (_serverTime)
        {
            UpdateServer();
        }
        else
        {
            InvokeRepeating("UpdateDevice", 0f, 30f);
        }
    }

    void UpdateServer()
    {

    }

    void UpdateDevice()
    {
        PlayerPrefs.SetString("then", GetStringTime());
    }

    private TimeSpan GetTimeSpan()
    {
        CultureInfo myCultureInfo = new CultureInfo("de-DE");
        if (_serverTime)
        {
            return new TimeSpan();
        }
        else
        {
            return DateTime.Now - Convert.ToDateTime(PlayerPrefs.GetString("then"), myCultureInfo);
        }
    }

    private string GetStringTime()
    {
        DateTime now = DateTime.Now;
        return now.Month + "/" + now.Day + "/" + now.Year + " " + now.Hour + ":"
            + now.Minute + ":" + now.Second;
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

    public string name
    {
        get { return _name; }
        set { _name = value; }
    }

    public void UpdateHappiness(int i)
    {
        happiness += i;
        if (happiness > 100)
        {
            happiness = 100;
        }
    }
}
