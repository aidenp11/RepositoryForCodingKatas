using System.Collections.Generic;
using MississippiMarbles.Classes;
using MississippiMarbles.Interfaces;

namespace MississippiMarbles.Classes
{
    /// <summary>
    /// Mediates all game events and manages observers.
    /// </summary>
    internal class GameMediator : IGameMediator
    {
        private readonly List<IGameObserver> _observers = new();

        public void AddObserver(IGameObserver observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
        }

        public void RemoveObserver(IGameObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyPlayerRoll(Player player, Roll roll)
        {
            foreach (var observer in _observers)
                observer.onPlayerRoll(player, roll);
        }

        public void NotifyPlayerChoice(Player player, Roll roll)
        {
            foreach (var observer in _observers)
                observer.onPlayerChoice(player, roll);
        }

        public void NotifyGameEnd(Player winner)
        {
            foreach (var observer in _observers)
                observer.onGameEnd(winner);
        }

        public void NotifyPointsChanged(Player player, int oldPoints, int newPoints)
        {
            foreach (var observer in _observers)
                observer.onPointsChanged(player, oldPoints, newPoints);
        }
    }
}