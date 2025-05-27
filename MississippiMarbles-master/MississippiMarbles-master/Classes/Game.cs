using MississippiMarbles.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MississippiMarbles.Classes
{
	internal class Game
	{
		private static Game _instance;
		private static readonly object _lock = new object();

		private List<Player> _players;
		private int _turn;
		public int winnerTurn;

		private GameMediator _mediator;

		private Game(List<Player> players)
		{
			_players = players;
			_turn = 0;
			_mediator = new GameMediator();
		}

		public void AddObserver(IGameObserver observer)
		{
			_mediator.AddObserver(observer);
		}

		public void RemoveObserver(IGameObserver observer)
		{
			_mediator.RemoveObserver(observer);
		}

		public void Start()
		{
			StartGameLoop();
		}

		public static Game GetInstance(List<Player> players)
		{
			if (_instance == null)
			{
				lock (_lock)
				{
					if (_instance == null)
					{
						_instance = new Game(players);
					}
				}
			}
			return _instance;
		}

		private void StartGameLoop()
		{
			bool gameEnded = false;

			while (!gameEnded)
			{
				Player player = _players[_turn];
				player.diceNum = 6;
				player.turn = true;
				player.pointsToAdd = 0;
				Roll roll;

				Console.WriteLine($"{player.getPlayerName}'s turn\nScore: {player.getPoints}");

				while (player.turn)
				{
					if (player.diceNum == 0)
					{
						player.diceNum = 6;

						if (player.getPoints == 0 && player.pointsToAdd < 700)
						{
							Console.WriteLine("You need 700 or more points to start!\n");
							break;
						}
						else
						{
							int oldPoints = player.getPoints;
							player.setPoints(player.getPoints + player.pointsToAdd);
							Console.WriteLine("\n*** Turn Summary ***");
							Console.WriteLine("Player's total score for this turn: " + player.pointsToAdd);
							_mediator.NotifyPointsChanged(player, oldPoints, player.getPoints);
							Console.WriteLine("\n*** End of Turn ***\n");
							break;
						}
					}

					Console.WriteLine("\n*** Dice Status ***");
					Console.WriteLine($"{player.diceNum} dice left\nTotal score to be added for this turn: {player.pointsToAdd}");
					Console.WriteLine("\n*** Choose Your Action ***");
					Console.WriteLine("1) Roll");
					Console.WriteLine("2) End Turn");
					Console.Write("Choice: ");
					string input = Console.ReadLine();

					try
					{
						int option = int.Parse(input);

						switch (option)
						{
							case 1:
								roll = new Roll(player, _mediator); 
								roll.TryOpen(player.diceNum);
								break;

							case 2:
								if (player.getPoints == 0 && player.pointsToAdd < 700)
								{
									Console.WriteLine("You need 700 or more points to start!\n");
									player.turn = false;
								}
								else
								{
									int oldPoints = player.getPoints;
									player.setPoints(player.getPoints + player.pointsToAdd);
									Console.WriteLine("\n*** Turn Summary ***");
									Console.WriteLine("Player's total score for this turn: " + player.pointsToAdd);
									_mediator.NotifyPointsChanged(player, oldPoints, player.getPoints);
									Console.WriteLine("\n*** End of Turn ***\n");
									player.turn = false;
								}
								break;

							default:
								Console.WriteLine("Input must be 1 or 2\n");
								break;
						}
					}
					catch (FormatException)
					{
						Console.WriteLine("Input must be 1 or 2\n");
					}
				}

				if (player.getPoints >= 11000)
				{
					winnerTurn = _turn;
					gameEnded = true;
					_mediator.NotifyGameEnd(player);
				}

				_turn = (_turn + 1) % _players.Count;
			}
		}
	}
}