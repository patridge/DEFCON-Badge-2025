using Meadow;
using Meadow.Devices;
using Meadow.Foundation;
using Meadow.Foundation.Graphics;
// using Meadow.Foundation.Sensors.Motion;
// using Meadow.Foundation.Sensors.Atmospheric;
// using Meadow.Foundation.Sensors.Light;
using Meadow.Peripherals.Sensors.Atmospheric;

using System;
using System.Threading;
using System.Threading.Tasks;
using Meadow.Peripherals.Sensors;
using Meadow.Peripherals.Sensors.Light;

namespace DefConBadge2025
{
    public class EnvironmentPage : IBadgePage
    {
        IProjectLabHardware config;

        MicroGraphics graphics;
        // Bme688 EnvironmentSensor { get; set; }
        // Bh1750 LightSensor { get; set; }
        // Bmi270 MotionSensor { get; set; }
        ITemperatureSensor TemperatureSensor { get; set; }
        IBarometricPressureSensor BarometricPressureSensor { get; set; }
        IHumiditySensor HumiditySensor { get; set; }
        ILightSensor LightSensor { get; set; }

        public bool IsUpdating = false;

        public void StartUpdating(IProjectLabHardware config, MicroGraphics graphics)
        {
            this.config = config;
            this.graphics = graphics;

            IsUpdating = true;

            // EnvironmentSensor.StartUpdating();
            // LightSensor.StartUpdating();
            // MotionSensor.StartUpdating();
            TemperatureSensor.StartUpdating();
            BarometricPressureSensor.StartUpdating();
            HumiditySensor.StartUpdating();
            LightSensor.StartUpdating();

            Task.Run(() =>
            {
                while (IsUpdating)
                {
                    Draw();
                    Thread.Sleep(TimeSpan.FromSeconds(3));
                }
            });
        }

        public void StopUpdating()
        {
            // EnvironmentSensor.StartUpdating();
            // LightSensor.StartUpdating();
            // MotionSensor.StartUpdating();
            TemperatureSensor.StopUpdating();
            BarometricPressureSensor.StopUpdating();
            HumiditySensor.StopUpdating();
            LightSensor.StopUpdating();

            IsUpdating = false;
        }

        public void Init(IProjectLabHardware hardware)
        {
            // EnvironmentSensor = hardware.EnvironmentalSensor;
            // LightSensor = hardware.LightSensor;
            // MotionSensor = hardware.MotionSensor;
            TemperatureSensor = hardware.TemperatureSensor;
            BarometricPressureSensor = hardware.BarometricPressureSensor;
            HumiditySensor = hardware.HumiditySensor;
            LightSensor = hardware.LightSensor;
        }

        public void Reset()
        {
        }

        public void Down()
        {
        }

        public void Left()
        {
        }

        public void Right()
        {
        }

        public void Up()
        {
        }

        //helper method
        void DrawStatus(string label, string value, Color color, int yPosition)
        {
            graphics.DrawText(x: 0, y: yPosition, label, color: WildernessLabsColors.AzureBlue);
            graphics.DrawText(x: 10, y: yPosition + 18, value,
                scaleFactor: ScaleFactor.X2,
                alignmentH: HorizontalAlignment.Left, 
                color: color);
        }

        void Draw()
        {
            graphics.Clear();
          
            if (config.TemperatureSensor.Temperature is { } temp)
            {
                DrawStatus("Temperature:", $"{temp.Celsius:N1}C", WildernessLabsColors.GalleryWhite, 0);
            }

            if (config.BarometricPressureSensor.Pressure is { } pressure)
            {
                DrawStatus("Pressure:", $"{pressure.StandardAtmosphere:N2}atm", WildernessLabsColors.GalleryWhite, 60);
            }

            if (config.HumiditySensor.Humidity is { } humidity)
            {
                DrawStatus("Humidity:", $"{humidity.Percent:N1}%", WildernessLabsColors.GalleryWhite, 120);
            }
            

            if (config.LightSensor.Illuminance is { } light)
            {
                if (light is { } lightReading)
                {
                    DrawStatus("Lux:", $"{lightReading:N0}Lux", WildernessLabsColors.GalleryWhite, 180);
                }
            }

            graphics.Show();
         }
    }
}
