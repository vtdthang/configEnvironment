using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using WebApplication5.Models;
using WebApplication5.Core;
using Newtonsoft.Json;
using System.Dynamic;

namespace WebApplication5.Controllers
{
    public class TestController : ApiController
    {
        private const string SERVER_FORMAT = "{0} SERVER ON {1}";
        private readonly string ConnectionStr = ConfigurationManager.ConnectionStrings["LetsPartyConnectionString"].ConnectionString;

        #region GET /api/ping
        [HttpGet]
        [Route("api/ping")]
        public IHttpActionResult PingServer()
        {
            var serverContent = "EMPTY";

#if Staging
            serverContent = string.Format(SERVER_FORMAT, "staging", DateTime.UtcNow);
#elif Production
            serverContent = string.Format(SERVER_FORMAT, "production", DateTime.UtcNow);
#else
            serverContent = string.Format(SERVER_FORMAT, "dev", DateTime.UtcNow);
#endif
            return Ok(serverContent);
        }
        #endregion

        [Route("api/test")]
        [HttpGet]
        public IHttpActionResult Test()
        {
            IEnumerable<User> users = new List<User>();

            using (var db = new SqlConnection(ConnectionStr))
            {
                SqlMapper.ResetTypeHandlers();
                SqlMapper.AddTypeHandler(new TypeHandler<List<User>>());

                var sqlQuery = @"
                    DECLARE @result NVARCHAR(max);
                    SET @result = (
	                    SELECT u.Id, u.FullName, u.Email,
	                        (
		                        SELECT Id, Name FROM [UserTag] WHERE UserId = u.Id FOR JSON AUTO
	                        ) AS Tags
                        FROM [User] u                  
                        FOR JSON AUTO, INCLUDE_NULL_VALUES
                    )
                    SELECT @result;";

                var test = db.Query<dynamic>(sqlQuery);

                var result = db.QueryFirstOrDefault<List<User>>(sqlQuery);               
            }

            return Ok(users);
        }
    }
}
