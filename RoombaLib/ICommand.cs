namespace RoombaLib
{
    internal interface ICommand
    {
        string Command { get; }
        long Time { get; }
        string Initiator { get; }
    }
}