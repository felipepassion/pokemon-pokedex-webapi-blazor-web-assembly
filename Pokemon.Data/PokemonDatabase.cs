using Microsoft.Data.Sqlite;
using Pokemon.Application.DTO;

namespace Pokemon.Data
{
    public interface IPokemonDatabase
    {
        Task<List<PokemonMasterDTO>> GetAllPokemonMastersAsync();
        Task<List<CapturedPokemonDTO>> GetAllCapturedPokemonsAsync(int? masterId = null);
        Task<CapturedPokemonDTO> SaveCapturedPokemonAsync(PokemonDTO pokemon, PokemonMasterDTO master);
        Task<PokemonMasterDTO> SearchMasterPokemonByNameAsync(string Name);
        Task<PokemonMasterDTO> SearchMasterPokemonByIdAsync(int Name);
        Task<PokemonMasterDTO> SaveMasterPokemonAsync(PokemonMasterDTO master);
        Task<PokemonMasterDTO> SearchMasterPokemonByCPFAsync(string CPF);
        void ClearDatabase();
    }

    public class PokemonDatabase : IPokemonDatabase
    {
        string dbName = "pokemon-api.db";
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
                        MasterName TEXT NOT NULL,
                        PokemonId INTEGER NOT NULL,
                        PokemonName TEXT NOT NULL,
                        CaptureRate INTEGER NOT NULL);";

                createTableCommand.ExecuteNonQuery();
            }
        }

        public void ClearDatabase()
        {
            using (var connection = new SqliteConnection($"Data Source={dbName}"))
            {
                connection.Open();

                var dropTablePokemonCommand = connection.CreateCommand();
                dropTablePokemonCommand.CommandText = @"
                    DROP TABLE IF EXISTS Pokemon;";
                dropTablePokemonCommand.ExecuteNonQuery();
                var dropTableMastersCommand = connection.CreateCommand();
                dropTableMastersCommand.CommandText = @"
                    DROP TABLE IF EXISTS MasterPokemon;";
                dropTableMastersCommand.ExecuteNonQuery();
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

            return (await GetAllPokemonMastersAsync()).FirstOrDefault(x => x.Name == master.Name);
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

        public async Task<CapturedPokemonDTO> SaveCapturedPokemonAsync(PokemonDTO pokemon, PokemonMasterDTO master)
        {
            using (var connection = new SqliteConnection($"Data Source={dbName}"))
            {
                connection.Open();

                var captureRate = pokemon.Capture_Rate;

                var insertCommand = connection.CreateCommand();
                insertCommand.CommandText = @"
                    INSERT INTO Pokemon (Id, MasterId, PokemonName,PokemonId, CaptureRate, MasterName)
                                VALUES (@Id, @MasterId, @PokemonName, @PokemonId, @CaptureRate, @MasterName);";
                insertCommand.Parameters.AddWithValue("@PokemonName", pokemon.Name);
                insertCommand.Parameters.AddWithValue("@PokemonId", pokemon.Id);
                insertCommand.Parameters.AddWithValue("@CaptureRate", captureRate);
                insertCommand.Parameters.AddWithValue("@Id", (await GetAllCapturedPokemonsAsync(null)).Count);
                insertCommand.Parameters.AddWithValue("@MasterId", master.Id);
                insertCommand.Parameters.AddWithValue("@MasterName", master.Name);

                await insertCommand.ExecuteNonQueryAsync();

                return (await GetAllCapturedPokemonsAsync(master.Id)).FirstOrDefault(x => x.PokemonName == pokemon.Name);
            }
        }

        public async Task<List<CapturedPokemonDTO>> GetAllCapturedPokemonsAsync(int? masterId = null)
        {
            var capturedPokemons = new List<CapturedPokemonDTO>();

            if (masterId.HasValue)
                if ((await GetAllPokemonMastersAsync()).FirstOrDefault(x => x.Id == masterId) == null)
                    throw new Exception($"Mestre pokemon não encontrado com o id '{masterId}'");

            using (var connection = new SqliteConnection($"Data Source={dbName}"))
            {
                connection.Open();

                var selectCommand = connection.CreateCommand();
                selectCommand.CommandText = $@"
                    SELECT PokemonName, CaptureRate, Id, MasterName, MasterId, PokemonName, PokemonId
                    FROM Pokemon
                    {(masterId.HasValue ? $"where MasterId={masterId}" : "")};";

                using (var reader = await selectCommand.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var name = reader.GetString(0);
                        var captureRate = reader.GetFloat(1);
                        var id = reader.GetInt32(2);
                        var masterName = reader.GetString(3);
                        var pokemonName = reader.GetString(4);
                        var myMasterId = reader.GetInt32(5);
                        var pokemonId = reader.GetInt16(6);

                        var pokemon = new CapturedPokemonDTO
                        {
                            Id = id,
                            PokemonName = name,
                            CaptureRate = captureRate,
                            MasterId = myMasterId,
                            MasterName = masterName,
                            PokemonId = pokemonId
                        };

                        capturedPokemons.Add(pokemon);
                    }
                }
            }

            return capturedPokemons;
        }
    }
}