using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.Intrinsics.Arm;

public class Snowflake
{
    private const int WorkerIdBits = 5;
    private const int SequenceBits = 12;
    private const long MaxWorkerId = -1L ^ (-1L << WorkerIdBits); // gives 00000000000000000000000000011111 

    private readonly object _lock = new object();

    private long _lastTimestamp = -1L;
    private long _sequence = 0L;

    public long WorkerId { get; }
    public long Sequence { get => _sequence; }

    public Snowflake(long workerId)
    {
        if (workerId > MaxWorkerId || workerId < 0)
        {
            throw new ArgumentException($"Worker ID must be between 0 and {MaxWorkerId}");
        }

        WorkerId = workerId;
    }

    public long NextId()
    {
        lock (_lock)
        {
            long timestamp = GetCurrentTimestamp();

            if (timestamp < _lastTimestamp)
            {
                throw new Exception($"Clock moved backwards. Refusing to generate ID for {_lastTimestamp - timestamp} milliseconds.");
            }

            if (_lastTimestamp == timestamp)
            {
                // (1 << SequenceBits) - 1) gives 00000000000000000000111111111111
                _sequence = (_sequence + 1) & ((1 << SequenceBits) - 1);
                if (_sequence == 0)
                {
                    timestamp = WaitNextMillis(_lastTimestamp);
                }
            }
            else
            {
                _sequence = 0L;
            }
            _lastTimestamp = timestamp;

            Console.WriteLine($"Timestamp: {Convert.ToString(timestamp, 2)}, Worker Id: {Convert.ToString(WorkerId, 2)}, sequence: {Convert.ToString(_sequence, 2)}.");
            return ((timestamp) << (WorkerIdBits + SequenceBits))
                | (WorkerId << SequenceBits)
                | _sequence;
        }
    }

    private long GetCurrentTimestamp()
    {
        return (long)(DateTime.UtcNow - new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMicroseconds;
    }

    private long WaitNextMillis(long lastTimestamp)
    {
        long timestamp = GetCurrentTimestamp();
        while (timestamp <= lastTimestamp)
        {
            timestamp = GetCurrentTimestamp();
        }
        return timestamp;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Example usage
        Snowflake snowflake = new Snowflake(workerId: 2);

        for (int i = 0; i < 10; i++)
        {
            long id = snowflake.NextId();
            Console.WriteLine($"Generated ID: {id}");
        }
    }
}
