using BicycleStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace BicycleStore.Controllers.Api
{
    [ApiController]
    [Authorize(AuthenticationSchemes =
    JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class BicyclesController : Controller
    {
        private readonly BicycleContext bicycleContext;

        public BicyclesController(BicycleContext bicycleContext)
        {
            this.bicycleContext = bicycleContext;
        }

        public async Task<ActionResult<IEnumerable<Bicycle>>> Get()
        {
            return bicycleContext.Bicycles.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Bicycle>> Get(int id)
        {
            Bicycle bicycle = await bicycleContext.Bicycles.FirstOrDefaultAsync(x => x.BicycleId == id);
            if(bicycle == null)
            {
                return NotFound();
            }
            return bicycle;
        }

        [HttpPost]
        public async Task<ActionResult<Bicycle>> Post(Bicycle bicycle)
        {
            if (bicycle.Price <= 50)
            {
                ModelState.AddModelError("Price", "Price should be more than 50$");
            }

            if (bicycle.Manufacturer.ToLower() == "some" ||
                bicycle.Manufacturer.ToLower() == "any" ||
                bicycle.Manufacturer.ToLower() == "example" ||
                bicycle.Manufacturer.ToLower() == "newone" ||
                bicycle.Manufacturer.ToLower() == "1")
            {
                ModelState.AddModelError("Manufacturer", $"You cannot add product with such name as \"{bicycle.Manufacturer}\" to our shop");
            }
            if (bicycle.Brakes <= 0 || bicycle.Brakes > 10)
            {
                ModelState.AddModelError("Brakes", "Parameter \"brakes\" should be from 1 to 10");
            }
            if (bicycle.WeelsRadius <= 0 || bicycle.WeelsRadius > 50)
            {
                ModelState.AddModelError("WeelsRadius", $"Parameter \"weels radius\" should be positive and not bigger than 50");
            }
            if (bicycle.Weight <= 0 || bicycle.Weight > 50)
            {
                ModelState.AddModelError("Weight", $"Parameter \"weight\" should be positive and not bigger than 50kg");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bicycleContext.Bicycles.Add(bicycle);
            await bicycleContext.SaveChangesAsync();
            return Ok(bicycle);
        }

        [HttpPut]
        public async Task<ActionResult<Bicycle>> Put(Bicycle bicycle)
        {
            if (bicycle.Price <= 50)
            {
                ModelState.AddModelError("Price", "Price should be more than 50$");
            }

            if (bicycle.Manufacturer.ToLower() == "some" ||
                bicycle.Manufacturer.ToLower() == "any" ||
                bicycle.Manufacturer.ToLower() == "example" ||
                bicycle.Manufacturer.ToLower() == "newone" ||
                bicycle.Manufacturer.ToLower() == "1")
            {
                ModelState.AddModelError("Manufacturer", $"You cannot add product with such name as \"{bicycle.Manufacturer}\" to our shop");
            }
            if (bicycle.Brakes <= 0 || bicycle.Brakes > 10)
            {
                ModelState.AddModelError("Brakes", "Parameter \"brakes\" should be from 1 to 10");
            }
            if (bicycle.WeelsRadius <= 0 || bicycle.WeelsRadius > 50)
            {
                ModelState.AddModelError("WeelsRadius", $"Parameter \"weels radius\" should be positive and not bigger than 50");
            }
            if (bicycle.Weight <= 0 || bicycle.Weight > 50)
            {
                ModelState.AddModelError("Weight", $"Parameter \"weight\" should be positive and not bigger than 50kg");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (bicycle == null)
            {
                return BadRequest(ModelState);
            }
            if(!bicycleContext.Bicycles.Any(x => x.BicycleId == bicycle.BicycleId))
            {
                NotFound();
            }
            bicycleContext.Update(bicycle);
            await bicycleContext.SaveChangesAsync();
            return Ok(bicycle);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Bicycle>> Delete(int id)
        {
            Bicycle bicycle = bicycleContext.Bicycles.FirstOrDefault(x => x.BicycleId == id);
            if (bicycle == null)
            {
                return NotFound();
            }
            bicycleContext.Bicycles.Remove(bicycle);
            await bicycleContext.SaveChangesAsync();
            return Ok(bicycle);
        }
    }
}
