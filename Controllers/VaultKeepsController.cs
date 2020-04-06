using System;
using System.Security.Claims;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _service;
    private readonly KeepsService _ks;
    public VaultKeepsController(VaultKeepsService vks, KeepsService ks)
    {
      _service = vks;
      _ks = ks;
    }

    [HttpPost]
    [Authorize]
    public ActionResult<VaultKeep> Create([FromBody] VaultKeep newVaultKeep)
    {
      try
      {
        string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        newVaultKeep.UserId = userId;
        return Ok(_service.Create(newVaultKeep));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }



  }
}