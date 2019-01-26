using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour {

    public Color color;
    public HighlightingSystem.Highlighter highlighter;

    private void Start()
    {
        highlighter = gameObject.AddComponent<HighlightingSystem.Highlighter>();
    }

    // Update is called once per frame
    void Update () {
        highlighter.On(color);
	}
}
