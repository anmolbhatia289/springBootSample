using System;
using System.Collections.Generic;

// Class to hold user input data
class Payload
{
    public Dictionary<string, string> Data { get; set; }

    public Payload()
    {
        Data = new Dictionary<string, string>();
    }
}

// Class to represent a user
class User
{
    public string UserId { get; private set; }

    public User(string userId)
    {
        UserId = userId;
    }
}

// Class to represent a journey
class Journey
{
    public string JourneyId { get; private set; }
    public JourneyStage OnboardingStage { get; private set; }
    public bool IsActive { get; private set; }

    // Constructor
    public Journey(string journeyId, JourneyStage stage)
    {
        JourneyId = journeyId;
        OnboardingStage = stage;
        IsActive = false;
    }

    // Method to update the state of the journey
    public void UpdateState(bool active)
    {
        IsActive = active;
        Console.WriteLine($"Journey {JourneyId} is now {(active ? "active" : "inactive")}.");
    }
}

// Interface representing the chain of responsibility pattern for journey stages
interface IJourneyChainObject
{
    string GetStageNumber(); // Get the stage number
    List<JourneyStage> GetNextStages(); // Get the next stages
    bool MoveToNextStage(Payload payload, User user); // Move to the next stage based on payload and user
}

// Abstract class representing a journey stage
abstract class JourneyStage : IJourneyChainObject
{
    protected string stageNumber;
    List<JourneyStage> nextStage;

    // Constructor
    public JourneyStage(string stageNumber, List<JourneyStage> nextStage)
    {
        this.stageNumber = stageNumber;
        this.nextStage = nextStage;
    }

    // Get the stage number
    public string GetStageNumber()
    {
        return stageNumber;
    }

    // Get the next stages
    public List<JourneyStage> GetNextStages()
    {
        return nextStage;
    }

    // Move to the next stage based on payload and user
    public bool MoveToNextStage(Payload payload, User user)
    {
        foreach (var stage in nextStage)
        {
            if (stage.Validate(user, payload))
            {
                stage.Process(user, payload);
                return true;
            }
        }

        return false;
    }

    // Abstract method for validation
    public abstract bool Validate(User user, Payload payload);

    // Abstract method for processing
    public abstract void Process(User user, Payload payload);
}

// Concrete class representing the login stage of the journey
class LoginStage : JourneyStage
{
    // Constructor
    public LoginStage(string stageNumber, List<JourneyStage> nextStage) : base(stageNumber, nextStage)
    {
    }

    // Validate the payload for the login stage
    public override bool Validate(User user, Payload payload)
    {
        Console.WriteLine($"Validating payload for user: {user.UserId} for stage {this.stageNumber}");
        bool result = false;
        if (payload.Data.ContainsKey("password"))
        {
            result = true;
        }
        else
        {
            result = false;
        }

        Console.WriteLine($"Result for validation for user: {user.UserId} for stage {this.stageNumber}: {result}");
        return result;
    }

    // Process the payload for the login stage
    public override void Process(User user, Payload payload)
    {
        Console.WriteLine($"Processing payload for user: {user.UserId} for stage {this.stageNumber}");
        // processing logic.
        Console.WriteLine($"Processed payload for user: {user.UserId} for stage {this.stageNumber}");
    }
}

// Concrete class representing the open recharge stage of the journey
class OpenRecharge : JourneyStage
{
    // Constructor
    public OpenRecharge(string stageNumber, List<JourneyStage> nextStage) : base(stageNumber, nextStage)
    {
    }

    // Validate the payload for the open recharge stage
    public override bool Validate(User user, Payload payload)
    {
        Console.WriteLine($"Validating payload for user: {user.UserId} for stage {this.stageNumber}");
        bool result = false;
        if (payload.Data.ContainsKey("provider"))
        {
            result = true;
        }
        else
        {
            result = false;
        }

        Console.WriteLine($"Result for validation for user: {user.UserId} for stage {this.stageNumber}: {result}");
        return result;
    }

    // Process the payload for the open recharge stage
    public override void Process(User user, Payload payload)
    {
        Console.WriteLine($"Processing payload for user: {user.UserId} for stage {this.stageNumber}");
        // processing logic.
        Console.WriteLine($"Processed payload for user: {user.UserId} for stage {this.stageNumber}");
    }
}

// Concrete class representing the perform recharge stage of the journey
class PerformRecharge : JourneyStage
{
    // Constructor
    public PerformRecharge(string stageNumber, List<JourneyStage> nextStage) : base(stageNumber, nextStage)
    {
    }

    // Validate the payload for the perform recharge stage
    public override bool Validate(User user, Payload payload)
    {
        Console.WriteLine($"Validating payload for user: {user.UserId} for stage {this.stageNumber}");
        bool result = false;
        if (payload.Data.ContainsKey("rechargeAmount"))
        {
            result = true;
        }
        else
        {
            result = false;
        }

        Console.WriteLine($"Result for validation for user: {user.UserId} for stage {this.stageNumber}: {result}");
        return result;
    }

    // Process the payload for the perform recharge stage
    public override void Process(User user, Payload payload)
    {
        Console.WriteLine($"Processing payload for user: {user.UserId} for stage {this.stageNumber}");
        // processing logic.
        Console.WriteLine($"Processed payload for user: {user.UserId} for stage {this.stageNumber}");
    }
}

// Singleton class representing the journey service
class JourneyService
{
    private static JourneyService _instance;
    private Dictionary<string, Journey> journeys;
    private Dictionary<string, User> users;
    private Dictionary<string, JourneyStage> journeyStages;
    private Dictionary<string, string> userToJourneyStageMap;

    // Constructor
    private JourneyService()
    {
        journeys = new Dictionary<string, Journey>();
        users = new Dictionary<string, User>();
        userToJourneyStageMap = new Dictionary<string, string>();
        journeyStages = new Dictionary<string, JourneyStage>();
    }

    // Get instance method for singleton
    public static JourneyService GetInstance()
    {
        if (_instance == null)
        {
            _instance = new JourneyService();
        }

        return _instance;
    }

    // Register a user
    public void RegisterUser(User userId)
    {
        users.Add(userId.UserId, userId);
        Console.WriteLine($"User {userId.UserId} registered successfully.");
    }

    // Create a journey
    public void CreateJourney(Journey journey)
    {
        journeys.Add(journey.JourneyId, journey);
        JourneyStage onboardingUserJourney = journey.OnboardingStage;

        // Do a BFS to store the journey stages in the dictionary.
        var queue = new Queue<JourneyStage>();
        queue.Enqueue(onboardingUserJourney);

        while (queue.Count != 0)
        {
            var currentStage = queue.Dequeue();
            journeyStages.Add(currentStage.GetStageNumber(), currentStage);
            if (currentStage.GetNextStages() != null)
            {
                foreach (var stage in currentStage.GetNextStages())
                {
                    queue.Enqueue(stage);
                }
            }
        }

        Console.WriteLine($"Journey {journey.JourneyId} created successfully.");
    }

    // Update the state of a journey
    public void UpdateState(string journeyId, bool active)
    {
        if (journeys.ContainsKey(journeyId))
        {
            journeys[journeyId].UpdateState(active);
        }
        else
        {
            Console.WriteLine($"Journey with ID {journeyId} not found.");
        }
    }

    // Get a journey
    public Journey GetJourney(string journeyId)
    {
        if (journeys.ContainsKey(journeyId))
        {
            return journeys[journeyId];
        }
        else
        {
            Console.WriteLine($"Journey with ID {journeyId} not found.");
            return null;
        }
    }

    // Onboard a user to a journey
    public void OnboardUserToJourney(string userId, string journeyId, Payload payload)
    {
        // Note that this onboarding stage is special. If user fails validation, user is not associated with the journey.
        if (users.ContainsKey(userId) && journeys.ContainsKey(journeyId))
        {
            if (userToJourneyStageMap.ContainsKey(userId))
            {
                throw new Exception($"User {userId} is already onboarded to a journey.");
            }
            else
            {
                // Get journey.
                Journey journey = journeys[journeyId];
                if (!journey.IsActive)
                {
                    throw new Exception($"Journey {journeyId} is not active.");
                }

                // Get onboarding stage.
                JourneyStage onboardingStage = journey.OnboardingStage;
                if (onboardingStage == null)
                {
                    throw new Exception($"Onboarding stage not found for journey {journeyId}.");
                }

                if (!onboardingStage.Validate(users[userId], payload))
                {
                    throw new Exception($"User {userId} failed validation for onboarding stage.");
                }

                userToJourneyStageMap[userId] = $"{journeyId}_{onboardingStage.GetStageNumber()}";
                Console.WriteLine($"User {userId} onboarded to journey {journeyId}.");
            }
        }
        else
        {
            Console.WriteLine($"User {userId} not found or journey for {journeyId} was not found.");
        }
    }

    // Evaluate the payload for a user
    public void Evaluate(string userId, Payload payload)
    {
        // Logic to evaluate payload and progress through stages
        Console.WriteLine($"Evaluating payload for user {userId}...");
        // For demonstration, pass payload to a dummy method for processing
        ProcessPayload(userId, payload);
    }

    // Process the payload for a user
    private void ProcessPayload(string userId, Payload payload)
    {
        if (users.ContainsKey(userId) && userToJourneyStageMap.ContainsKey(userId))
        {
            // Get journey.
            string journeyStage = userToJourneyStageMap[userId].Split('_')[1];
            string journeyId = userToJourneyStageMap[userId].Split('_')[0];
            JourneyStage journeyStageObject = journeyStages[journeyStage];

            User user = users[userId];
            if (journeyStageObject.GetNextStages() == null)
            {
                Console.WriteLine($"User {userId} has reached the end of journey {journeyId}.");
                userToJourneyStageMap.Remove(userId);
                return;
            }
            foreach (var stage in journeyStageObject.GetNextStages())
            {
                if (stage.Validate(user, payload))
                {
                    stage.Process(user, payload);
                    userToJourneyStageMap[userId] = $"{journeyId}_{stage.GetStageNumber()}";
                    Console.WriteLine($"User {userId} moved to next stage {stage.GetStageNumber()} of {journeyId}.");
                    return;
                }
            }

            Console.WriteLine($"Ignoring request of user {userId} can not move to next stage " +
                $"of journey {journeyId}.");
            return;
        }
        else
        {
            Console.WriteLine($"User {userId} not found or user is not onboarded to any journey.");
        }
    }

    // Get the current stage of a user in a journey
    public string GetCurrentStage(string userId, string journeyId)
    {
        if (users.ContainsKey(userId) && userToJourneyStageMap.ContainsKey(userId))
        {
            // user is onboarded to a journey.
            string journeyStage = userToJourneyStageMap[userId].Split('_')[1];
            Console.WriteLine($"User {userId} is on current stage: {journeyStage}");
            return journeyStage;
        }
        else
        {
            return "User or journey not found.";
        }
    }

    // Check if a user is onboarded to a journey
    public bool IsOnboarded(string userId, string journeyId)
    {
        return users.ContainsKey(userId) && userToJourneyStageMap.ContainsKey(userId);
    }
}

class Program
{
    public static void Main()
    {
        // Test the journey service.
        var journey =
            new Journey(
                "Recharge",
                new LoginStage(
                    "Login",
                    new List<JourneyStage> {
                        new OpenRecharge(
                            "Open Recharge page",
                            new List<JourneyStage> {
                                new PerformRecharge(
                                    "Perform recharge",
                                    null) }) }));
        JourneyService journeyService = JourneyService.GetInstance();
        journeyService.CreateJourney(journey);
        journeyService.UpdateState("Recharge", true);
        User user = new User("123");
        journeyService.RegisterUser(user);

        var loginPayload = new Payload();
        loginPayload.Data.Add("password", "123456");
        journeyService.OnboardUserToJourney("123", "Recharge", loginPayload);

        var openRechargePayload = new Payload();
        openRechargePayload.Data.Add("provider", "Airtel");
        journeyService.Evaluate("123", openRechargePayload);

        var performRechargePayload = new Payload();
        try
        {
            // This would fail, as the payload does not contain the required key.
            journeyService.Evaluate("123", performRechargePayload);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        performRechargePayload.Data.Add("rechargeAmount", "100");

        // Get current state.
        journeyService.GetCurrentStage("123", "Recharge");

        journeyService.Evaluate("123", performRechargePayload);

        journeyService.Evaluate("123", performRechargePayload);
    }
}
