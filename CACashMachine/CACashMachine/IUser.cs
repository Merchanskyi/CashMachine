namespace CACashMachine
{
    interface IUser
    {
        string Login { get; set; }
        string Password { get; set; }
        int Balance { get; set; }
        bool IsDeleted { get; set; }
    }
}
