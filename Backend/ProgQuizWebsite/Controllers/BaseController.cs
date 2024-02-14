using AutoMapper;
using Business_Layer.Extensions;
using Data_Layer.Enums;
using Data_Layer.ResponseObjects;
using Data_Layer.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ProgQuizWebsite.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult ProcessItem<T, V>(T result, IMapper mapper, string failureMessage)
        {
            if (result is null)
                return StatusCode(404, new ResponseObject(ResponseType.NoResult.GetDisplayNameProperty(), failureMessage));
            var mappedResult = mapper.Map<V>(result);
            return StatusCode(200, mappedResult);
        }

        protected IActionResult ProcessItems<T, V>(List<T> results, IMapper mapper, string failureMessage)
        {
            if (results is not null && results.Count > 0)
            {
                var mappedResults = mapper.Map<List<V>>(results);
                return StatusCode(200, mappedResults);
            }
            return StatusCode(404, new ResponseObject(ResponseType.NoResult.GetDisplayNameProperty(), failureMessage));
        }

        protected IActionResult ProcessAdding(bool isAdded, string successMessage, string failureMessage)
        {
            if (isAdded)
                return StatusCode(201, new ResponseObject(ResponseType.Success.GetDisplayNameProperty(), successMessage));
            return StatusCode(404, new ResponseObject(ResponseType.Failure.GetDisplayNameProperty(),
                failureMessage));
        }

        protected IActionResult ProcessUpdating(bool isUpdated, string successMessage, string failureMessage)
        {
			if (isUpdated)
				return StatusCode(200, new ResponseObject(ResponseType.Success.GetDisplayNameProperty(), successMessage));
			return StatusCode(404, new ResponseObject(ResponseType.Failure.GetDisplayNameProperty(),
				failureMessage));
		}

        protected IActionResult ProcessDeleting(bool isDeleted, string successMessage, string failureMessage)
        {
            if (isDeleted)
				return StatusCode(200, new ResponseObject(ResponseType.Success.GetDisplayNameProperty(), successMessage));
			return StatusCode(404, new ResponseObject(ResponseType.Failure.GetDisplayNameProperty(),
				failureMessage));
		}
    }
}
