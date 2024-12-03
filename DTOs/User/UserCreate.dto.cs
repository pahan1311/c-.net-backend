namespace backend.DTOs.User{

    public class CreateUserDTO {


        
        public required string Email {get; set;}
        public required string ConfirmPassword { get; set; }
        public required string Username {get; set;}
        public required string Password {get; set;}
       // public required decimal UserId {get; set;}
    }

}