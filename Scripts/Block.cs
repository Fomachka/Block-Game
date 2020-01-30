using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitSprites;


    // cached reference
    Level level;

    // state variable
    [SerializeField] int timesHit; // serialized only for debugging purposes
    

    private void Start()
    {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        { 
           level.CountBlocks();
        }
    }

    //creating a method on Collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(tag == "Breakable")
        {
            timesHit++;
            // maxHits is equal to the length of the array + 1 , since array starts from 0
            int maxHits = hitSprites.Length + 1;

            if(timesHit >= maxHits)
            {
                DestroyBlock();
            }
            else
            {
                ShowNextHitSprite();
            }
        }
    }

    private void ShowNextHitSprite()
    {
        // when timesHit hits once, hitSprites[] initiates from zero like all other arrays
        int spriteIndex = timesHit - 1;
        // if sprite element is not empty (every sprite was added)...
        if(hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError(gameObject.name + " is missing from array");
        }
    }

    private void DestroyBlock()
    {
        PlayBlockDestroySoundSFX();
        Destroy(gameObject);
        TriggerSparklesVFX();
        level.BlockDestroyed();
    }

    private void PlayBlockDestroySoundSFX()
    {
        //play sound at the camera position so you can hear it
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        FindObjectOfType<GameSession>().AddToScore();
    }

    private void TriggerSparklesVFX()
    {
        //instantiating sparkles and destroying the effect after a delay
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }

}
