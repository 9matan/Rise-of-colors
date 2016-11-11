using UnityEngine;
using UnityEditor;
using System.Collections;
using RoC;
using UniRx;

namespace RoC
{

	[CustomPropertyDrawer(typeof(RoCColorsInfoContainer))]
	public class RoCColorsInfoContainerDrawer : VOSDictionaryDrawer<ERoCColor, RoCColorInfo>
	{

	}

}
