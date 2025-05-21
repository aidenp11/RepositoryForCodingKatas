using MississippiMarbles.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MississippiMarbles.Interfaces
{
	internal interface GameObserver
	{
		void onPlayerRoll(Player player, Roll roll);
		void onGameEnd(Player winner);
	}
}
