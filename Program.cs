using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

public class RoomData
{
    [JsonPropertyName("Room")]
    public Room[] Rooms { get; set; }
}

public class Room
{
    [JsonPropertyName("roomId")]
    public string roomId { get; set; }

    [JsonPropertyName("roomName")]
    public string roomName { get; set; }

    [JsonPropertyName("capacity")]
    public int capacity { get; set; }
}

public class Reservation{
    public DateTime time {get; set;}
    public DateTime date {get; set;}
    public required string ReserverName {get; set;}
    public required Room Room {get; set;}
}

public class ReservationHandler{
    private Reservation[,] reservations {get; set;}

    public ReservationHandler(int daysofweek, int totalroom){
        reservations = new Reservation[daysofweek,totalroom];
    }
    public void addReservation(Reservation r, int day, int roomId){
        if (day >= reservations.GetLength(0) ||  roomId >= reservations.GetLength(1)){
            Console.WriteLine("Invalid Day or RoomID!!!");
            return;
        }
        else if (reservations[day, roomId] != null)
        {
            reservations[day, roomId] = r;
            Console.WriteLine("Cannot reserve at this date!!!");
            return;
        }
        else if (reservations[day, roomId] == null)
        {
            reservations[day, roomId] = r;
            Console.WriteLine("Reservation Added...");
            return;
        }
    }

    public void DeleteReservation(int dayOfWeek, int roomIndex)
    {
        if (dayOfWeek >= reservations.GetLength(0) || roomIndex >= reservations.GetLength(1)){
            Console.WriteLine("Invalid Day or RoomID!!!");
            return;
        }
        else if (reservations[dayOfWeek, roomIndex] == null)
        {
            Console.WriteLine("Cannot delete unexisted reservation...");
            return;
        }
        else if (reservations[dayOfWeek, roomIndex] != null)
        {
            reservations[dayOfWeek, roomIndex] = null;
            Console.WriteLine("Reservation Deleted...");
            return;
        }
    }
    public void DisplayWeeklySchedule()
    {
        Console.WriteLine("This week's schedule:");
        for (int i = 0; i < reservations.GetLength(0); i++)
        {
            Console.WriteLine($"Day {i + 1}:");
            for (int j = 0; j < reservations.GetLength(1); j++)
            {
                if (reservations[i, j] != null)
                {
                    Console.WriteLine($"Room: {reservations[i, j].Room.roomName}, Reserved by: {reservations[i, j].ReserverName}, Date: {reservations[i, j].date}");
                }
            }
            Console.WriteLine();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        //path to json
        string jsonFilePath = "Data.json";

        string jsonString = File.ReadAllText(jsonFilePath);

        //options to read
        var options = new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString
        };

        var roomData = JsonSerializer.Deserialize<RoomData>(jsonString, options);
        int roomnum = 0;
        
        if (roomData?.Rooms != null)
        {
            foreach (var room in roomData.Rooms)
            {
                roomnum++;
                Console.WriteLine($"Room ID : {room.roomId}, Name :  {room.roomName}, Capacity : {room.capacity}");

            }
        }
        ReservationHandler handler = new ReservationHandler(7,roomnum);

        while(true){
            Console.WriteLine("Welcome to the Reservation System!");
            Console.WriteLine("1. Add Reservation");
            Console.WriteLine("2. Delete Reservation");
            Console.WriteLine("3. Display Weekly Schedule");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    while(true){
                        Console.Write("Enter your Day(1 = Monday, 2 = Tuesday, ... 7 = Sunday): ");
                        string Result = Console.ReadLine();
                        int a;
                        while(!Int32.TryParse(Result, out a))
                        {
                            Console.WriteLine("Not a valid number, try again.");

                            Result = Console.ReadLine();
                        }
                        if(a >= 1 && a <= 7){
                            break;
                        }
                    }
                    while(true){
                        Console.Write("Enter your Room choice (1 to 16): ");
                        string Result = Console.ReadLine();
                        int a;
                        while(!Int32.TryParse(Result, out a))
                        {
                            Console.WriteLine("Not a valid number, try again.");

                            Result = Console.ReadLine();
                        }
                        if(a >= 1 && a <= 16){
                            break;
                        }
                    }

                    break;
                case "2":
                    //DeleteReservation(reservationHandler);
                    break;
                case "3":
                    //reservationHandler.DisplayWeeklySchedule();
                    break;
                case "4":
                    //exit = true;
                    break;
                default:
                    //Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}