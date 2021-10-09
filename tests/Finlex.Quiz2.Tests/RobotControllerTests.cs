using Finlex.Quiz2.Core.Enums;
using Finlex.Quiz2.Interfaces;
using Finlex.Quiz2.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace Finlex.Quiz2.Tests
{
    public class RobotControllerTests
    {
        private IRobotController _robotController;

        [SetUp]
        public void Setup()
        {
            _robotController = new RobotController();
        }

        [Test]
        public void Move_EmptyCommandIsGiven_GetRightPosition()
        {
            _robotController.Move(new List<ICommand>());
            var position = _robotController.GetPosition();

            Assert.AreEqual(0, position.X);
            Assert.AreEqual(0, position.Y);
        }

        [Test]
        public void Move_NullIsGiven_GetRightPosition()
        {
            _robotController.Move(null);
            var position = _robotController.GetPosition();

            Assert.AreEqual(0, position.X);
            Assert.AreEqual(0, position.Y);
        }

        [Test]
        public void Move_OneCommandIsGiven_GetRightPosition()
        {
            _robotController.Move(new List<ICommand>()
            {
                new Command()
                {
                    Direction = Direction.Left,
                    Steps = 1
                } 
            });
            var position = _robotController.GetPosition();

            Assert.AreEqual(-1, position.X);
            Assert.AreEqual(0, position.Y);
        }

        [Test]
        public void Move_TwoCommandIsGiven_GetRightPosition()
        {
            _robotController.Move(new List<ICommand>() 
            {   
                new Command()
                { 
                    Direction = Direction.Right,
                    Steps = 2
                },
                new Command()
                {
                    Direction = Direction.Forward,
                    Steps = 4
                },
            });
            var position = _robotController.GetPosition();

            Assert.AreEqual(2, position.X);
            Assert.AreEqual(4, position.Y);
        }

        [Test]
        public void SetPosition_XAndYIsGiven_GetRightPosition()
        {
            _robotController.SetPosition(1, 2);
            var position = _robotController.GetPosition();

            Assert.AreEqual(1, position.X);
            Assert.AreEqual(2, position.Y);
        }

        [Test]
        public void GetNumberAndSum_NestedArrayWithStringIsGiven_ThrowException()
        {
            var position = new Position()
            {
                X = 1,
                Y = 2
            };
            _robotController.SetPosition(position);
            var newPosition = _robotController.GetPosition();

            Assert.AreEqual(newPosition, position);
        }       
    }
}