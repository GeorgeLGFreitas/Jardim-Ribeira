using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

[ExecuteInEditMode]
public class GetSpriteAtlas : MonoBehaviour
{
    [SerializeField] private SpriteAtlas atlas;
    [SerializeField] private string spriteName;
    [SerializeField] private bool isSprite;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();

        if (isSprite)
        {
            if (atlas != null && spriteName != null) this.GetComponent<SpriteRenderer>().sprite = atlas.GetSprite(spriteName);
        }
        else
        {
            if (atlas != null && spriteName != null) this.GetComponent<Image>().sprite = atlas.GetSprite(spriteName);
        }
    }

    public void SetSpriteAtlas(string spriteName)
    {
        sprite.sprite = atlas.GetSprite(spriteName);
    }

    public Sprite GetSpriteatlas() { return this.GetComponent<SpriteRenderer>().sprite = atlas.GetSprite(spriteName); }
}
