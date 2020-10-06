using System.Threading.Tasks;

namespace RoombaLib
{
    public interface IRoomba
    {
        Task StartAsync();
        Task CleanAsync();
        Task PauseAsync();
        Task StopAsync();
        Task ResumeAsync();
        Task DockAsync();
        Task EvacAsync();
        Task TrainAsync();
    }
}