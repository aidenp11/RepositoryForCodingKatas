using MississippiMarbles.Classes;

bool preGame = true;
bool game = false;
int playerCount = 0;
int turn = 0; 
List<Player> players = new List<Player>();
void DisplayMenu()
{
    Console.WriteLine("\n=== Welcome to Mississippi Marbles ===");
    Console.WriteLine("1) Add Player");
    Console.WriteLine("2) Start Game");
    Console.WriteLine("3) View Extended Rules");
    Console.WriteLine("4) Quit");
    Console.WriteLine("======================================");
}

void InitialGameRules()
{
    Console.WriteLine("\n=== Game Rules ===");
    Console.WriteLine("\nThis is a heavy gambling game! You have 6 Dice to roll with per turn.");
    Console.WriteLine("Points must be accumulated in a single turn. If you fail to score, your turn ends and your 'pot' goes away.");
    Console.WriteLine("On your turn you roll the dice, choosing which ones to put into your 'pot'.");
    Console.WriteLine("If you choose to end your turn you add your pot to your total score \n(you need a minimum of 700 in your 'pot' if you have 0 points.)");
    Console.WriteLine("\nSpecial Rolls:");
    Console.WriteLine("- 4 twos: Lose all accumulated points (A Flood)");
    Console.WriteLine("- Straight (1-6): 2000 points (Big Muddy)");
    Console.WriteLine("- 6 of a kind: 6000 points (All the Marbles)");
    Console.WriteLine("\nWinning Condition: First player to reach or surpass 11,000 points wins!");
    Console.WriteLine("==================\n");
}

InitialGameRules();

while (preGame)
{
    DisplayMenu();
    Console.Write("Choice: ");
    string input = Console.ReadLine();

    try
    {
        int option = int.Parse(input);

        if (option < 1 || option > 4)
        {
            Console.WriteLine("Invalid choice. Please select a number between 1 and 4.\n");
        }
        else
        {
            switch (option)
            {
                case 1:
                    Console.Write("\nEnter player name (1–10 characters): ");
                    string nameInput = Console.ReadLine();

                    if(playerCount == 4)
                    {
                        Console.WriteLine("Looks like the max player count has been reached. Please start the game!\n");
                    }
                    else if (!string.IsNullOrWhiteSpace(nameInput) && nameInput.Length <= 10 && playerCount <= 3)
                    {
                        Player p = new Player(nameInput);
                        players.Add(p);
                        Console.WriteLine($"\nPlayer '{nameInput}' has been added!");
                        playerCount++;
                    }
                    else
                    {
                        Console.WriteLine("Invalid name. Name must be 1–10 characters.\n");
                    }
                    Console.WriteLine($"Current Players: {playerCount}");
                    break;

                case 2:
                    if (players.Count <= 0)
                    {
                        Console.WriteLine("\nYou need at least one player to start the game!\n");
                    }
                    else
                    {
                        Console.WriteLine("\nStarting the game...\n");
                        preGame = false;
                        game = true;
                    }
                    break;

                case 3:
                    Console.WriteLine("\n=== Extended Game Rules ===");
                    Console.WriteLine("- You start with 6 dice to roll each turn.");
                    Console.WriteLine("- Your goal is to collect points in a 'pot' during your turn.");
                    Console.WriteLine("- You can choose which dice to keep and which to re-roll.");
                    Console.WriteLine("- At the end of your turn, you can add your 'pot' to your total score.");
                    Console.WriteLine("- If your 'pot' is below 700 points and you have no total score, you cannot end your turn yet. \n   You'll lose everything in the 'pot' if you don't score.");
                    Console.WriteLine("- If you choose to end your turn, your 'pot' adds to your total score.");

                    Console.WriteLine("\nSpecial Rolls:");
                    Console.WriteLine("- 4 twos: Known as 'A Flood'—you lose all the accumulated points (yes you do have to get 700 again).");
                    Console.WriteLine("- Straight (1-6): 'Big Muddy'—2000 points!");
                    Console.WriteLine("- 6 of a kind: 'All the Marbles'—6000 points!");

                    Console.WriteLine("\nOther Combinations:");
                    Console.WriteLine("- 3 of a kind: Earn points based on the number you rolled (e.g., 3 ones = 100 points).");
                    Console.WriteLine("- Rolling 1's gives you 100 points, and 5's give you 50 points.");
                    Console.WriteLine("- Keep in mind you are only able to use each dice once per roll. \n    so if you choose to take a one for 100p, you can't use that same one in a three of a kind for 500p.");

                    Console.WriteLine("\nWinning Condition:");
                    Console.WriteLine("- The first player to reach or exceed 11,000 points wins!");
                    Console.WriteLine("- Each other player will have one more turn to try and beat the winner's score.");

                    Console.WriteLine("\nRemember: If you don't score during your turn, you lose your 'pot' and end your turn with nothing.");
                    Console.WriteLine("Use strategy and luck to get as many points as you can and beat your opponents!");

                    Console.WriteLine("==================\n");
                    break;

                case 4:
                    Console.WriteLine("\nExiting the game. Goodbye!");
                    preGame = false;
                    break;
            }
        }
    }
    catch
    {
        Console.WriteLine("Invalid input. Please enter a number between 1 and 4.\n");
    }
}

while (game)
{
	Game g = new Game(players, turn);
	Console.WriteLine(players.ElementAt(g.winnerTurn).getPlayerName + " won the game!");
	game = false;
}

//Console.WriteLine("Please enter player name!");
//string name = Console.ReadLine();
//Player player = new Player(name);
//player.addToPot(6);
//player.addToPot(3);
//player.addToPot(4);
//player.addToPot(1);
//Console.WriteLine(player.getPlayerName
//+ "\n" + player.getPoints 
//+ "\n" + player.getSavedDice());
