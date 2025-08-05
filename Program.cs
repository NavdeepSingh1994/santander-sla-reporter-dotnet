using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("📊 Santander .NET SLA Reporter");

        string path = "incidents.csv";

        if (!File.Exists(path))
        {
            Console.WriteLine($"❌ Datei {path} nicht gefunden.");
            return;
        }

        var lines = File.ReadAllLines(path).Skip(1); // erste Zeile = Header
        int total = 0;
        int violations = 0;

        foreach (var line in lines)
        {
            var parts = line.Split(',');

            if (parts.Length < 6)
                continue;

            int resolutionTime = int.Parse(parts[4]);
            int slaThreshold = int.Parse(parts[5]);

            total++;
            if (resolutionTime > slaThreshold)
                violations++;
        }

        double compliance = total == 0 ? 0 : (100.0 * (total - violations)) / total;

        Console.WriteLine($"Total Incidents: {total}");
        Console.WriteLine($"SLA Violations: {violations}");
        Console.WriteLine($"Compliance: {compliance:0.00}%");
    }
}
