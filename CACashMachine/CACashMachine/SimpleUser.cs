namespace CACashMachine
{
    class SimpleUser : IUser
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int Balance { get; set; }
        public bool IsDeleted { get; set; }
    }
}
