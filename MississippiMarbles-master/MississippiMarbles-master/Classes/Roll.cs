using System;
using System.Collections.Generic;
using System.Linq;
using MississippiMarbles.Classes;
using MississippiMarbles.Interfaces;

namespace MississippiMarbles.Classes
{
	internal class Roll
	{
		private readonly List<IGameObserver> _observers;
		Player player;
		public int pointsToAdd;
		public List<int> dice = new List<int>();
		public List<int>? selectedDice;
		public int pointsScored = 0;

		public enum Concepts
		{
			ONE, FIVE, STRAIGHT, TOK1, TOK2, TOK3, TOK4, TOK5, TOK6, FROK, FVOK, SOK
		}

		public Roll(Player playerTurn, List<IGameObserver> observers)
		{
			player = playerTurn;
			_observers = observers;
		}


		#region Observer Pattern
		private void NotifyPlayerRoll()
		{
			foreach (var observer in _observers)
				observer.onPlayerRoll(player, this);
		}

		private void NotifyPlayerChoice()
		{
			foreach (var observer in _observers)
				observer.onPlayerChoice(player, this);
		}

		#endregion

		public void TryOpen(int diceNum)
		{
			Random r = new Random();
			int roll;
			int choiceNum = 0;
			bool choosingDice = true;
			List<Concepts> possibilities = new List<Concepts>();
			for (int i = 0; i < diceNum; i++)
			{
				roll = r.Next(1, 1);
				dice.Add(roll);
			}

			NotifyPlayerRoll();

			if (DiceFlood(dice))
			{
				Console.WriteLine("Dice Flood! You've lost all your points!\n");
				player.setPoints(0);
				player.turn = false;
				return;
			}
			else
			{
				while (choosingDice)
				{
					if (choiceNum > 0) DisplayDiceAgain();
					possibilities.Clear();
					if (Straight(dice)) possibilities.Add(Concepts.STRAIGHT);
					for (int i = 0; i < dice.Count; i++)
					{
						if (dice.ElementAt(i) == 1) possibilities.Add(Concepts.ONE);
						if (dice.ElementAt(i) == 5) possibilities.Add(Concepts.FIVE);
					}
					if (MultipleValue(dice, 1, 0, 0, 3)) possibilities.Add(Concepts.TOK1);
					if (MultipleValue(dice, 2, 0, 0, 3)) possibilities.Add(Concepts.TOK2);
					if (MultipleValue(dice, 3, 0, 0, 3)) possibilities.Add(Concepts.TOK3);
					if (MultipleValue(dice, 4, 0, 0, 3)) possibilities.Add(Concepts.TOK4);
					if (MultipleValue(dice, 5, 0, 0, 3)) possibilities.Add(Concepts.TOK5);
					if (MultipleValue(dice, 6, 0, 0, 3)) possibilities.Add(Concepts.TOK6);
					for (int i = 1; i <= 6; i++)
					{
						if (MultipleValue(dice, i, 0, 0, 4)) possibilities.Add(Concepts.FROK);
					}
					for (int i = 1; i <= 6; i++)
					{
						if (MultipleValue(dice, i, 0, 0, 5)) possibilities.Add(Concepts.FVOK);
					}
					for (int i = 1; i <= 6; i++)
					{
						if (MultipleValue(dice, i, 0, 0, 6)) possibilities.Add(Concepts.SOK);
					}

					if (possibilities.Count <= 0 && choiceNum == 0)
					{
						Console.WriteLine("No options, no points gained for this turn!\n");
						player.pointsToAdd = 0;
						choosingDice = false;
						player.turn = false;
						return;
					}
					else if (possibilities.Count <= 0)
					{
						Console.WriteLine("All choices chosen, turn over\n");
						choosingDice = false;
						return;
					}


					for (int i = 0; i < possibilities.Count; i++)
					{
						if (possibilities.ElementAt(i) == Concepts.ONE) Console.WriteLine(i + 1 + ") 1 one = 100 points");
						else if (possibilities.ElementAt(i) == Concepts.FIVE) Console.WriteLine(i + 1 + ") 1 five = 50 points");
						else if (possibilities.ElementAt(i) == Concepts.TOK1) Console.WriteLine(i + 1 + ") 3 ones = 500 points");
						else if (possibilities.ElementAt(i) == Concepts.TOK2) Console.WriteLine(i + 1 + ") 3 twos = 200 points");
						else if (possibilities.ElementAt(i) == Concepts.TOK3) Console.WriteLine(i + 1 + ") 3 threes = 300 points");
						else if (possibilities.ElementAt(i) == Concepts.TOK4) Console.WriteLine(i + 1 + ") 3 fours = 400 points");
						else if (possibilities.ElementAt(i) == Concepts.TOK5) Console.WriteLine(i + 1 + ") 3 fives = 500 points");
						else if (possibilities.ElementAt(i) == Concepts.TOK6) Console.WriteLine(i + 1 + ") 3 sixes = 600 points");
						else if (possibilities.ElementAt(i) == Concepts.FROK) Console.WriteLine(i + 1 + ") Four of a kind = 1000 points");
						else if (possibilities.ElementAt(i) == Concepts.FVOK) Console.WriteLine(i + 1 + ") Five of a kind = 3000 points");
						else if (possibilities.ElementAt(i) == Concepts.SOK) Console.WriteLine(i + 1 + ") Six of a kind = 6000 points");
						else if (possibilities.ElementAt(i) == Concepts.STRAIGHT) Console.WriteLine(i + 1 + ") Straight = 2000 points");
						if (possibilities.Count - 1 == i) Console.WriteLine(i + 2 + ") End this roll");
					}

					Console.Write("Choice: ");
					int option;
					string input = Console.ReadLine();
					try
					{
						option = int.Parse(input);
						if (option > possibilities.Count + 1 || option < 1)
						{
							Console.WriteLine("Input must be between 1 and " + possibilities.Count + "\n");
						}
						else
						{
							if (option == possibilities.Count + 1 && choiceNum > 0)
							{
								Console.WriteLine("Roll Ended\n");
								choosingDice = false;
								return;
							}
							else if (option == possibilities.Count + 1)
							{
								Console.WriteLine("Turn ended without making choice, points lost!\n");
								choosingDice = false;
								player.pointsToAdd = 0;
								player.turn = false;
								return;
							}

							if (possibilities.ElementAt(option - 1) == Concepts.ONE)
							{
								player.diceNum -= 1;
								player.pointsToAdd += 100;
								Console.WriteLine("1 chosen\n");
								dice.Remove(1);
								choiceNum++;
								//choosingDice = false;

							}
							else if (possibilities.ElementAt(option - 1) == Concepts.FIVE)
							{
								player.diceNum -= 1;
								player.pointsToAdd += 50;
								Console.WriteLine("5 chosen\n");
								dice.Remove(5);
								choiceNum++;
								//choosingDice = false;

							}
							else if (possibilities.ElementAt(option - 1) == Concepts.STRAIGHT)
							{
								player.diceNum -= 6;
								player.setPoints(2000);
								Console.WriteLine("Straight chosen\n");
								choosingDice = false;

							}
							else if (possibilities.ElementAt(option - 1) == Concepts.TOK1)
							{
								player.diceNum -= 3;
								player.pointsToAdd += 500;
								Console.WriteLine("3 ones chosen\n");
								dice.Remove(1);
								dice.Remove(1);
								dice.Remove(1);
								choiceNum++;
								//choosingDice = false;

							}
							else if (possibilities.ElementAt(option - 1) == Concepts.TOK2)
							{
								player.diceNum -= 3;
								player.pointsToAdd += 200;
								Console.WriteLine("3 twos chosen\n");
								dice.Remove(2);
								dice.Remove(2);
								dice.Remove(2);
								choiceNum++;
								//choosingDice = false;

							}
							else if (possibilities.ElementAt(option - 1) == Concepts.TOK3)
							{
								player.diceNum -= 3;
								player.pointsToAdd += 300;
								Console.WriteLine("3 threes chosen\n");
								dice.Remove(3);
								dice.Remove(3);
								dice.Remove(3);
								choiceNum++;
								//choosingDice = false;

							}
							else if (possibilities.ElementAt(option - 1) == Concepts.TOK4)
							{
								player.diceNum -= 3;
								player.pointsToAdd += 400;
								Console.WriteLine("3 fours chosen\n");
								dice.Remove(4);
								dice.Remove(4);
								dice.Remove(4);
								choiceNum++;
								//choosingDice = false;

							}
							else if (possibilities.ElementAt(option - 1) == Concepts.TOK5)
							{
								player.diceNum -= 3;
								player.pointsToAdd += 500;
								Console.WriteLine("3 fives chosen\n");
								dice.Remove(5);
								dice.Remove(5);
								dice.Remove(5);
								choiceNum++;
								//choosingDice = false;

							}
							else if (possibilities.ElementAt(option - 1) == Concepts.TOK6)
							{
								player.diceNum -= 3;
								player.pointsToAdd += 600;
								Console.WriteLine("3 sixes chosen\n");
								dice.Remove(6);
								dice.Remove(6);
								dice.Remove(6);
								dice.Remove(6);
								choiceNum++;
								//choosingDice = false;

							}
							else if (possibilities.ElementAt(option - 1) == Concepts.FROK)
							{
								player.diceNum -= 4;
								player.pointsToAdd += 1000;
								Console.WriteLine("Four of a kind chosen\n");
								for (int i = 1; i < 7; i++)
								{
									if (MultipleValue(dice, i, 0, 0, 4))
									{
										dice.Remove(i);
										dice.Remove(i);
										dice.Remove(i);
										dice.Remove(i);
										break;
									}
								}
								choiceNum++;
								//choosingDice = false;

							}
							else if (possibilities.ElementAt(option - 1) == Concepts.FVOK)
							{
								player.diceNum -= 5;
								player.pointsToAdd += 3000;
								Console.WriteLine("Five of a kind chosen\n");
								for (int i = 1; i < 7; i++)
								{
									if (MultipleValue(dice, i, 0, 0, 5))
									{
										dice.Remove(i);
										dice.Remove(i);
										dice.Remove(i);
										dice.Remove(i);
										dice.Remove(i);
										break;
									}
								}
								choiceNum++;
								//choosingDice = false;

							}
							else if (possibilities.ElementAt(option - 1) == Concepts.SOK)
							{
								player.diceNum -= 6;
								player.pointsToAdd += 6000;
								Console.WriteLine("Six of a kind chosen\n");
								choosingDice = false;

							}
						}
					}
					catch
					{
						Console.WriteLine("Input must be a number \n");
					}
				}
			}
		}

		public void DisplayDiceAgain()
		{
			NotifyPlayerChoice();
		}


		public void TryRoll(int diceNum)
		{
			Random r = new Random();
			int roll;
			for (int i = 0; i < diceNum; i++)
			{
				roll = r.Next(1, 6);
				dice.Append(roll);
			}
		}

		public void SmoothWater()
		{
			if (selectedDice != null)
			{
				ScoreThreeOfAKind(selectedDice, 1, 500); //Three Ones
				ScoreThreeOfAKind(selectedDice, 2, 200); //Three Twos
				ScoreThreeOfAKind(selectedDice, 3, 300); //Three Threes
				ScoreThreeOfAKind(selectedDice, 4, 400); //Three Fours
				ScoreThreeOfAKind(selectedDice, 5, 500); //Three Fives
				ScoreThreeOfAKind(selectedDice, 6, 500); //Three Sixes
			}
		}
		public void RidinRapids()
		{
			if (selectedDice != null)
			{
				for (int target = 1; target <= 6; target++)
				{
					// Call MultipleValue for each target number

					bool threeRepeats = MultipleValue(selectedDice, target, 0, 0, 3);
					bool fourRepeats = MultipleValue(selectedDice, target, 0, 0, 4);
					bool fiveRepeats = MultipleValue(selectedDice, target, 0, 0, 5);
					bool sixRepeats = MultipleValue(selectedDice, target, 0, 0, 6);

					// Award points based on the highest repetition achieved
					if (sixRepeats) pointsScored += 6000;
					else if (fiveRepeats) pointsScored += 3000;
					else if (fourRepeats) pointsScored += 1000;
					else if (threeRepeats) pointsScored += 0;
				}
			}
		}

		public bool DiceFlood(List<int> dice)
		{
			int floodCount = 0;
			foreach (int value in dice)
			{
				if (value == 2) floodCount++;
			}
			if (floodCount == 4) return true;
			else return false;
		}
		private bool MultipleValue(List<int> dice, int target, int index, int count, int repNum)
		{
			// Base case: If we reach the end of the array, check if the count is equal to or over repetitive num
			if (index >= dice.Count)
			{
				return count == repNum;
			}
			// Increment count if the current element matches the target number
			if (dice[index] == target)
			{
				count++;
			}
			// Recurse to the next index
			return MultipleValue(dice, target, index + 1, count, repNum);
		}

		private int ScoreThreeOfAKind(List<int> selectedDice, int targetNumber, int score)
		{
			if (MultipleValue(selectedDice, targetNumber, 0, 0, 3))
			{
				return pointsScored += score;
			}
			return 0;
		}
		private bool Straight(List<int> selectedDice)
		{
			// Define the straight sequence we are looking for
			int[] straight = { 1, 2, 3, 4, 5, 6 };

			// Get distinct numbers and sort them
			int[] sortedUnique = selectedDice.Distinct().OrderBy(x => x).ToArray();

			// Check if the straight sequence exists in the sorted unique numbers
			if (straight.All(sortedUnique.Contains)) return true;
			else return false;
		}

	}
}