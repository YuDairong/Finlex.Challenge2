using Finlex.Quiz2.Core.Enums;
using Finlex.Quiz2.Core.Logger;
using Finlex.Quiz2.Interfaces;
using Finlex.Quiz2.Interfaces.Models;
using Finlex.Quiz2.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Finlex.Quiz2
{
    public class RobotController : IRobotController
    {
        private Position _position;

        public RobotController()
        {
            _position = new Position();
            CLogger.InitLogFileName("../../../../../LogFile.log");
        }

        public void Move(List<ICommand> commands)
        {
            CLogger.Info(DateTime.Now + " The position of the robot is: (" + _position.X + "," + _position.Y + ")");
            if (commands == null || !commands.Any())
            {
                CLogger.Info(DateTime.Now + " There is no command. The robot will not move.");
                return;
            }

            var step = 1;
            foreach (var command in commands)
            {
                switch (command.Direction)
                {
                    case Direction.Left:
                        _position.X -= command.Steps;
                        break;
                    case Direction.Right:
                        _position.X += command.Steps;
                        break;
                    case Direction.Forward:
                        _position.Y += command.Steps;
                        break;
                    case Direction.Backward:
                        _position.Y -= command.Steps;
                        break;
                }
                CLogger.Info(DateTime.Now + " Move " + step + " step.");
                CLogger.Info(DateTime.Now + " The position of the robot is: (" + _position.X + "," + _position.Y + ")");
                step++;
            }           
        }

        public IPosition GetPosition()
        {
            return _position;
        }

        public void SetPosition(int x, int y)
        {
            _position.X = x;
            _position.Y = y;
        }

        public void SetPosition(IPosition position)
        {
            _position = (Position)position;
        }
    }
}
