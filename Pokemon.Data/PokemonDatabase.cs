using Microsoft.Data.Sqlite;
using Pokemon.Application.DTO;

namespace Pokemon.Data
{
    public interface IPokemonDatabase
    {
        Task<List<PokemonMasterDTO>> GetAllPokemonMastersAsync();
        Task<List<PokemonDTO>> GetAllCapturedPokemonsAsync(int? masterId = null);
        Task SaveCapturedPokemonAsync(PokemonDTO pokemon, int masterId);
        Task<PokemonMasterDTO> SearchMasterPokemonByNameAsync(string Name);
        Task<PokemonMasterDTO> SearchMasterPokemonByIdAsync(int Name);
        Task<PokemonMasterDTO> SaveMasterPokemonAsync(PokemonMasterDTO master);
        Task<PokemonMasterDTO> SearchMasterPokemonByCPFAsync(string CPF);
    }

    public class PokemonDatabase : IPokemonDatabase
    {
        string dbName = "pokemon2323.db";
        public PokemonDatabase()
        {
            SQLitePCL.Batteries_V2.Init();
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using (var connection = new SqliteConnection($"Data Source={dbName}"))
            {
                connection.Open();

                //var dropTablePokemonCommand = connection.CreateCommand();
                //dropTablePokemonCommand.CommandText = @"
                //    DROP TABLE IF EXISTS Pokemon;";
                //dropTablePokemonCommand.ExecuteNonQuery();
                //var dropTableMastersCommand = connection.CreateCommand();
                //dropTableMastersCommand.CommandText = @"
                //    DROP TABLE IF EXISTS MasterPokemon;";
                //dropTableMastersCommand.ExecuteNonQuery();

                using var command = connection.CreateCommand();
                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS MasterPokemon (" +
                    "Id INTEGER PRIMARY KEY, " +
                    "Name TEXT NOT NULL, " +
                    "Age INTEGER NOT NULL, " +
                    "CPF TEXT NOT NULL)";

                command.ExecuteNonQuery();

                var createTableCommand = connection.CreateCommand();

                createTableCommand.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Pokemon (
                        Id INTEGER PRIMARY KEY,                        
                        MasterId INTEGER NOT NULL,
                        Name TEXT NOT NULL,
                        CaptureRate INTEGER NOT NULL);";

                createTableCommand.ExecuteNonQuery();

            }
        }

        public async Task<PokemonMasterDTO> SaveMasterPokemonAsync(PokemonMasterDTO master)
        {
            var existing = (await SearchMasterPokemonByNameAsync(master.Name));
            if (existing != null) throw new Exception($"Mestre pokemon já existe com este Nome: {master.Name}");

            existing = (await SearchMasterPokemonByCPFAsync(master.CPF));
            if (existing != null) throw new Exception($"Mestre pokemon já existe com este CPF: {master.CPF}");

            using (var connection = new SqliteConnection($"Data Source={dbName}"))
            {
                connection.Open();
                
                var count = (await GetAllPokemonMastersAsync()).Count;

                var insertCommand = connection.CreateCommand();
                insertCommand.CommandText = @"
                    INSERT INTO MasterPokemon (Id, Name, Age, CPF)
                    VALUES (@Id, @Name, @Age, @CPF);";
                insertCommand.Parameters.AddWithValue("@Id", count);
                insertCommand.Parameters.AddWithValue("@Name", master.Name);
                insertCommand.Parameters.AddWithValue("@Age", master.Age);
                insertCommand.Parameters.AddWithValue("@CPF", master.CPF);
                await insertCommand.ExecuteNonQueryAsync();
            }

            return (await GetAllPokemonMastersAsync()).FirstOrDefault(x=>x.Name == master.Name);
        }

        public async Task<List<PokemonMasterDTO>> GetAllPokemonMastersAsync()
        {
            var mestres = new List<PokemonMasterDTO>();

            using (var connection = new SqliteConnection($"Data Source={dbName}"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM MasterPokemon";

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var mestre = new PokemonMasterDTO
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Age = reader.GetInt32(2),
                            CPF = reader.GetString(3)
                        };

                        mestres.Add(mestre);
                    }
                }
            }

            return mestres;
        }

        public async Task<PokemonMasterDTO> SearchMasterPokemonByNameAsync(string CPF)
        {
            using (var connection = new SqliteConnection($"Data Source={dbName}"))
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = "SELECT * FROM MasterPokemon WHERE Name = @Name";
                command.Parameters.AddWithValue("@Name", CPF);

                using (var leitor = await command.ExecuteReaderAsync())
                {
                    if (leitor.Read())
                    {
                        var mestrePokemon = new PokemonMasterDTO();
                        mestrePokemon.Name = leitor["Name"].ToString();
                        mestrePokemon.Age = int.Parse(leitor["Age"].ToString());
                        mestrePokemon.CPF = leitor["CPF"].ToString();

                        return mestrePokemon;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public async Task<PokemonMasterDTO> SearchMasterPokemonByCPFAsync(string CPF)
        {
            using (var connection = new SqliteConnection($"Data Source={dbName}"))
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = "SELECT * FROM MasterPokemon WHERE CPF = @CPF";
                command.Parameters.AddWithValue("@CPF", CPF);

                using (var leitor = await command.ExecuteReaderAsync())
                {
                    if (leitor.Read())
                    {
                        var mestrePokemon = new PokemonMasterDTO();
                        mestrePokemon.Name = leitor["Name"].ToString();
                        mestrePokemon.Age = int.Parse(leitor["Age"].ToString());
                        mestrePokemon.CPF = leitor["CPF"].ToString();

                        return mestrePokemon;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public async Task<PokemonMasterDTO> SearchMasterPokemonByIdAsync(int id)
        {
            using (var connection = new SqliteConnection($"Data Source={dbName}"))
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = "SELECT * FROM MasterPokemon WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", id);

                using (var leitor = await command.ExecuteReaderAsync())
                {
                    if (leitor.Read())
                    {
                        var mestrePokemon = new PokemonMasterDTO();
                        mestrePokemon.Name = leitor["Name"].ToString();
                        mestrePokemon.Age = int.Parse(leitor["Age"].ToString());
                        mestrePokemon.CPF = leitor["CPF"].ToString();

                        return mestrePokemon;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public async Task SaveCapturedPokemonAsync(PokemonDTO pokemon, int masterId)
        {
            using (var connection = new SqliteConnection($"Data Source={dbName}"))
            {
                connection.Open();

                var captureRate = pokemon.Capture_Rate;

                var insertCommand = connection.CreateCommand();
                insertCommand.CommandText = @"
                    INSERT INTO Pokemon (Id, MasterId, Name, CaptureRate)
                    VALUES (@Id, @MasterId, @Name, @CaptureRate);";
                insertCommand.Parameters.AddWithValue("@Name", pokemon.Name);
                insertCommand.Parameters.AddWithValue("@CaptureRate", captureRate);
                insertCommand.Parameters.AddWithValue("@Id", (await GetAllCapturedPokemonsAsync(null)).Count);
                insertCommand.Parameters.AddWithValue("@MasterId", masterId);

                await insertCommand.ExecuteNonQueryAsync();
            }
        }

        public async Task<List<PokemonDTO>> GetAllCapturedPokemonsAsync(int? masterId = null)
        {
            var capturedPokemons = new List<PokemonDTO>();

            using (var connection = new SqliteConnection($"Data Source={dbName}"))
            {
                connection.Open();

                var selectCommand = connection.CreateCommand();
                selectCommand.CommandText = $@"
                    SELECT Name, CaptureRate, Id
                    FROM Pokemon;                    
                    {(masterId.HasValue ? $"where MasterId={masterId}" : "")}";

                using (var reader = selectCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var name = reader.GetString(0);
                        var captureRate = reader.GetInt32(1);
                        var id = reader.GetInt32(2);

                        var pokemon = new PokemonDTO(id, name, captureRate);

                        capturedPokemons.Add(pokemon);
                    }
                }
            }

            return capturedPokemons;
        }

    }
}