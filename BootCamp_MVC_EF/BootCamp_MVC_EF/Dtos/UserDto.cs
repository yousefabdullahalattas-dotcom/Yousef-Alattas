namespace BootCamp_MVC_EF.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsLocked { get; set; }
    }




    public class UserCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsLocked { get; set; }
    }


    public class UserUpdateDto : UserCreateDto
    {
        public int Id { get; set; }

    }

}