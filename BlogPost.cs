using System.Threading.Tasks;
using MySqlConnector;
using System.Data;

namespace BlogPostApi
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string  Content { get; set; }

        internal AppDb Db { get; set; }

        public BlogPost()
        {
            
        }

        internal BlogPost(AppDb db)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"insert into BlogPost (Title, Content) 
            values(@title, @content)";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync() 
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"update BlogPost 
            set Title = @title, Content = @content where Id = @id";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"delete from BlogPost where Id = @id";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = Id,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@title",
                DbType = DbType.String,
                Value = Title,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@content",
                DbType = DbType.String,
                Value = Content,
            });
        }
    }
}