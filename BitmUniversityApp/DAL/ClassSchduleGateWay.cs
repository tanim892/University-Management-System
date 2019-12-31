using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BitmUniversityApp.Models;
using UniversitycourseResultManagementSystem.GateWay;
using UniversityMS.DAL;

namespace BitmUniversityApp.DAL
{
    public class ClassSchduleGateWay : GateWay
    {
        public List<ClassSchedule> ClassSchedule(int deptId,int courseId)
        {
            Query = "Select * from ScheduleOfClass where DepartmentId='" + deptId + "'AND CourseId='" + courseId +
                    "' AND RoomStatus='Allocated'";

                   
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<ClassSchedule> schedules = new List<ClassSchedule>();
            while (Reader.Read())
            {
                ClassSchedule schedule = new ClassSchedule()
                {
                    CourseCode = Reader["CourseCode"].ToString(),
                    CourseName = Reader["CourseName"].ToString(),
                    RoomName = Reader["Room_Name"].ToString(),
                    DayName = Reader["Day_Name"].ToString(),
                    StartTime = Reader["StartTime"].ToString(),
                    EndTime = Reader["EndTime"].ToString(),
                    Status = Reader["RoomStatus"].ToString()
                };



                schedules.Add(schedule);
            }

            Reader.Close();
            Connection.Close();
            return schedules;
        }
    }
}