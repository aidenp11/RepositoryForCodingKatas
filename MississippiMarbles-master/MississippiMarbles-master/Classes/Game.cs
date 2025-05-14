using System;
using System.Collections.Generic;
using System.Linq;

namespace MississippiMarbles.Classes
{
	internal class Game
	{
		public int winnerTurn;

		public Game(List<Player> players, int turn)
		{
			Player player = players.ElementAt(turn);
			player.diceNum = 6;
			player.turn = true;
			player.pointsToAdd = 0;
			Roll roll;

			Console.WriteLine(player.getPlayerName + "'s turn\nScore: " + player.getPoints);

			while (player.turn)
			{
				if (player.diceNum == 0)
				{
					player.diceNum = 6;

                    if (player.diceNum == 0)
                    {
                        player.setPoints(player.getPoints + player.pointsToAdd);
                        Console.WriteLine("\n*** Turn Summary ***");
                        Console.WriteLine("Player's total score for this turn: " + player.pointsToAdd);
                        Console.WriteLine("Player's updated score: " + player.getPoints);
                        Console.WriteLine("\n*** End of Turn ***\n");
                        player.turn = true;
                    }

                    else
					{
						player.setPoints(player.getPoints + player.pointsToAdd);
						Console.WriteLine("Player score is now " + player.getPoints + '\n');
						player.turn = false;
						break;
					}
				}

                Console.WriteLine("\n*** Dice Status ***");
                Console.WriteLine(player.diceNum + " dice left\nTotal score to be added for this turn: " + player.pointsToAdd);
                Console.WriteLine("\n*** Choose Your Action ***");
                Console.WriteLine("1) Roll");
                Console.WriteLine("2) End Turn");
				Console.Write("Choice: ");
                string input = Console.ReadLine();

                try
				{
					int option = int.Parse(input);

					if (option < 1 || option > 2)
					{
						Console.WriteLine("Input must be 1 or 2\n");
					}
					else
					{
						switch (option)
						{
							case 1:
								roll = new Roll(player);
								roll.TryOpen(player.diceNum);  // Handle the roll of the dice
								break;

                            case 2:
                                if (player.getPoints == 0 && player.pointsToAdd < 700)
                                {
                                    Console.WriteLine("You need 700 or more points to start!\n");
                                    player.turn = false;
                                }
                                else
                                {
                                    player.setPoints(player.getPoints + player.pointsToAdd);
                                    Console.WriteLine("Player score is now " + player.getPoints + '\n');
                                    player.turn = false;
                                }
                                break;
                        }
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Input must be 1 or 2\n");
                }
            }

			if (player.getPoints >= 11000)
			{
				winnerTurn = turn;
				return;
			}
			else if (players.Count == turn + 1)
			{
				turn = 0;
				new Game(players, turn);
			}
			else
			{
				new Game(players, turn + 1);
			}
		}
	}
}