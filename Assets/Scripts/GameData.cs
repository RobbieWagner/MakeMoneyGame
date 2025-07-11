using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobbieWagnerGames.MakeMoney
{
	public class GameData
	{
		public string saveName = "default";
		public int workers = 0;

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			GameData other = (GameData)obj;
			return workers == other.workers && saveName.Equals(other.saveName);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(workers.GetHashCode(), saveName.GetHashCode());
		}
	}
}