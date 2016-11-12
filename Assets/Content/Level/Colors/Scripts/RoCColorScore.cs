using UnityEngine;
using System.Collections;
using RoC;
using UniRx;
using System.Collections.Generic;

namespace RoC
{
	public class RoCColorScore : MonoBehaviour
	{

		public static RoCColorScore colorsManager
		{
			get
			{
				if (_colorsManager == null)
					_colorsManager = GameObject.FindObjectOfType<RoCColorScore>();

				return _colorsManager;
			}
		}

		private static RoCColorScore _colorsManager = null;


		public delegate void ChangeScore( ERoCColor color, int value );
		public static event ChangeScore updatescore;
		public static event ChangeScore addscore;

		private Dictionary<ERoCColor,int> score = new Dictionary<ERoCColor,int> ();

		public void addScore( ERoCColor color, int value )
		{
			if ( !score.ContainsKey( color ) )
				score.Add( color, value );
			else
				score[color]+=value;
			addscore( color, value );
			updatescore( color, score[color] );
		}

		public int getScore( ERoCColor color )
		{
			if ( !score.ContainsKey( color ) )
				return 0;

			return score[color];
		}
	}

}