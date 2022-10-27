using Script.Services.Input;

namespace Script.Infrastructure
{
    public class Game
    {
        public static InputService inputService;

        public Game()
        {
            inputService = new InputService();
        }
    }
}