using System.Collections;
using MasterTable;
using MasterTable.Core;
using GameDB.Core;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace GameDB2
{
    /*---------------------Schema Start---------------------*/
    public partial class Unit : ISchema
    {
        public Data DataTable { get; private set; } = new Data();
        public Level LevelTable { get; private set; } = new Level();

        private List<IncludedTable> _included;
        public List<IncludedTable> g_Included => _included ??= new List<IncludedTable>()
        {
            new IncludedTable()
            {
                Name = "Data",
                SchemaName = "Unit",
                SchemaId = "Unit",
                Id = "Data"
            },
            new IncludedTable()
            {
                Name = "Level",
                SchemaName = "Unit",
                SchemaId = "Unit",
                Id = "Level"
            }
        };

        /*Auto Generated Tables*/
        /*---------------------Table Start---------------------*/
        public partial class Data : ITable<Unit>, IEnumerable<Data.Entity>
        {
            /*---------------------Entity Start---------------------*/
            public partial class Entity : IEntity<Data>
            {
                public int Key { get; private set; }
                public float Damage { get; private set; }
                public double Percentage { get; private set; }
                public global::System.Collections.Generic.List<int> Datas { get; private set; }

                public static Entity CreateWithVirtualCells(List<global::MasterTable.Core.VirtualCell> values)
                {
                    Entity entity = new Entity();
                    entity.Key = TypeReader<int>.Reader.Read(values[0].Value);
                    entity.Damage = TypeReader<float>.Reader.Read(values[1].Value);
                    entity.Percentage = TypeReader<double>.Reader.Read(values[2].Value);
                    entity.Datas = TypeReader<global::System.Collections.Generic.List<int>>.Reader.Read(values[3].Value);
                    return entity;
                }
            }

            /*---------------------Entity End---------------------*/
            /*---------------------DataField Start---------------------*/
            public global::System.Collections.Generic.List<GameDB2.Unit.Data.Entity> List = new();
            public global::System.Collections.Generic.Dictionary<int, GameDB2.Unit.Data.Entity> Map = new();
            public Entity Get(int key)
            {
                if (Map.ContainsKey(key))
                    return Map[key];
                return null;
            }

            public bool IsExist(int key)
            {
                return Map.ContainsKey(key);
            }

            public IEnumerator<Entity> GetEnumerator()
            {
                return List.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public Entity this[int i]
            {
                get
                {
                    return Get(i);
                }
            }

            public int Count => List.Count;

            public async UniTask LoadAsync(global::MasterTable.Core.VirtualSheet sheet)
            {
                List.Clear();
                Map.Clear();
                int dataCount = sheet.DataRanges.Table.Count;
                int perRead = dataCount > 128 ? dataCount / 8 : 0;
                int loadedCount = 0;
                for (int i = 0; i < sheet.DataRanges.Table.Count; i++)
                {
                    Entity entity = Entity.CreateWithVirtualCells(sheet.DataRanges.Table[i]);
                    if (!Map.ContainsKey(entity.Key))
                    {
                        List.Add(entity);
                        Map.Add(entity.Key, entity);
                        loadedCount++;
                        if (perRead > 0 && perRead == loadedCount)
                        {
                            await UniTask.Yield();
                        }
                    }
                    else
                    {
                        throw new System.Exception($"{entity.Key} is already exist in the table! (key value must be unique..)");
                    }
                }
            }
        }

        /*---------------------DataField End---------------------*/
        /*---------------------Table End---------------------*/
        /*---------------------Table Start---------------------*/
        public partial class Level : ITable<Unit>, IEnumerable<Level.Entity>
        {
            /*---------------------Entity Start---------------------*/
            public partial class Entity : IEntity<Level>
            {
                public int Key { get; private set; }
                public int Level { get; private set; }
                public float Miyao { get; private set; }
                public global::System.Collections.Generic.List<int> ListList { get; private set; }

                public static Entity CreateWithVirtualCells(List<global::MasterTable.Core.VirtualCell> values)
                {
                    Entity entity = new Entity();
                    entity.Key = TypeReader<int>.Reader.Read(values[0].Value);
                    entity.Level = TypeReader<int>.Reader.Read(values[1].Value);
                    entity.Miyao = TypeReader<float>.Reader.Read(values[2].Value);
                    entity.ListList = TypeReader<global::System.Collections.Generic.List<int>>.Reader.Read(values[3].Value);
                    return entity;
                }
            }

            /*---------------------Entity End---------------------*/
            /*---------------------LevelField Start---------------------*/
            public global::System.Collections.Generic.List<GameDB2.Unit.Level.Entity> List = new();
            public global::System.Collections.Generic.Dictionary<int, GameDB2.Unit.Level.Entity> Map = new();
            public Entity Get(int key)
            {
                if (Map.ContainsKey(key))
                    return Map[key];
                return null;
            }

            public bool IsExist(int key)
            {
                return Map.ContainsKey(key);
            }

            public IEnumerator<Entity> GetEnumerator()
            {
                return List.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public Entity this[int i]
            {
                get
                {
                    return Get(i);
                }
            }

            public int Count => List.Count;

            public async UniTask LoadAsync(global::MasterTable.Core.VirtualSheet sheet)
            {
                List.Clear();
                Map.Clear();
                int dataCount = sheet.DataRanges.Table.Count;
                int perRead = dataCount > 128 ? dataCount / 8 : 0;
                int loadedCount = 0;
                for (int i = 0; i < sheet.DataRanges.Table.Count; i++)
                {
                    Entity entity = Entity.CreateWithVirtualCells(sheet.DataRanges.Table[i]);
                    if (!Map.ContainsKey(entity.Key))
                    {
                        List.Add(entity);
                        Map.Add(entity.Key, entity);
                        loadedCount++;
                        if (perRead > 0 && perRead == loadedCount)
                        {
                            await UniTask.Yield();
                        }
                    }
                    else
                    {
                        throw new System.Exception($"{entity.Key} is already exist in the table! (key value must be unique..)");
                    }
                }
            }
        }
    /*---------------------LevelField End---------------------*/
    /*---------------------Table End---------------------*/
    }
/*---------------------Schema End---------------------*/
}