using Npgsql;
using System;

namespace CopaFilmes.Api.Util
{
    public static class DatabaseCommon
    {
        public static string ParseConnectionString(string conn)
        {
            NpgsqlConnectionStringBuilder builder;

            try
            {
                var isUri = Uri.TryCreate(conn, UriKind.Absolute, out var uri);

                if (isUri)
                {
                    builder = new NpgsqlConnectionStringBuilder
                    {
                        Host = uri.Host,
                        Port = uri.Port,
                        Database = uri.LocalPath.Substring(1),
                        Username = uri.UserInfo.Split(':')[0],
                        Password = uri.UserInfo.Split(':')[1],
                        Pooling = true
                    };
                }
                else
                {
                    builder = new NpgsqlConnectionStringBuilder(conn);
                }
            }
            catch (Exception)
            {
                throw new FormatException("Invalid connection string!");
            }

            return builder.ConnectionString;
        }
    }
}
