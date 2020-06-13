﻿//using LAB456.DTOs;
using LAB456.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LAB456.Controllers
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _dbContext;
        public AttendancesController()
        {
            _dbContext = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend([FromBody] int courseId)
        {
            var userId = User.Identity.GetUserId();
            if (_dbContext.Attendances.Any(a => a.AttendeeId == userId && a.CourseId == courseId))
                return BadRequest("The Attendance already exist!");

            var attendance = new Attendance
            {
                CourseId = courseId,
                AttendeeId = userId

            };
            _dbContext.Attendances.Add(attendance);
            _dbContext.SaveChanges();
            return Ok();
        }

        //[HttpDelete]
        //public IHttpActionResult DeleteAttendance(int id)
        //{
        //    var userId = User.Identity.GetUserId();
        //    var attendance = _dbContext.Attendances.SingleOrDefault(a => a.AttendeeId == userId && a.CourseId == id);
        //    if (attendance == null)
        //    {
        //        return NotFound();
        //    }
        //    _dbContext.Attendances.Remove(attendance);
        //    _dbContext.SaveChanges();
        //    return Ok(id);
        //}
    }
}
