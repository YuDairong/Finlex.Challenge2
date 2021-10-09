using Finlex.Quiz2.Interfaces.Models;
using System.Collections.Generic;

namespace Finlex.Quiz2.Interfaces
{
    public interface IRobotController
    {
        public void Move(List<ICommand> commands);
        public IPosition GetPosition();
        public void SetPosition(int x, int y);
        public void SetPosition(IPosition position);
    }
}
