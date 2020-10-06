using System;
using System.Threading.Tasks;

namespace RoombaLib
{
    public interface IRoombaAdapter : IDisposable
    {
        bool Connected { get; }
        
        Task SendCommandAsync(string payload);
    }
}