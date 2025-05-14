using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MississippiMarbles.Classes
{ 
	//Player Class { int points, string name, array saved_dice}

	internal class Player
	{ 
		private string playerName;
		private int points = 0;
		private List<Roll.Concepts> savedDice = new List<Roll.Concepts>();

		public int diceNum;
		public bool turn;
		public int pointsToAdd;

		public Player(string playerName) { this.playerName = playerName; }

		public string getPlayerName { get { return playerName;}}
		public int getPoints { get { return points;}}
		public string getSavedDice() 
		{ 
			string values = "";
			foreach (var i in savedDice)
			{
				values = values + i.ToString();
			}
		 return values;
		}
		public void setPoints(int points) { this.points = points;}
		public void addToPot(Roll.Concepts concept) 
		{
			savedDice.Add(concept);
			//for (int i = 0; i < savedDice.Length; i++)
			//{
			//	if(savedDice[i] == 0)
			//	{
			//		savedDice[i] = diceValue;
			//		break;
			//	}
			//}

		}
	}
}
