namespace UserDataManagementAPI
{
    public class CreateUserDTO
    {
        public string Name { get; set; }

        public bool Active { get; set; }

        public DateOnly Birthdate { get; set; }
    }
}
