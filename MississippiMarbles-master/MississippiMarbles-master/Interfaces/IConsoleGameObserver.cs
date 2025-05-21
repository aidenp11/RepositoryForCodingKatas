using MississippiMarbles.Classes;
using MississippiMarbles.Interfaces;

namespace MississippiMarbles.Interfaces
{
    internal class IConsoleGameObserver : IGameObserver
    {
        public void onPlayerRoll(Player player, Roll roll)
        {
            Console.WriteLine($"\n[Observer] {player.getPlayerName} rolled: {string.Join(",", roll.dice)}\n");
        }

        public void onPlayerChoice(Player player, Roll roll)
        {
            Console.WriteLine($"\n[Observer] {player.getPlayerName} dice left: {string.Join(",", roll.dice)}\n");
        }


		public void onGameEnd(Player winner)
        {
            Console.WriteLine($"[Observer] Game ended! Winner: {winner.getPlayerName}");
        }

        public void onPointsChanged(Player player, int oldPoints, int newPoints)
        {
            Console.WriteLine($"[Observer] {player.getPlayerName} points changed: {oldPoints} -> {newPoints}");
        }
    }
}