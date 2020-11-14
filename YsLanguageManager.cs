/***
*   Company:Frozen Wolf Sudio
*	Title："LockstepFrame" Lockstep框架项目
*		主题：XXX
*	Description：
*		功能：XXX
*	Date：2019
*	Version：0.1版本
*	Author：xxx
*	Modify Recoder：
*/


using DuloGames.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace FightClub
{
  public enum LanguageType
  {
    Chinese,
    English,
  }
  public enum LanguageUpdateMode
  {
     ALL,
  }
  public  class YsLanguageManager:MonoBehaviour
  {
    public static YsLanguageManager TO;
    public LanguageType UseLanguageType;
    public YsBaseLanguage[] ysTexts;
    public UITooltipShow[] ysTooltipShows;
    private TextAsset UseLanguageAsset;
    public Dictionary<int, string> TargetDictionary;

    private TextAsset ChineseAsset;
    private TextAsset EnglishAsset;

    public static Dictionary<int,string> ChineseDictionary=new Dictionary<int, string>();
   public static Dictionary<int,string> EnglishDictionary=new Dictionary<int, string>();


   void Awake()
   {

     TO = this;

      ChineseAsset = Resources.Load<TextAsset>("TextAssets/Chinese");
     EnglishAsset = Resources.Load<TextAsset>("TextAssets/English");
    
     ChineseDictionary.Clear();
     EnglishDictionary.Clear();
      //if (Application.systemLanguage == SystemLanguage.ChineseSimplified ||
      //    Application.systemLanguage == SystemLanguage.ChineseTraditional ||
      //    Application.systemLanguage == SystemLanguage.Chinese)
      //{
      //  UseLanguageAsset = ChineseAsset;
      //  UseLanguageType = LanguageType.Chinese;
      //}
      //else
      //{
      //  UseLanguageAsset = EnglishAsset;
      //  UseLanguageType = LanguageType.English;
      // }

      //chinese
      string[] lines = ChineseAsset.text.Split('^');
     for (int i = 0; i < lines.Length; i++)
     {
       if (string.IsNullOrEmpty(lines[i]) == false)
       {
         string[] keyvalues = lines[i].Split('=');

        // print(keyvalues[1]);
       //  print(keyvalues[0].Trim());
          ChineseDictionary.Add(Int32.Parse(keyvalues[1]),keyvalues[0].Trim());
        }
     }
      //english
      string[] lines2 = EnglishAsset.text.Split('^');
      for (int i = 0; i < lines2.Length; i++)
      {
        if (string.IsNullOrEmpty(lines2[i]) == false)
        {
          string[] keyvalues = lines2[i].Split('=');
          EnglishDictionary.Add(Int32.Parse(keyvalues[1]), keyvalues[0].Trim());
        }
      }


      switch (UseLanguageType)
     {
       case LanguageType.Chinese:
         UseLanguageAsset = ChineseAsset;
         UseLanguageType = LanguageType.Chinese;
         TargetDictionary = ChineseDictionary;
         break;
       case LanguageType.English:
         UseLanguageAsset = EnglishAsset;
         UseLanguageType = LanguageType.English;
         TargetDictionary = EnglishDictionary;
         break;
     }

    
   }

   void Start()
   {
      this.UpdateLanguage(UseLanguageType);
    }

   private void UpdateFields(LanguageType type)
   {
     switch (type)
     {
       case LanguageType.Chinese:
         UseLanguageAsset = ChineseAsset;
         UseLanguageType = LanguageType.Chinese;
         TargetDictionary = ChineseDictionary;
         break;
       case LanguageType.English:
         UseLanguageAsset = EnglishAsset;
         UseLanguageType = LanguageType.English;
         TargetDictionary = EnglishDictionary;
         break;
     }
    }

   public string GetStringByUUID(int uuid)
   {
     string message = "";
     TargetDictionary.TryGetValue(uuid, out message);
     return message;
    }

   public string TryGetConcentsByUuid(Dictionary<int,string> targetdict,int uuid)
   {
     string message = "";
     targetdict.TryGetValue(uuid, out message);
     return message;
   }

   public void UpdateLanguage(LanguageType type)
   {
     UpdateFields(type);
   
      //ysText
      for (int i = 0; i < ysTexts.Length; i++)
      {
        if (ysTexts[i] is YsText)
        {
          YsText _ysText = ysTexts[i] as YsText;
          int uuid = _ysText.languauuid;
          string message = TryGetConcentsByUuid(TargetDictionary, uuid);
          ysTexts[i].UpdateText(message);
        }else if (ysTexts[i] is YsDropDown)
        {
          YsDropDown _ysDropDown = ysTexts[i] as YsDropDown;
         
          int[] uuids = _ysDropDown.languauuidArrays;
          _ysDropDown.Clear();

          foreach (var VARIABLE in uuids)
          {
            string message = TryGetConcentsByUuid(TargetDictionary, VARIABLE);
            _ysDropDown.UpdateText(message);
          }

          Dropdown targetDropdown = _ysDropDown.GetComponent<Dropdown>();
          int currentindex = targetDropdown.value;
          _ysDropDown.transform.Find("Label").GetComponent<Text>().text =
            targetDropdown.options[currentindex].text;
        }
      }

      //uitooltip
      for (int i = 0; i < ysTooltipShows.Length; i++)
      {
        for (int j = 0; j < ysTooltipShows[i].contentLines.Length; j++)
        {
          string message = TryGetConcentsByUuid(TargetDictionary, ysTooltipShows[i].contentLines[j].languageuuid);
          ysTooltipShows[i].contentLines[j].Content = message;
        }
       
      }
      YsUI.TO.uIOptionPanel.ChangedLanguage(UseLanguageType);
      print("转换完毕！！！");
    }
  }
}


