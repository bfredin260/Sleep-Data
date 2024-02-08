// See https://aka.ms/new-console-template for more information
string? resp;
do {

    // ask for input
    Console.WriteLine("Enter 1 to create data file.");
    Console.WriteLine("Enter 2 to parse data.");
    Console.WriteLine("Enter anything else to quit.");
    // input response
    resp = Console.ReadLine();
    if (resp == "1")
    {
        // create data file

        // ask a question
        Console.WriteLine("How many weeks of data is needed?");
        // input the response (convert to int)
        int weeks = int.Parse(Console.ReadLine());
        // determine start and end date
        DateTime today = DateTime.Now;
        // we want full weeks sunday - saturday
        DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);
        // subtract # of weeks from endDate to get startDate
        DateTime dataDate = dataEndDate.AddDays(-(weeks * 7));
        // random number generator
        Random rnd = new Random();
        // create file
        StreamWriter sw = new StreamWriter("data.txt");

        // loop for the desired # of weeks
        while (dataDate < dataEndDate)
        {
            // 7 days in a week
            int[] hours = new int[7];
            for (int i = 0; i < hours.Length; i++)
            {
                // generate random number of hours slept between 4-12 (inclusive)
                hours[i] = rnd.Next(4, 13);
            }
            // M/d/yyyy,#|#|#|#|#|#|#
            // Console.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
            sw.WriteLine($"{dataDate:M/d/yyyy},{string.Join("|", hours)}");
            // add 1 week to date
            dataDate = dataDate.AddDays(7);
        }
        sw.Close();
    }
    else if (resp == "2")
    {
        // TODO: parse data file
        StreamReader sr = new StreamReader("data.txt");

        int total = 0;
        int days = 0;

        while(!sr.EndOfStream) {
            
            string[] dateHourSep = sr.ReadLine().Split(",");

            DateTime date = DateTime.Parse(dateHourSep[0]);
            int[] hoursOfSleep = Array.ConvertAll(dateHourSep[1].Split("|"), int.Parse);

            for (int i = 0; i < hoursOfSleep.Length; i++) {
                total += hoursOfSleep[i];
                days++;
            }

            double avg = (double)total/days;

            Console.WriteLine("Week of {0:MMM}, {0:dd}, {0:yyyy}\n"
            + "Su Mo Tu We Th Fr Sa Tot Avg\n"
            + "-- -- -- -- -- -- -- --- ---\n"
            + "{1,2} {2,2} {3,2} {4,2} {5,2} {6,2} {7,2} {8,3} {9,3:N1}\n",
            date, hoursOfSleep[0], hoursOfSleep[1], hoursOfSleep[2], hoursOfSleep[3], hoursOfSleep[4], hoursOfSleep[5], hoursOfSleep[6], total, avg);
        }
    }
} while (resp == "1" || resp == "2");
