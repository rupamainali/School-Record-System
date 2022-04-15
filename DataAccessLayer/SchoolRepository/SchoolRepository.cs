using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using DataAccessLayer.Repository;
using Microsoft.Extensions.Configuration;
using SharedModel.Response;
using SharedModel.School;

namespace DataAccessLayer.SchoolRepository
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly IDataAccessObject _dataAccessObject;
        private readonly string _connectionString;
        public SchoolRepository(IDataAccessObject dataAccessObject, IConfiguration configuration)
        {
            _dataAccessObject = dataAccessObject;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<SchoolDetails> GetSchoolList()
        {
            List<SchoolDetails> listSchool = new List<SchoolDetails>();
            var sql = "EXEC SP_SCHOOL_MANAGEMENT ";
            sql += "@Flag='GetAllSchool'";
            try
            {
                var dataTable = _dataAccessObject.ExecuteDataTable(sql);
                if (dataTable != null)
                {
                    foreach (DataRow school in dataTable.Rows)
                    {
                        SchoolDetails schoolDetails = new SchoolDetails();
                        schoolDetails.SchoolId = school["SchoolId"].ToString();
                        schoolDetails.SchoolName = school["SchoolName"].ToString();
                        schoolDetails.SchoolAddress = school["SchoolAddress"].ToString();
                        schoolDetails.SchoolRegistrationNumber = school["SchoolRegistrationNumber"].ToString();
                        schoolDetails.SchoolPrincipal = school["SchoolPrincipal"].ToString();
                        schoolDetails.NumberOfTeachers = school["NumberOfTeachers"].ToString();
                        listSchool.Add(schoolDetails);
                    }
                    return listSchool;
                }
                return listSchool;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("School List: " + ex.Message);
                return listSchool;
            }
        }
        public Response AddSchool(SchoolDetails school)
        {
            SchoolDetails schoolDetails = new SchoolDetails();
            var sql = "EXEC SP_SCHOOL_MANAGEMENT";
            sql += " @Flag='AddSchool'";
            sql += $" ,@SchoolId='{school.SchoolId}'";
            sql += $" ,@SchoolName='{school.SchoolName}'";
            sql += $" ,@SchoolAddress='{school.SchoolAddress}'";
            sql += $" ,@SchoolRegistrationNumber='{school.SchoolRegistrationNumber}'";
            sql += $" ,@SchoolPrincipal='{school.SchoolPrincipal}'";
            sql += $" ,@NumberOfTeachers='{school.NumberOfTeachers}'";
            try
            {
                var dataRow = _dataAccessObject.ExecuteDataRow(sql);
                Response response = new Response();
                if (dataRow != null)
                {
                    response.Code = dataRow["Code"].ToString();
                    response.Message = dataRow["Messsage"].ToString();

                }
                return response;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("School ADD: " + ex.Message);
                return new Response();
            }
        }

        public SchoolDetails GetSchoolDetails(string Id)
        {
            SchoolDetails schoolDetails = new SchoolDetails();
            var sql = "EXEC SP_SCHOOL_MANAGEMENT ";
            sql += "@flag='GetSchoolDetails'";
            sql += $" ,@SchoolId='{Id}'";
            var dataRow = _dataAccessObject.ExecuteDataRow(sql);
            if (dataRow != null)
            {
                schoolDetails.SchoolId = dataRow["SchoolId"].ToString();
                schoolDetails.SchoolName = dataRow["SchoolName"].ToString();
                schoolDetails.SchoolAddress = dataRow["SchoolAddress"].ToString();
                schoolDetails.SchoolRegistrationNumber = dataRow["SchoolRegistrationNumber"].ToString();
                schoolDetails.SchoolPrincipal = dataRow["SchoolPrincipal"].ToString();
                schoolDetails.NumberOfTeachers = dataRow["NumberOfTeachers"].ToString();
            }
            return schoolDetails;
        }

        public Response EditSchool(SchoolDetails schoolDetails)
        {
            // SchoolDetails schoolDetails = new SchoolDetails();
            var sql = "EXEC SP_SCHOOL_MANAGEMENT ";
            sql += "@flag='EditSchool'";
            sql += $" ,@SchoolId='{schoolDetails.SchoolId}'";
            sql += $" ,@SchoolName='{schoolDetails.SchoolName}'";
            sql += $" ,@SchoolAddress='{schoolDetails.SchoolAddress}'";
            sql += $" ,@SchoolRegistrationNumber='{schoolDetails.SchoolRegistrationNumber}'";
            sql += $" ,@SchoolPrincipal='{schoolDetails.SchoolPrincipal}'";
            sql += $" ,@NumberOfTeachers='{schoolDetails.NumberOfTeachers}'";

            var dataRow = _dataAccessObject.ExecuteDataRow(sql);
            Response response = new Response();
            if (dataRow != null)
            {

                response.Code = dataRow["Code"].ToString();
                response.Message = dataRow["Message"].ToString();
            }
            return response;
        }

        public Response DeleteSchool(string Id)
        {

            Response response = new Response();
            var sql = "EXEC SP_SCHOOL_MANAGEMENT ";
            sql += "@flag='DeleteSchool'";
            sql += $" ,@SchoolId='{Id}'";
            var dataRow = _dataAccessObject.ExecuteDataRow(sql);
            if (dataRow != null)
            {
                response.Code = dataRow["Code"].ToString();
                response.Message = dataRow["Message"].ToString();
            }
            return response;
        }

        public List<SchoolDetails> SearchSchoolList(string searchText)
        {
            List<SchoolDetails> listSchool = new List<SchoolDetails>();
            var sql = "EXEC SP_SCHOOL_MANAGEMENT ";
            sql += "@Flag='SearchSchoolList' ";
            sql += $",@SchoolName='{searchText}' ";
            try
            {
                var dataTable = _dataAccessObject.ExecuteDataTable(sql);
                if (dataTable != null)
                {
                    foreach (DataRow school in dataTable.Rows)
                    {
                        SchoolDetails schoolDetails = new SchoolDetails();
                        schoolDetails.SchoolId = school["SchoolId"].ToString();
                        schoolDetails.SchoolName = school["SchoolName"].ToString();
                        schoolDetails.SchoolAddress = school["SchoolAddress"].ToString();
                        schoolDetails.SchoolRegistrationNumber = school["SchoolRegistrationNumber"].ToString();
                        schoolDetails.SchoolPrincipal = school["SchoolPrincipal"].ToString();
                        schoolDetails.NumberOfTeachers = school["NumberOfTeachers"].ToString();
                        listSchool.Add(schoolDetails);
                    }
                    return listSchool;
                }
                return listSchool;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("School List: " + ex.Message);
                return listSchool;
            }
        }
    }
}


