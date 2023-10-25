using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    [SerializeField] private Color _startColor;
    [SerializeField] private Color _endColor;
    [SerializeField] private float _duration = 2.75f;
    [SerializeField] private float _startTime = 2.5f;

    IEnumerator Start()
    {
        // get the text mesh pro text component attached
        TMPro.TextMeshProUGUI text = GetComponent<TMPro.TextMeshProUGUI>();

        text.color = _startColor;

        yield return new WaitForSeconds(_startTime);

        // lerp the color from the start color to the end color over the duration
        var time = 0f;
        while (time < _duration)
        {
            text.color = Color.Lerp(_startColor, _endColor, time / _duration);
            time += Time.deltaTime;
            yield return null;
        }
        text.color = _endColor;
    }
}
