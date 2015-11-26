using System;
using System.Collections.Generic;
using LawnMowers.Domain.Model;

namespace LawnMowers.Domain.Services
{
    public class LawnMowerMovementSimulator
    {
        public IEnumerable<LawnMowerState> SimulateMovement(
            LawnMowerState initialState,
            LawnSize lawnSize,
            IEnumerable<LawnMowerCommand> commands)
        {
            if (initialState == null)
            {
                throw new ArgumentNullException(nameof(initialState));
            }
            if (lawnSize == null)
            {
                throw new ArgumentNullException(nameof(lawnSize));
            }
            if (commands == null)
            {
                throw new ArgumentNullException(nameof(commands));
            }
            
            // actual implementation is in separate method to force execution of arguments checks before iterating the result
            return SimulateMovementImpl(initialState, lawnSize, commands);
        }

        private IEnumerable<LawnMowerState> SimulateMovementImpl(
            LawnMowerState initialState, 
            LawnSize lawnSize, 
            IEnumerable<LawnMowerCommand> commands)
        {
            var currentState = initialState;
            foreach (var command in commands)
            {
                currentState = PerformCommand(currentState, command);

                if (!lawnSize.IsPositionWithinLawn(currentState.Position))
                {
                    throw new CannotMoveOutsideLawnBoundariesException();
                }

                yield return currentState;
            }
        }

        private LawnMowerState PerformCommand(
            LawnMowerState state,
            LawnMowerCommand command)
        {
            switch (command)
            {
                case LawnMowerCommand.TurnRight:
                    return state.GetStateAfterRightTurn();
                case LawnMowerCommand.TurnLeft:
                    return state.GetStateAfterLeftTurn();
                case LawnMowerCommand.MoveForward:
                    return state.GetStateAfterMoveForward();
                default:
                    throw new UnknownLawnMowerCommandException();
            }
        }
    }
}