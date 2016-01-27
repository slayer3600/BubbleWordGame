using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Levels", menuName = "Custom Asset/Bubble Level", order = 1)]
public class LevelItemList : ScriptableObject {


    public List<LevelItem> level;
	
}
