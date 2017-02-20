using Character;

namespace GameInput
{
    public struct UserInput
    {
        public float move;
        public CharacterStatus status;

        public UserInput(float move, CharacterStatus status)
        {
            this.move = move;
            this.status = status;
        }
    }
}

