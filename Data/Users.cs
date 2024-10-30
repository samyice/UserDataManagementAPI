namespace UserDataManagementAPI.Data;

public partial class Users
{
    public int Id { get; set; }

    public string Name { get; set; }

    public bool Active { get; set; }

    public DateOnly Birthdate { get; set; }
}
