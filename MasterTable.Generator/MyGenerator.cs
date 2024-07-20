using System;
using System.Collections.Generic; 
using System.Linq;
using MasterTable.Shared;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MasterTable.Generator
{
    [Generator(LanguageNames.CSharp)]
    public class MyGenerator : IIncrementalGenerator
    {
        IncrementalValuesProvider<SchemaStructure> GetSchemaStructProvider(
            IncrementalGeneratorInitializationContext context)
        { 
            var addtionalTextProvider = context.AdditionalTextsProvider.Where(static x => x.Path.EndsWith(".struct"));
            IncrementalValuesProvider<SchemaStructure> schemaStructureProvider = addtionalTextProvider.Select(
                static (x, token) =>
                {
 
                    if (x != null)
                    {
                        var sheet = System.Text.Json.JsonSerializer
                            .Deserialize<SchemaStructure>(x.GetText().ToString());
                        return sheet;
                    }

                    return default;
                });

            return schemaStructureProvider;
        }
        /// <summary>
        /// DeclareType 어트리뷰트가 붙어있는 모든 클래스 찾기 
        /// </summary> 
        IncrementalValuesProvider<DeclareInfo> GetDeclaredTypeProvider(
            IncrementalGeneratorInitializationContext context)
        {
            var p = context.SyntaxProvider.CreateSyntaxProvider<DeclareInfo>(predicate: static (node, _) =>
            {
                // 노드가 타입 지정된 경우
                return node is ClassDeclarationSyntax typeDeclared && typeDeclared.AttributeLists.Count != 0;
            }, transform: (syntax,
                _) =>
            {
                ClassDeclarationSyntax? typeDeclarationSyntax = syntax.Node as ClassDeclarationSyntax;
                foreach (var attributeList in typeDeclarationSyntax!.AttributeLists)
                {
                    foreach (var attribute in attributeList.Attributes)
                    {
                        if (attribute.Name.ToString() == "DeclareType")
                        {
                            // 0번 어규먼트는 무조건 타입지정
                            TypeOfExpressionSyntax? typeOfExpressionSyntax =
                                attribute?.ArgumentList?.Arguments[0].Expression as TypeOfExpressionSyntax;
                            // 1번부터는 스트링 파라미터
                            List<LiteralExpressionSyntax?>? expressions = attribute?.ArgumentList?.Arguments.Skip(1)
                                .Select(x => x.Expression as LiteralExpressionSyntax).ToList();

                            var declaredInfo = new DeclareInfo()
                            {
                                GeneratorSyntaxContext = syntax,
                                TypeOfExpressionSyntax = typeOfExpressionSyntax,
                                ClassDeclarationSyntax = typeDeclarationSyntax,
                                DeclaredAliasSyntaxes = expressions, 
                                DeclaredAliases = expressions.Select(x => x.Token.Value.ToString().ToLower()).ToList()
                            };
                            return declaredInfo;
                        }
                    }
                }

                return null;
            });

            return p;
        }
        
        IncrementalValuesProvider<DeclaredInfoContainer> GetDeclaredTypeContainerProvider(
            IncrementalGeneratorInitializationContext context)
        {
            DeclaredInfoContainer container = new DeclaredInfoContainer();

            var p = context.SyntaxProvider.CreateSyntaxProvider<DeclaredInfoContainer>(predicate: static (node, _) =>
            {
                // 노드가 타입 지정된 경우
                return node is ClassDeclarationSyntax typeDeclared && typeDeclared.AttributeLists.Count != 0;
            }, transform: (syntax,
                _) =>
            {
                ClassDeclarationSyntax? typeDeclarationSyntax = syntax.Node as ClassDeclarationSyntax;
                foreach (var attributeList in typeDeclarationSyntax!.AttributeLists)
                {
                    foreach (var attribute in attributeList.Attributes)
                    {
                        if (attribute.Name.ToString() == "DeclareType")
                        { 
                            // 0번 어규먼트는 무조건 타입지정
                            TypeOfExpressionSyntax? typeOfExpressionSyntax =
                                attribute?.ArgumentList?.Arguments[0].Expression as TypeOfExpressionSyntax;
                            // 1번부터는 스트링 파라미터
                            List<LiteralExpressionSyntax?>? expressions = attribute?.ArgumentList?.Arguments.Skip(1)
                                .Select(x => x.Expression as LiteralExpressionSyntax).ToList();

                            
                            var declaredInfo = new DeclareInfo()
                            {
                                GeneratorSyntaxContext = syntax,
                                TypeOfExpressionSyntax = typeOfExpressionSyntax,
                                ClassDeclarationSyntax = typeDeclarationSyntax,
                                DeclaredAliasSyntaxes = expressions,
                                DeclaredAliases = expressions.Select(x => x.Token.Value.ToString().ToLower()).ToList()
                            };
                            container.Add(declaredInfo);
                        }
                    }
                }

                return container;
            });

            return p;
        }


        public void Initialize(IncrementalGeneratorInitializationContext context)
        {  
            context.RegisterPostInitializationOutput(x =>
            {
                x.AddSource("Gen.g.cs", "//Generator Work!");
                    x.AddSource(@$"Interfaces.g.cs", $@"
    namespace GameDB.Core{{


       public class IncludedTable{{ 
            public string SchemaName {{get; set;}}
            public string SchemaId {{get; set;}}
            public string Name {{get;set;}}
            public string Id{{get;set;}}
       }}
       public interface ISchema{{
                List<IncludedTable> g_Included {{get;}} 
       }}

        public interface ITable;
        public interface ITable<TFrom> : ITable
       where TFrom : ISchema
       {{   
       }}
 
        public interface IEntity<TFrom>
        where TFrom : ITable
       {{

        }}
    }}
    ");
            });
             
            
            /*
             * IncrementalGeneratorInitializationContext 자체가 프로그램이다.
             * IncrementalGeneratorInitializationContext 에 provider를 등록하면
             *
             * 내부에서 캐싱되고, RegisterSourceOutput를 통해 provider의 내부에 있는 아이템들의
             * 변화에 따라 소스를 생성할 수 있다.
             *
             * 예를들어 캐싱한 Syntax와 관련있는 다른 Syntax가 변경되면 context는 통지를 받고
             * 소스코드를 생성한다.
             *
             * 파일의 경우도 마찬가지다. FileWatcher가 내부적으로 있는지는 잘 모르겠지만
             * 파일이 변경되면 context는 통지를 받고 소스코드를 생성한다.
             */


            var schemaProvider = GetSchemaStructProvider(context);
            var delcareTypeCollectContainer = GetDeclaredTypeContainerProvider(context).Where(x => x != null);
            var combined = schemaProvider.Combine(delcareTypeCollectContainer.Collect());
 
          
            
            
            context.RegisterSourceOutput(delcareTypeCollectContainer.Collect(), (spc, value) =>
            {

         
                
                var container = value[0]; 
                foreach(var item in container.InfoList)
                {

                    if (item.ClassDeclarationSyntax.BaseList == null ||
                        item.ClassDeclarationSyntax.BaseList.Types.Count == 0 ||
                        item.ClassDeclarationSyntax.BaseList.Types.Select(x => x.Type.ToString()).Count(x=>x.Contains("IType")) == 0)
                    {
                         
                        spc.ReportDiagnostic(Diagnostic.Create(new DiagnosticDescriptor( "MT001", "Error", "DeclareType 어트리뷰트를 사용하려는 대상은 IType<T>를 상속받아야 합니다.", "Error", DiagnosticSeverity.Error, true),
                            item.ClassDeclarationSyntax.GetLocation(), item.GetFullClassName()));
                        return;
                    }  
                }
                
                
                var TypeReaderInitializers = 
                    string.Join("\n", container.InfoList.Select(x 
                        => $"global::MasterTable.Core.TypeReader<{x.GetTypeFullName()}>.Reader = new {x.GetFullClassName()}();"));
               
                string InitializeClassCode = @$"
namespace MasterTable.Core{{
                    public static class TypeInitializer
                    {{
                        static bool m_initialized; 
                        public static void Initialize()
                        {{
                            if(m_initialized == false){{
                                         {TypeReaderInitializers}
                            }}
                        m_initialized = true; 
                        }}
                    }}
}}
                ";
                 
               spc.AddSource("TypeReaderInitializer.g.cs", InitializeClassCode);
            });


 
            context.RegisterSourceOutput(combined,
                (sourceProductionContext, combinedContext) =>
                { 
                    CodeGenerator(combinedContext.Left, combinedContext.Right[0], sourceProductionContext);
                });
        }

 
        //GameDB
        string GetTableNameSpace(SchemaStructure schema, TableStructure table)
        {
            return $"GameDB.Table";
        }
        
        //GameDB
        string GetSchemaNameSpace(SchemaStructure schema)
        {
            return $"GameDB";
        }

        //GameDB.Unit.Level
        string GetEntityNameSpace(SchemaStructure schema, TableStructure table)
        {
            return $"GameDB.{schema.SchemaName}.{table.TableName}";
        }
        public void CodeGenerator(SchemaStructure currentSchema, DeclaredInfoContainer declaredContainer,
            SourceProductionContext spc)
        {

            
            /*스키마의 토대를 만든다*/
            
            string SchemaBase = $@"
using MasterTable;
using MasterTable.Core;
using System.Collections;
using GameDB.Core;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
namespace GameDB
{{ 
 
/*        
            MasterMemory (author : shlifedev@gmail.com, https://github.com/shlifedev) @
            This file is generated by MasterMemory, Do not modify this file directly.
*/
public partial class {currentSchema.SchemaName} : ISchema{{
    <SchemaTableFields>

      private List<IncludedTable> _included; 
      public List<IncludedTable> g_Included => _included ??= new List<IncludedTable>() {{ {string.Join(",", currentSchema.Tables.Select(x=>
          $@"
new IncludedTable(){{
Name =  ""{x.TableName}"",
SchemaName =  ""{currentSchema.SchemaName}"",
SchemaId =  ""{currentSchema.SchemaName}"",
Id = ""{x.TableName}""
}}")) } }}; 

 



      /*Auto Generated Tables*/
      <SchemaAddedTables>
}}
 /*---------------------Schema End---------------------*/
}}
";

            string SchemaAddedTables = null;
            
            
            /*---------------------------------------------------------------------------------------------*/
            // 1. 가장 작은 단위인 엔티티를 먼저 생성한다
            /*---------------------------------------------------------------------------------------------*/
            foreach (var table in currentSchema.Tables)
            {
                string TableEntityField = $@"";
                string TableEntityReaders = $@"";
                string TableCreateFactory = $@"
public static Entity CreateWithVirtualCells(List<global::MasterTable.Core.VirtualCell> values)
{{
       Entity entity = new Entity();
       //ReplaceHere

       return entity;
}}


"; 
                
                for (int i = 0; i < table.Fields.Count; i++)
                {
                    var field = table.Fields[i];
                    var type = table.Types[i];
                    var declareInfo = declaredContainer.DeclaredInfoMap[type]; 
 
                    TableEntityField += $@"public {declareInfo.GetTypeFullName()} {field} {{get; private set;}}";
                    TableEntityReaders +=
                        "\n" +
                        $@"entity.{field} = TypeReader<{declareInfo.GetTypeFullName()}>.Reader.Read(values[{i}].Value);";
                }

                TableCreateFactory = TableCreateFactory.Replace("//ReplaceHere", TableEntityReaders);
                
                
                string TableEntity = $@"  
public partial class Entity : IEntity<{table.TableName}>{{
    //TableEntityField
    //TableCreateFactory
}} 
";
                
                TableEntity = TableEntity.Replace("//TableEntityField", TableEntityField);
                TableEntity = TableEntity.Replace("//TableCreateFactory", TableCreateFactory);

 
 

/*---------------------------------------------------------------------------------------------*/
                //2. 이후 두번쨰로 큰 단위인 테이블을 생성한다.
/*---------------------------------------------------------------------------------------------*/


      
                string TableKeyType = declaredContainer.DeclaredInfoMap[table.Types[0]].GetTypeFullName();
                string TableBase =
                    $@" 
 

 /*---------------------Table Start---------------------*/
public partial class {table.TableName} : ITable<{currentSchema.SchemaName}>, IEnumerable<{table.TableName}.Entity>
{{  
 /*---------------------Entity Start---------------------*/
        {TableEntity}       
 /*---------------------Entity End---------------------*/


 /*---------------------{table.TableName}Field Start---------------------*/
       private global::System.Collections.Generic.List<GameDB.{currentSchema.SchemaName}.{table.TableName}.Entity> _list = new ();
       private global::System.Collections.Generic.Dictionary<{TableKeyType}, GameDB.{currentSchema.SchemaName}.{table.TableName}.Entity> _map = new ();

        public global::System.Collections.Generic.IReadOnlyList<GameDB.{currentSchema.SchemaName}.{table.TableName}.Entity> List => _list;
          public global::System.Collections.Generic.IReadOnlyDictionary<{TableKeyType}, GameDB.{currentSchema.SchemaName}.{table.TableName}.Entity> Map => _map;
       


       public Entity Get({TableKeyType} key){{
            if(Map.ContainsKey(key))
                return Map[key];

            return null;
}} 

       public bool IsExist({TableKeyType} key){{
            return Map.ContainsKey(key);
       }}


        public IEnumerator<Entity> GetEnumerator()
        {{
            return List.GetEnumerator();
        }}

    IEnumerator IEnumerable.GetEnumerator()
    {{
        return GetEnumerator();
    }}

        public Entity this[{TableKeyType} i]{{ 
            get{{
                return Get(i);
            }}             
        }}


        public int Count => List.Count;

     
        
       public async UniTask LoadAsync(global::MasterTable.Core.VirtualSheet sheet){{
           
            _list.Clear();
            _map.Clear();
    
            for (int i = 0; i < sheet.DataRanges.Table.Count; i++)
            {{
                Entity entity = Entity.CreateWithVirtualCells(sheet.DataRanges.Table[i]);
                if(!Map.ContainsKey(entity.Key)){{
                    _list.Add(entity);
                    _map.Add(entity.Key, entity);  
                }}
                else
                {{
                    throw new System.Exception($""{{entity.Key}} is already exist in the table! (key value must be unique..)"");
                }}
            }}
       }}
}}


 /*---------------------{table.TableName}Field End---------------------*/


 /*---------------------Table End---------------------*/
";
                
                SchemaAddedTables += TableBase;

                
 
                 
            }

            SchemaBase = SchemaBase.Replace("<SchemaAddedTables>", SchemaAddedTables);
            SchemaBase = SchemaBase.Replace("<SchemaTableFields>", string.Join("\n", currentSchema.Tables.Select(x => 
                @$"
 public {x.TableName} {x.TableName}Table {{get; private set;}} = new {x.TableName}();
")));
            spc.AddSource($"{currentSchema.SchemaName}.g.cs",   CSharpSyntaxTree.ParseText(SchemaBase).GetRoot().NormalizeWhitespace().ToFullString());


        }
    }
}