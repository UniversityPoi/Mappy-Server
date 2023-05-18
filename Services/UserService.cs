using Mappy.Models.Requests;

namespace Mappy.Services;

public class UserService
{
    public List<RegisterUserModel> Users = new List<RegisterUserModel>();

    public bool CreateUser(RegisterUserModel userModel)
    {
        Users.Add(userModel);
        
        // TODO: Implement register user logic here
        
        return true;
    }

    public bool DoesUserExist(LoginUserModel userModel)
    {
        // TODO: Implement user exist logic here

        return Users.Exists(user => user.Email == userModel.Email);
    }
}