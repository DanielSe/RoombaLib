using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace RoombaLib
{
    public class Roomba : IRoomba, IDisposable
    {
        private readonly IRoombaAdapter _adapter;
        private readonly JsonSerializerOptions _serializerOptions;

        public Roomba(IRoombaAdapter adapter)
        {
            _adapter = adapter ?? throw new ArgumentNullException(nameof(adapter));

            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task StartAsync()
        {
            await Execute(new BaseCommand {Command = "start"});
        }

        public async Task CleanAsync()
        {
            await Execute(new BaseCommand {Command = "clean"});
        }

        public async Task PauseAsync()
        {
            await Execute(new BaseCommand {Command = "pause"});
        }

        public async Task StopAsync()
        {
            await Execute(new BaseCommand {Command = "stop"});
        }

        public async Task ResumeAsync()
        {
            await Execute(new BaseCommand {Command = "resume"});
        }

        public async Task DockAsync()
        {
            await Execute(new BaseCommand {Command = "dock"});
        }

        public async Task EvacAsync()
        {
            await Execute(new BaseCommand {Command = "evac"});
        }

        public async Task TrainAsync()
        {
            await Execute(new BaseCommand {Command = "train"});
        }

        private async Task Execute(ICommand command)
        {
            var payload = JsonSerializer.Serialize(command, _serializerOptions);
            await _adapter.SendCommandAsync(payload);
        }

        public void Dispose()
        {
            _adapter?.Dispose();
        }
    }
}