using Microsoft.Data.SqlClient;
using Roommates.Models;
using System.Collections.Generic;

namespace Roommates.Repositories
{
    public class RoommateRepository : BaseRepository
    {
        public RoommateRepository(string connectionString) : base(connectionString) { }

        public Roommate GetById(int roommateId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT FirstName, RentPortion, RoomId FROM Roommate JOIN Room On Roommate.RoomId = Room.Id  WHERE roommateId = @id";
                    cmd.Parameters.AddWithValue("@id", roommateId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    Roommate roommate = null;

                    if (reader.Read())
                    {
                        roommate = new Roommate
                        {
                            Id = roommateId,
                            FirstName = reader.GetString(reader.GetOrdinal("Name")),
                            RentPortion = reader.GetInt32(reader.GetOrdinal("RentPortion")),
                            RoomId = reader.GetInt32(reader.GetOrdinal("RoomId")),

                        };
                    }

                    reader.Close();

                    return roommate;
                }
            }
        }
    }


}