using Microsoft.Data.Sqlite;
using Pokemon.Application.DTO;

namespace Pokemon.Data
{
    public interface IPokemonDatabase
    {
        void SaveMasterPokemon(string name, int age, string cpf);
        Task SaveCapturedPokemonAsync(PokemonDTO pokemon);
        IEnumerable<PokemonDTO> GetAllCapturedPokemons();
    }

    public class PokemonDatabase : IPokemonDatabase
    {
        public PokemonDatabase()
        {
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using (var connection = new SqliteConnection("Data Source=pokemon.db"))
            {
                connection.Open();

                var createTableCommand = connection.CreateCommand();
                createTableCommand.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Pokemon (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        CaptureRate INTEGER NOT NULL,
                        Id INTEGER NOT NULL);";

                createTableCommand.ExecuteNonQuery();
            }
        }

        public void SaveMasterPokemon(string name, int age, string cpf)
        {
            using (var connection = new SqliteConnection("Data Source=pokemon.db"))
            {
                connection.Open();

                var insertCommand = connection.CreateCommand();
                insertCommand.CommandText = @"
                    INSERT INTO MasterPokemon (Name, Age, CPF)
                    VALUES (@Name, @Age, @CPF);";
                insertCommand.Parameters.AddWithValue("@Name", name);
                insertCommand.Parameters.AddWithValue("@Age", age);
                insertCommand.Parameters.AddWithValue("@CPF", cpf);
                insertCommand.ExecuteNonQuery();
            }
        }

        public async Task SaveCapturedPokemonAsync(PokemonDTO pokemon)
        {
            using (var connection = new SqliteConnection("Data Source=pokemon.db"))
            {
                connection.Open();

                var captureRate = pokemon.Capture_Rate;
                var Id = pokemon.Id;

                var insertCommand = connection.CreateCommand();
                insertCommand.CommandText = @"
                    INSERT INTO Pokemon (Name, CaptureRate, Id)
                    VALUES (@Name, @CaptureRate, @Id);";
                insertCommand.Parameters.AddWithValue("@Name", pokemon.Name);
                insertCommand.Parameters.AddWithValue("@CaptureRate", captureRate);
                insertCommand.Parameters.AddWithValue("@Id", Id);

                insertCommand.ExecuteNonQueryAsync();
            }
        }

        public IEnumerable<PokemonDTO> GetAllCapturedPokemons()
        {
            var capturedPokemons = new List<PokemonDTO>();

            using (var connection = new SqliteConnection("Data Source=pokemon.db"))
            {
                connection.Open();

                var selectCommand = connection.CreateCommand();
                selectCommand.CommandText = @"
                    SELECT Name, CaptureRate, Id
                    FROM Pokemon;";

                using (var reader = selectCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var name = reader.GetString(0);
                        var captureRate = reader.GetInt32(1);
                        var id = reader.GetString(2);

                        var pokemon = new PokemonDTO(id, name, captureRate);

                        capturedPokemons.Add(pokemon);
                    }
                }
            }

            return capturedPokemons;
        }

    }
}