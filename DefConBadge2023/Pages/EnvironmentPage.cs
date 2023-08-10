using Meadow.Devices;
using Meadow.Foundation;
using Meadow.Foundation.Graphics;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DefConBadge2023
{
    public class EnvironmentPage : IBadgePage
    {
        IProjectLabHardware config;

        MicroGraphics graphics;

        public bool IsUpdating = false;

        public void StartUpdating(IProjectLabHardware config, MicroGraphics graphics)
        {
            this.config = config;
            this.graphics = graphics;

            IsUpdating = true;

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
            IsUpdating = false;
        }

        public void Init(IProjectLabHardware hardware)
        {
         
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
            throw new NotImplementedException();
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
          
            if (config.EnvironmentalSensor.Temperature is { } temp)
            {
                DrawStatus("Temperature:", $"{temp.Celsius:N1}C", WildernessLabsColors.GalleryWhite, 0);
            }

            if (config.EnvironmentalSensor.Pressure is { } pressure)
            {
                DrawStatus("Pressure:", $"{pressure.StandardAtmosphere:N2}atm", WildernessLabsColors.GalleryWhite, 60);
            }

            if (config.EnvironmentalSensor.Humidity is { } humidity)
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