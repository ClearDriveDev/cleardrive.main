﻿namespace ClearDrive.mobil.Responses
{
    public class ControllerResponse : ErrorStore
    {
        public bool IsSuccess => !HasError;

        public ControllerResponse() : base() { }
    }
}