using hrmsapi.Model;
using System;
using System.Data;
using System.Data.SqlClient;

namespace hrmsapi.DataAccessLayer
{
    public class DAL
    {
        private readonly string _connectionString;

        public DAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public bool AddDepartment(Department department)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("SP_Department", con)) // ✅ Ensure procedure name matches your SQL
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Mode", 1);
                cmd.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
                cmd.Parameters.AddWithValue("@Status", department.IsActive);
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@CreateBy", department.CreatedBy);

               

                con.Open();
                int rowafftected = (int)cmd.ExecuteNonQuery();
                if (rowafftected > 0)
                {
                    return true;
                }
                else
                {
                    return false;

                }
            }
        }


    }
}
