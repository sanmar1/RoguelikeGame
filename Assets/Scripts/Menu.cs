﻿using UnityEngine;
using System.Collections;

public class Menu: MonoBehaviour
{
   private int labelA = 375, labelB = 10, labelC = 215, labelD = 105;
   private int buttonA = 400, buttonB = 270, buttonC = 70, buttonD = 30;
   private int _labelA = 310, _labelB = 50, _labelC = 270, _labelD =20;
   private int __labelA = 270, __labelB = 80, __labelC = 400, __labelD = 20;
   private int ___labelA = 350, ___labelB = 110;
   public GUIStyle customGuiStyle;

   void OnGUI()
   {
      GUI.backgroundColor = Color.yellow;
      //GUI.Button(new Rect(100, 100, 70, 30), "Start");
      if (GUI.Button(new Rect(buttonA, buttonB, buttonC, buttonD), "Start"))
      {
         Application.LoadLevel("Main");
      }
      GUI.Label(new Rect(labelA, labelB, labelC, labelD), "Dungeon Adventure");
      GUI.Label(new Rect(_labelA, _labelB, _labelC, _labelD), "By Nathan Roberts and Amadeus Sanchez");
      GUI.Label(new Rect(__labelA, __labelB, __labelC, __labelD), "Use the arrow keys to move. Press 'P' to pause/play music.");
      GUI.Label(new Rect(___labelA, ___labelB, __labelC, __labelD), "Press 'R' to restart the game.");
   }
}