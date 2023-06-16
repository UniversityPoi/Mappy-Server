using Mappy.Models.Requests;
using Mappy.Models.Responses;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;

namespace Mappy.Services;

public class LocationService
{
  //=============================================================================================
  private readonly string _locationApiUrl;
	private readonly RestClient _client;


	//=============================================================================================
  public LocationService(IConfiguration config)
  {
    _locationApiUrl = config["LocationApiUrl"];
		_client = new RestClient(_locationApiUrl);
  }



	//=============================================================================================
  public async Task<ApiResponse> GetLocationById(string id)
  {
    var request = new RestRequest($"api/location/{id}", Method.Get);

		var response = await _client.ExecuteAsync(request);


		var result = new ApiResponse();

		switch (response.StatusCode)
		{
			case HttpStatusCode.OK:
				result.Data = JsonConvert.DeserializeObject<Location>(response.Content ?? "");
				result.IsSuccessful = true;
				break;
			case HttpStatusCode.NotFound:
				dynamic? responseObj = JsonConvert.DeserializeObject(response.Content ?? "");
				result.Message = responseObj?.message ?? "Failed to get location!";
				break;
			default: break;
		}

		return result;
  }


	//=============================================================================================
  public async Task<ApiResponse> GetLastLocationByUser(string id)
  {
    var request = new RestRequest($"api/location/user/last/{id}", Method.Get);

		var response = await _client.ExecuteAsync(request);


		var result = new ApiResponse();

		switch (response.StatusCode)
		{
			case HttpStatusCode.OK:
				result.Data = JsonConvert.DeserializeObject<Location>(response.Content ?? "");
				result.IsSuccessful = true;
				break;
			case HttpStatusCode.NotFound:
				dynamic? responseObj = JsonConvert.DeserializeObject(response.Content ?? "");
				result.Message = responseObj?.message ?? "Failed to get last location!";
				break;
			default: break;
		}

		return result;
  }


	//=============================================================================================
  public async Task<ApiResponse> AddLocation(LocationRequestModel location)
  {
    var request = new RestRequest("api/location", Method.Post);
		request.AddHeader("Content-Type", "application/json");
		request.AddBody(location, "application/json");

		var response = await _client.ExecuteAsync(request);


		var result = new ApiResponse();

		switch (response.StatusCode)
		{
			case HttpStatusCode.OK:
				result.Data = JsonConvert.DeserializeObject<Location>(response.Content ?? "");
				result.IsSuccessful = true;
				break;
			case HttpStatusCode.BadRequest:
				dynamic? responseObj = JsonConvert.DeserializeObject(response.Content ?? "");
				result.Message = responseObj?.message ?? "Failed to add location!";
				break;
			default: break;
		}

		return result;
  }
}