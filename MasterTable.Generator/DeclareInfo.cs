using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MasterTable.Generator;

public class DeclareInfo
{
    public GeneratorSyntaxContext GeneratorSyntaxContext;
    public TypeOfExpressionSyntax TypeOfExpressionSyntax;
    public ClassDeclarationSyntax ClassDeclarationSyntax; 
    public List<LiteralExpressionSyntax> DeclaredAliasSyntaxes;
    public List<string> DeclaredAliases = new(); // a,b,c
    private string m_fullClassName = null;
    public string GetFullClassName()
    {
        var namespaceDeclaration = ClassDeclarationSyntax.Ancestors().OfType<NamespaceDeclarationSyntax>().FirstOrDefault();
        string namespaceName = namespaceDeclaration != null ? namespaceDeclaration.Name.ToString() : string.Empty;

        string className = ClassDeclarationSyntax.Identifier.Text;

// If namespaceName is empty, it means the class is declared without a namespace
        string fullyQualifiedName = string.IsNullOrEmpty(namespaceName) 
            ? $"global::{className}" 
            : $"global::{namespaceName}.{className}";
        return fullyQualifiedName;

    }

    public string GetTypeFullName()
    {
        SemanticModel model = GeneratorSyntaxContext.SemanticModel;
        ITypeSymbol? typeSymbol =
            ModelExtensions.GetSymbolInfo(model, TypeOfExpressionSyntax!.Type).Symbol as ITypeSymbol;
        string typeFullName = typeSymbol!.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

        return typeFullName;
    }
}