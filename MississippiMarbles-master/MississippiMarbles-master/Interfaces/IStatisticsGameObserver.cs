using MississippiMarbles.Classes;
using MississippiMarbles.Interfaces;
using System;
using System.Collections.Generic;

namespace MississippiMarbles.Interfaces
{
    internal class IStatisticsGameObserver : IGameObserver
    {
        private readonly Dictionary<string, int> _rollCounts = new();
        private readonly Dictionary<string, int> _choiceCounts = new();
        private readonly Dictionary<string, int> _playerPoints = new();

        public void onPlayerRoll(Player player, Roll roll)
        {
            if (!_rollCounts.ContainsKey(player.getPlayerName))
                _rollCounts[player.getPlayerName] = 0;
            _rollCounts[player.getPlayerName]++;
        }

        public void onPlayerChoice(Player player, Roll roll)
        {
            if (!_choiceCounts.ContainsKey(player.getPlayerName))
                _choiceCounts[player.getPlayerName] = 0;
            _choiceCounts[player.getPlayerName]++;
		}

		public void onGameEnd(Player winner)
        {
            Console.WriteLine("[Stats] Final points per player:");
            foreach (var kvp in _playerPoints)
            {
                Console.WriteLine($"[Stats] {kvp.Key}: {kvp.Value} points");
            }
            Console.WriteLine("[Stats] Rolls per player:");
            foreach (var kvp in _rollCounts)
            {
                Console.WriteLine($"[Stats] {kvp.Key}: {kvp.Value} rolls");
            }
            Console.WriteLine("[Stats] Choices per player:");
            foreach (var kvp in _choiceCounts)
            {
                Console.WriteLine($"[Stats] {kvp.Key}: {kvp.Value} choices");
			}
			Console.WriteLine($"[Stats] Winner: {winner.getPlayerName}");
        }

        public void onPlayerAdded(Player player)
        {
            // Optionally initialize points to 0
            if (!_playerPoints.ContainsKey(player.getPlayerName))
                _playerPoints[player.getPlayerName] = player.getPoints;
        }

        public void onPointsChanged(Player player, int oldPoints, int newPoints)
        {
            _playerPoints[player.getPlayerName] = newPoints;
        }
    }
}