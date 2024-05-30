//Author: Small Hedge Games
//Date: 30/05/2024

using UnityEngine;
using System;

public class ParallaxManager : MonoBehaviour
{
    [SerializeField] private Background[] backgrounds;
    [SerializeField] private bool isBackground;
    [SerializeField] private bool changeAnchorsOnStart;
    [SerializeField] private float heightMultiplier;
    [SerializeField] private Transform anchor;
    public Transform gameCamera;

    private Vector3 anchorPosition;

    private void Start()
    {
        if (!anchor) anchorPosition = gameCamera.position;

        if(changeAnchorsOnStart)
            for (int i = 0; i < backgrounds.Length; i++)
                backgrounds[i].anchor = backgrounds[i].sprite.position;
    }

    private void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            if (anchor) anchorPosition = anchor.position;
            float adjustedIntensity = backgrounds[i].intensity;
            if (isBackground) adjustedIntensity = (1 - adjustedIntensity) * -1;
            backgrounds[i].sprite.position = (Vector2)anchorPosition + backgrounds[i].anchor + new Vector2(-adjustedIntensity, adjustedIntensity * heightMultiplier) * (gameCamera.position - anchorPosition);
        }
    }

    public void ChangeAnchor(int index, Vector2 anchor)
    {
        backgrounds[index].anchor = anchor;
    }

    public void GetBackgrounds()
    {
        Array.Resize(ref backgrounds, transform.childCount);
        for (int i = 0; i < backgrounds.Length; i++)
        {
            if (!backgrounds[i].sprite) 
                backgrounds[i].intensity = GetIntensity(i);

            backgrounds[i].sprite = transform.GetChild(i);
            backgrounds[i].name = backgrounds[i].sprite.name;
        }
    }

    public void SetIntensities()
    {
        for (int i = 0; i < backgrounds.Length; ++i)
            backgrounds[i].intensity = GetIntensity(i);
    }

    private float GetIntensity(float n)
    {
        return (n + 1) / (backgrounds.Length + 1);
    }
}

[Serializable]
public struct Background
{
    [HideInInspector] public string name;
    [Range(0, 1)] public float intensity;
    public Transform sprite;
    [HideInInspector] public Vector2 anchor;
}
