using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICodebox : MonoBehaviour {

    public Text[] inputs;
    private int inputIdx = 0;
    public Codebox codeBox;

    private PlayerLogic player
    {
        get
        {
            if(m_Player == null)
            {
                m_Player = FindObjectOfType<PlayerLogic>();
            }
            return m_Player;
        }
    }
    private PlayerLogic m_Player;

    public void Input(int num)
    {
        if (inputIdx >= inputs.Length)
            return;
        inputs[inputIdx++].text = num.ToString();
        if (inputIdx == inputs.Length)
            Check();
    }

    public void Backspace()
    {
        if (inputIdx == 0)
            return;
        inputs[--inputIdx].text = "";
    }

    private string GetCode()
    {
        string res = "";
        for(int i=0; i<inputs.Length; i++)
        {
            res = res + inputs[i].text;
        }
        return res;
    }

    private void Check()
    {
        if(codeBox != null)
        {
            string code = GetCode();
            bool result = codeBox.CheckCode(code);
            if(result)
            {
                codeBox.EnterCode(code);
                player.AllowMove = true;
                gameObject.SetActive(false);
            }
        }
    }

    public void Return()
    {
        player.AllowMove = true;
        gameObject.SetActive(false);
    }

    public void Init()
    {
        for (int i = 0; i < inputs.Length; i++)
        {
            inputs[i].text = "";
        }
        inputIdx = 0;
    }
}
