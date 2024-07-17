using System;
using System.Collections.Generic;
using System.Text;

namespace Renting.Drones
{
    public enum DroneUtility
    {
        Undefined = 0,
        Farming = 1,
        PhotographyVideo = 2,
        Transport = 3,
        Monitoring = 4
    }
    public static class DroneUtilityExtensions
    {
        public static string ToString(this DroneUtility utility)
        {
            switch (utility)
            {
                case DroneUtility.Undefined:
                    return "Undefined";
                case DroneUtility.Farming:
                    return "Farming";
                case DroneUtility.PhotographyVideo:
                    return "PhotographyVideo";
                case DroneUtility.Transport:
                    return "Transport";
                case DroneUtility.Monitoring:
                    return "Monitoring";
                default:
                    return "Unknown";
            }
        }
    }
}
