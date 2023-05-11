using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteCentralizer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Get the sprite
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;

        // Calculate the offset to move the image to the center of the sprite
        Vector2 offset = new Vector2(sprite.rect.width / 2f - sprite.pivot.x, sprite.rect.height / 2f - sprite.pivot.y);

        // Set the textureRectOffset to the offset
        //sprite.textureRectOffset = offset;

        // Set the textureRect to the full texture size
        //sprite.textureRect = new Rect(0f, 0f, sprite.texture.width, sprite.texture.height);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
