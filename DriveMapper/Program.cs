using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DriveMapper
{
    class Program
    {
        static void Main(string[] args)
        {
            ListDrives();

            UnmapDrive(827967385);

            ListDrives();

            MapDrive(827967385, "D:");

            ListDrives();

            Console.WriteLine("Done!");
            Console.ReadLine();
        }

        public static void ListDrives()
        {
            Console.WriteLine("Listing All Drives");
            ManagementObjectSearcher disks = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Volume");
            foreach (ManagementObject disk in disks.Get())
            {
                disk.Get();
                Console.WriteLine($"{disk["DriveLetter"]} - {disk["SerialNumber"]}");
            }
            Console.WriteLine("Done Listing All Drives\n\n");
            
        }

        public static void UnmapDrive(int serialNumber)
        {
            Console.WriteLine("Unmapping Drive");

            ManagementObjectSearcher disks = new ManagementObjectSearcher("root\\CIMV2", $"SELECT * FROM Win32_Volume WHERE SerialNumber = {serialNumber}");
            foreach (ManagementObject disk in disks.Get())
            {
                disk.Get();
                disk["DriveLetter"] = null;
                disk.Put();
            }

            Console.WriteLine("Done Unmapping Drive\n\n");
        }

        public static void MapDrive(int serialNumber, string drive)
        {
            Console.WriteLine("Mapping Drive");

            ManagementObjectSearcher disks = new ManagementObjectSearcher("root\\CIMV2", $"SELECT * FROM Win32_Volume WHERE SerialNumber = {serialNumber}");
            foreach (ManagementObject disk in disks.Get())
            {
                disk.Get();
                disk["DriveLetter"] = drive;
                disk.Put();
            }

            Console.WriteLine("Done Mapping Drive\n\n");
        }

    }
}
