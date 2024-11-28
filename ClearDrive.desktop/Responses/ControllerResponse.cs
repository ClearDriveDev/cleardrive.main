namespace ClearDrive.desktop.Responses
{
    public class ControllerResponse : ErrorStore
    {
        public bool IsSuccess => !HasError;

        public ControllerResponse() : base() { }
    }
}