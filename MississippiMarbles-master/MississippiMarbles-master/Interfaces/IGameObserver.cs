using MississippiMarbles.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MississippiMarbles.Interfaces
{
	internal interface IGameObserver
	{
		void onPlayerRoll(Player player, Roll roll);
		void onPlayerChoice(Player player, Roll roll);
		void onGameEnd(Player winner);
		void onPointsChanged(Player player, int oldPoints, int newPoints);
	}
}
