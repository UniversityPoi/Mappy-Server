using Mappy.Models.Requests;
using Mappy.Models.Responses;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;

namespace Mappy.Services;

public class UserService
{
  //=============================================================================================
  private readonly string _userApiUrl;
	private readonly RestClient _client;


	//=============================================================================================
  public UserService(IConfiguration config)
  {
    _userApiUrl = config["UserApiUrl"];
		_client = new RestClient(_userApiUrl);
  }



	//=============================================================================================
  public async Task<ApiResponse> GetUserById(string id)
  {
    var request = new RestRequest($"api/user/{id}", Method.Get);

		var response = await _client.ExecuteAsync(request);


		var result = new ApiResponse();

		switch (response.StatusCode)
		{
			case HttpStatusCode.OK:
				result.Data = JsonConvert.DeserializeObject<SecureUserModel>(response.Content ?? "");
				result.IsSuccessful = true;
				break;
			case HttpStatusCode.NotFound:
				dynamic? responseObj = JsonConvert.DeserializeObject(response.Content ?? "");
				result.Message = responseObj?.message ?? "Failed to get user!";
				break;
			default: break;
		}

		return result;
  }


	//=============================================================================================
  public async Task<ApiResponse> RegisterUser(RegisterUserModel user)
  {
    var request = new RestRequest("api/user/register", Method.Post);
		request.AddHeader("Content-Type", "application/json");
		request.AddBody(user, "application/json");

		var response = await _client.ExecuteAsync(request);

		var result = new ApiResponse();

		switch (response.StatusCode)
		{
			case HttpStatusCode.OK:
				dynamic? responseObj = JsonConvert.DeserializeObject(response.Content ?? "");
				result.Message = responseObj?.isAdded ?? "Failed to register!";
				result.IsSuccessful = true;
				break;
			case HttpStatusCode.BadRequest:
				responseObj = JsonConvert.DeserializeObject(response.Content ?? "");
				result.Message = responseObj?.message ?? "Failed to register!";
				break;
			default: break;
		}

		return result;
  }


	//=============================================================================================
  public async Task<ApiResponse> LoginUser(LoginUserModel user)
  {
    var request = new RestRequest("api/user/login", Method.Post);
		request.AddHeader("Content-Type", "application/json");
		request.AddBody(user, "application/json");

		var response = await _client.ExecuteAsync(request);


		var result = new ApiResponse();

		switch (response.StatusCode)
		{
			case HttpStatusCode.OK:
				result.Data = JsonConvert.DeserializeObject<SecureUserModel>(response.Content ?? "");
				result.IsSuccessful = true;
				break;
			case HttpStatusCode.NotFound:
			case HttpStatusCode.BadRequest:
				dynamic? responseObj = JsonConvert.DeserializeObject(response.Content ?? "");
				result.Message = responseObj?.message ?? "Failed to login!";
				break;
			default: break;
		}

		return result;
  }
}