﻿namespace ClearDrive.shared.Responses
{
    public class ControllerResponse : ErrorStore
    {
        public bool IsSuccess => !HasError;

        public ControllerResponse() : base() { }
    }
}