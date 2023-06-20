using Mappy.Models.Requests;
using Mappy.Models.Responses;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;

namespace Mappy.Services;

public class FavoriteLocationService
{
  //=============================================================================================
  private readonly string _locationApiUrl;
	private readonly RestClient _client;


	//=============================================================================================
  public FavoriteLocationService(IConfiguration config)
  {
    _locationApiUrl = config["LocationApiUrl"];
		_client = new RestClient(_locationApiUrl);
  }



	//=============================================================================================
  public async Task<ApiResponse> GetAllFavoriteLocationsById(string id)
  {
    var request = new RestRequest($"api/favorite-location/user/{id}", Method.Get);

		var response = await _client.ExecuteAsync(request);


		var result = new ApiResponse();

		switch (response.StatusCode)
		{
			case HttpStatusCode.OK:
				result.Data = JsonConvert.DeserializeObject<List<FavoriteLocation>>(response.Content ?? "");
				result.IsSuccessful = true;
				break;
			case HttpStatusCode.NotFound:
				dynamic? responseObj = JsonConvert.DeserializeObject(response.Content ?? "");
				result.Message = responseObj?.message ?? "Failed to get favorite locations!";
				break;
			default: break;
		}

		return result;
  }


	//=============================================================================================
  public async Task<ApiResponse> AddFavoriteLocation(FavoriteLocationRequestModel location)
  {
    var request = new RestRequest("api/favorite-location", Method.Post);
		request.AddHeader("Content-Type", "application/json");
		request.AddBody(location, "application/json");

		var response = await _client.ExecuteAsync(request);


		var result = new ApiResponse();

		switch (response.StatusCode)
		{
			case HttpStatusCode.OK:
				result.Data = JsonConvert.DeserializeObject<FavoriteLocation>(response.Content ?? "");
				result.IsSuccessful = true;
				break;
			case HttpStatusCode.BadRequest:
				dynamic? responseObj = JsonConvert.DeserializeObject(response.Content ?? "");
				result.Message = responseObj?.message ?? "Failed to add favorite location!";
				break;
			default: break;
		}

		return result;
  }


	//=============================================================================================
  public async Task<ApiResponse> DeleteFavoriteLocation(Guid id)
  {
    var request = new RestRequest($"api/favorite-location/{id}", Method.Delete);

		var response = await _client.ExecuteAsync(request);


		var result = new ApiResponse();

		switch (response.StatusCode)
		{
			case HttpStatusCode.OK:
				result.Data = JsonConvert.DeserializeObject<FavoriteLocation>(response.Content ?? "");
				result.IsSuccessful = true;
				break;
			case HttpStatusCode.NotFound:
				dynamic? responseObj = JsonConvert.DeserializeObject(response.Content ?? "");
				result.Message = responseObj?.message ?? "Failed to delete favorite location!";
				break;
			default: break;
		}

		return result;
  }
}