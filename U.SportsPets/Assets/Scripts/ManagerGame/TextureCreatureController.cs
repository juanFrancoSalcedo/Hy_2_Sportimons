using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureCreatureController : MonoBehaviour
{
    public Renderer renderObjectMat;
    [SerializeField] Texture2D flicker;
    [SerializeField] Texture2D poker;
    [SerializeField] Texture2D stunned;
    [SerializeField] Texture2D cool;
    public Material powMaterial;

    public void SetTextures(SportCreature _athlete, int colorIndex)
    {
        Debug.Log(Constants.teamColor[colorIndex].colorTeam);
        renderObjectMat.materials[0].SetColor("_ColorAlbedo",Constants.teamColor[colorIndex].colorTeam);
        PokerTexture();
    }

    public void ToBlinkTexture()
    {
        renderObjectMat.material.SetTexture("_SecundaryMap",flicker);
    }

    public void PokerTexture()
    {
        renderObjectMat.material.SetTexture("_SecundaryMap", poker);
    }

    public void StunTexture()
    {
        renderObjectMat.material.SetTexture("_SecundaryMap", stunned);
    }

    public void CoolTexture()
    {
        renderObjectMat.material.SetTexture("_SecundaryMap", cool);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) { PokerTexture(); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { ToBlinkTexture(); }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { CoolTexture(); }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { StunTexture(); }
        //if (Input.GetKeyDown(KeyCode.E)) { SetTextures(null,3); }
    }
}
