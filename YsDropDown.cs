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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FightClub
{
  public class YsDropDown : YsBaseLanguage
  {
    public int[] languauuidArrays;

    public override Dropdown TargeDropdown
    {
      get
      {
        return GetComponent<Dropdown>();
      }
    }
    public override void UpdateText(string str)
    {
      TargeDropdown.options.Add(new Dropdown.OptionData(str));
    }

    public void Clear()
    {
      TargeDropdown.options.Clear();
    }
   
  }

}
