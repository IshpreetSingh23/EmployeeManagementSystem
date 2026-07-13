using EmployeeManagementSystem.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace EmployeeManagementSystem.Data
{
    public class DbHelper
    {
        private readonly string connectionString =
            "Server=127.0.0.1;Port=3306;Database=EmployeeManagementDB;Uid=root;Pwd=Harsimran@23;SslMode=None;";

        // Login
        public User? ValidateUser(string email, string password)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();

                string query = "SELECT * FROM Users WHERE Email=@Email AND Password=@Password";

                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            UserId = Convert.ToInt32(reader["UserId"]),
                            Name = reader["Name"].ToString()!,
                            Email = reader["Email"].ToString()!,
                            Password = reader["Password"].ToString()!,
                            Role = reader["Role"].ToString()!,
                            Phone = reader["Phone"].ToString()!,
                            Address = reader["Address"].ToString()!,
                            Status = reader["Status"].ToString()!
                        };
                    }
                }
            }

            return null;
        }

        // View Employees
        public List<Employee> GetEmployees()
        {
            List<Employee> list = new List<Employee>();

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();

                string query = "SELECT * FROM Employees";

                MySqlCommand cmd = new MySqlCommand(query, con);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Employee
                        {
                            EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                            Name = reader["Name"].ToString()!,
                            Email = reader["Email"].ToString()!,
                            Phone = reader["Phone"].ToString()!,
                            Department = reader["Department"].ToString()!,
                            Salary = Convert.ToDecimal(reader["Salary"])
                        });
                    }
                }
            }

            return list;
        }

        // Add Employee
        public bool AddEmployee(Employee emp)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();

                    string query = @"INSERT INTO Employees
                                    (Name, Email, Phone, Department, Salary)
                                    VALUES
                                    (@Name, @Email, @Phone, @Department, @Salary)";

                    MySqlCommand cmd = new MySqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@Name", emp.Name);
                    cmd.Parameters.AddWithValue("@Email", emp.Email);
                    cmd.Parameters.AddWithValue("@Phone", emp.Phone);
                    cmd.Parameters.AddWithValue("@Department", emp.Department);
                    cmd.Parameters.AddWithValue("@Salary", emp.Salary);

                    int rows = cmd.ExecuteNonQuery();

                    Console.WriteLine("Rows Inserted = " + rows);

                    return rows > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("DATABASE ERROR");
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        // Get Employee By Id
        public Employee? GetEmployeeById(int id)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();

                string query = "SELECT * FROM Employees WHERE EmployeeId=@EmployeeId";

                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@EmployeeId", id);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Employee
                        {
                            EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                            Name = reader["Name"].ToString()!,
                            Email = reader["Email"].ToString()!,
                            Phone = reader["Phone"].ToString()!,
                            Department = reader["Department"].ToString()!,
                            Salary = Convert.ToDecimal(reader["Salary"])
                        };
                    }
                }
            }

            return null;
        }

        // Update Employee
        public bool UpdateEmployee(Employee emp)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();

                string query = @"UPDATE Employees
                                 SET Name=@Name,
                                     Email=@Email,
                                     Phone=@Phone,
                                     Department=@Department,
                                     Salary=@Salary
                                 WHERE EmployeeId=@EmployeeId";

                MySqlCommand cmd = new MySqlCommand(query, con);

                cmd.Parameters.AddWithValue("@EmployeeId", emp.EmployeeId);
                cmd.Parameters.AddWithValue("@Name", emp.Name);
                cmd.Parameters.AddWithValue("@Email", emp.Email);
                cmd.Parameters.AddWithValue("@Phone", emp.Phone);
                cmd.Parameters.AddWithValue("@Department", emp.Department);
                cmd.Parameters.AddWithValue("@Salary", emp.Salary);

                return cmd.ExecuteNonQuery() > 0;
                
            }
        
        }
                // Delete Employee
        public bool DeleteEmployee(int id)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();

                string query = "DELETE FROM Employees WHERE EmployeeId=@EmployeeId";

                MySqlCommand cmd = new MySqlCommand(query, con);

                cmd.Parameters.AddWithValue("@EmployeeId", id);

                int rows = cmd.ExecuteNonQuery();

                return rows > 0;
            }
        }
// ==========================
// DEPARTMENT METHODS
// ==========================

// View Departments
public List<Department> GetDepartments()
{
    List<Department> list = new List<Department>();

    using (MySqlConnection con = new MySqlConnection(connectionString))
    {
        con.Open();

        string query = "SELECT * FROM Departments";

        MySqlCommand cmd = new MySqlCommand(query, con);

        using (MySqlDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                list.Add(new Department
                {
                    DepartmentId = Convert.ToInt32(reader["DepartmentId"]),
                    DepartmentName = reader["DepartmentName"].ToString()!,
                    Location = reader["Location"].ToString()!
                });
            }
        }
    }

    return list;
}

// Add Department
public bool AddDepartment(Department dept)
{
    using (MySqlConnection con = new MySqlConnection(connectionString))
    {
        con.Open();

        string query = @"INSERT INTO Departments
                        (DepartmentName, Location)
                        VALUES
                        (@DepartmentName, @Location)";

        MySqlCommand cmd = new MySqlCommand(query, con);

        cmd.Parameters.AddWithValue("@DepartmentName", dept.DepartmentName);
        cmd.Parameters.AddWithValue("@Location", dept.Location);

        return cmd.ExecuteNonQuery() > 0;
    }
}

// Get Department By Id
public Department? GetDepartmentById(int id)
{
    using (MySqlConnection con = new MySqlConnection(connectionString))
    {
        con.Open();

        string query = "SELECT * FROM Departments WHERE DepartmentId=@DepartmentId";

        MySqlCommand cmd = new MySqlCommand(query, con);

        cmd.Parameters.AddWithValue("@DepartmentId", id);

        using (MySqlDataReader reader = cmd.ExecuteReader())
        {
            if (reader.Read())
            {
                return new Department
                {
                    DepartmentId = Convert.ToInt32(reader["DepartmentId"]),
                    DepartmentName = reader["DepartmentName"].ToString()!,
                    Location = reader["Location"].ToString()!
                };
            }
        }
    }

    return null;
}

// Update Department
public bool UpdateDepartment(Department dept)
{
    using (MySqlConnection con = new MySqlConnection(connectionString))
    {
        con.Open();

        string query = @"UPDATE Departments
                         SET DepartmentName=@DepartmentName,
                             Location=@Location
                         WHERE DepartmentId=@DepartmentId";

        MySqlCommand cmd = new MySqlCommand(query, con);

        cmd.Parameters.AddWithValue("@DepartmentId", dept.DepartmentId);
        cmd.Parameters.AddWithValue("@DepartmentName", dept.DepartmentName);
        cmd.Parameters.AddWithValue("@Location", dept.Location);

        return cmd.ExecuteNonQuery() > 0;
    }
}

// Delete Department
public bool DeleteDepartment(int id)
{
    using (MySqlConnection con = new MySqlConnection(connectionString))
    {
        con.Open();

        string query = "DELETE FROM Departments WHERE DepartmentId=@DepartmentId";

        MySqlCommand cmd = new MySqlCommand(query, con);

        cmd.Parameters.AddWithValue("@DepartmentId", id);

        return cmd.ExecuteNonQuery() > 0;
    }
}
// ==========================
// ATTENDANCE METHODS
// ==========================

// View Attendance
public List<Attendance> GetAttendance()
{
    List<Attendance> list = new List<Attendance>();

    using (MySqlConnection con = new MySqlConnection(connectionString))
    {
        con.Open();

        string query = "SELECT * FROM Attendance";

        MySqlCommand cmd = new MySqlCommand(query, con);

        using (MySqlDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                list.Add(new Attendance
                {
                    AttendanceId = Convert.ToInt32(reader["AttendanceId"]),
                    EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                    EmployeeName = reader["EmployeeName"].ToString()!,
                    AttendanceDate = Convert.ToDateTime(reader["AttendanceDate"]),
                    Status = reader["Status"].ToString()!
                });
            }
        }
    }

    return list;
}
// Add Attendance
public bool AddAttendance(Attendance attendance)
{
    using (MySqlConnection con = new MySqlConnection(connectionString))
    {
        con.Open();

        string query = @"INSERT INTO Attendance
                        (EmployeeId, EmployeeName, AttendanceDate, Status)
                        VALUES
                        (@EmployeeId, @EmployeeName, @AttendanceDate, @Status)";

        MySqlCommand cmd = new MySqlCommand(query, con);

        cmd.Parameters.AddWithValue("@EmployeeId", attendance.EmployeeId);
        cmd.Parameters.AddWithValue("@EmployeeName", attendance.EmployeeName);
        cmd.Parameters.AddWithValue("@AttendanceDate", attendance.AttendanceDate);
        cmd.Parameters.AddWithValue("@Status", attendance.Status);

        return cmd.ExecuteNonQuery() > 0;
    }
}
// Get Attendance By Id
public Attendance? GetAttendanceById(int id)
{
    using (MySqlConnection con = new MySqlConnection(connectionString))
    {
        con.Open();

        string query = "SELECT * FROM Attendance WHERE AttendanceId=@AttendanceId";

        MySqlCommand cmd = new MySqlCommand(query, con);
        cmd.Parameters.AddWithValue("@AttendanceId", id);

        using (MySqlDataReader reader = cmd.ExecuteReader())
        {
            if (reader.Read())
            {
                return new Attendance
                {
                    AttendanceId = Convert.ToInt32(reader["AttendanceId"]),
                    EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                    EmployeeName = reader["EmployeeName"].ToString()!,
                    AttendanceDate = Convert.ToDateTime(reader["AttendanceDate"]),
                    Status = reader["Status"].ToString()!
                };
            }
        }
    }

    return null;
}
// Update Attendance
public bool UpdateAttendance(Attendance attendance)
{
    using (MySqlConnection con = new MySqlConnection(connectionString))
    {
        con.Open();

        string query = @"UPDATE Attendance
                         SET EmployeeId=@EmployeeId,
                             EmployeeName=@EmployeeName,
                             AttendanceDate=@AttendanceDate,
                             Status=@Status
                         WHERE AttendanceId=@AttendanceId";

        MySqlCommand cmd = new MySqlCommand(query, con);

        cmd.Parameters.AddWithValue("@AttendanceId", attendance.AttendanceId);
        cmd.Parameters.AddWithValue("@EmployeeId", attendance.EmployeeId);
        cmd.Parameters.AddWithValue("@EmployeeName", attendance.EmployeeName);
        cmd.Parameters.AddWithValue("@AttendanceDate", attendance.AttendanceDate);
        cmd.Parameters.AddWithValue("@Status", attendance.Status);

        return cmd.ExecuteNonQuery() > 0;
    }
}
// Delete Attendance
public bool DeleteAttendance(int id)
{
    using (MySqlConnection con = new MySqlConnection(connectionString))
    {
        con.Open();

        string query = "DELETE FROM Attendance WHERE AttendanceId=@AttendanceId";

        MySqlCommand cmd = new MySqlCommand(query, con);

        cmd.Parameters.AddWithValue("@AttendanceId", id);

        return cmd.ExecuteNonQuery() > 0;
    }
}
// Total Employees
public int GetEmployeeCount()
{
    using (MySqlConnection con = new MySqlConnection(connectionString))
    {
        con.Open();

        string query = "SELECT COUNT(*) FROM Employees";

        MySqlCommand cmd = new MySqlCommand(query, con);

        return Convert.ToInt32(cmd.ExecuteScalar());
    }
}

// Total Departments
public int GetDepartmentCount()
{
    using (MySqlConnection con = new MySqlConnection(connectionString))
    {
        con.Open();

        string query = "SELECT COUNT(*) FROM Departments";

        MySqlCommand cmd = new MySqlCommand(query, con);

        return Convert.ToInt32(cmd.ExecuteScalar());
    }
}

// Total Present
public int GetPresentCount()
{
    using (MySqlConnection con = new MySqlConnection(connectionString))
    {
        con.Open();

        string query = "SELECT COUNT(*) FROM Attendance WHERE Status='Present'";

        MySqlCommand cmd = new MySqlCommand(query, con);

        return Convert.ToInt32(cmd.ExecuteScalar());
    }
}

// Total Absent
public int GetAbsentCount()
{
    using (MySqlConnection con = new MySqlConnection(connectionString))
    {
        con.Open();

        string query = "SELECT COUNT(*) FROM Attendance WHERE Status='Absent'";

        MySqlCommand cmd = new MySqlCommand(query, con);

        return Convert.ToInt32(cmd.ExecuteScalar());
    }
}

}
}
    
    
