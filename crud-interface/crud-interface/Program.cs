using crud_interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

bool running = true;
while (running)
{
    try
    {
        ConsoleApp.PrintSelectionMenu();
    }
    catch(ArgumentException e)
    {
        running = false;
    }
}

public class ConsoleApp
{
    public static void PrintSelectionMenu()
    {
        Console.WriteLine("Select action by entering one of the following letters:");
        Console.WriteLine("c:\tCreate new department.");
        Console.WriteLine("u:\tUpdate department.");
        Console.WriteLine("m:\tUpdate manager.");
        Console.WriteLine("d:\tDelete department.");
        Console.WriteLine("p:\tPrint department.");
        Console.WriteLine("a:\tPrint all departments.");
        Console.WriteLine("Other keys will exit the application.");
        char input = Console.ReadKey().KeyChar;
        switch (input)
        {
            case 'a':
                PrintAllDepartments();
                break;
            case 'p':
                PrintDepartment();
                break;
            case 'd':
                PrintDeleteDepartment();
                break;
            case 'm':
                PrintUpdateManager();
                break;
            case 'c':
                PrintCreateDepartment();
                break;
            case 'u':
                PrintUpdateDepartment();
                break;
            default:
                Console.WriteLine("Quitting...");
                throw new ArgumentException("Quitting");
        }
    }

    public static void PrintAllDepartments()
    {
        Console.WriteLine("Print all departments:");
        Procedures.GetAllDepartments();
    }

    public static void PrintDepartment()
    {
        Console.WriteLine("Print department:");
        Console.WriteLine("Enter department number");
        try
        {
            int dno = int.Parse(Console.ReadLine());
            Procedures.GetDepartment(dno);
        }catch(Exception e)
        {
            Console.WriteLine("Input was not allowed");
        }
    }

    public static void PrintDeleteDepartment()
    {
        Console.WriteLine("Delete department:");
        Console.WriteLine("Enter department number");
        try { 
            int dno = int.Parse(Console.ReadLine());
            Procedures.DeleteDepartment(dno);
        }
        catch (Exception e)
        {
            Console.WriteLine("Input was not allowed");
        }
    }

    public static void PrintUpdateManager()
    {
        Console.WriteLine("Update manager:");
        Console.WriteLine("Enter department number");
        try { 
            int dno = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter mgr ssn");
            int mgr = int.Parse(Console.ReadLine());
            Procedures.UpdateManager(dno, mgr);
        }
        catch (Exception e)
        {
            Console.WriteLine("Input was not allowed");
        }
    }

    public static void PrintUpdateDepartment()
    {
        Console.WriteLine("Update a Department:");
        Console.WriteLine("Enter department number");
        try { 
            int dno = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter department name");
            string name = Console.ReadLine().ToString();
            if (name.Contains('\"') || name.Contains('\'') || name.Contains('´') || name.Contains('`'))
            {
                throw new Exception("Character not allowed");
            }
            Procedures.UpdateDepartment(dno, name);
        }
        catch (Exception e)
        {
            Console.WriteLine("Input was not allowed");
        }
    }

    public static void PrintCreateDepartment()
    {
        Console.WriteLine("Create a Department:");
        Console.WriteLine("Enter department name");
        try { 
            string name = Console.ReadLine().ToString();
            if(name.Contains('\"') || name.Contains('\'') || name.Contains('´') || name.Contains('`'))
            {
                throw new Exception("Character not allowed");
            }
            Console.WriteLine("Enter mgr ssn");
            int mgr = int.Parse(Console.ReadLine());
            Procedures.CreateDepartment(name, mgr);
        }
        catch (Exception e)
        {
            Console.WriteLine("Input was not allowed");
        }
    }
}

public class Procedures
{
    public static void GetAllDepartments()
    {
        using (DbContext _context = new Company_OnlineContext())
        {
            _context.Database.OpenConnection();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "usp_GetAllDepartments";
                command.CommandType = CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("DName: " + String.Format("{0}", reader[0]));
                        Console.WriteLine("DNumber: " + String.Format("{0}", reader[1]));
                        Console.WriteLine("MgrSSN: " + String.Format("{0}", reader[2]));
                        Console.WriteLine("Mgr start date: " + String.Format("{0}", reader[3]));
                        Console.WriteLine("Employee count: " + String.Format("{0}", reader[4]));
                    }
                }
            }
        }
    }

    public static void GetDepartment(int dno)
    {
        using (DbContext _context = new Company_OnlineContext())
        {
            _context.Database.OpenConnection();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "usp_GetDepartment";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@DNumber", dno));
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("DName: " + String.Format("{0}", reader[0]));
                        Console.WriteLine("DNumber: " + String.Format("{0}", reader[1]));
                        Console.WriteLine("MgrSSN: " + String.Format("{0}", reader[2]));
                        Console.WriteLine("Mgr start date: " + String.Format("{0}", reader[3]));
                        Console.WriteLine("Employee count: " + String.Format("{0}", reader[4]));
                    }
                }
            }
        }
    }

    public static void DeleteDepartment(int dno)
    {
        using (DbContext _context = new Company_OnlineContext())
        {
            _context.Database.OpenConnection();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "usp_DeleteDepartment";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@DNumber", dno));
                Console.WriteLine(command.ExecuteNonQuery());
            }
        }
    }

    public static void UpdateManager(int dno, int mgr)
    {
        using (DbContext _context = new Company_OnlineContext())
        {
            _context.Database.OpenConnection();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "usp_UpdateDepartmentManager";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@DNumber", dno));
                command.Parameters.Add(new SqlParameter("@MgrSSN", mgr));
                Console.WriteLine(command.ExecuteNonQuery());
            }
        }
    }

    public static void UpdateDepartment(int dno, string name)
    {
        using (DbContext _context = new Company_OnlineContext())
        {
            _context.Database.OpenConnection();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "usp_UpdateDepartmentName";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@DName", name));
                command.Parameters.Add(new SqlParameter("@DNumber", dno));
                Console.WriteLine(command.ExecuteNonQuery());
            }
        }
    }

    public static void CreateDepartment(string name, int mgr)
    {
        using (DbContext _context = new Company_OnlineContext())
        {
            _context.Database.OpenConnection();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "usp_CreateDepartment";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@DName", name));
                command.Parameters.Add(new SqlParameter("@MgrSSN", mgr));
                Console.WriteLine(command.ExecuteNonQuery());
            }
        }
    }
}