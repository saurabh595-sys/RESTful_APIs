using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using restApidemo.Models;

namespace restApidemo.Controllers
{
    public class ValuesController : ApiController
    {
        SqlConnection con = new SqlConnection(@"server=SaurabhLaptop\SQLEXPRESS;database=testApi;Integrated Security=true;");
        // GET api/values
        public string Get()
        {
            /*return new string[] { "value1", "value2" };*/
            SqlDataAdapter da= new SqlDataAdapter("select * from test", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count>0)
            {
                return JsonConvert.SerializeObject(dt);
            }
            else
            {
                return "No data found";
            }

        }

        // GET api/values/5
        public string Get(int id)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from test where id='"+id+"'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return JsonConvert.SerializeObject(dt);
            }
            else
            {
                return "No data found";
            }

        }

        // POST api/values
        [HttpPost]
        public string Post( [FromBody]UserModel model)
        {
            SqlCommand cmd = new SqlCommand("insert into [test] values('"+model.userName  + "','" + model.password + "')",con);
            con.Open();
            int i =cmd.ExecuteNonQuery();
            con.Close();
            if (i==1)
            {
                return "User regesterd sucessfully";
            }
            else
            {
                return "failed to regesterd User";
            }
        }

        // PUT api/values/5
        public string Put(int id, [FromBody]UserModel model)
        {
            SqlCommand cmd = new SqlCommand(" UPDATE test SET username = '" + model.userName+ "', password = '" + model.password+ "' WHERE id='"+id+"' ", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i == 1)
            {
                return "User updated sucessfully";
            }
            else
            {
                return "User updated UnSucessfully";
            }
           
        }

        // DELETE api/values/5
        public string Delete(int id)
        {
            SqlCommand cmd = new SqlCommand(" Delete from test where id='"+id+"'", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i == 1)
            {
                return "User deleted sucessfully";
            }
            else
            {
                return "User deleted UnSucessfully";
            }
        }
    }
}
