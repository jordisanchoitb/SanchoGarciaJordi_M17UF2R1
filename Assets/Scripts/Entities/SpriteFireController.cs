using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFireController : MonoBehaviour
{
    // Lista de sprites de fuego naranja
    [SerializeField] private List<Sprite> orangeFireSprites = new List<Sprite>();
    [SerializeField] private List<Sprite> blueFireSprites = new List<Sprite>();

    private ParticleSystem particleSystem;
    private WeaponManager weaponManager;
    private float timeToChangeSprite = 5f;
    private float currentTime = 0f;

    private void OnEnable()
    {
        currentTime = 0f;
        ChangeDefaultSprite();
    }

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        weaponManager = GetComponentInParent<WeaponManager>();
    }

    void Update()
    {
        if (weaponManager.IsFiring())
        {
            currentTime += Time.deltaTime;
            if (currentTime >= timeToChangeSprite)
            {
                ChangeBlueSprite();
                currentTime = 0f;
            }
        }
        else
        {
            currentTime = 0f;
            ChangeDefaultSprite();
        }
    }

    private void ChangeBlueSprite()
    {
        particleSystem.textureSheetAnimation.SetSprite(0, blueFireSprites[0]);
        particleSystem.textureSheetAnimation.SetSprite(1, blueFireSprites[1]);
        particleSystem.textureSheetAnimation.SetSprite(2, blueFireSprites[2]);
        particleSystem.textureSheetAnimation.SetSprite(3, blueFireSprites[3]);
        particleSystem.textureSheetAnimation.SetSprite(4, blueFireSprites[4]);
        particleSystem.textureSheetAnimation.SetSprite(5, blueFireSprites[5]);
        particleSystem.textureSheetAnimation.SetSprite(6, blueFireSprites[6]);
        particleSystem.textureSheetAnimation.SetSprite(7, blueFireSprites[7]);
    }

    private void ChangeDefaultSprite()
    {
        particleSystem.textureSheetAnimation.SetSprite(0, orangeFireSprites[0]);
        particleSystem.textureSheetAnimation.SetSprite(1, orangeFireSprites[1]);
        particleSystem.textureSheetAnimation.SetSprite(2, orangeFireSprites[2]);
        particleSystem.textureSheetAnimation.SetSprite(3, orangeFireSprites[3]);
        particleSystem.textureSheetAnimation.SetSprite(4, orangeFireSprites[4]);
        particleSystem.textureSheetAnimation.SetSprite(5, orangeFireSprites[5]);
        particleSystem.textureSheetAnimation.SetSprite(6, orangeFireSprites[6]);
        particleSystem.textureSheetAnimation.SetSprite(7, orangeFireSprites[7]);
    }


}
