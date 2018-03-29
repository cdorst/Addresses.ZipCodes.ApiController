// Copyright © Christopher Dorst. All rights reserved.
// Licensed under the GNU General Public License, Version 3.0. See the LICENSE document in the repository root for license information.

using Addresses.ZipCodes.DatabaseContext;
using DevOps.Code.DataAccess.Interfaces.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Addresses.ZipCodes.ApiController
{
    /// <summary>ASP.NET Core web API controller for ZipCode entities</summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ZipCodesController : ControllerBase
    {
        /// <summary>Represents the application events logger</summary>
        private readonly ILogger<ZipCodesController> _logger;

        /// <summary>Represents repository of ZipCode entity data</summary>
        private readonly IRepository<ZipCodeDbContext, ZipCode, int> _repository;

        /// <summary>Constructs an API controller for ZipCode entities using the given repository service</summary>
        public ZipCodesController(ILogger<ZipCodesController> logger, IRepository<ZipCodeDbContext, ZipCode, int> repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>Handles HTTP GET requests to access ZipCode resources at the given ID</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ZipCode>> Get(int id)
        {
            if (id < 1) return NotFound();
            var resource = await _repository.FindAsync(id);
            if (resource == null) return NotFound();
            return resource;
        }

        /// <summary>Handles HTTP HEAD requests to access ZipCode resources at the given ID</summary>
        [HttpHead("{id}")]
        public ActionResult<ZipCode> Head(int id) => null;

        /// <summary>Handles HTTP POST requests to save ZipCode resources</summary>
        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<ActionResult<ZipCode>> Post(ZipCode resource)
        {
            var saved = await _repository.AddAsync(resource);
            return CreatedAtAction(nameof(Get), new { id = saved.GetKey() }, saved);
        }
    }
}
