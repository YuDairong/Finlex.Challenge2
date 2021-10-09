using Finlex.Quiz2.Core.Enums;
using Finlex.Quiz2.Interfaces;

namespace Finlex.Quiz2.Models
{
    public class Command : ICommand
    {
        public Direction Direction { get; set; }
        public int Steps { get; set; }
    }
}
