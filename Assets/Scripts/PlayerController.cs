﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class PlayerController : MovingObject
{
    public float restartLevelDelay = 1f;

    public int stamina;                           
    public int health;
    public int strength;

    private Dictionary<int, int> potions;
    

    protected override void Start()
    {
        stamina = GameManager.instance.playerCurrentStamina;
        health = GameManager.instance.playerCurrentHealth;
        strength = GameManager.instance.playerStrength;
        potions = GameManager.instance.foundPotions;
        
        base.Start();
    }

    private void OnDisable()
    {
        GameManager.instance.playerCurrentStamina = stamina;
        GameManager.instance.playerCurrentHealth = health;
        GameManager.instance.foundPotions = potions;
        GameManager.instance.playerStrength = strength;
    }


    private void Update()
    {
        if (!GameManager.instance.playersTurn) 
            return;

        int horizontal = 0;     //Used to store the horizontal move direction.
        int vertical = 0;       //Used to store the vertical move direction.


        horizontal = (int)(Input.GetAxisRaw("Horizontal"));

        vertical = (int)(Input.GetAxisRaw("Vertical"));

        if (horizontal != 0)
        {
            vertical = 0;
        }

        if (horizontal != 0 || vertical != 0)
        {
            AttemptMove<EnemyController>(horizontal, vertical);
            GameManager.instance.playersTurn = false;
        }
    }

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        base.AttemptMove<T>(xDir, yDir);

        RaycastHit2D hit;

        LoseStamina(1);
        if (Move(xDir, yDir, out hit))
        {
            //sound effect?
        }

        GameManager.instance.playersTurn = false;
    }

    protected override void OnCantMove<T>(T component)
    {
        if (typeof(T) == typeof(EnemyController))
        {
            EnemyController hitEnemy = component as EnemyController;

            int attack = strength + Random.Range(-2, 3);
            if (attack < 0)
                attack = 0;
            Debug.Log("You attack the " + hitEnemy.tag + "!  (-" + attack + " hp)");
            hitEnemy.LoseHealth(attack);
            if (GameManager.instance.enemiesKilled >= Math.Pow(2, GameManager.instance.playerLevel))
                LevelUp();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Exit")
        {
            Invoke("Restart", restartLevelDelay);
            PlayerPrefs.SetInt("Player Health", health);
            enabled = false;
        }
        #region items
        else if (other.tag == "Food1")
        {
            if (Random.Range(1, 11) == 1)
            {
                Debug.Log("You find a piece of bread on the floor and decide to eat it.");
            }
            else
            {
                Debug.Log("You eat a piece of bread.");
            }
            GainStamina(10);
            Destroy(other.gameObject);
        }
        else if (other.tag == "Food2")
        {
            if (Random.Range(1, 11) == 1)
            {
                Debug.Log("You find an apple on the floor and decide to eat it.");
            }
            else
            {
                Debug.Log("You eat an apple.");
            }
            GainStamina(20);
            Destroy(other.gameObject);
        }
        else if (other.tag == "Food3")
        {
            if (Random.Range(1, 11) == 1)
            {
                Debug.Log("You eat a raw chicken leg off of the floor.");
            }
            else
            {
                Debug.Log("You eat a chicken leg.");
            }
            GainStamina(30);
            Destroy(other.gameObject);
        }
        else if (other.tag == "Food4")
        {
            if (Random.Range(1, 11) == 1)
            {
                Debug.Log("You eat a chunk of meat you found on the floor.");
            }
            else
            {
                Debug.Log("You eat a chunk of meat.");
            }
            GainStamina(40);
            Destroy(other.gameObject);
        }
        else if (other.tag == "Food5")
        {
            if (Random.Range(1, 11) == 1)
            {
                Debug.Log("You eat a giant raw steak off of the ground.");
            }
            else
            {
                Debug.Log("You eat a large steak.");
            }
            GainStamina(50);
            Destroy(other.gameObject);
        }
        else if (other.tag == "Treasure1")
        {
            Debug.Log("You find some gold coins!");
            GameManager.instance.playerPoints += 100;
            Destroy(other.gameObject);
        }
        else if (other.tag == "Treasure2")
        {
            Debug.Log("You find a stack of gold coins!");
            GameManager.instance.playerPoints += 200;
            Destroy(other.gameObject);
        }
        else if (other.tag == "Treasure3")
        {
            Debug.Log("You find a large bag of gold coins!");
            GameManager.instance.playerPoints += 300;
            Destroy(other.gameObject);
        }
        else if (other.tag == "Potion1")
        {
            GetPotionEffect(1);
            Destroy(other.gameObject);
        }
        else if (other.tag == "Potion2")
        {
            GetPotionEffect(2);
            Destroy(other.gameObject);
        }
        else if (other.tag == "Potion3")
        {
            GetPotionEffect(3);
            Destroy(other.gameObject);
        }
        else if (other.tag == "Potion4")
        {
            GetPotionEffect(4);
            Destroy(other.gameObject);
        }
        else if (other.tag == "Potion5")
        {
            GetPotionEffect(5);
            Destroy(other.gameObject);
        }
        else if (other.tag == "Potion6")
        {
            GetPotionEffect(6);
            Destroy(other.gameObject);
        }
        else if (other.tag == "Potion7")
        {
            GetPotionEffect(7);
            Destroy(other.gameObject);
        }
        else if (other.tag == "Potion8")
        {
            GetPotionEffect(8);
            Destroy(other.gameObject);
        }
        else if (other.tag == "Potion9")
        {
            GetPotionEffect(9);
            Destroy(other.gameObject);
        }
        else if (other.tag == "Potion10")
        {
            GetPotionEffect(10);
            Destroy(other.gameObject);
        }
        else if (other.tag == "Potion11")
        {
            GetPotionEffect(11);
            Destroy(other.gameObject);
        }
        else if (other.tag == "Potion12")
        {
            GetPotionEffect(12);
            Destroy(other.gameObject);
        }
        #endregion

    }

    private void GetPotionEffect(int potionNum)
    {
        if (!potions.ContainsKey(potionNum))
        {
            int value = Random.Range(1, 13);
            while(potions.ContainsValue(value))
            {
                value = Random.Range(1, 13);
            }
            potions.Add(potionNum, value);
        }
        #region potions
        if (potions[potionNum] == 1)
        {
            Debug.Log("You drink a potion of healing!");
            GainHealth(50);
        }
        else if (potions[potionNum] == 2)
        {
            Debug.Log("You drink a potion of poison...");
            LoseHealth(20);
        }
        else if (potions[potionNum] == 3)
        {
            Debug.Log("You drink a potion of stamina!");
            GainStamina(100);
        }
        else if (potions[potionNum] == 4)
        {
            Debug.Log("You drink a potion of hunger...");
            LoseStamina(100);
        }
        else if (potions[potionNum] == 5)
        {
            Debug.Log("You drink a potion of strength!");
            GainStrength(4);
        }
        else if (potions[potionNum] == 6)
        {
            Debug.Log("You drink a potion of weakness...");
            LoseStrength(4);
        }
        else if (potions[potionNum] == 7)
        {
            Debug.Log("You drink a potion of augmented health!");
            GameManager.instance.playerMaxHealth += 10;
            GainHealth(10);
        }
        else if (potions[potionNum] == 8)
        {
            Debug.Log("You drink a potion of diminished health...");
            GameManager.instance.playerMaxHealth -= 10;
            LoseHealth(10);
        }
        else if (potions[potionNum] == 9)
        {
            Debug.Log("You drink a potion of augmented stamina!");
            GameManager.instance.playerMaxStamina += 50;
            GainStamina(50);
        }
        else if (potions[potionNum] == 10)
        {
            Debug.Log("You drink a potion of diminished stamina...");
            GameManager.instance.playerMaxStamina -= 50;
            LoseStamina(50);
        }
        else if (potions[potionNum] == 11)
        {
            Debug.Log("You drink an awful potion...");
            LevelDown();
        }
        else if (potions[potionNum] == 12)
        {
            Debug.Log("You drink a wondrous potion!");
            LevelUp();
        }
        #endregion

    }

    private void Restart()
    {
        GameManager.instance.NextLevel();
    }
    public void LoseHealth(int loss)
    {
        health -= loss;
        GameManager.instance.playerCurrentHealth -= loss;
        CheckIfGameOver();
    }

    public void GainHealth(int gain)
    {
        health += gain;
        GameManager.instance.playerCurrentHealth += gain;
        if (health > GameManager.instance.playerMaxHealth)
        {
            health = GameManager.instance.playerMaxHealth;
        }
    }

    public void LoseStamina(int loss)
    {
        stamina -= loss;
        GameManager.instance.playerCurrentStamina -= loss;
        
        CheckIfGameOver();
    }

    public void GainStamina(int gain)
    {
        stamina += gain;
        GameManager.instance.playerCurrentStamina+= gain;
        if (stamina > GameManager.instance.playerMaxStamina)
        {
            GainHealth(stamina - GameManager.instance.playerMaxStamina);
            stamina = GameManager.instance.playerMaxStamina;
        }
    }

    public void LoseStrength(int loss)
    {
        strength -= loss;
        if (strength < 0)
            strength = 0;
    }
    public void GainStrength(int gain)
    {
        strength += gain;
    }

<<<<<<< HEAD
            int levelStats = Random.Range(1, 4);
            if (levelStats != 1)
            {
                GainStamina(50);
                Debug.Log("Max Stamina rose to " + GameManager.instance.playerMaxStamina);
            }
            if (levelStats != 2)
            {
                GameManager.instance.playerMaxHealth += 10;
                GainHealth(10);
                Debug.Log("Max Health rose to " + GameManager.instance.playerMaxHealth);
            }
            if (levelStats != 3)
            {
                GameManager.instance.playerStrength += 2;
                strength += 2;
                Debug.Log("Strength rose to " + GameManager.instance.playerStrength);
            }
=======
    private void LevelDown()
    {
        GameManager.instance.playerPoints -= 100 * GameManager.instance.playerLevel;
        if (GameManager.instance.playerPoints < 0)
            GameManager.instance.playerPoints = 0;
        GameManager.instance.playerLevel--;
        Debug.Log("You have leveled down to level " + GameManager.instance.playerLevel + "...");
        if (GameManager.instance.playerLevel <= 0)
            health = 0;
        CheckIfGameOver();
    }

    private void LevelUp()
    {
        GameManager.instance.enemiesKilled = 0;
        GameManager.instance.playerPoints += 100 * GameManager.instance.playerLevel;
        GameManager.instance.playerLevel++;
            
        Debug.Log("You have leveled up to level " + GameManager.instance.playerLevel + "!");

        int levelStats = Random.Range(1, 4);
        if (levelStats != 1)
        {
            GameManager.instance.playerMaxStamina += 50;
            GainStamina(50);
            Debug.Log("Max Stamina rose to " + GameManager.instance.playerMaxStamina + ".");
        }
        if (levelStats != 2)
        {
            GameManager.instance.playerMaxHealth += 10;
            GainHealth(10);
            Debug.Log("Max Health rose to " + GameManager.instance.playerMaxHealth + ".");
        }
        if (levelStats != 3)
        {
            GameManager.instance.playerStrength += 2;
            strength += 2;
            Debug.Log("Strength rose to " + GameManager.instance.playerStrength + ".");
>>>>>>> cd997a47f6608cb91d378983c641195464239c92
        }
    }

    private void CheckIfGameOver()
    {
        if (stamina <= 0 || health <= 0)
        {
            if (stamina <= 0)
                Debug.Log("You have starved to death!");
            else if (health <= 0)
                Debug.Log("You have died!");
            Destroy(gameObject);
            GameManager.instance.GameOver();
        }
    }
}
