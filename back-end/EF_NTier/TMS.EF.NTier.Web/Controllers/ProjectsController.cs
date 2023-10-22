﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMS.EF.NTier.DAL.Context;
using TMS.EF.NTier.DAL.Entities;

namespace TMS.EF.NTier.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly TaskManagementSystemDbContext _context;

        public ProjectsController(TaskManagementSystemDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Project> GetProjects()
        {
            return _context.Projects;
        }
    }
}
