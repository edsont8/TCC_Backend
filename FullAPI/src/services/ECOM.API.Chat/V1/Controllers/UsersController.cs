﻿using ECOM.WebChat.Hubs2;
using ECOM.WebChat.Models2.ViewModels;
using ECOM.WebChat.Services2.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECOM.API.Chat.V1.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IHubContext<ChatHub> hubContext;

        public UsersController(IUserService userService, IHubContext<ChatHub> hubContext)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        }

        [HttpGet("search")]
        public ActionResult<List<UserViewModel>> FindUsers([FromQuery(Name = "name")] string user)
        {
            var result = userService.FindUserByMatch(user, User.Identity.Name).ToList();

            return result;
        }

        [HttpGet("getprofile")]
        public ActionResult<ProfileViewModel> GetProfile()
        {
            var currentUserId = this.User.Identity.Name;

            return this.userService.GetUserProfile(currentUserId);
        }

        [HttpPost("update")]
        public async Task<ActionResult> UpdateProfile([FromBody]ProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var currentUserId = this.User.Identity.Name;
            //Validation TODO: Extract to validations
            var curentProfile = this.userService.GetUserProfile(model.Id);
            if (curentProfile == model)
            {
                return Ok();
            }
            //Updating
            this.userService.UpdateProfile(model);
            //Invoke WSS to udate frontend
            await this.hubContext.Clients.All.SendAsync("ReviceUpdatedOpponentProfile", model);
            return Ok();
        }

    }
}
