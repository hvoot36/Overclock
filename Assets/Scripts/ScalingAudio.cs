using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScalingAudio : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioSource source;
    public Vector2 minScale;
    public Vector2 maxScale;


    [SerializeField]
    private AudioDetection detector;

    TMP_Text textBox;

    public float loudnessSensitivity = 100;
    void Start()
    {
        textBox = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        float loudness = detector.loudnessValue * loudnessSensitivity;
        

        

        textBox.text = "Audio Level: " + Mathf.RoundToInt(loudness).ToString();
    }
}
