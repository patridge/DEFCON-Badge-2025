using Meadow.Devices;
using Meadow.Foundation.Graphics;
using System.Threading.Tasks;

namespace DefConBadge2024
{
    public interface IBadgePage
    {
        void Init(IProjectLabHardware projLab);

        void StartUpdating(IProjectLabHardware projLab, MicroGraphics graphics);

        void StopUpdating();

        void Reset();

        void Left();
        void Right();
        void Up();
        void Down();
    }
}