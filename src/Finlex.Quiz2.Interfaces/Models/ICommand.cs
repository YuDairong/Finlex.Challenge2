using Finlex.Quiz2.Core.Enums;

namespace Finlex.Quiz2.Interfaces
{
    public interface ICommand
    {
        public Direction Direction { get; }
        public int Steps { get; }
    }
}
