using MississippiMarbles.Classes;

namespace MississippiMarbles.Interfaces
{
    /// <summary>
    /// Interface for mediating game events and managing observers.
    /// </summary>
    internal interface IGameMediator
    {
        void AddObserver(IGameObserver observer);
        void RemoveObserver(IGameObserver observer);
        void NotifyPlayerRoll(Player player, Roll roll);
        void NotifyPlayerChoice(Player player, Roll roll);
        void NotifyGameEnd(Player winner);
        void NotifyPointsChanged(Player player, int oldPoints, int newPoints);
    }
}