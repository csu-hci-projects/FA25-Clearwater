using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
   [SerializeField] [TextArea] private string[] dialogue;

   public string[] Dialogue => dialogue;

   public void ClearAndFill(string[] newDialogue)
   {
      dialogue = new string[newDialogue.Length];
      for (int i = 0; i < newDialogue.Length; i++) dialogue[i] = newDialogue[i];
   }
}
