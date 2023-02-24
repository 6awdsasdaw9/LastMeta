using System;

namespace Code.Services.Input
{
    public interface IInputController
    {
        Action PlayerJumpEvent { get; set; }
    }
}