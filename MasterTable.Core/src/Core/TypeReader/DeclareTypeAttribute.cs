using System.Runtime.InteropServices;

namespace MasterTable.Core
{

    /// <summary>
    /// 1. 이 어트리뷰트가 달려있는 클래스를 수집한다.
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DeclareTypeAttribute : System.Attribute
    {
        public System.Type TargetType;
        public List<String> Declares;

        public DeclareTypeAttribute(Type targetType, params string[] declares)
        {
            TargetType = targetType;
            Declares = declares.Select(x => x.ToLower()).ToList();
        }
    }
}