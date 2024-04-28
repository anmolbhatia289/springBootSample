/*
The game can be played by multiple players.
Primary goal is to knock off all the pins to gain scores. The game consist of ten frames. In each frame the player has two chances to knock off the pins.
If the bowler is able to knock down all the pins in one strike, it is called a strike. If the bowler does it with two it is called a spare.
If there is a spare, the player gets 5 bonus points. For strike, the player gets 10 bonus points.
In the final set, a player who rolls a spare or a strike is allowed to roll the extra balls to complete the set. However, only a maximum of three balls can be rolled in the final set.
During the game, players and their scores will be maintained and shown by the system and winner will be declared at the end of the game.
Likewise, multiple games can be played in parallel on multiple free lanes.*/

// Actors
// Players
// Administrator

// Classes
// Player
// Administrator
// Game
// Pin
// BowlingAlley
// Ball
// ScoreBoard
// Lane

namespace BowlingGameProgram
{
    public class Lane
    {
        public Dictionary<string, int> scores;

        public int LaneNumber { get; set; }

        public List<Ball> balls { get; set; }

        public List<Pins> pins { get; set; }

        public Lane(int laneNumber)
        {
            LaneNumber = laneNumber;
            balls = new List<Ball>();
            pins = new List<Pins>();
            scores = new Dictionary<string, int>();
        }

        public void ResetScores()
        {
            scores = new Dictionary<string, int>();
        }

        public void AddScore(string playerName, int score)
        {
            if (scores.ContainsKey(playerName))
            {
                scores[playerName] += score;
            }
            else
            {
                scores.Add(playerName, score);
            }
        }

        public void printScores()
        {
            foreach (var elem in scores)
            {
                Console.WriteLine(elem.Key + " : " + elem.Value);
            }
        }
    }

    public class Ball
    {
        public int BallSize { get; set; }

        public Ball(int ballNumber)
        {
            BallSize = ballNumber;
        }
    }

    public class Pins
    {
        public int PinNumber { get; set; }

        public Pins(int pinNumber)
        {
            PinNumber = pinNumber;
        }
    }

    public class BowlingAlley
    {
        public List<Lane> lanes { get; set; }

        public BowlingAlley()
        {
            lanes = new List<Lane>();
        }

        public void AddLane(Lane lane)
        {
            lanes.Add(lane);
        }

        public void RemoveLane(Lane lane)
        {
            lanes.Remove(lane);
        }
    }

    public class User
    {
        public string Name { get; set; }

        public string EmailId { get; set; }

        public string Number { get; set; }

        public User(string name, string emailId, string number)
        {
            Name = name;
            EmailId = emailId;
            Number = number;
        }
    }

    public class Player: User
    {

        public Player(string name, string number, string emaildId) 
            : base(name, number, emaildId)
        {
        }

        public void playGame(Lane lane)
        {
            lock (lane)
            {
                int chance = 2;
                int pinsLeft = 10;
                while (chance != 0 && pinsLeft > 0)
                {
                    var random = new Random();
                    int pinsDown = random.Next(0, pinsLeft + 1);
                    pinsLeft -= pinsDown;
                    chance--;
                    lane.AddScore(this.Name, pinsDown);
                }

                if (pinsLeft == 0)
                {
                    int bonusPoints = chance == 1 ? 10 : 5;
                    lane.AddScore(this.Name, bonusPoints);
                }
            }
        }
    }

    public class Game
    {
        public Queue<Player> playerTurn { get; set; }

        public BowlingAlley bowlingAlley { get; set; }

        public Lane lane { get; set; }

        public Game(BowlingAlley bowlingAlley, List<Player> players, Lane lane)
        {
            playerTurn = new Queue<Player>();
            foreach (var player in players)
            {
                playerTurn.Enqueue(player);
            }
            this.bowlingAlley = bowlingAlley;
            this.lane = lane;
            foreach (var player in players)
            {
                lane.AddScore(player.Name, 0);
            }
        }

        public void Play()
        {
            int chances = 10;
            while (chances > 0)
            {
                var player = playerTurn.Dequeue();
                player.playGame(lane);
                lane.printScores();
                chances--;
                playerTurn.Enqueue(player);
                Console.WriteLine("Chances left: " + chances);
            }

            Console.WriteLine("Game Over");
            lane.printScores();
        }
    }

    public class Administrator : User
    {
        public Administrator(string name, string number, string emaildId) 
            : base(name, number, emaildId)
        {
        }

        public void AddLane(BowlingAlley bowlingAlley, Lane lane)
        {
            bowlingAlley.AddLane(lane);
        }

        public void RemoveLane(BowlingAlley bowlingAlley, Lane lane)
        {
            bowlingAlley.RemoveLane(lane);
        }

        public void ResetLane(Lane lane)
        {
            lane.ResetScores();
        }

        public static void Main()
        {
            var bowlingAlley = new BowlingAlley();
            var lane1 = new Lane(1);
            var lane2 = new Lane(2);
            Administrator administrator = new Administrator("admin", "1234567890", "");
            administrator.AddLane(bowlingAlley, lane1);
            administrator.AddLane(bowlingAlley, lane2);
            var player1 = new Player("player1", "1234567890", "");
            var player2 = new Player("player2", "1234567890", "");
            Game game = new Game(bowlingAlley, new List<Player> { player1, player2 }, lane1);
            game.Play();
        }
    }

    
}
