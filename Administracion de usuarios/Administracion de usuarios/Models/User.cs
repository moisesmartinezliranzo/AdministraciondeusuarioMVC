using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Administracion_de_usuarios.Models
{
    public class User
    {
        DataTable myDataTable = new DataTable();
        SqlCommand myCommand;
        SqlDataAdapter mySqlAdapter;

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserLastName { get; set; }
        public string UserAddr { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public string UserGender { get; set; }
        public Dictionary<string, string> userGenderOption { get; set; }

        public User()
        {
        }

        public User(int id, string name, string last_name, string addr, string email, string phone, string gender)
        {
            this.UserId = id;
            this.UserName = name;
            this.UserLastName = last_name;
            this.UserAddr = addr;
            this.UserEmail = email;
            this.UserPhone = phone;
            this.UserGender = gender;
        }

        public IEnumerable<User> GetUsers()
        {
            IEnumerable<User> userList;

            myDataTable = new DataTable();
            myCommand = new SqlCommand("SP_ListUsers", MyConnection.GetConnection());
            myCommand.CommandType = CommandType.StoredProcedure;
            mySqlAdapter = new SqlDataAdapter(myCommand);

            mySqlAdapter.Fill(myDataTable);
            if (myCommand.Connection.State == ConnectionState.Open)
            {
                myCommand.Connection.Close();
            }

            userList = myDataTable.AsEnumerable().Select(u => new User
            {
                UserId = (int)u["id"],
                UserName = UpperFirstLetter(u["Nombre"].ToString()),
                UserLastName = UpperFirstLetter(u["Apellido"].ToString()),
                UserAddr = u["Dirección"].ToString(),
                UserEmail = u["Email"].ToString(),
                UserPhone = u["Teléfono"].ToString(),
                UserGender = u["Género"].ToString()
            });

            return userList;
        }

        public User GetUser(int id)
        {
            myCommand = new SqlCommand("SP_ListUsersById", MyConnection.GetConnection());
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;

            mySqlAdapter = new SqlDataAdapter(myCommand);
         
            mySqlAdapter.Fill(myDataTable);

            User user = myDataTable.AsEnumerable().Select(u => new User
            {
                UserId = (int)u["id"],
                UserName = UpperFirstLetter(u["Nombre"].ToString()),
                UserLastName = UpperFirstLetter(u["Apellido"].ToString()),
                UserAddr = u["Dirección"].ToString(),
                UserEmail = u["Email"].ToString(),
                UserPhone = u["Teléfono"].ToString(),
                UserGender = u["Género"].ToString()
            }).First();

            return user;
        }

        public bool NewUser(string name,string last_name, string addr, string email, string phone, string gender)
        {
            myCommand = new SqlCommand("SP_CreateUser",MyConnection.GetConnection());
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("@name",SqlDbType.VarChar).Value = name;
            myCommand.Parameters.Add("@last_name",SqlDbType.VarChar).Value = last_name;
            myCommand.Parameters.Add("@addr",SqlDbType.VarChar).Value = addr;
            myCommand.Parameters.Add("@email",SqlDbType.VarChar).Value = email;
            myCommand.Parameters.Add("@phone", SqlDbType.VarChar).Value = phone;
            myCommand.Parameters.Add("@gender", SqlDbType.VarChar).Value = gender;

            myCommand.ExecuteNonQuery();

            return true;
        }

        public User UpdateUser(int id, string name, string last_name, string addr, string email, string phone, string gender)
        {
            myCommand = new SqlCommand("SP_UpdateUser", MyConnection.GetConnection());
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
            myCommand.Parameters.Add("@last_name", SqlDbType.VarChar).Value = last_name;
            myCommand.Parameters.Add("@addr", SqlDbType.VarChar).Value = addr;
            myCommand.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
            myCommand.Parameters.Add("@phone", SqlDbType.VarChar).Value = phone;
            myCommand.Parameters.Add("@gender", SqlDbType.VarChar).Value = gender;
            myCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
            myCommand.ExecuteNonQuery();

            mySqlAdapter = new SqlDataAdapter(myCommand);

            mySqlAdapter.Fill(myDataTable);

            User user = myDataTable.AsEnumerable().Select(u => new User
            {
                UserId = (int)u["id"],
                UserName = UpperFirstLetter(u["Nombre"].ToString()),
                UserLastName = UpperFirstLetter(u["Apellido"].ToString()),
                UserAddr = u["Dirección"].ToString(),
                UserEmail = u["Email"].ToString(),
                UserPhone = u["Teléfono"].ToString(),
                UserGender = u["Género"].ToString()
            }).First();

            return user;
        }

        public bool DeleteUser(int id)
        {
            myCommand = new SqlCommand("SP_DeleteUser", MyConnection.GetConnection());
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;

            int bol = myCommand.ExecuteNonQuery();

            return (bol>0);
        }

        private string UpperFirstLetter(string firtLetterUpper)
        {
            if (string.IsNullOrEmpty(firtLetterUpper))
            {
                return string.Empty;
            }
            return char.ToUpper(firtLetterUpper[0]) + firtLetterUpper.Substring(1);
        }

    }
}